﻿using System;
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
        private List<Shop> _shopList;
        private List<DataForCalendary> _holidayList;

        public HolidayUnloader(List<Shop> shops, List<DataForCalendary> holidayList)
        {
            _shopList = shops;
            _holidayList = holidayList;
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
                    Console.WriteLine(ex);
                    errorShops.Add($"{shop.getAddress()}");
                }

            }

            MessageBox.Show(!errorShops.Any()
                ? "Дни типа 8 и 9 успешно выгружены для всех магазинов"
                : $"Дни типа 8 и 9 не выгружены для магазинов:\n{string.Join(",", errorShops)}");
        }
    }
}
