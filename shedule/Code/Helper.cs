﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shedule.Code
{
    public static class Helper
    {
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
                var book = new LinqToExcel.ExcelQueryFactory(filepath);
                var getData = from a in book.Worksheet(Constants.ListName) select a;

                int cntr = 0;
                foreach (var a in getData)
                {
                    cntr++;
                    string dayOfWeek = a["День недели"];
                    string time = a["Время"];
                    string dateS = a["Дата"];
                    string productS = a["Кол-во товаров"];
                    string checkS = a["Кол-во чеков"];
                    string scansS = a["Кол-во сканиирований"];

                    DateTime dt;
                    int checkCount = 0;
                    int scansCount = 0;
                    double productCount = 0;

                    //если эти ячейки не смогли преобразоваться в нормальный тип данных - скорее всего там какое-то говнище, такую пропускаем
                    var resultDt = DateTime.TryParse(dateS, out dt);
                    var resultChCount = int.TryParse(checkS, out checkCount);
                    var resultScCount = int.TryParse(scansS, out scansCount);
                    var resultPrCount = double.TryParse(productS, out productCount);
                    var resultDayOfWeek = DayOfWeeksDictionary.ContainsKey(dayOfWeek);
                    bool resultHour = !(time.Split(':').Length < 1);

                    if (resultDt && resultChCount && resultScCount && resultPrCount && resultDayOfWeek && resultHour)
                    {
                        hourSales.Add(new hourSale(shopId, dt, time.Split(':')[0], DayOfWeeksDictionary[dayOfWeek], checkCount, scansCount, productCount));
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
                var responseFromServer=reader.ReadToEnd();
                reader.Close();

                using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                {
                    sw.Write(responseFromServer);
                }
            }
            catch{}
            
        }

        public static bool CheckNextYearCalendarIsExist()
        {
            string path = Environment.CurrentDirectory + $@"\Calendars\{DateTime.Now.AddYears(1).Year}.xml";
            if (File.Exists(path)) return true;
            return false;
        }
    }
}
