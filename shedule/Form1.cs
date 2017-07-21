using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using SD = System.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.IO.Compression;
using Ionic.Zip;
using shedule.Code;
using Point = System.Drawing.Point;
using System.Diagnostics;
using Application = System.Windows.Forms.Application;

namespace shedule
{
    public partial class Form1 : Form
    {
        private BackgroundWorker bw = new BackgroundWorker();
        private BackgroundWorker bw1 = new BackgroundWorker();
        static public int[] chartX;
        static public int[] chartY2;
        static public int[] chartY1;
        static public int Nday = 0;
        static string filename;
        private bool errorOnExecuting = false;
        public bool isConnected = false;
        public string PathToZip = "";

        /*  public void ShowProizvCalendar() {
              foreach (DataForCalendary d in  Program.currentShop.DFCs)
              {
                  if(DataForCalendary.isHolyday(d.getData()))
                      monthCalendar1.AddBoldedDate(d.getData());
                  if (DataForCalendary.isPrHolyday(d.getData()))
                      monthCalendar1.AddAnnuallyBoldedDate(d.getData());
              }

          }*/

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {

            }
            else if (e.Error != null)
            {
                CloseProcessOnError();
            }
            else
            {
                progressBar1.Value = progressBar1.Maximum;
                progressBar1.Visible = false;
                label3.Visible = false;
                Program.HandledShops.Add(Program.currentShop.getIdShop());
                UpdateStatusShops();
            }
            Program.isProcessing = false;
            EnableControlsOnFinish();
        }



        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 2:
                    label3.Text = "Создание прогноза продаж";
                    break;
                case 4:
                    label3.Text = "Подсчет оптимальной загруженности";
                    break;
                case 6:
                    label3.Text = "Оптимальная расстановка смен";
                    break;
                case 8:
                    label3.Text = "Запись в Excel";
                    break;

            }
            progressBar1.Value = e.ProgressPercentage;
        }


        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Program.isProcessing = true;
            switch (Program.TipExporta)
            {
                case 0:
                    {
                        BackgroundWorker bg = sender as BackgroundWorker;
                        bg.ReportProgress(2);
                        try
                        {
                            Program.createPrognoz();
                            bg.ReportProgress(4);
                            //MessageBox.Show("Время создание примерно 2 минуты");


                            // 
                            Sotrudniki.OptimCountSotr();
                            bg.ReportProgress(6);


                            //  
                            Sotrudniki.CreateSmens();

                            bg.ReportProgress(8);

                            if (!Sotrudniki.CheckGrafic())
                            {
                                MessageBox.Show("Расписание не оптимально, из-за слишком жестких параметров выберите другие варианты смен или уменьшите минимальное число сотрудников.");
                                //  return;
                            }
                        }
                        catch (Exception ex)

                        {

                            //  label3.Visible = false;
                            //  progressBar1.Visible = false;
                            MessageBox.Show(ex.Message);
                            string[] s = new string[2];
                            // s = listBox1.Text.Split('_');
                            //  Program.currentShop = new Shop(Int16.Parse(s[0]), s[1]);
                            //  Program.getListDate(DateTime.Today.Year);
                            Program.readTSR();
                            MessageBox.Show("Расписание не создано");
                            CloseProcessOnError();
                            throw ex;

                        }
                        System.Drawing.Color color;
                        Excel.Range excelcells;
                        Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                        Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                        ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);

                        ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                        ObjWorkSheet.Name = "График";
                        excelcells = ObjWorkSheet.get_Range("A3", "AL100");
                        excelcells.Font.Size = 10;
                        excelcells.NumberFormat = "@";
                        bg.ReportProgress(10);
                        excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                        excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                        int i = 7;
                        foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                        {

                            ObjWorkSheet.Cells[1, i] = twd.GetWeekDay();
                            ObjWorkSheet.Cells[2, i] = twd.getData().Day;
                            i++;
                        }
                        Excel.Range excelcells2 = ObjWorkSheet.get_Range("A3", "AL50");
                        excelcells2.ColumnWidth = Program.currentShop.getAddress().Length;
                        bg.ReportProgress(12);


                        ObjWorkSheet.Cells[2, 1] = "Адрес";

                        ObjWorkSheet.Cells[2, 2] = "Должность";
                        ObjWorkSheet.Cells[2, 3] = "Тип занятости";
                        ObjWorkSheet.Cells[2, 4] = "Оклад";
                        ObjWorkSheet.Cells[2, 5] = "Общее число часов";
                        ObjWorkSheet.Cells[2, 6] = "Количество смен";

                        // MessageBox.Show(Program.currentShop.employes.Count+" count");
                        int j = 3;
                        foreach (employee emp in Program.currentShop.employes)
                        {

                            i = 7;
                            foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                            {
                                //  MessageBox.Show(emp.getTip() + "");
                                switch (emp.getID() / 100)
                                {
                                    case 0: color = System.Drawing.Color.LightSkyBlue; break;
                                    case 1: color = System.Drawing.Color.LightGreen; break;
                                    case 2: color = System.Drawing.Color.DarkSeaGreen; break;
                                    case 3: color = System.Drawing.Color.PaleGoldenrod; break;

                                    default: color = System.Drawing.Color.White; break;

                                }
                                ObjWorkSheet.Cells[j, i].Interior.Color = color;

                                if ((emp.smens.Find(t => t.getData() == twd.getData()) != null) && (emp.smens.Count != 0))
                                {
                                    // MessageBox.Show(emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + " - " + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena());

                                    ObjWorkSheet.Cells[j, i] =
                                        emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + " - " +
                                        emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena();
                                }
                                //else
                                // { ObjWorkSheet.Cells[emp.getID() + 4, i] = emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + "-" + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena(); }
                                i++;

                                ObjWorkSheet.Cells[j, 1] = Program.currentShop.getAddress();
                                ObjWorkSheet.Cells[j, 1].Interior.Color = color;
                                ObjWorkSheet.Cells[j, 2] = emp.GetDolgnost();
                                ObjWorkSheet.Cells[j, 2].Interior.Color = color;
                                ObjWorkSheet.Cells[j, 3] = emp.getTipZan();
                                if (Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()) != null)
                                {
                                    ObjWorkSheet.Cells[j, 4] =
                                        Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()).getZarp();
                                }
                                ObjWorkSheet.Cells[j, 5] = Program.normchas;
                                ObjWorkSheet.Cells[j, 5].Interior.Color = System.Drawing.Color.LightSkyBlue; ;
                                ObjWorkSheet.Cells[j, 6] = emp.smens.Count;
                                ObjWorkSheet.Cells[j, 6].Interior.Color = color;

                            }

                            j++;

                        }
                        bg.ReportProgress(14);

                        ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[2];

                        ObjWorkSheet.Name = "Часы";

                        excelcells = ObjWorkSheet.get_Range("A3", "AL100");
                        excelcells.Font.Size = 10;
                        excelcells.NumberFormat = "@";

                        excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                        excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                        i = 7;
                        foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                        {

                            ObjWorkSheet.Cells[1, i] = twd.GetWeekDay();
                            ObjWorkSheet.Cells[2, i] = twd.getData().Day;
                            i++;
                        }
                        Excel.Range excelcells3 = ObjWorkSheet.get_Range("A3", "AL50");
                        excelcells3.ColumnWidth = Program.currentShop.getAddress().Length;

                        bg.ReportProgress(16);


                        ObjWorkSheet.Cells[2, 1] = "Адрес";

                        ObjWorkSheet.Cells[2, 2] = "Должность";
                        ObjWorkSheet.Cells[2, 3] = "Тип занятости";
                        ObjWorkSheet.Cells[2, 4] = "Оклад";
                        ObjWorkSheet.Cells[2, 5] = "Общее число часов";
                        ObjWorkSheet.Cells[2, 6] = "Количество смен";

                        // MessageBox.Show(Program.currentShop.employes.Count+" count");
                        j = 3;
                        foreach (employee emp in Program.currentShop.employes)
                        {

                            i = 7;
                            foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                            {
                                switch (emp.getID() / 100)
                                {
                                    case 0: color = System.Drawing.Color.LightSkyBlue; break;
                                    case 1: color = System.Drawing.Color.LightGreen; break;
                                    case 2: color = System.Drawing.Color.DarkSeaGreen; break;
                                    case 3: color = System.Drawing.Color.PaleGoldenrod; break;

                                    default: color = System.Drawing.Color.White; break;

                                }
                                ObjWorkSheet.Cells[j, i].Interior.Color = color;


                                if ((emp.smens.Find(t => t.getData() == twd.getData()) != null) && (emp.smens.Count != 0))
                                {
                                    // MessageBox.Show(emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + " - " + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena());
                                    ObjWorkSheet.Cells[j, i].Interior.Color = color;
                                    ObjWorkSheet.Cells[j, i] =
                                        (emp.smens.Find(t => t.getData() == twd.getData()).getLenght() - 1).ToString();
                                }
                                //else
                                // { ObjWorkSheet.Cells[emp.getID() + 4, i] = emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + "-" + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena(); }
                                i++;

                                ObjWorkSheet.Cells[j, 1] = Program.currentShop.getAddress();
                                ObjWorkSheet.Cells[j, 1].Interior.Color = color;
                                ObjWorkSheet.Cells[j, 2] = emp.GetDolgnost();
                                ObjWorkSheet.Cells[j, 2].Interior.Color = color;
                                ObjWorkSheet.Cells[j, 3] = emp.getTipZan();
                                if (Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()) != null)
                                {
                                    ObjWorkSheet.Cells[j, 4] =
                                        Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()).getZarp();
                                }
                                ObjWorkSheet.Cells[j, 5] = Program.normchas;
                                ObjWorkSheet.Cells[j, 5].Interior.Color = System.Drawing.Color.LightSkyBlue; ;
                                ObjWorkSheet.Cells[j, 6] = emp.smens.Count;
                                ObjWorkSheet.Cells[j, 6].Interior.Color = color;


                            }

                            j++;

                        }
                        bg.ReportProgress(18);

                        ObjExcel.Visible = false;
                        ObjExcel.UserControl = true;
                        ObjExcel.DisplayAlerts = false;
                        ObjWorkBook.Saved = true;
                        try
                        {
                            ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookNormal);
                            Program.HandledShops.Add(Program.currentShop.getIdShop());
                            // ObjWorkBook.SaveAs(filename);

                            ObjWorkBook.Close();

                            ObjExcel.Quit();
                            bg.ReportProgress(20);
                            MessageBox.Show("Расписание создано");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка записи в файл " + ex.Message);
                            ObjWorkBook.Close(0);
                            ObjExcel.Quit();
                        }

                        break;
                    }
                case 1:
                    {
                        object misValue = System.Reflection.Missing.Value;
                        BackgroundWorker bg = sender as BackgroundWorker;
                        bg.ReportProgress(2);
                        Program.createPrognoz3();
                        foreach (TemplateWorkingDay t in Program.currentShop.MouthPrognozT)
                        {
                            t.createChartTemplate();
                            t.DS.CreateChartDaySale();
                            t.DS.CreateChartDaySaleCheck();
                            t.DS.CreateChartDaySaleClick();
                        }
                        bg.ReportProgress(4);
                        Excel.Range excelcells;
                        Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                        Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                        ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
                        int i = 1;
                        foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                        {
                            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[i];
                            ObjWorkSheet.Name = "Прогноз" + twd.getData().ToShortDateString();
                            excelcells = ObjWorkSheet.get_Range("A3", "AL100");
                            excelcells.Font.Size = 10;
                            //  excelcells.NumberFormat = "@";
                            bg.ReportProgress(10);
                            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                            excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                            int j = 1;
                            foreach (int x in twd.Chart.X)
                            {
                                ObjWorkSheet.Cells[1, j] = x.ToString() + ":00";
                                ObjWorkSheet.Cells[2, j] = twd.DS.ChartCheck.Y[j - 1];
                                ObjWorkSheet.Cells[3, j] = twd.DS.ChartClick.Y[j - 1];
                                j++;
                            }

                            bg.ReportProgress(12);

                            Excel.Range chartRange1;




                            Excel.ChartObjects xlCharts = (Excel.ChartObjects)ObjWorkSheet.ChartObjects(Type.Missing);

                            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(20, 80, 300, 250);

                            Excel.Chart chartPage = myChart.Chart;



                            chartRange1 = ObjWorkSheet.get_Range("a1", "C" + twd.DS.hoursSale.Count);
                            // chartRange1 = ObjWorkSheet.get_Range("a1", "c" + twd.DS.hoursSale.Count);

                            chartPage.SetSourceData(chartRange1, misValue);

                            chartPage.ChartType = Excel.XlChartType.xlLineMarkers;
                            i++;

                        }
                        bg.ReportProgress(18);


                        ObjExcel.Visible = false;
                        ObjExcel.UserControl = true;
                        ObjExcel.DisplayAlerts = false;
                        ObjWorkBook.Saved = true;
                        try
                        {

                            ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookNormal);
                            Program.HandledShops.Add(Program.currentShop.getIdShop());
                            // ObjWorkBook.SaveAs(filename);


                            ObjWorkBook.Close(0);

                            ObjExcel.Quit();
                            MessageBox.Show("Прогноз создан");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка записи в файл " + ex.Message);
                            ObjWorkBook.Close(0);
                            ObjExcel.Quit();
                        }


                        bg.ReportProgress(18);
                        break;
                    }

                case 2:
                    {
                        BackgroundWorker bg = sender as BackgroundWorker;
                        bg.ReportProgress(2);
                        try
                        {
                            Program.createPrognoz();
                            bg.ReportProgress(4);

                            Sotrudniki.OptimCountSotr();
                            bg.ReportProgress(6);
                        }
                        catch (Exception ex)
                        {
                            CloseProcessOnError();
                            MessageBox.Show(ex.Message);
                        }
                        object misValue = System.Reflection.Missing.Value;




                        Excel.Range excelcells;
                        Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                        Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                        ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);


                        ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                        excelcells = ObjWorkSheet.get_Range("A3", "AL100");
                        excelcells.Font.Size = 10;
                        // excelcells.NumberFormat = "@";
                        bg.ReportProgress(10);
                        excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                        excelcells.VerticalAlignment = Excel.Constants.xlCenter;

                        string[] chartXStr = new string[Program.currentShop.tsr.Count + 1];
                        chartY1 = new int[Program.currentShop.tsr.Count + 1];
                        chartY2 = new int[Program.currentShop.tsr.Count + 1];

                        int i = 1;
                        foreach (TSR tsr in Program.currentShop.tsr)
                        {
                            //  chartXStr[i] = tsr.getOtobragenie();
                            ObjWorkSheet.Cells[1, i] = tsr.getOtobragenie();
                            // chartY1[i] = tsr.getCount() * tsr.getZarp();
                            ObjWorkSheet.Cells[2, i] = tsr.getCount();
                            //chartY2[i] = Program.currentShop.employes.FindAll(t => t.getTip() == tsr.getTip()).Count * tsr.getZarp();
                            ObjWorkSheet.Cells[3, i] =
                                Program.currentShop.employes.FindAll(t => t.GetTip() == tsr.getTip()).Count;
                            i++;
                        }








                        bg.ReportProgress(12);

                        Excel.Range chartRange;



                        Excel.ChartObjects xlCharts = (Excel.ChartObjects)ObjWorkSheet.ChartObjects(Type.Missing);

                        Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(20, 80, 300, 250);

                        Excel.Chart chartPage = myChart.Chart;



                        chartRange = ObjWorkSheet.get_Range("a1", "h3");

                        chartPage.SetSourceData(chartRange, misValue);

                        chartPage.ChartType = Excel.XlChartType.xlColumnClustered;



                        bg.ReportProgress(18);





                        ObjExcel.Visible = false;
                        ObjExcel.UserControl = true;
                        ObjExcel.DisplayAlerts = false;
                        ObjWorkBook.Saved = true;
                        try
                        {

                            ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookNormal);
                            Program.HandledShops.Add(Program.currentShop.getIdShop());
                            // ObjWorkBook.SaveAs(filename);


                            ObjWorkBook.Close(0);

                            ObjExcel.Quit();
                            MessageBox.Show("Файл создан");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка записи в файл " + ex.Message);
                            ObjWorkBook.Close(0);
                            ObjExcel.Quit();
                        }


                        bg.ReportProgress(18);



                        break;
                    }
                case 3:
                    {
                        BackgroundWorker bg = sender as BackgroundWorker;
                        bg.ReportProgress(2);
                        try
                        {
                            Program.createPrognoz();
                            bg.ReportProgress(4);



                            // 
                            Sotrudniki.OptimCountSotr();
                            bg.ReportProgress(6);
                        }
                        catch (Exception ex)
                        {
                            CloseProcessOnError();
                            MessageBox.Show(ex.Message);
                        }
                        object misValue = System.Reflection.Missing.Value;




                        Excel.Range excelcells;
                        Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                        Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                        ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);


                        ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                        excelcells = ObjWorkSheet.get_Range("A3", "AL100");
                        excelcells.Font.Size = 10;
                        // excelcells.NumberFormat = "@";
                        bg.ReportProgress(10);
                        excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                        excelcells.VerticalAlignment = Excel.Constants.xlCenter;

                        string[] chartXStr = new string[Program.currentShop.tsr.Count + 1];
                        chartY1 = new int[Program.currentShop.tsr.Count + 1];
                        chartY2 = new int[Program.currentShop.tsr.Count + 1];

                        int i = 0;
                        foreach (TSR tsr in Program.currentShop.tsr)
                        {
                            //  chartXStr[i] = tsr.getOtobragenie();
                            ObjWorkSheet.Cells[1, i] = tsr.getOtobragenie();
                            // chartY1[i] = tsr.getCount() * tsr.getZarp();
                            ObjWorkSheet.Cells[2, i] = tsr.getCount() * tsr.getZarp();
                            //chartY2[i] = Program.currentShop.employes.FindAll(t => t.getTip() == tsr.getTip()).Count * tsr.getZarp();
                            ObjWorkSheet.Cells[3, i] =
                                Program.currentShop.employes.FindAll(t => t.GetTip() == tsr.getTip()).Count * tsr.getZarp();
                            i++;
                        }








                        bg.ReportProgress(12);

                        Excel.Range chartRange;



                        Excel.ChartObjects xlCharts = (Excel.ChartObjects)ObjWorkSheet.ChartObjects(Type.Missing);

                        Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(20, 80, 300, 250);

                        Excel.Chart chartPage = myChart.Chart;



                        chartRange = ObjWorkSheet.get_Range("a1", "h3");

                        chartPage.SetSourceData(chartRange, misValue);

                        chartPage.ChartType = Excel.XlChartType.xlColumnClustered;



                        bg.ReportProgress(18);





                        ObjExcel.Visible = false;
                        ObjExcel.UserControl = true;
                        ObjExcel.DisplayAlerts = false;
                        ObjWorkBook.Saved = true;
                        try
                        {

                            ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookNormal);
                            Program.HandledShops.Add(Program.currentShop.getIdShop());
                            // ObjWorkBook.SaveAs(filename);


                            ObjWorkBook.Close(0);

                            ObjExcel.Quit();
                            MessageBox.Show("Файл создан");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка записи в файл " + ex.Message);
                            ObjWorkBook.Close(0);
                            ObjExcel.Quit();
                        }


                        bg.ReportProgress(18);



                        break;
                    }
                default:
                    break;
            }

        }

        private SD.DataTable viewTSR()
        {
            //создаём таблицу
            string[] months = Program.getMonths();
            SD.DataTable dt = new SD.DataTable("norm");
            //создаём три колонки
            Program.readTSR();

            DataColumn colCountDayInMonth = new DataColumn("Должность", typeof(string));
            DataColumn colCountDayRab = new DataColumn("Количество", typeof(Int16));
            DataColumn colCountDayVuh = new DataColumn("Зарплата", typeof(Int16));
            DataColumn normCh = new DataColumn("Зарплата за 1/2", typeof(Int16));

            //добавляем колонки в таблицу

            dt.Columns.Add(colCountDayInMonth);
            dt.Columns.Add(colCountDayRab);
            dt.Columns.Add(colCountDayVuh);
            dt.Columns.Add(normCh);
            DataRow row = null;
            //создаём новую строку

            //заполняем строку значениями
            if (Program.TSRTG)
            {
                foreach (TSR tsr in Program.currentShop.tsr)
                {
                    row = dt.NewRow();
                    row["Должность"] = tsr.getOtobragenie();
                    row["Количество"] = tsr.getCount();
                    row["Зарплата"] = tsr.getZarp();
                    row["Зарплата за 1/2"] = tsr.getZarp1_2();
                    dt.Rows.Add(row);
                }
            }
            else
            {
                foreach (TSR tsr in Program.currentShop.tsrBG)
                {
                    row = dt.NewRow();
                    row["Должность"] = tsr.getOtobragenie();
                    row["Количество"] = tsr.getCount();
                    row["Зарплата"] = tsr.getZarp();
                    row["Зарплата за 1/2"] = tsr.getZarp1_2();
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }



        private SD.DataTable viewFactors()
        {
            //создаём таблицу
            string[] months = Program.getMonths();
            SD.DataTable dt = new SD.DataTable("norm");
            //создаём три колонки


            DataColumn colCountDayInMonth = new DataColumn("Название", typeof(string));
            DataColumn colCountDayRab = new DataColumn("Текущее значение", typeof(Int16));
            DataColumn colCountDayVuh = new DataColumn("действует на текущую дату", typeof(bool));
            DataColumn normCh = new DataColumn("Действует до даты", typeof(DateTime));
            DataColumn rr = new DataColumn("Новое значение", typeof(int));

            //добавляем колонки в таблицу

            dt.Columns.Add(colCountDayInMonth);
            dt.Columns.Add(colCountDayRab);
            dt.Columns.Add(colCountDayVuh);
            dt.Columns.Add(normCh);
            dt.Columns.Add(rr);
            DataRow row = null;
            //создаём новую строку

            //заполняем строку значениями

            foreach (Factor f in Program.currentShop.factors)
            {
                row = dt.NewRow();
                row["Название"] = f.getOtobragenie();
                row["Текущее значение"] = f.getTZnach();
                row["Действует на текущую дату"] = f.getDeistvie();
                row["Действует до даты"] = f.getData();
                row["Новое значение"] = f.getNewZnach();
                dt.Rows.Add(row);
            }
            return dt;
        }

        private SD.DataTable viewVarSmen()
        {
            //создаём таблицу
            string[] months = Program.getMonths();
            SD.DataTable dt = new SD.DataTable("norm");
            //создаём три колонки


            DataColumn colCountDayInMonth = new DataColumn("Количество рабочих дней", typeof(string));
            DataColumn colCountDayRab = new DataColumn("Количество выходных дней", typeof(Int16));
            DataColumn colCountDayVuh = new DataColumn("действует на текущую дату", typeof(bool));


            //добавляем колонки в таблицу

            dt.Columns.Add(colCountDayInMonth);
            dt.Columns.Add(colCountDayRab);
            dt.Columns.Add(colCountDayVuh);

            DataRow row = null;
            //создаём новую строку

            //заполняем строку значениями
            Program.currentShop.VarSmens.Clear();
            Program.readVarSmen();
            foreach (VarSmen f in Program.currentShop.VarSmens)
            {
                row = dt.NewRow();
                row["Количество рабочих дней"] = f.getR();
                row["Количество выходных дней"] = f.getV();
                row["действует на текущую дату"] = f.getDeistvie();

                dt.Rows.Add(row);
            }
            return dt;
        }


        public void getChart()
        {


            List<hourSale> Hss = new List<hourSale>();
            // MessageBox.Show(Program.HSS[1].getData().Date.ToString());
            // Hss=HSS.FindAll(p => p.getData().Date == );

            int[] TimeObr = new int[15];
            int[] TimeSotr = new int[15];


            //     labelData.Text = (Nday + 1) + " марта";

            chartX = new int[15];
            chartY2 = new int[15];
            chartY1 = new int[15];

            for (int i = 8, n = 0; i < 23; n++, i++)
            {
                //   if ((textBoxSpeed.Text == "") || (textBoxTimeClick.Text == "") || (textBoxTimeTell.Text == "")) { MessageBox.Show("Не все данные введены"); break; }
                // else
                {
                    //    TimeObr[n] = (Program.CountCheck[Nday, n] * Int32.Parse(textBoxTimeTell.Text) + Program.CountClick[Nday, n] * Int32.Parse(textBoxTimeClick.Text))/60;
                    //   TimeSotr[n] = Program.CountS[Nday, n] * Int32.Parse(textBoxSpeed.Text)/60;
                    chartX[n] = i;

                    chartY2[n] = TimeSotr[n];
                    chartY1[n] = TimeObr[n];
                }
            }

        }


        private void ExportToExcel()
        {
            Excel.Application exApp = new Excel.Application();
            exApp.Visible = false;
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1, 1] = "ID";
            workSheet.Cells[1, 2] = "Name";
            workSheet.Cells[1, 3] = "Age";
            int rowExcel = 2;
            for (int i = 0; i < dataGridViewFactors.Rows.Count; i++)
            {
                workSheet.Cells[rowExcel, "A"] = dataGridViewFactors.Rows[i].Cells["ID"].Value;
                workSheet.Cells[rowExcel, "B"] = dataGridViewFactors.Rows[i].Cells["Name"].Value;
                workSheet.Cells[rowExcel, "C"] = dataGridViewFactors.Rows[i].Cells["Age"].Value;
                ++rowExcel;
            }
            workSheet.SaveAs("MyFile.xls");
            exApp.Quit();
        }

        public Form1()
        {
            InitializeComponent();
            SetNextYearCalendarButton();
            Resize += Form1_Resize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                if (Program.isProcessing)
                {
                    Hide();
                }
                notifyIcon1.Visible = true;
            }
        }

        private void Form1_GotFocus(object sender, EventArgs e)
        {
            
        }

        private void SetNextYearCalendarButton()
        {
            if (Helper.CheckNextYearCalendarIsExist())
            {
                buttonRaspisanie.Location = new Point(315, 12);
                buttonCalendarNextYear.Visible = true;
                buttonCalendarNextYear.Text = DateTime.Now.AddYears(1).Year.ToString();
            }
            else
            {
                buttonRaspisanie.Location = new Point(287, 12);
                buttonCalendarNextYear.Visible = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportToExcel();


        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Graphics g; // pictureBox1.CreateGraphics();
            Pen myPen = new Pen(Color.Blue);
            int count = 10;
            int x = 1;
            float[] y = { 304, 384, 444, 351, 502, 499, 325, 364, 380, 561, 560 };
            float xmax = count, ymax = 2500;

            float kx = xmax, ky = ymax;

            for (int i = 0; i < count; i++)
            {

                System.Drawing.Point point1 = new System.Drawing.Point(Convert.ToInt32(x * kx),
                    Convert.ToInt32(y[i] * ky));
                x++;
                System.Drawing.Point point2 = new System.Drawing.Point(Convert.ToInt32(kx * x),
                    Convert.ToInt32(y[i + 1] * ky));
                //   g.DrawLine(myPen, point1, point2);


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }


        private void releaseObject(object obj)

        {

            try

            {

                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);

                obj = null;

            }

            catch (Exception ex)

            {

                obj = null;

                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());

            }

            finally

            {

                GC.Collect();

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string writePath = @"C:\1\Hours.txt";
            using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
            {
                List<hourSale> hSs = new List<hourSale>();
                hourSale h;
                var connectionString =
                    $"Data Source={Settings.Default.DatabaseAddress};Persist Security Info=True;User ID={Settings.Default.DatabaseLogin};Password={Settings.Default.DatabasePassword}";
                string sql = "select * from dbo.get_StatisticByShopsDayHour('301', '2017/01/02', '2017/01/04 23:59:00')";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.CommandTimeout = 300;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            h = new hourSale(reader.GetInt16(0), reader.GetDateTime(1), reader.GetString(2),
                                reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                            hSs.Add(h);

                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        MessageBox.Show("Ошибка соединения с базой данных" + ex);
                    }
                }

                List<hourSale>[,] hss = new List<hourSale>[7, 24];
                for (int i = 0; i < Program.collectionweekday.Length; i++)
                {
                    for (int j = 0; j < Program.collectionHours.Length; j++)
                    {
                        int max = 0;
                        int min = 100000;
                        float sr = 0;
                        hss[i, j] =
                            hSs.FindAll(
                                t =>
                                    ((t.getWeekday() == Program.collectionweekday[i]) &&
                                     (t.getNHour() == Program.collectionHours[j])));
                        if (hss[i, j].Count != 0)
                        {
                            foreach (hourSale hh in hss[i, j])
                            {
                                if (hh.getMinut() < min)
                                    min = hh.getMinut();
                                if (hh.getMinut() > max)
                                    max = hh.getMinut();
                                sr += hh.getMinut();
                                // sw.WriteLine(hh.getWeekday()+" "+hh.getNHour()+" "+hh.getCountClick());
                            }
                            sr = sr / (hss[i, j].Count);
                            sw.WriteLine(Program.collectionweekday[i] + " Время=" + Program.collectionHours[j + 1] +
                                         " Количество данных=" + hss[i, j].Count + " среднее=" + sr + " минимум=" + min +
                                         " максимум=" + max);
                        }

                    }


                }
                MessageBox.Show("Запись завершена");
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {


            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;

            bw1.WorkerReportsProgress = true;
            bw1.WorkerSupportsCancellation = true;
            bw1.DoWork += bw1_DoWork;
            //bw1.ProgressChanged += bw1_ProgressChanged;
            bw1.RunWorkerCompleted += bw1_RunWorkerCompleted;
            radioButtonObRabTime.Checked = true;
            Program.ReadListShops();
            // Program.setListShops();
            tabControl1.Visible = false;
            buttonTest.Visible = false;
            progressBar1.Visible = false;
            label3.Visible = false;

            SetDataGridViewValidating();


            if (Program.listShops != null)
            {
                foreach (mShop h in Program.listShops)
                {

                    listBox1.Items.Add(h.getIdShop() + "_" + h.getAddress());
                }
            }
            //Helper.CheckShopsStatus();
            // textBoxSpeed.Text = 20 + "";
            // textBoxTimeTell.Text = 25 + "";
            // textBoxTimeClick.Text = 4 + "";


            //if (/*Program.isConnect()*/true) {   }
            UpdateStatusShops();
            //labelStatus1.Text = "Статус: Обработано " + Program.HandledShops.Count + " магазинов из " + Program.listShops.Count;
            labelStatus2.Text = " режим работы локальный";
            radioButtonIzFile.Checked = true;
        }



        private void buttonFactors_Click(object sender, EventArgs e)
        {
            buttonFactors.BackColor = Color.MistyRose;
            buttonVariantsSmen.BackColor = Color.White;
            buttonParamOptimiz.BackColor = Color.White;
            panelFactors.BringToFront();
            dataGridViewFactors.DataSource = viewFactors();
            dataGridViewFactors.Columns[0].ReadOnly = true;
        }

        private void buttonVariantsSmen_Click(object sender, EventArgs e)
        {
            buttonVariantsSmen.BackColor = Color.MistyRose;
            buttonFactors.BackColor = Color.White;
            buttonParamOptimiz.BackColor = Color.White;
            panelDopusVarSmen.BringToFront();
            dataGridViewVarSmen.DataSource = viewVarSmen();


            dataGridViewVarSmen.Columns[0].ReadOnly = true;
            dataGridViewVarSmen.Columns[1].ReadOnly = true;





            if (Program.currentShop.minrab.getOtobragenie())
            {
                tbKassirCount.Text = Program.currentShop.minrab.getMinCount().ToString();
                tbLastHour.Text = Program.currentShop.minrab.getTimeMinRab().ToString();
            }
            else
            {
                tbKassirCount.Text = "";
                tbLastHour.Text = "";
            }
            // Program.ReadMinRab();






        }


        private void buttonParamOptimiz_Click(object sender, EventArgs e)
        {
            buttonParamOptimiz.BackColor = Color.MistyRose;
            buttonFactors.BackColor = Color.White;
            buttonVariantsSmen.BackColor = Color.White;
            panelParamOptim.BringToFront();
            String readPath = Environment.CurrentDirectory + "/Shops/" + Program.currentShop.getIdShop() +
                              "/parametrOptimization.txt";
            ;
            try
            {
                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {

                    Program.ParametrOptimization = short.Parse(sr.ReadLine());
                }

            }
            catch
            {
                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    sw.WriteLine("0");
                    Program.ParametrOptimization = 0;
                }
                Program.HandledShops.Add(Program.currentShop.getIdShop());
                UpdateStatusShops();
            }

            switch (Program.ParametrOptimization)
            {
                case 0:
                    break;
                case 1:
                    radioButtonMinFondOpl.Select();
                    break;
                case 2:
                    radioButtonMinTime.Select();
                    break;
                case 3:
                    radioButtonObRabTime.Select();
                    break;
                default:
                    Program.ParametrOptimization = 0;
                    break;
            }

        }






        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBox31_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBox32_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBox41_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBox42_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBox51_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBox52_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void buttonImportKasOper_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "*|*.xls";
            openFileDialog.RestoreDirectory = true;
            string filepath = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog.FileName;
            }

            try
            {
                List<hourSale> hourSales = Helper.FillHourSalesList(filepath, Program.currentShop.getIdShop());
                foreach (hourSale hs in hourSales)
                {
                    if (Program.currentShop.daysSale.Find(t => t.getData() == hs.getData()) != null)
                    {
                        Program.currentShop.daysSale.Find(t => t.getData() == hs.getData()).Add(hs);
                    }
                    else
                    {
                        Program.currentShop.daysSale.Add(new daySale(Program.currentShop.getIdShop(), hs.getData()));
                        Program.currentShop.daysSale.Find(t => t.getData() == hs.getData()).Add(hs);
                    }
                }
                MessageBox.Show("Чтение завершено");
                Program.ExistFile = true;


                /* //Если вдруг папки нет, надо создать
                 string writePath = @"C:\1\Hours.txt";
 
                 if (!Directory.Exists(@"C:\1\"))
                 {
                     Directory.CreateDirectory(@"C:\1\");
                 }
                 
                 using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                 {
                     List<hourSale>[,] hss = new List<hourSale>[7, 24];
                     for (int i = 0; i < Program.collectionweekday.Length; i++)
                     {
                         for (int j = 0; j < Program.collectionHours.Length; j++)
                         {
                             int max = 0;
                             int min = 100000;
                             float sr = 0;
                             hss[i, j] = hourSales.FindAll(t => ((t.getWeekday() == Program.collectionweekday[i]) && (t.getNHour() == Program.collectionHours[j])));
                             if (hss[i, j].Count != 0)
                             {
                                 foreach (hourSale hh in hss[i, j])
                                 {
                                     if (hh.getMinut() < min)
                                         min = hh.getMinut();
                                     if (hh.getMinut() > max)
                                         max = hh.getMinut();
                                     sr += hh.getMinut();
                                     // sw.WriteLine(hh.getWeekday()+" "+hh.getNHour()+" "+hh.getCountClick());
                                 }
                                 sr = sr / (hss[i, j].Count);
                                 sw.WriteLine(Program.collectionweekday[i] + " Время=" + Program.collectionHours[j + 1] + " Количество данных=" + hss[i, j].Count + " среднее=" + sr + " минимум=" + min + " максимум=" + max);
                             }
                         }
                     }
                     MessageBox.Show("Запись завершена");
                 }*/
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Файл {ex.FileName} поврежден или не найден");
            }
            catch
            {
                MessageBox.Show("Произошла критическая ошибка! Использование данных из файла невозможно!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButtonIzFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIzFile.Checked)
            {
                buttonImportKasOper.Visible = true;
                buttonVygr.Visible = false;
                comboBox2.Visible = false;

            }

        }

        private void radioButtonIzBD_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIzBD.Checked && !isConnected)
            {
                buttonImportKasOper.Visible = false;
                buttonVygr.Visible = true;
                comboBox2.Visible = true;

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Program.ReadConfigShop();
            //MessageBox.Show(listBox1.Text);
            // tabControl1.PerformLayout();
            tabControl1.SelectTab(tabPage1);
            buttonKassov.PerformClick();
            Program.currentShop = null;
            string[] s = new string[2];
            s = listBox1.Text.Split('_');
            Program.currentShop = new Shop(Int16.Parse(s[0]), s[1]);
            Program.currentShop.setMinRab(Program.ReadMinRab());
            buttonParamOptimiz.PerformClick();
            Program.getListDate(DateTime.Today.Year);
            Program.readTSR();
            Program.readFactors();
            Program.readVarSmen();
            Program.ReadMinRab();
            Program.ReadPrilavki();
            Program.ReadParametrOptimizacii();
            Program.ExistFile = false;
            if (Program.currentShop.VarSmens.Count == 0)
            {
                //  VarSmen.CreateVarSmen();
            }
            tabControl1.Visible = true;
        }



        private void button_refresh_list_shops_Click(object sender, EventArgs e)
        {
            Program.setListShops();
            Program.refreshFoldersShops();
            foreach (mShop h in Program.listShops)
            {

                listBox1.Items.Add(h.getIdShop() + "_" + h.getAddress());
            }
        }

        private void buttonGruz_Click(object sender, EventArgs e)
        {
            panelGruz.BringToFront();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            Nday++;
            if (Nday < Program.currentShop.templates.Count)
            {
                /* getChart();
                 // 
                 chart1.Series["s1"].Points.DataBindXY(chartX, chartY1);
                 chart1.Series["s2"].Points.DataBindXY(chartX, chartY2);
                 */

                Program.currentShop.templates[Nday].createChartTemplate();
                Program.currentShop.templates[Nday].DS.CreateChartDaySale();

            }
            else
            {
                Nday--;
                MessageBox.Show("Больше данных нет");
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Program.ReadTekChedule(openFileDialog1.FileName);
        }

        private void buttonReadTekShedule_Click(object sender, EventArgs e)
        {
            // openFileDialog1.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {


        }

        private void buttonReadCalendarFromXML_Click(object sender, EventArgs e)
        {
            //  Program.ReadCalendarFromXML();
        }

        private void buttonCalendar_Click(object sender, EventArgs e)
        {
            buttonCalendar.BackColor = Color.MistyRose;
            buttonRaspisanie.BackColor = Color.White;
            buttonKassov.BackColor = Color.White;
            panelCalendar.BringToFront();

            buttonImportKasOper.Visible = false;
            // ShowProizvCalendar();
            Form4 f4 = new Form4(DateTime.Now.Year);
            f4.Show();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {

            Excel.Application xlApp;

            Excel.Workbook xlWorkBook;

            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;



            xlApp = new Excel.Application();

            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);



            //add data 

            xlWorkSheet.Cells[1, 1] = "";

            xlWorkSheet.Cells[1, 2] = "Student1";

            xlWorkSheet.Cells[1, 3] = "Student2";

            xlWorkSheet.Cells[1, 4] = "Student3";



            xlWorkSheet.Cells[2, 1] = "Term1";

            xlWorkSheet.Cells[2, 2] = "80";

            xlWorkSheet.Cells[2, 3] = "65";

            xlWorkSheet.Cells[2, 4] = "45";



            xlWorkSheet.Cells[3, 1] = "Term2";

            xlWorkSheet.Cells[3, 2] = "78";

            xlWorkSheet.Cells[3, 3] = "72";

            xlWorkSheet.Cells[3, 4] = "60";



            xlWorkSheet.Cells[4, 1] = "Term3";

            xlWorkSheet.Cells[4, 2] = "82";

            xlWorkSheet.Cells[4, 3] = "80";

            xlWorkSheet.Cells[4, 4] = "65";



            xlWorkSheet.Cells[5, 1] = "Term4";

            xlWorkSheet.Cells[5, 2] = "75";

            xlWorkSheet.Cells[5, 3] = "82";

            xlWorkSheet.Cells[5, 4] = "68";



            Excel.Range chartRange;



            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);

            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(20, 80, 300, 250);

            Excel.Chart chartPage = myChart.Chart;



            chartRange = xlWorkSheet.get_Range("B1", "d3");

            chartPage.SetSourceData(chartRange, misValue);

            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;



            xlWorkBook.SaveAs(@"C:\1\csharp.net-informations.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue,
                misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue,
                misValue, misValue);
            Program.HandledShops.Add(Program.currentShop.getIdShop());

            xlWorkBook.Close(true, misValue, misValue);

            xlApp.Quit();



            releaseObject(xlWorkSheet);

            releaseObject(xlWorkBook);

            releaseObject(xlApp);



            MessageBox.Show("Excel file created , you can find the file c:\\csharp.net-informations.xls");
        }

        private void buttonKassov_Click(object sender, EventArgs e)
        {
            buttonKassov.BackColor = Color.MistyRose;
            buttonCalendar.BackColor = Color.White;
            buttonRaspisanie.BackColor = Color.White;
            panelKassOper.BringToFront();

        }

        private void buttonRaspisanie_Click(object sender, EventArgs e)
        {
            Program.TSRTG = true;
            buttonRaspisanie.BackColor = Color.MistyRose;
            buttonCalendar.BackColor = Color.White;
            buttonKassov.BackColor = Color.White;
            panelTRasp.BringToFront();
            dataGridViewForTSR.DataSource = viewTSR();
            dataGridViewForTSR.Columns[0].ReadOnly = true;
            textBox6.Text = Program.currentShop.prilavki.GetCount().ToString();
            checkBox1.Checked = Program.currentShop.prilavki.GetNalichie();


        }






        private void Form1_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void buttonPTSR_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                Program.currentShop.prilavki.SetCount(int.Parse(textBox6.Text));
            }
            else {
                Program.currentShop.prilavki.SetCount(0);
            }
            Program.currentShop.prilavki.SetNalichie(checkBox1.Checked);
            Program.WriteTSR();
            Program.WritePrilavki();
            MessageBox.Show("Данные сохранены");
        }

        private void buttonAplyFactors_Click(object sender, EventArgs e)
        {

            Program.WriteFactors();
            MessageBox.Show("Данные сохранены");
        }

        private void buttonAplyVarSmen_Click(object sender, EventArgs e)
        {
            String readPath = Environment.CurrentDirectory + @"\Shops\" + Program.currentShop.getIdShop() + @"\VarSmen";

            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {
                foreach (VarSmen vs in Program.currentShop.VarSmens)
                {
                    sw.WriteLine(vs.getR() + "#" + vs.getV() + "#" + vs.getDeistvie());
                }

            }
            Program.HandledShops.Add(Program.currentShop.getIdShop());
            UpdateStatusShops();

            Program.WriteMinRab();
            MessageBox.Show("Данные сохранены");
        }

        private void buttonMultShops_Click(object sender, EventArgs e)
        {
            panelMultShops.BringToFront();
            Program.shops = new List<Shop>();
            Program.currentShop = null;
            Program.IsMpRezhim = true;

            Program.currentShop = new Shop(0, "");
            Program.currentShop.setMinRab(Program.ReadMinRab());
            Program.getListDate(DateTime.Today.Year);
            Program.readTSR();
            Program.readFactors();
            Program.readVarSmen();
            Program.ReadPrilavki();
            Program.ReadParametrOptimizacii();
            if (Program.currentShop.VarSmens.Count == 0)
            {
                // VarSmen.CreateVarSmen();
            }
            tabControl1.Visible = true;


            listBoxMShops.Items.AddRange(listBox1.Items);
        }

        private void buttonSingleShop_Click(object sender, EventArgs e)
        {
            panelSingleShop.BringToFront();
            Program.IsMpRezhim = false;
            //Program.shops = new List<Shop>();
        }

        private void buttonMadd_Click(object sender, EventArgs e)
        {
            string ss = listBoxMShops.Text;
            string[] aaa = new string[2];
            aaa = ss.Split('_');
            foreach (string s in listBoxMPartShops.Items)
            {
                if (listBoxMShops.Text == s)
                {
                    ss = "";
                }
            }


            if (ss != "")
            {
                listBoxMPartShops.Items.Add(ss);
                Program.shops.Add(new Shop(0, aaa[1], int.Parse(aaa[0])));
            }

        }

        private void buttonMdel_Click(object sender, EventArgs e)
        {
            try
            {
                string shopAddress = listBoxMPartShops.SelectedItem.ToString().Split('_')[1];
                if (listBoxMPartShops.Text != "")
                {
                    listBoxMPartShops.Items.Remove(listBoxMPartShops.SelectedItem);
                    Shop s = Program.shops.Find(t => t.getAddress() == shopAddress);
                    if (s != null && Program.shops.Contains(s))
                    {
                        Program.shops.Remove(s);
                    }
                }
            }
            catch { }

        }

        private void radioButtonMinFondOpl_CheckedChanged(object sender, EventArgs e)
        {
            Program.ParametrOptimization = 0;
        }

        private void radioButtonMinTime_CheckedChanged(object sender, EventArgs e)
        {
            Program.ParametrOptimization = 2;
        }

        private void radioButtonObRabTime_CheckedChanged(object sender, EventArgs e)
        {
            Program.ParametrOptimization = 1;
        }

        private void buttonApplyParamsOptim_Click(object sender, EventArgs e)
        {
            String writePath = Environment.CurrentDirectory + @"\parametrzoptimization";
            using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
            {

                try
                {
                    sw.Write(Program.ParametrOptimization.ToString());
                    Program.HandledShops.Add(Program.currentShop.getIdShop());
                    UpdateStatusShops();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }

            MessageBox.Show("Данные сохранены");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonExport1_Click(object sender, EventArgs e)
        {

            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите что отобразить!");
                return;
            }

            if (radioButtonIzBD.Checked && !isConnected)
            {
                Form3 f3 = new Form3(3);
                f3.Show(this);
                this.Enabled = false;
                return;
            }

            StartExportingToExcel();
        }

        public void StartExportingToExcel()
        {
            saveFileDialog1.DefaultExt = ".XLS";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "Файл Excel|*.XLSX;*.XLS";
            saveFileDialog1.InitialDirectory = Environment.CurrentDirectory + @"\Shops\" +
                                               Program.currentShop.getIdShop();
            saveFileDialog1.Title = "Выберите папку для сохранения расписания";

            if (radioButtonMinFondOpl.Checked)
            {
                Program.ParametrOptimization = 0;
            }

            if (radioButtonMinTime.Checked)
            {
                Program.ParametrOptimization = 2;
            }
            if (radioButtonObRabTime.Checked)
            {
                Program.ParametrOptimization = 1;
            }


            switch (comboBox3.SelectedIndex)

            {
                case 0:
                    saveFileDialog1.FileName = "График_" + Program.currentShop.getAddress() + "_" +
                                               Program.getMonths(DateTime.Now.AddMonths(1).Month);
                    Program.TipExporta = 0;
                    break;
                case 1:
                    saveFileDialog1.FileName = "Прогноз_" + Program.currentShop.getAddress();
                    Program.TipExporta = 1;
                    break;
                case 2:
                    saveFileDialog1.FileName = "Потребность в персонале" + Program.currentShop.getAddress();
                    Program.TipExporta = 2;
                    break;
                case 3:
                    saveFileDialog1.FileName = "Экономический эффект" + Program.currentShop.getAddress();
                    Program.TipExporta = 3;
                    break;
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл

            if (radioButtonIzBD.Checked && !Program.isConnected(Program.login, Program.password))
            {
                MessageBox.Show("Соединение с базой не установлено! Выберите режим \"из файла\" или подключитесь к базе данных.");
                return;
            }
            if (radioButtonIzFile.Checked && !Program.ExistFile)
            {
                MessageBox.Show("Загрузите данные из файла");
                return;
            }

            try
            {
                filename = saveFileDialog1.FileName;
                listBox1.Enabled = false;
                progressBar1.Visible = true;

                label3.Text = "";
                label3.Visible = true;
                progressBar1.Maximum = 20;
                progressBar1.Minimum = 0;
                progressBar1.Step = 2;
                ReadTipOptimizacii();

                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                CloseProcessOnError();
                MessageBox.Show(ex.Message);
            }
        }



        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void ReadTipOptimizacii()
        {

            if (radioButtonMinFondOpl.Checked)
            {
                Program.ParametrOptimization = 0;
            }
            if (radioButtonMinTime.Checked)
            {
                Program.ParametrOptimization = 2;
            }
            if (radioButtonObRabTime.Checked)
            {
                Program.ParametrOptimization = 1;
            }

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }



        private void button6_Click_1(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите что отобразить!");
                return;
            }
            else if (radioButtonIzBD.Checked && !isConnected)
            {
                Form3 f3 = new Form3(2);
                f3.Show(this);
                this.Enabled = false;
                return;
            }
            else if (radioButtonIzFile.Checked && !Program.ExistFile)
            {
                MessageBox.Show("Загрузите данные из файла");
                return;
            }
            else if ((radioButtonIzBD.Checked && isConnected) || (radioButtonIzFile.Checked && Program.ExistFile))
            {
                StartDiagramForm();
            }
        }

        public void StartDiagramForm()
        {
            Form5 f5;
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    Program.tipDiagram = 0;
                    MessageBox.Show("Для графика доступен только экспорт в excel");
                    break;
                case 1:
                    Program.tipDiagram = 1;
                    f5 = new Form5();
                    f5.Show();
                    break;
                case 2:
                    Program.tipDiagram = 2;
                    f5 = new Form5();
                    f5.Show();
                    break;
                case 3:
                    Program.tipDiagram = 3;
                    f5 = new Form5();
                    f5.Show();
                    break;
            }
        }


        private void dataGridViewForTSR_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Program.TSRTG)
            {
                switch (e.ColumnIndex)
                {
                    case 1:
                        Program.currentShop.tsr.Find(
                                t => t.getOtobragenie() == dataGridViewForTSR[0, e.RowIndex].Value.ToString())
                            .setCount(int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString()));
                        break;
                    case 2:
                        if (int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString())<8000)
                        {
                            MessageBox.Show("Зарплата не может быть меньше 8 тыс. руб.");
                            dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value = 8000;
                        }
                        Program.currentShop.tsr.Find(
                                t => t.getOtobragenie() == dataGridViewForTSR[0, e.RowIndex].Value.ToString())
                            .setZarp(int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString()));
                        break;
                    case 3:

                        if (int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString()) < 8000)
                        {
                            MessageBox.Show("Зарплата не может быть меньше 4 тыс. руб.");
                            dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value = 4000;
                        }
                        Program.currentShop.tsr.Find(
                                t => t.getOtobragenie() == dataGridViewForTSR[0, e.RowIndex].Value.ToString())
                            .setZarp1_2(int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString()));
                        break;

                }
            }
            else
            {
                switch (e.ColumnIndex)
                {
                    case 1:
                        Program.currentShop.tsrBG.Find(
                                t => t.getOtobragenie() == dataGridViewForTSR[0, e.RowIndex].Value.ToString())
                            .setCount(int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString()));
                        break;
                    case 2:
                        if (int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString()) < 8000)
                        {
                            MessageBox.Show("Зарплата не может быть меньше 8 тыс. руб.");
                            dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value = 8000;
                        }
                        Program.currentShop.tsrBG.Find(
                                t => t.getOtobragenie() == dataGridViewForTSR[0, e.RowIndex].Value.ToString())
                            .setZarp(int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString()));
                        break;
                    case 3:
                        if (int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString()) < 8000)
                        {
                            MessageBox.Show("Зарплата не может быть меньше 4 тыс. руб.");
                            dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value = 4000;
                        }
                            Program.currentShop.tsrBG.Find(
                                t => t.getOtobragenie() == dataGridViewForTSR[0, e.RowIndex].Value.ToString())
                            .setZarp1_2(int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString()));
                        break;

                }
            }
        }

        private void dataGridViewFactors_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    Program.currentShop.factors.Find(
                            t => t.getOtobragenie() == dataGridViewFactors[0, e.RowIndex].Value.ToString())
                        .setTZnach(int.Parse(dataGridViewFactors[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;
                case 2:
                    Program.currentShop.factors.Find(
                            t => t.getOtobragenie() == dataGridViewFactors[0, e.RowIndex].Value.ToString())
                        .setDeistvie(bool.Parse(dataGridViewFactors[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;
                case 3:
                    Program.currentShop.factors.Find(
                            t => t.getOtobragenie() == dataGridViewFactors[0, e.RowIndex].Value.ToString())
                        .setData(DateTime.Parse(dataGridViewFactors[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;
                case 4:
                    Program.currentShop.factors.Find(
                            t => t.getOtobragenie() == dataGridViewFactors[0, e.RowIndex].Value.ToString())
                        .setNewZnach(int.Parse(dataGridViewFactors[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;

            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            button5.BackColor = Color.MistyRose;
            button8.BackColor = Color.White;
            button7.BackColor = Color.White;
            panel3.BringToFront();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.BackColor = Color.MistyRose;
            button7.BackColor = Color.White;
            button5.BackColor = Color.White;
            panel2.BringToFront();
            dataGridView1.DataSource = viewFactors();
            dataGridView1.Columns[0].ReadOnly = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Program.currentShop.minrab.getOtobragenie())
            {
                tbKassirCount.Text = Program.currentShop.minrab.getMinCount().ToString();
                tbLastHour.Text = Program.currentShop.minrab.getTimeMinRab().ToString();
            }
            else
            {
                tbKassirCount.Text = "";
                tbLastHour.Text = "";
            }

            button7.BackColor = Color.MistyRose;
            button8.BackColor = Color.White;
            button5.BackColor = Color.White;
            panel4.BringToFront();

            dataGridViewMVarSmen.DataSource = viewVarSmen();


            dataGridViewMVarSmen.Columns[0].ReadOnly = true;
            dataGridViewMVarSmen.Columns[1].ReadOnly = true;


        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Program.shops.Count + "");
            if (Program.isConnected(Program.login, Program.password))
            {
                CreateZip();
            }
            else
            {
                Form3 f3 = new Form3(1);
                f3.Show(this);
                this.Enabled = false;
            }

        }


        public void CreateZip()
        {
            saveFileDialog1.DefaultExt = ".zip";
            saveFileDialog1.FileName = "Архив графиков, гистограм по нескольким магазинам";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "Архив|*.zip";
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.Title = "Выберите папку для сохранения архива";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PathToZip = Path.GetFullPath(saveFileDialog1.FileName);
            }
            else
            {
                return;
            }


            DisableControlsOnStart();
            MessageBox.Show($"Выбрано {Program.shops.Count} магазинов, примерное время ожидания {Program.shops.Count * 5} минут");
            progressBar2.Visible = true;

            label3.Text = "";
            label3.Visible = true;
            progressBar1.Maximum = 20;
            progressBar1.Minimum = 0;
            progressBar1.Step = 2;
           // progressBar2.Visible = true;

            Program.TipExporta = comboBox1.SelectedIndex;
            bw1.RunWorkerAsync();


        }

        private void bw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {

            }
            else if (e.Error != null)
            {

            }
            else
            {
                if (!errorOnExecuting)
                {
                    progressBar1.Value = progressBar1.Maximum;
                    progressBar1.Visible = false;
                    label3.Visible = false;
                    listBox1.Enabled = true;
                    MessageBox.Show("Архив создан");
                }
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Maximized;
                Application.Exit();
            }
            EnableControlsOnFinish();
            Program.isProcessing = false;

        }

        private void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }

        private delegate void updateLabel3Delegate(string text);
        private void bw1_DoWork(object sender, DoWorkEventArgs e)
        {
            Program.isProcessing = true;
            try
            {
                foreach (Process proc in Process.GetProcessesByName("EXCEL"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (Directory.Exists(Environment.CurrentDirectory + @"\mult\"))
            {
                foreach (var file in Directory.GetFiles(Environment.CurrentDirectory + @"\mult\"))
                {

                    File.Delete(file);
                }
            }

            int ShopStep = 100 / Program.shops.Count;
            int TaskStep = ShopStep / 4;
            BackgroundWorker bg = sender as BackgroundWorker;

            Program.currentShop = new Shop(0, "");
            bg.ProgressChanged += bg_ProgressChanged;
            Program.BgProgress = 0;

            switch (Program.TipExporta)
            {
                case 0:
                    {
                        TaskStep = ShopStep / 4;
                        break;
                    }
                default:
                    {
                        TaskStep = ShopStep / 3;
                        break;
                    }
            }
            int shopCounter = 0;
            shopCounter++;

            Program.getListDate(DateTime.Today.Year);
            Program.readTSR();
            Program.readFactors();
            Program.readVarSmen();


            foreach (Shop shop in Program.shops)
            {
                shop.setMinRab(Program.ReadMinRab());
                Program.currentShop.setIdShop(shop.getIdShopFM());
                Program.currentShop.setMinRab(shop.minrab);
                Program.ReadPrilavki();
                if (Program.currentShop.VarSmens.Count == 0)
                {
                    //  VarSmen.CreateVarSmen();
                }

                if (!Directory.Exists(Environment.CurrentDirectory + @"\mult\"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + @"\mult\");
                }

                filename = Environment.CurrentDirectory + @"\mult\" + Program.currentShop.getIdShopFM() + ".xls";
                switch (Program.TipExporta)
                {
                    case 0:
                        {
                            try
                            {
                                bg.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Создание прогноза продаж");

                                Program.createPrognoz(Program.IsMpRezhim);

                                bg.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Подсчет оптимальной загруженности");

                                Sotrudniki.OptimCountSotr();

                                bg.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Оптимальная расстановка смен");
                                Sotrudniki.CreateSmens();


                            }
                            catch (Exception ex)
                            {

                                //  label3.Visible = false;
                                //  progressBar1.Visible = false;
                                MessageBox.Show(ex.Message);
                                string[] s = new string[2];
                                // s = listBox1.Text.Split('_');
                                //  Program.currentShop = new Shop(Int16.Parse(s[0]), s[1]);
                                //  Program.getListDate(DateTime.Today.Year);
                                Program.readTSR();
                                MessageBox.Show("Расписание не создано");
                                //CloseProcessOnError();
                                //return;
                            }
                            //  System.Drawing.Color color;

                            System.Drawing.Color color;
                            Excel.Range excelcells;
                            Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                            ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);

                            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                            ObjWorkSheet.Name = "График";
                            excelcells = ObjWorkSheet.get_Range("A3", "AL100");
                            excelcells.Font.Size = 10;
                            excelcells.NumberFormat = "@";
                            bg.ReportProgress(10);
                            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                            excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                            int i = 7;
                            foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                            {

                                ObjWorkSheet.Cells[1, i] = twd.GetWeekDay();
                                ObjWorkSheet.Cells[2, i] = twd.getData().Day;
                                i++;
                            }
                            Excel.Range excelcells2 = ObjWorkSheet.get_Range("A3", "AL50");
                            excelcells2.ColumnWidth = Program.currentShop.getAddress().Length;
                            bg.ReportProgress(12);


                            ObjWorkSheet.Cells[2, 1] = "Адрес";

                            ObjWorkSheet.Cells[2, 2] = "Должность";
                            ObjWorkSheet.Cells[2, 3] = "Тип занятости";
                            ObjWorkSheet.Cells[2, 4] = "Оклад";
                            ObjWorkSheet.Cells[2, 5] = "Общее число часов";
                            ObjWorkSheet.Cells[2, 6] = "Количество смен";

                            // MessageBox.Show(Program.currentShop.employes.Count+" count");
                            int j = 3;
                            foreach (employee emp in Program.currentShop.employes)
                            {

                                i = 7;
                                foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                                {
                                    //  MessageBox.Show(emp.getTip() + "");
                                    switch (emp.getID() / 100)
                                    {
                                        case 0: color = System.Drawing.Color.LightSkyBlue; break;
                                        case 1: color = System.Drawing.Color.LightGreen; break;
                                        case 2: color = System.Drawing.Color.DarkSeaGreen; break;
                                        case 3: color = System.Drawing.Color.PaleGoldenrod; break;

                                        default: color = System.Drawing.Color.White; break;

                                    }
                                    ObjWorkSheet.Cells[j, i].Interior.Color = color;

                                    if ((emp.smens.Find(t => t.getData() == twd.getData()) != null) && (emp.smens.Count != 0))
                                    {
                                        // MessageBox.Show(emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + " - " + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena());

                                        ObjWorkSheet.Cells[j, i] =
                                             emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + " - " +
                                             emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena();
                                    }
                                    //else
                                    // { ObjWorkSheet.Cells[emp.getID() + 4, i] = emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + "-" + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena(); }
                                    i++;

                                    ObjWorkSheet.Cells[j, 1] = Program.currentShop.getAddress();
                                    ObjWorkSheet.Cells[j, 1].Interior.Color = color;
                                    ObjWorkSheet.Cells[j, 2] = emp.GetDolgnost();
                                    ObjWorkSheet.Cells[j, 2].Interior.Color = color;
                                    ObjWorkSheet.Cells[j, 3] = emp.getTipZan();
                                    if (Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()) != null)
                                    {
                                        ObjWorkSheet.Cells[j, 4] =
                                            Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()).getZarp();
                                    }
                                    ObjWorkSheet.Cells[j, 5] = Program.normchas;
                                    ObjWorkSheet.Cells[j, 5].Interior.Color = System.Drawing.Color.LightSkyBlue; ;
                                    ObjWorkSheet.Cells[j, 6] = emp.smens.Count;
                                    ObjWorkSheet.Cells[j, 6].Interior.Color = color;

                                }

                                j++;

                            }
                            bg.ReportProgress(14);

                            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[2];

                            ObjWorkSheet.Name = "Часы";

                            excelcells = ObjWorkSheet.get_Range("A3", "AL100");
                            excelcells.Font.Size = 10;
                            excelcells.NumberFormat = "@";

                            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                            excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                            i = 7;
                            foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                            {

                                ObjWorkSheet.Cells[1, i] = twd.GetWeekDay();
                                ObjWorkSheet.Cells[2, i] = twd.getData().Day;
                                i++;
                            }
                            Excel.Range excelcells3 = ObjWorkSheet.get_Range("A3", "AL50");
                            excelcells3.ColumnWidth = Program.currentShop.getAddress().Length;

                            bg.ReportProgress(16);


                            ObjWorkSheet.Cells[2, 1] = "Адрес";

                            ObjWorkSheet.Cells[2, 2] = "Должность";
                            ObjWorkSheet.Cells[2, 3] = "Тип занятости";
                            ObjWorkSheet.Cells[2, 4] = "Оклад";
                            ObjWorkSheet.Cells[2, 5] = "Общее число часов";
                            ObjWorkSheet.Cells[2, 6] = "Количество смен";

                            // MessageBox.Show(Program.currentShop.employes.Count+" count");
                            j = 3;
                            foreach (employee emp in Program.currentShop.employes)
                            {

                                i = 7;
                                foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                                {
                                    switch (emp.getID() / 100)
                                    {
                                        case 0: color = System.Drawing.Color.LightSkyBlue; break;
                                        case 1: color = System.Drawing.Color.LightGreen; break;
                                        case 2: color = System.Drawing.Color.DarkSeaGreen; break;
                                        case 3: color = System.Drawing.Color.PaleGoldenrod; break;

                                        default: color = System.Drawing.Color.White; break;

                                    }
                                    ObjWorkSheet.Cells[j, i].Interior.Color = color;


                                    if ((emp.smens.Find(t => t.getData() == twd.getData()) != null) && (emp.smens.Count != 0))
                                    {
                                        // MessageBox.Show(emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + " - " + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena());
                                        ObjWorkSheet.Cells[j, i].Interior.Color = color;
                                        ObjWorkSheet.Cells[j, i] =
                                            (emp.smens.Find(t => t.getData() == twd.getData()).getLenght() - 1).ToString();
                                    }
                                    //else
                                    // { ObjWorkSheet.Cells[emp.getID() + 4, i] = emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + "-" + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena(); }
                                    i++;

                                    ObjWorkSheet.Cells[j, 1] = Program.currentShop.getAddress();
                                    ObjWorkSheet.Cells[j, 1].Interior.Color = color;
                                    ObjWorkSheet.Cells[j, 2] = emp.GetDolgnost();
                                    ObjWorkSheet.Cells[j, 2].Interior.Color = color;
                                    ObjWorkSheet.Cells[j, 3] = emp.getTipZan();
                                    if (Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()) != null)
                                    {
                                        ObjWorkSheet.Cells[j, 4] =
                                            Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()).getZarp();
                                    }
                                    ObjWorkSheet.Cells[j, 5] = Program.normchas;
                                    ObjWorkSheet.Cells[j, 5].Interior.Color = System.Drawing.Color.LightSkyBlue; ;
                                    ObjWorkSheet.Cells[j, 6] = emp.smens.Count;
                                    ObjWorkSheet.Cells[j, 6].Interior.Color = color;


                                }

                                j++;

                            }


                            ObjExcel.Visible = false;
                            ObjExcel.UserControl = true;
                            ObjExcel.DisplayAlerts = false;
                            ObjWorkBook.Saved = true;
                            try
                            {
                                ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookNormal);
                                Program.HandledShops.Add(Program.currentShop.getIdShop());
                                // ObjWorkBook.SaveAs(filename);

                                ObjWorkBook.Close();

                                ObjExcel.Quit();
                             //   MessageBox.Show("Расписание создано");

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка записи в файл " + ex.Message);
                                ObjWorkBook.Close(0);
                                ObjExcel.Quit();
                            }


                            break;

                        }
                    case 1:
                        {

                            try
                            {
                                bg.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Создание прогноза продаж");
                                Program.createPrognoz();

                                bg.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Подсчет оптимальной загруженности");
                                Sotrudniki.OptimCountSotr();


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            object misValue = System.Reflection.Missing.Value;

                            Excel.Range excelcells;
                            Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                            ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);


                            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                            excelcells = ObjWorkSheet.get_Range("A3", "AL100");
                            excelcells.Font.Size = 10;
                            // excelcells.NumberFormat = "@";

                            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                            excelcells.VerticalAlignment = Excel.Constants.xlCenter;

                            string[] chartXStr = new string[Program.currentShop.tsr.Count + 1];
                            chartY1 = new int[Program.currentShop.tsr.Count + 1];
                            chartY2 = new int[Program.currentShop.tsr.Count + 1];

                            int i = 1;
                            foreach (TSR tsr in Program.currentShop.tsr)
                            {
                                //  chartXStr[i] = tsr.getOtobragenie();
                                ObjWorkSheet.Cells[1, i] = tsr.getOtobragenie();
                                // chartY1[i] = tsr.getCount() * tsr.getZarp();
                                ObjWorkSheet.Cells[2, i] = tsr.getCount();
                                //chartY2[i] = Program.currentShop.employes.FindAll(t => t.getTip() == tsr.getTip()).Count * tsr.getZarp();
                                ObjWorkSheet.Cells[3, i] =
                                    Program.currentShop.employes.FindAll(t => t.GetTip() == tsr.getTip()).Count;
                                i++;
                            }

                            Excel.Range chartRange;

                            Excel.ChartObjects xlCharts = (Excel.ChartObjects)ObjWorkSheet.ChartObjects(Type.Missing);
                            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(20, 80, 300, 250);
                            Excel.Chart chartPage = myChart.Chart;

                            chartRange = ObjWorkSheet.get_Range("a1", "h3");

                            chartPage.SetSourceData(chartRange, misValue);

                            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;

                            ObjExcel.Visible = false;
                            ObjExcel.UserControl = true;
                            ObjExcel.DisplayAlerts = false;
                            ObjWorkBook.Saved = true;
                            try
                            {
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Запись в Excel");
                                bg.ReportProgress(Program.BgProgress += TaskStep);
                                ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookNormal);
                                Program.HandledShops.Add(Program.currentShop.getIdShop());
                                // ObjWorkBook.SaveAs(filename);

                                ObjWorkBook.Close(0);
                                ObjExcel.Quit();
                                MessageBox.Show("Файл создан");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка записи в файл " + ex.Message);
                                ObjWorkBook.Close(0);
                                ObjExcel.Quit();
                            }

                            break;
                        }
                    case 2:
                        {

                            try
                            {
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Создание прогноза продаж");
                                bg.ReportProgress(Program.BgProgress += TaskStep);
                                Program.createPrognoz();

                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Подсчет оптимальной загруженности");
                                bg.ReportProgress(Program.BgProgress += TaskStep);
                                Sotrudniki.OptimCountSotr();



                            }
                            catch (Exception ex)
                            {
                                CloseProcessOnError();
                                MessageBox.Show(ex.Message);
                            }
                            object misValue = System.Reflection.Missing.Value;

                            Excel.Range excelcells;
                            Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                            ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);


                            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                            excelcells = ObjWorkSheet.get_Range("A3", "AL100");
                            excelcells.Font.Size = 10;
                            // excelcells.NumberFormat = "@";

                            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                            excelcells.VerticalAlignment = Excel.Constants.xlCenter;

                            string[] chartXStr = new string[Program.currentShop.tsr.Count + 1];
                            chartY1 = new int[Program.currentShop.tsr.Count + 1];
                            chartY2 = new int[Program.currentShop.tsr.Count + 1];

                            int i = 0;
                            foreach (TSR tsr in Program.currentShop.tsr)
                            {
                                //  chartXStr[i] = tsr.getOtobragenie();
                                ObjWorkSheet.Cells[1, i] = tsr.getOtobragenie();
                                // chartY1[i] = tsr.getCount() * tsr.getZarp();
                                ObjWorkSheet.Cells[2, i] = tsr.getCount() * tsr.getZarp();
                                //chartY2[i] = Program.currentShop.employes.FindAll(t => t.getTip() == tsr.getTip()).Count * tsr.getZarp();
                                ObjWorkSheet.Cells[3, i] =
                                    Program.currentShop.employes.FindAll(t => t.GetTip() == tsr.getTip()).Count *
                                    tsr.getZarp();
                                i++;
                            }
                            Excel.Range chartRange;



                            Excel.ChartObjects xlCharts = (Excel.ChartObjects)ObjWorkSheet.ChartObjects(Type.Missing);

                            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(20, 80, 300, 250);

                            Excel.Chart chartPage = myChart.Chart;



                            chartRange = ObjWorkSheet.get_Range("a1", "h3");

                            chartPage.SetSourceData(chartRange, misValue);

                            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;

                            ObjExcel.Visible = false;
                            ObjExcel.UserControl = true;
                            ObjExcel.DisplayAlerts = false;
                            ObjWorkBook.Saved = true;
                            try
                            {
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Запись в Excel");
                                bg.ReportProgress(Program.BgProgress += TaskStep);
                                ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookNormal);
                                Program.HandledShops.Add(Program.currentShop.getIdShop());
                                // ObjWorkBook.SaveAs(filename);

                                ObjWorkBook.Close(0);

                                ObjExcel.Quit();
                                MessageBox.Show("Файл создан");

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка записи в файл " + ex.Message);
                                ObjWorkBook.Close(0);
                                ObjExcel.Quit();
                            }

                            break;
                        }
                    default:
                        {
                            CloseProcessOnError();
                            break;
                        }
                }
                bg.ReportProgress(ShopStep * shopCounter);
                Program.HandledShops.Add(shop.getIdShop());
               // UpdateStatusShops();
            }

            if (!Directory.Exists(Environment.CurrentDirectory + @"\mult\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\mult\");
            }
            string startPath = Environment.CurrentDirectory + @"\mult\";
            //string zipPath = Environment.CurrentDirectory + @"\mult\result.zip";

            if (File.Exists(PathToZip))
            {
                File.Delete(PathToZip);
            }

            ZipFile zf = new ZipFile(PathToZip);
            zf.AddDirectory(startPath);
            bg.ReportProgress(100);

            zf.Save(); //Сохраняем архив.

        }

        private void tbKassirCount_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbKassirCount.Text))
            {
                if (int.Parse(tbKassirCount.Text) < 1)
                {
                    MessageBox.Show("Меньше 1 нельзя");
                    tbKassirCount.Text = "";
                    return;
                }

                if (int.Parse(tbKassirCount.Text) >2)
                {
                    MessageBox.Show("Не оптимальное количество. Допустимо 1 или 2.");
                    tbKassirCount.Text = "";
                    return;
                }

                int kassirCount;

                if (int.TryParse(tbKassirCount.Text, out kassirCount))
                {
                    Program.currentShop.minrab.setMinCount(kassirCount);

                }

            }

        }

   /*     private void tbLastHour_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbLastHour.Text))
            {
                if (int.Parse(tbLastHour.Text) < 7 || int.Parse(tbLastHour.Text) > 10)
                {
                    MessageBox.Show("Введите число от 7 до 10");
                    tbLastHour.Text = "";
                    return;
                }
                int lastHour;
                if (int.TryParse(tbLastHour.Text, out lastHour))
                {

                    Program.currentShop.minrab.setTime(lastHour);
                }

            }
        }*/

        private void ChangeLabel3Text(string text)
        {
            lbProgressMessages.Visible = true;
            lbProgressMessages.Text = text;
        }



        #region ControlsControl

        private void DisableControlsOnStart()
        {
            listBox1.Enabled = false;
            buttonMadd.Enabled = false;
            buttonMdel.Enabled = false;
            lbProgressMessages.Visible = true;
        }

        private void EnableControlsOnFinish()
        {
            progressBar1.Visible = false;
            progressBar2.Visible = false;
            lbProgressMessages.Visible = false;
            label3.Visible = false;
            listBox1.Enabled = true;
            buttonMadd.Enabled = true;
            buttonMdel.Enabled = true;
        }

        #endregion


        #region FinalizeWorkers

        private void CloseProcessOnError()
        {
            if (bw.IsBusy)
            {
                bw.ReportProgress(0);
                bw.CancelAsync();
                
            }

            if (bw1.IsBusy)
            {
                bw1.ReportProgress(0);
                bw1.CancelAsync();
            }
            
            errorOnExecuting = true;
        }

        #endregion

        private void buttonTSRPG_Click(object sender, EventArgs e)
        {
            Program.TSRTG = false;
            dataGridViewForTSR.DataSource = viewTSR();
            dataGridViewForTSR.Columns[0].ReadOnly = true;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (Program.exit)
            {

            }
            else
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            String writePath = Environment.CurrentDirectory + @"\parametrzoptimization";
            using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
            {

                try
                {
                    sw.Write(Program.ParametrOptimization.ToString());
                    Program.HandledShops.Add(Program.currentShop.getIdShop());
                    UpdateStatusShops();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
            MessageBox.Show("Данные сохранены");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        #region ValidatingDataGridView

        void SetDataGridViewValidating()
        {
            dataGridViewForTSR.DataError += dataGridViewForTSR_DataError;
            dataGridViewFactors.DataError += dataGridViewFactors_DataError;
            dataGridViewVarSmen.DataError += dataGridViewVarSmen_DataError;
            dataGridView1.DataError += dataGridViewFactors_DataError;
            dataGridViewMVarSmen.DataError += dataGridViewVarSmen_DataError;
        }

        void dataGridViewForTSR_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // you can obtain current editing value like this:
            //string value = null;
            //var ctl = dataGridViewForTSR.EditingControl as DataGridViewTextBoxEditingControl;

            //if (ctl != null)
            //    value = ctl.Text;

            // you can obtain the current commited value
            object current = dataGridViewForTSR.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string message;
            switch (e.ColumnIndex)
            {
                // other columns
                default:
                    message = "Введите число!";
                    break;
            }

            MessageBox.Show(message);
        }

        void dataGridViewFactors_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string message;
            switch (e.ColumnIndex)
            {
                case 1:
                    {
                        message = "Введите число!";
                        break;
                    }
                case 3:
                    {
                        message = "Введите дату в формате 01.01.1970!";
                        break;
                    }
                case 4:
                    {
                        message = "Введите число!";
                        break;
                    }
                default:
                    message = "Введите число!";
                    break;
            }

            MessageBox.Show(message);
        }

        void dataGridViewVarSmen_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string message;
            switch (e.ColumnIndex)
            {
                default:
                    message = "Введите число!";
                    break;
            }

            MessageBox.Show(message);
        }
        #endregion

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void buttonTSRPG_Click_1(object sender, EventArgs e)
        {

            if (Program.TSRTG)
            {

                Program.TSRTG = false;
                dataGridViewForTSR.DataSource = viewTSR();
                // dataGridViewForTSR.Refresh();
                // dataGridViewForTSR.Update();
                buttonTSRPG.Text = "На текущий год";
            }
            else
            {

                Program.TSRTG = true;
                dataGridViewForTSR.DataSource = viewTSR();
                //dataGridViewForTSR.Refresh();
                // dataGridViewForTSR.Update();
                buttonTSRPG.Text = "На будущий год";
            }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            if (radioButtonIzBD.Checked && !isConnected)
            {
                Form3 f3 = new Form3();
                f3.Show(this);
                this.Enabled = false;
            }
            else
            {
                try
                {
                    buttonRaspisanie.Enabled = false;
                    buttonCalendar.Enabled = false;
                    buttonKassov.Enabled = false;
                    tabControl1.Enabled = false;
                    button_refresh_list_shops.Enabled = false;
                    buttonMultShops.Enabled = false;

                    string filename;
                    saveFileDialog1.DefaultExt = ".XLS";
                    saveFileDialog1.AddExtension = true;
                    saveFileDialog1.Filter = "Файл Excel|*.XLSX;*.XLS";
                    saveFileDialog1.InitialDirectory = Environment.CurrentDirectory + @"\Shops\" + Program.currentShop.getIdShop();
                    saveFileDialog1.Title = "Выберите папку для сохранения выгрузки из БД";
                    saveFileDialog1.FileName = "Выгрузка по кассовым операциям за " + comboBox2.Text;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        filename = Path.GetFullPath(saveFileDialog1.FileName);
                    }
                    else
                    {
                        return;
                    }
                    int year;


                    if (DateTime.Now.Month < comboBox2.SelectedIndex + 1)
                    {
                        year = DateTime.Now.Year - 1;


                    }
                    else { year = DateTime.Now.Year; }

                    DateTime dt = new DateTime(year, comboBox2.SelectedIndex + 1, 1);
                    Program.createListDaySale(dt, dt.AddDays(31), Program.currentShop.getIdShop());

                    Excel.Application ObjExcel = new Excel.Application();
                    Workbook ObjWorkBook;
                    Worksheet ObjWorkSheet;

                    ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
                    Excel.Range excelcells;
                    ObjWorkBook.Sheets.Add();
                    ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                    ObjWorkSheet.Name = "График";
                    excelcells = ObjWorkSheet.get_Range("C1", "E1000");
                    excelcells.Font.Size = 10;
                    excelcells.ColumnWidth = 20;

                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;

                    ObjWorkSheet.Cells[1, 1] = "День недели";
                    ObjWorkSheet.Cells[1, 2] = "Время";
                    ObjWorkSheet.Cells[1, 3] = "Дата";
                    ObjWorkSheet.Cells[1, 4] = "Количество товаров";
                    ObjWorkSheet.Cells[1, 5] = "Количество чеков";
                    ObjWorkSheet.Cells[1, 6] = "Количество сканирований";

                    int i = 2;
                    foreach (daySale twd in Program.currentShop.daysSale)
                    {
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
                    try
                    {

                        ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookNormal);
                        Program.HandledShops.Add(Program.currentShop.getIdShop());


                        ObjWorkBook.Close(0);

                        ObjExcel.Quit();
                        MessageBox.Show("Файл создан");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка записи в файл " + ex.Message);
                        ObjWorkBook.Close(0);
                        ObjExcel.Quit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    buttonRaspisanie.Enabled = true;
                    buttonCalendar.Enabled = true;
                    buttonKassov.Enabled = true;
                    tabControl1.Enabled = true;
                    button_refresh_list_shops.Enabled = true;
                    buttonMultShops.Enabled = true;
                }
            }

        }

        private void dataGridViewVarSmen_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    Program.currentShop.VarSmens.Find(t => t.getR().ToString() == dataGridViewVarSmen[0, e.RowIndex].Value.ToString()).setR(int.Parse(dataGridViewVarSmen[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;
                case 1:
                    Program.currentShop.VarSmens.Find(t => t.getR().ToString() == dataGridViewVarSmen[0, e.RowIndex].Value.ToString()).setV(int.Parse(dataGridViewVarSmen[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;
                case 2:
                    Program.currentShop.VarSmens.Find(t => t.getR().ToString() == dataGridViewVarSmen[0, e.RowIndex].Value.ToString()).setDeistvie(bool.Parse(dataGridViewVarSmen[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;

            }
        }

        private void buttonCalendarNextYear_Click(object sender, EventArgs e)
        {
            buttonCalendar.BackColor = Color.MistyRose;
            buttonRaspisanie.BackColor = Color.White;
            buttonKassov.BackColor = Color.White;
            panelCalendar.BringToFront();

            buttonImportKasOper.Visible = false;
            // ShowProizvCalendar();
            Program.getListDate(DateTime.Now.AddYears(1).Year);
            Form4 f4 = new Form4(DateTime.Now.AddYears(1).Year);
            f4.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Program.WriteFactors();
            MessageBox.Show("Данные сохранены");
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            String readPath = Environment.CurrentDirectory + @"\Shops\" + Program.currentShop.getIdShop() + @"\VarSmen";

            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {
                foreach (VarSmen vs in Program.currentShop.VarSmens)
                {
                    sw.WriteLine(vs.getR() + "#" + vs.getV() + "#" + vs.getDeistvie());
                }
                Program.HandledShops.Add(Program.currentShop.getIdShop());
                UpdateStatusShops();

            }

            Program.WriteMinRab();
            MessageBox.Show("Данные сохранены");
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            if (!Program.addsmena)
            {
                label12.Visible = true;
                label13.Visible = true;
                label15.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                Program.addsmena = true;
                button10.Text = "ok";

            }
            else
            {
                Program.currentShop.VarSmens.Add(new VarSmen(int.Parse(textBox3.Text), int.Parse(textBox4.Text), int.Parse(textBox5.Text), false));
                Program.writeVarSmen();
                dataGridViewVarSmen.DataSource = viewVarSmen();
                label12.Visible = false;
                label13.Visible = false;
                label15.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                Program.addsmena = false;
            }
        }

        private void panelParamOptim_Paint(object sender, PaintEventArgs e)
        {

        }

        public void UpdateStatusShops()
        {
            labelStatus1.Text = "Статус: Обработано " + Program.HandledShops.Count + " магазинов из " + Program.listShops.Count;
        }

        private void bSettings_Click(object sender, EventArgs e)
        {
            var form = new fSettings();
            form.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
       
        {
            String readPath = Environment.CurrentDirectory + "/Shops/" + Program.currentShop.getIdShop() + "/Prilavki";
            if (checkBox1.Checked)
            {
               // MessageBox.Show("Число продавцов стало зависить от прилавков");
             
                Program.currentShop.prilavki.SetNalichie( true);
                label19.Visible = true;
                textBox6.Visible = true;
                }
            else {
                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    sw.WriteLine(false);
                    Program.HandledShops.Add(Program.currentShop.getIdShop());
                    UpdateStatusShops();
                }
                Program.currentShop.prilavki.SetNalichie( false);
                label19.Visible = false;
                textBox6.Visible = false;
            }
        }

        private void button12_Click_2(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateStatusShops();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbKassirCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbLastHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(textBox3.Text) < int.Parse(textBox4.Text) || int.Parse(textBox3.Text) > 5 || int.Parse(textBox3.Text) < 1)
            {
                MessageBox.Show("Меньше 1 нельзя");
                textBox3.Text = "";
                return;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(textBox5.Text) <7 || int.Parse(textBox3.Text) > 10)
            {
                MessageBox.Show("Введите число от 7 до 10");
                textBox5.Text = "";
                return;
            }
        }

     /*   private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbLastHour.Text))
            {
                if (int.Parse(textBox6.Text) < 0 || int.Parse(textBox6.Text) > 10)
                {
                    MessageBox.Show("Введите число продавцов от 1 до 10");
                    textBox6.Text = "";
                    return;
                }
            }
        }*/

        private void textBox6_CursorChanged(object sender, EventArgs e)
        {
            
        }

        private void tbLastHour_CursorChanged(object sender, EventArgs e)
        {
           
            
        }

        private void tbLastHour_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void textBox6_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbLastHour.Text))
            {
                if (int.Parse(textBox6.Text) < 0 || int.Parse(textBox6.Text) > 10)
                {
                    MessageBox.Show("Введите число продавцов от 1 до 10");
                    textBox6.Text = "";
                    return;
                }
            }
        }

        private void tbLastHour_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbLastHour.Text))
            {
                if (int.Parse(tbLastHour.Text) < 7 || int.Parse(tbLastHour.Text) > 10)
                {
                    MessageBox.Show("Введите число от 7 до 10");
                    tbLastHour.Text = "";
                    return;
                }
                int lastHour;
                if (int.TryParse(tbLastHour.Text, out lastHour))
                {

                    Program.currentShop.minrab.setTime(lastHour);
                }

            }
        }
    }
}

