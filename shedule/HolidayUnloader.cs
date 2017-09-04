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
        private List<mShop> _shopList;
        private List<DataForCalendary> _holidayList;

        public HolidayUnloader(List<mShop> shops, List<DataForCalendary> holidayList)
        {
            _shopList = shops;

            var March8 = new DateTime(2017, 03, 08);
            var March7 = new DateTime(2017, 03, 07);

            //оставляем только 7 и 8 марта по совету Димы
            _holidayList = holidayList.Where(x => x.getData() == March8 || x.getData() == March7).ToList();
        }

        /// <summary>
        /// Запускает процесс
        /// </summary>
        public void MakeHolidayDaysForShops()
        {
            List<string> errorShops = new List<string>();
            foreach (var shop in _shopList)
            {
                try
                {
                    Helper.SaveHolidayDaysOfShop(shop, _holidayList);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex);
                    errorShops.Add($"{shop.getAddress()}");
                }

            }

            MessageBox.Show(!errorShops.Any()
                ? "Дни типа 8 и 9 успешно выгружены для всех магазинов"
                : $"Дни типа 8 и 9 не выгружены для магазинов:\n{string.Join(",", errorShops)}");
        }
    }
}
