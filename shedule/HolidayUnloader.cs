using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using shedule.Code;

namespace shedule
{
    /// <summary>
    /// Класс для генерации файлика с праздничными и предпраздничными днями для всех магазинов
    /// </summary>
    public class HolidayUnloader
    {
        private int _shopid;
        private List<DataForCalendary> _holidayList;

        public HolidayUnloader(int idshop, List<DataForCalendary> holidayList)
        {
            _shopid = idshop;
            _holidayList = holidayList;
        }

        /// <summary>
        /// Запускает процесс
        /// </summary>
        public void MakeHolidayDaysForShops(int year)
        {
            List<string> errorShops = new List<string>();
           
                try
                {
                    Helper.SaveHolidayDaysOfShop(_shopid, _holidayList, year);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex);
                    errorShops.Add($"{_shopid}");
                }

            if (errorShops.Any())
            { }
             //MessageBox.Show($"Дни типа 8 и 9 не выгружены для магазинов:\n{string.Join(",", errorShops)}");
            //MessageBox.Show(!errorShops.Any()
            //    ? "Дни типа 8 и 9 успешно выгружены для всех магазинов"
            //    : $"Дни типа 8 и 9 не выгружены для магазинов:\n{string.Join(",", errorShops)}");
        }
    }
}
