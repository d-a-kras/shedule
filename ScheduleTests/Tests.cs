using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using schedule;
using schedule.Code;

namespace ScheduleTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestParseXlsFile()
        {
            string filepath = @"D:\Проекты\Kirovsky1(add)\testData.xls";
            List<hourSale> hours = Helper.FillHourSalesList(filepath, 120);

        }
    }
}
