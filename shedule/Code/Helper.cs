﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using LinqToExcel;

namespace shedule.Code
{
    public static class Helper
    {

           public static string dbFileName = "db/workScheduleDB.db";
        /// <summary>
        /// Парсит xls файл с часами/днями/прочим.
        /// Если значение даты или количества чеков или количества сканов 
        /// или количества товаров отсутствует в строке - пропускает ее 
        /// </summary>
        /// <param name="filepath">Путь до xls файла</param>
        /// <param name="shopId">Id магазина</param>
        /// <returns></returns>
        public static List<hourSale> FillHourSalesList(string filepath, int shopId)
         {
            List<hourSale> hourSales = new List<hourSale>();
            if (File.Exists(filepath))
            {
                //получаем лист
                var book = new ExcelQueryFactory(filepath);
                var getData = from a in book.Worksheet(Constants.ListName) select a;

                int cntr = 0;
                foreach (var a in getData)
                {
                    cntr++;
                    string dayOfWeek = a["День недели"];
                    string time = a["Время"];
                    time = time.Split(':')[0];
                    try
                    {
                        time = time.Split(' ')[1];
                    }
                    catch {

                    }
                    string dateS = a["Дата"];
                    string productS = a["Количество товаров"];
                    string checkS = a["Количество чеков"];
                    string scansS = a["Количество сканирований"];

                    DateTime dt;
                    int checkCount = 0;
                    int scansCount = 0;
                    int chas;
                    double productCount = 0;

                    //если эти ячейки не смогли преобразоваться в нормальный тип данных - скорее всего там какое-то говнище, такую пропускаем
                    var resultDt = DateTime.TryParse(dateS, out dt);
                    var resultChCount = Int32.TryParse(checkS, out checkCount);
                    var resultScCount = Int32.TryParse(scansS, out scansCount);
                    var resultPrCount = Double.TryParse(productS, out productCount);
                    var resultDayOfWeek = (DayOfWeeksDictionary.ContainsValue(dayOfWeek)||(DayOfWeeksDictionary.ContainsKey(dayOfWeek)));
                    bool resultHour = time!=null && Int32.TryParse(time, out chas);

                    if (resultDt && resultChCount && resultScCount && resultPrCount && resultDayOfWeek && resultHour)
                    {
                        hourSales.Add(new hourSale(shopId, dt, time.Split(':')[0], dayOfWeek/*DayOfWeeksDictionary[dayOfWeek]*/, checkCount, scansCount, productCount));
                    }
                    else
                    {
                        Console.WriteLine($"Не удалось распарсить данные либо отсутствуют данные в строке {cntr}");
                    }
                }
                if (hourSales.Count == 0)
                {
                    throw new Exception("Не было найдено ни одной строки в нужном формате");
                }
                return hourSales;
            }
            throw new FileNotFoundException();
        }

        public static void CheckFactorsActuality()
        {
            bool wasUpdated = false;
            foreach (Factor f in Program.currentShop.factors)
            {
                if (f.getData() <= DateTime.Now)
                {
                    wasUpdated = true;
                    f.setTZnach(f.getTZnach());
                    f.setData(DateTime.Now.AddYears(1));
                }
            }
            if (wasUpdated)
            {
                Program.WriteFactors();
            }
        }

        private static readonly Dictionary<string, string> DayOfWeeksDictionary = new Dictionary<string, string>
        {
            {
                "Пн", "понедельник"
            },
            {
                "Вт", "вторник"
            },
            {
                "Ср", "среда"
            },
            {
                "Чт", "четверг"
            },
            {
                "Пт", "пятница"
            },
            {
                "Сб", "суббота"
            },
            {
                "Вс", "воскресенье"
            }
        };

        /// <summary>
        /// Возвращает календарь на следующий год если возможно, null если невозможно
        /// </summary>
        /// <returns></returns>
        public static void CheckAndDownloadNextYearCalendar()
        {
            string url = $"http://xmlcalendar.ru/data/ru/{DateTime.Now.AddYears(1).Year}/calendar.xml";
            WebRequest request = WebRequest.Create(url);
            string writePath = Environment.CurrentDirectory + $@"\Calendars\{DateTime.Now.AddYears(1).Year}.xml";

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                var responseFromServer = reader.ReadToEnd();
                reader.Close();

                using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                {
                    sw.Write(responseFromServer);
                }
            }
            catch { }

        }

        public static bool CheckNextYearCalendarIsExist()
        {
            string path = Environment.CurrentDirectory + $@"\Calendars\{DateTime.Now.AddYears(1).Year}.xml";
            if (File.Exists(path)) return true;
            return false;
        }

        /// <summary>
        /// Проверяет наличие у магазина файлов из списка fileNames (внутри функции)
        /// Если все файлы есть - магазин считается обработанным
        /// Id этого магазина добавляется в список HandledShops
        /// </summary>
        /// <returns></returns>
        public static void CheckShopsStatus()
        {
            bool checkNotFullcoincidence = true;       //если true, то проверяются вхождения слов из xlsParts в именах файлов. Иначе точные совпадения
            List<string> fileNames = new List<string>
            {
                {
                    "factors"
                }
            };

            List<string> xlsParts = new List<string>
            {
                {
                    "График"
                }
            };

            if (checkNotFullcoincidence)
            {
                foreach (var s in Program.listShops)
                {
                    int shopFileCounter = 0;
                    var path = $"{Environment.CurrentDirectory}\\Shops\\{s.getIdShop()}";
                    if (Directory.Exists(path))
                    {
                        var folderFiles = Directory.GetFiles(path);

                        foreach (var f in xlsParts)
                        {
                            foreach (var file in folderFiles)
                            {
                                if (file.Contains(f)) shopFileCounter++;
                            }
                        }
                        if (shopFileCounter == xlsParts.Count)
                        {
                            Program.HandledShops.Add(s.getIdShop());
                        }
                    }

                }
            }
            else
            {
                foreach (var s in Program.listShops)
                {
                    int shopFileCounter = 0;
                    foreach (var f in fileNames)
                    {
                        var path = $"{Environment.CurrentDirectory}\\Shops\\{s.getIdShop()}\\{f}";
                        if (File.Exists(path)) shopFileCounter++;
                    }
                    if (shopFileCounter == fileNames.Count)
                    {
                        Program.HandledShops.Add(s.getIdShop());
                    }
                }
            }

        }

        /// <summary>
        /// Пытаемся убить долбаные эксели
        /// </summary>
        /// <param name="excelFileName"></param>
        /* public static void KillExcels(string excelFileName = "")
         {
             var processes = from p in Process.GetProcessesByName("EXCEL") select p;
 
             //лень думать, пусть будут разные форычи
             if (excelFileName != "")
             {
                 foreach (var process in processes)
                 {
                     if (process.MainWindowTitle == "Microsoft Excel - " + excelFileName)
                         process.Kill();
                 }
             }
             else
             {
                 foreach (var process in processes)
                 {
                     process.Kill();
                 }
             }
         }*/

        /// <summary>
        /// Возвращает сведения о чеках и кликах для магазина за промежуток времени
        /// </summary>
        /// <param name="shopId">Id магазина</param>
        /// <param name="startPeriod">Начало периода</param>
        /// <param name="endPeriod">Конец периода</param>
        /// <returns></returns>
        public static List<hourSale> GetHourSalesByDate(int shopId, DateTime startPeriod, DateTime endPeriod)
        {
            var connectionString =
                $"Data Source={Settings.Default.DatabaseAddress};Persist Security Info=True;User ID={Program.login};Password={Program.password}";
            string s1 = startPeriod.Year + "/" + startPeriod.Day + "/" + startPeriod.Month;
            string s2 = endPeriod.Year + "/" + endPeriod.Day + "/" + endPeriod.Month;

            string sql = "select * from dbo.get_StatisticByShopsDayHour('" + shopId + "', '" + s1 + "', '" + s2 +
                         " 23:59:00')";

            List<hourSale> hss = new List<hourSale>();

            int countAttemption = 0;
            int countRecords = 0;
            while (countRecords == 0 && countAttemption < 2)
            {
                countAttemption++;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection) { CommandTimeout = 3000 };
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        hourSale h = new hourSale(shopId, reader.GetDateTime(1), reader.GetString(2),
                            reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                        hss.Add(h);
                        countRecords++;
                    }
                }
                if (countRecords > 2) countAttemption = 2;
            }

            if (countRecords < 2 && Constants.IsThrowExceptionOnNullResult)
            {
                countRecords = 0;
                countAttemption = 0;
                throw new Exception("Соединение с базой нестабильно, данные не были получены.");
            }

            countRecords = 0;
            countAttemption = 0;
            return hss;
        }


        /// <summary>
        /// Возвращает daySale для определенного дня магазина
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="dayOfSale"></param>
        /// <param name="typeOfDay"></param>
        /// <returns></returns>
        public static daySale GetDaySaleByDate(int shopId, DateTime dayOfSale, int typeOfDay)
        {
            var hoursOfDay = Program.createDaySale(shopId, dayOfSale);
            var ds = new daySale(Program.currentShop.getIdShop(), dayOfSale, typeOfDay);

            if (hoursOfDay.Count == 0)
            {
                Logger.Log.Error($"Не удалось вытянуть из базы данные дня {dayOfSale.Date} о магазине {shopId}");
            }
            foreach (hourSale hs in hoursOfDay)
            {
                ds.Add(hs);
            }
            return ds;
        }

        public static void readDays8and9(int year) { 
        

            string filepath = Environment.CurrentDirectory + "/ShopsH/" + Program.currentShop.getIdShop() + "/days89_"+year+".dat";

            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filepath))
            {
                using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
                {
                    List<daySale> days89 = (List<daySale>)formatter.Deserialize(fs);
                    if (days89.Any())
                    {
                        Program.currentShop.daysSale.AddRange(days89);
                        return;
                    }
                }
            }
            //SaveHolidayDaysOfShop(Program.currentShop.getIdShop(), CreateListHolidays(year), year);
        }

        /// <summary>
        /// Сохраняет данные о праздничных и предпраздничных днях в файл
        /// </summary>
        /// <param name="shopId">Id магазина</param>
        public static void createListDays8and9(int shopId, List<daySale> shopHolidayDays,int year)
        {
            string filepath = Environment.CurrentDirectory + "/ShopsH/" + shopId;
            BinaryFormatter formatter = new BinaryFormatter();

            if (shopHolidayDays.Any())
            {
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                using (FileStream fs = new FileStream(filepath + "/days89_"+year+".dat", FileMode.OpenOrCreate))
                {
                    //пустые не записываем
                    if (shopHolidayDays.Count > 0 && shopHolidayDays.FirstOrDefault().hoursSale.Count > 0)
                    {
                        formatter.Serialize(fs, shopHolidayDays);
                        Logger.Log.Info($"Записано в файл для магазина {shopId}");
                    }

                }
            }
        }

        /// <summary>
        /// Получает и сохраняет данные о праздничных и предпраздничных днях для одного магазина
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="_holidayList"></param>
        public static void SaveHolidayDaysOfShop(int id, List<DataForCalendary> _holidayList, int year)
        {
            List<daySale> holidayDaySales = new List<daySale>(_holidayList.Count);

            foreach (var holiday in _holidayList)
            {
                holidayDaySales.Add(Helper.GetDaySaleByDate(id, holiday.getData(), holiday.Tip));
            }
            Logger.Log.Info($"Выгружено для магазина {id}");

            createListDays8and9(id, holidayDaySales, year);


        }

        public static void CreateHolidaysForAllShops(int year)
        {
            var completedShops = new List<string>();
            foreach (var shop in Program.listShops)
            {
                var hr = new HolidayUnloader(shop.getIdShop(), CreateListHolidays(year));
                hr.MakeHolidayDaysForShops(year);
                completedShops.Add(shop.getIdShop().ToString());
            }

            MessageBox.Show("Праздничные дни выгружены для: " + completedShops.ToString());
        }

        public static List<DataForCalendary> CreateListHolidays(int year)
        {
            List<DataForCalendary> dd = new List<DataForCalendary>();
            dd.Add(new DataForCalendary(new DateTime(year, 5, 1), 8));
            dd.Add(new DataForCalendary(new DateTime(year, 5, 8), 9));
            dd.Add(new DataForCalendary(new DateTime(year, 6, 11), 9));
            dd.Add(new DataForCalendary(new DateTime(year, 5, 9), 8));
            dd.Add(new DataForCalendary(new DateTime(year, 6, 12), 8));
            return dd;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey> (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static string NumberToString(int n)
        {
            string s = n.ToString();
            if (s.Length==1) {
                s = "0" + s;
            }
            return s;
        }
    }
}
