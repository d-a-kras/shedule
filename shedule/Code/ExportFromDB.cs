using Microsoft.Office.Interop.Excel;
using schedule;
using schedule.Code;
using schedule.Models;
using shedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace shedule.Code
{
    public class ExportFromDB
    {
        public static int combobox2;
        public static bool isExport;
        public static Thread thread;
        public static int barvalue;
        public static void BackgroundThread() {
            int year;
            if (DateTime.Now.Month < combobox2 + 1)
            {
                year = DateTime.Now.Year - 1;


            }
            else { year = DateTime.Now.Year; }

            DateTime dt = new DateTime(year, combobox2 + 1, 1);




            Program.currentShop.daysSale = ForForecast.createListDaySale(dt, dt.AddDays(31), Program.currentShop.getIdShop());


            Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            Workbook ObjWorkBook;
            Worksheet ObjWorkSheet;

            ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Range excelcells;
            ObjWorkBook.Sheets.Add();
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
            ObjWorkSheet.Name = "График";
            excelcells = ObjWorkSheet.get_Range("C1", "E1000");
            excelcells.Font.Size = 10;
            excelcells.ColumnWidth = 20;

            excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;

            ObjWorkSheet.Cells[1, 1] = "День недели";
            ObjWorkSheet.Cells[1, 2] = "Время";
            ObjWorkSheet.Cells[1, 3] = "Дата";
            ObjWorkSheet.Cells[1, 4] = "Количество товаров";
            ObjWorkSheet.Cells[1, 5] = "Количество чеков";
            ObjWorkSheet.Cells[1, 6] = "Количество сканирований";

            int i = 2;
           
            foreach (daySale twd in Program.currentShop.daysSale)
            {
               // progressBar3.PerformStep();
                foreach (hourSale hs in twd.hoursSale)
                {
                    ObjWorkSheet.Cells[i, 1] = twd.getWeekDay2();
                    ObjWorkSheet.Cells[i, 2] = hs.getNHour() + ":00";
                    ObjWorkSheet.Cells[i, 3] = hs.getData();
                    ObjWorkSheet.Cells[i, 4] = hs.getCountTov();
                    ObjWorkSheet.Cells[i, 5] = hs.getCountCheck();
                    ObjWorkSheet.Cells[i, 6] = hs.getCountClick();

                    i++;
                }
            }


            ObjExcel.Visible = false;
            ObjExcel.UserControl = true;
            ObjExcel.DisplayAlerts = false;
            ObjWorkBook.Saved = true;
            ObjExcel.DisplayAlerts = true;
            try
            {

                ObjWorkBook.SaveAs(Program.file, XlFileFormat.xlWorkbookDefault);
                Program.HandledShops.Add(Program.currentShop.getIdShop());


                ObjWorkBook.Close(0);

                ObjExcel.Quit();
               // MessageBox.Show("Файл создан");

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ошибка записи в файл " + ex.Message);
                ObjWorkBook.Close(0);
                ObjExcel.Quit();
            }
        }
    }
}
