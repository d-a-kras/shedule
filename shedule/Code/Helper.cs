using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Code
{
    public static class Helper
    {
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

                    //если эти ячейки не смогли преобразоваться в нормальный тип данных - скорее всего там какое-то говнище
                    var resultDt = DateTime.TryParse(dateS, out dt);
                    var resultChCount = int.TryParse(checkS, out checkCount);
                    var resultScCount = int.TryParse(scansS, out scansCount);
                    var resultPrCount = double.TryParse(productS, out productCount);

                    if (resultDt && resultChCount && resultScCount && resultPrCount)
                    {
                        hourSales.Add(new hourSale(shopId, dt, time, dayOfWeek, checkCount, scansCount, productCount));
                    }
                    else
                    {
                        Console.WriteLine($"Не удалось распарсить данные либо отсутствуют данные в строке {cntr}");
                    }
                    
                }
                return hourSales;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
