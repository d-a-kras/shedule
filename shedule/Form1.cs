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
using schedule.Code;
using Point = System.Drawing.Point;
using System.Diagnostics;
using System.Linq;
using Application = System.Windows.Forms.Application;
using schedule.Models;
using shedule.Models;

namespace schedule
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
        private List<Tuple<string, string>> listBox1List = new List<Tuple<string, string>>();

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

        private void bw1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:


                    break;
                case -1:
                    MessageBox.Show("Ошибка загрузки файла.");
                    break;
                case 6:
                    label3.Text = "Оптимальная расстановка смен";
                    break;
                case 8:
                    label3.Text = "Запись в Excel";
                    break;

            }
            progressBar2.Value = e.ProgressPercentage;
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
                            if (!ForForecast.createPrognoz(false, false, true,Program.isLocalDB))
                            {
                                MessageBox.Show("График не удалось построить. Ошибка в построении прогноза.");
                                return;
                            }
                            bg.ReportProgress(4);
                            //MessageBox.Show("Время создание примерно 2 минуты");


                            // 
                            Sotrudniki.OptimCountSotr();
                            bg.ReportProgress(6);
                            if (Program.currentShop.Semployes != null)
                            {
                                if (Program.currentShop.Semployes.Count != 0)
                                {
                                    Sotrudniki.NewOtrabotal();
                                }
                            }
                            //  
                            bg.ReportProgress(7);
                            //  


                            if (!Sotrudniki.CreateSmens(false, Program.currentShop.employes))
                            {
                                MessageBox.Show("График не удалось построить из-за большой нормы часов в текущем месяце. Все смены у одного из сотрудников 12 часов и не хватает часов до нормы. Перейдите на вкладку производственный календарь и уменьшите текущее значение.");
                                return;
                            }



                            bg.ReportProgress(8);
                            if (!Sotrudniki.CheckGrafic13())
                            {
                                MessageBox.Show("График не удалось построить из-за большой нормы часов в текущем месяце. Перейдите на вкладку производственный календарь и уменьшите текущее значение.");
                                return;
                            }
                            if (!Sotrudniki.CheckGrafic2())
                            {
                                //MessageBox.Show("График не удалось построить из-за выбранных вариантов смен и минимального числа сотрудников. Уменьшите минимальное число сотрудников и используйте другие смены");
                                // return;
                            }

                            if (!Sotrudniki.CheckGrafic())
                            {
                                if (Program.currentShop.employes.Count < 15)
                                {
                                    MessageBox.Show("Расписание не удовлетворяет всем выбранным параметрам, из-за выбранных вариантов смен и минимального числа сотрудников. Данный магазин является небольшим, поэтому рекомендуется использовать только смены 2/2, 3/3 и минимальное количество сотрудников 1.");
                                }

                                else if ((Program.currentShop.employes.Count >= 15) && ((Program.currentShop.employes.Count < 30)))
                                {
                                    MessageBox.Show("Расписание не удовлетворяет всем выбранным параметрам, из-за выбранных вариантов смен. Данный магазин является средним по размеру, поэтому предпочтительно использовать смены 2/2, 3/3 5/2 и минимальное количество сотрудников 2.");
                                }
                                else
                                {
                                    MessageBox.Show("Расписание не удовлетворяет всем выбранным параметрам, из-за выбранных вариантов смен. Данный магазин является крупным по размеру, поэтому для достижения оптимальности предпочтительно использовать смены 2/2, 3/3 5/2 и минимальное количество сотрудников 2.");

                                }
                                //  return;
                            }
                            bg.ReportProgress(10);
                            ForExcel.ExportExcel(filename, bg);
                            bg.ReportProgress(20);
                            MessageBox.Show("Расписание создано");
                        }
                        catch (Exception ex)

                        {

                            //  label3.Visible = false;
                            //  progressBar1.Visible = false;
                            MessageBox.Show(ex.ToString());
                            string[] s = new string[2];
                            // s = listBox1.Text.Split('_');
                            //  Program.currentShop = new Shop(Int16.Parse(s[0]), s[1]);
                            //  Program.getListDate(DateTime.Today.Year);
                            // Program.readTSR();
                            MessageBox.Show("Расписание не создано");
                            CloseProcessOnError();
                            //throw ex;

                        }





                        break;
                    }
                case 1:
                    {
                        object misValue = System.Reflection.Missing.Value;
                        BackgroundWorker bg = sender as BackgroundWorker;
                        bg.ReportProgress(2);
                        try
                        {
                            ForForecast.createPrognoz3();
                        }
                        catch
                        {
                            MessageBox.Show("Данные из БД не были получены");
                        }
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
                        for (int k = 0; k < Program.currentShop.MouthPrognozT.Count; k++)
                        {
                            ObjWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        }

                        int i = 1;
                        bg.ReportProgress(12);
                        foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                        {
                            try
                            {
                                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[i];
                                ObjWorkSheet.Name = "Прогноз" + twd.getData().ToShortDateString();
                                excelcells = ObjWorkSheet.get_Range("A1", "AL100");
                                excelcells.Font.Size = 10;
                                //  excelcells.NumberFormat = "@";
                                bg.ReportProgress(10);
                                excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                                excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                                ObjWorkSheet.Cells[2, 1] = "Чеки";
                                ObjWorkSheet.Cells[3, 1] = "Клики";

                                int j = 2;
                                foreach (int x in twd.Chart.X)
                                {
                                    ObjWorkSheet.Cells[1, j] = x.ToString() + ":00";
                                    ObjWorkSheet.Cells[2, j] = twd.DS.ChartCheck.Y[j - 1];
                                    ObjWorkSheet.Cells[3, j] = twd.DS.ChartClick.Y[j - 1];
                                    j++;
                                }



                                Range chartRange1;
                                ChartObjects xlCharts = (ChartObjects)ObjWorkSheet.ChartObjects(Type.Missing);
                                ChartObject myChart = (ChartObject)xlCharts.Add(20, 80, 300, 250);

                                //myChart.Legends.Add(new Legend("Legend2"));
                                Chart chartPage = myChart.Chart;
                                chartPage.ChartType = XlChartType.xlLineMarkers;
                                chartRange1 = ObjWorkSheet.get_Range("A1", "Q3");
                                // chartRange1 = ObjWorkSheet.get_Range("a1", "c" + twd.DS.hoursSale.Count);

                                chartPage.SetSourceData(chartRange1, misValue);

                                Excel.Range axis_range = ObjWorkSheet.get_Range("A1", "Q3");
                                Excel.Series series = (Excel.Series)chartPage.SeriesCollection(2);
                                series.XValues = axis_range;

                                /* Excel.Series series0 = (Excel.Series)chartPage.SeriesCollection(2);
                                 series0.Name = "Чеки";

                                 Excel.Series series1 = (Excel.Series)chartPage.SeriesCollection(2);
                                 series1.Name = "Клики";

                                 var serie = (SeriesCollection)chartPage.SeriesCollection();
                                 serie.Item(1).Delete();*/


                                i++;
                            }
                            catch (Exception ex)
                            {
                                Logger.Log.Error(ex.ToString());
                            }



                        }
                        bg.ReportProgress(18);


                        ObjExcel.Visible = false;
                        ObjExcel.UserControl = true;
                        ObjExcel.DisplayAlerts = false;
                        ObjWorkBook.Saved = true;
                        ObjExcel.DisplayAlerts = true;
                        try
                        {

                            ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookDefault);
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
                            if (!ForForecast.createPrognoz(false, false, true,Program.isLocalDB))
                            {
                                MessageBox.Show("График не удалось построить. Ошибка в построении прогноза.");
                                return;
                            }
                            bg.ReportProgress(4);

                            Sotrudniki.OptimCountSotr();
                            bg.ReportProgress(6);
                        }
                        catch (Exception ex)
                        {
                            CloseProcessOnError();
                            MessageBox.Show(ex.ToString());
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
                        ObjExcel.DisplayAlerts = true;
                        try
                        {

                            ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookDefault);
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
                            if (!ForForecast.createPrognoz(false, false, true, Program.isLocalDB))
                            {
                                MessageBox.Show("График не удалось построить. Ошибка в построении прогноза.");
                                return;
                            }
                            bg.ReportProgress(4);



                            // 
                            Sotrudniki.OptimCountSotr();
                            bg.ReportProgress(6);
                        }
                        catch (Exception ex)
                        {
                            CloseProcessOnError();
                            MessageBox.Show(ex.ToString());
                        }
                        object misValue = System.Reflection.Missing.Value;




                        Excel.Range excelcells;
                        Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                        Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                        ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);


                        ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                        excelcells = ObjWorkSheet.get_Range("A1", "E3");
                        excelcells.Font.Size = 10;
                        // excelcells.NumberFormat = "@";
                        bg.ReportProgress(10);
                        excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                        excelcells.VerticalAlignment = Excel.Constants.xlCenter;

                        string[] chartXStr = new string[Program.currentShop.tsr.Count + 1];
                        chartY1 = new int[Program.currentShop.tsr.Count + 1];
                        chartY2 = new int[Program.currentShop.tsr.Count + 1];
                        ObjWorkSheet.Cells[2, 1] = "текущие";
                        ObjWorkSheet.Cells[3, 1] = "оптимальные";

                        int i = 2;
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



                        chartRange = ObjWorkSheet.get_Range("a1", "e3");

                        chartPage.SetSourceData(chartRange, misValue);

                        chartPage.ChartType = Excel.XlChartType.xlColumnClustered;



                        bg.ReportProgress(18);





                        ObjExcel.Visible = false;
                        ObjExcel.UserControl = true;
                        ObjExcel.DisplayAlerts = false;
                        ObjWorkBook.Saved = true;
                        ObjExcel.DisplayAlerts = true;
                        try
                        {

                            ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookDefault);
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
                case 4:
                    {
                        BackgroundWorker bg = sender as BackgroundWorker;
                        bg.ReportProgress(2);
                        try
                        {
                            if (!ForForecast.createPrognoz(true, false, true, Program.isLocalDB))
                            {
                                MessageBox.Show("График не удалось построить. Ошибка в построении прогноза.");
                                return;
                            }
                            bg.ReportProgress(4);
                            //MessageBox.Show("Время создание примерно 2 минуты");


                            // 
                            Sotrudniki.OptimCountSotr(true);
                            bg.ReportProgress(6);

                            if (Program.currentShop.Semployes.Count != 0)
                            {
                                Sotrudniki.NewOtrabotal();
                                Sotrudniki.StarSmen();
                            }
                            //  
                            bg.ReportProgress(7);

                            if (!Sotrudniki.CreateSmens(true, Program.currentShop.employes))
                            {

                                MessageBox.Show("График не удалось построить из-за большой нормы часов в текущем месяце. Все смены у одного из сотрудников 12 часов и не хватает часов до нормы. Перейдите на вкладку производственный календарь и уменьшите текущее значение.");
                                return;
                            }

                            Sotrudniki.CheckDisp();


                            bg.ReportProgress(8);
                            if (!Sotrudniki.CheckGrafic13())
                            {
                                MessageBox.Show("График не удалось построить из-за большой нормы часов в текущем месяце. Перейдите на вкладку производственный календарь и уменьшите текущее значение.");
                                return;
                            }
                            if (!Sotrudniki.CheckGrafic2())
                            {
                               // MessageBox.Show("График не удалось построить из-за выбранных вариантов смен и минимального числа сотрудников. Уменьшите минимальное число сотрудников и используйте другие смены");
                                //
                              //  return;
                            }

                            if (!Sotrudniki.CheckGrafic())
                            {
                                if (Program.currentShop.employes.Count < 15)
                                {
                                    MessageBox.Show("Расписание не удовлетворяет всем выбранным параметрам, из-за выбранных вариантов смен и минимального числа сотрудников. Данный магазин является небольшим, поэтому рекомендуется использовать только смены 2/2, 3/3 и минимальное количество сотрудников 1.");
                                }

                                else if ((Program.currentShop.employes.Count >= 15) && ((Program.currentShop.employes.Count < 30)))
                                {
                                    MessageBox.Show("Расписание не удовлетворяет всем выбранным параметрам, из-за выбранных вариантов смен. Данный магазин является средним по размеру, поэтому предпочтительно использовать смены 2/2, 3/3 5/2 и минимальное количество сотрудников 2.");
                                }
                                else
                                {
                                    MessageBox.Show("Расписание не удовлетворяет всем выбранным параметрам, из-за выбранных вариантов смен. Данный магазин является крупным по размеру, поэтому для достижения оптимальности предпочтительно использовать смены 2/2, 3/3 5/2 и минимальное количество сотрудников 2.");

                                }
                                //  return;
                            }
                            bg.ReportProgress(9);
                            ForExcel.ExportExcel(filename, bg);
                            bg.ReportProgress(20);
                            MessageBox.Show("Расписание создано");
                        }
                        catch (Exception ex)

                        {

                            //  label3.Visible = false;
                            //  progressBar1.Visible = false;
                            MessageBox.Show(ex.ToString());
                            string[] s = new string[2];
                            // s = listBox1.Text.Split('_');
                            //  Program.currentShop = new Shop(Int16.Parse(s[0]), s[1]);
                            //  Program.getListDate(DateTime.Today.Year);
                            Program.readTSR();
                            MessageBox.Show("Расписание не создано");
                            CloseProcessOnError();
                            //throw ex;

                        }





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
            DataColumn colCountDayVuh = new DataColumn("Действует на текущую дату", typeof(bool));
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

        private SD.DataTable viewVarSmen(bool Mult)
        {
            //создаём таблицу
            string[] months = Program.getMonths();
            SD.DataTable dt = new SD.DataTable("norm");
            //создаём три колонки


            DataColumn colCountDayInMonth = new DataColumn("Количество рабочих дней", typeof(string));
            DataColumn colCountDayRab = new DataColumn("Количество выходных дней", typeof(Int16));
            DataColumn colCountDayVuh = new DataColumn("Действует на текущую дату", typeof(bool));


            //добавляем колонки в таблицу

            dt.Columns.Add(colCountDayInMonth);
            dt.Columns.Add(colCountDayRab);
            dt.Columns.Add(colCountDayVuh);

            DataRow row = null;
            //создаём новую строку

            //заполняем строку значениями
            Program.currentShop.VarSmens.Clear();
            Program.readVarSmen();
            List<VarSmen> lvs = new List<VarSmen>();
    
                switch (comboBox4.SelectedIndex)
                {
                    case 0: lvs = Program.currentShop.VarSmens.FindAll(t => t.getDolgnost() == "Кассир"); break;
                    case 1: lvs = Program.currentShop.VarSmens.FindAll(t => t.getDolgnost() == "Продавец"); break;
                    case 2: lvs = Program.currentShop.VarSmens.FindAll(t => t.getDolgnost() == "Грузчик"); break;
                    case 3: lvs = Program.currentShop.VarSmens.FindAll(t => t.getDolgnost() == "Гастроном"); break;
                }
            
            foreach (VarSmen f in lvs)
            {
                row = dt.NewRow();
                row["Количество рабочих дней"] = f.getR();
                row["Количество выходных дней"] = f.getV();
                row["Действует на текущую дату"] = f.getDeistvie();

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
            workSheet.SaveAs("MyFile.xlsx");
            exApp.Quit();
        }

        public Form1()
        {
            InitializeComponent();
            // SetNextYearCalendarButton();
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

        /*private void SetNextYearCalendarButton()
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
        }*/

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
                    $"Data Source={Settings.Default.DatabaseAddress};Persist Security Info=True;User ID={Program.login};Password={Program.password}";
                string sql = "select * from dbo.get_StatisticByShopsDayHour('301', '2017/01/02', '2017/01/04 23:59:00')";

                int countAttemption = 0;
                int countRecords = 0;
                while (countRecords == 0 && countAttemption < 2)
                {
                    countAttemption++;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sql, connection);
                            command.CommandTimeout = 500;
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                h = new hourSale(reader.GetInt16(0), reader.GetDateTime(1), reader.GetString(2),
                                    reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                                hSs.Add(h);
                                countRecords++;
                            }
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            MessageBox.Show("Ошибка соединения с базой данных" + ex);
                        }

                    }
                    if (countRecords > 2) countAttemption = 2;
                }

                if (countRecords < 2 && Code.Constants.IsThrowExceptionOnNullResult)
                {
                    countRecords = 0;
                    countAttemption = 0;
                    throw new Exception("Соединение с базой нестабильно, данные не были получены.");
                }

                countRecords = 0;
                countAttemption = 0;

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

            lbCurrentVersion.Text = $"v {Code.Constants.Version} от {Code.Constants.ReleaseDate} ";
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;

            bw1.WorkerReportsProgress = true;
            bw1.WorkerSupportsCancellation = true;
            bw1.DoWork += bw1_DoWork;
            bw1.ProgressChanged += bw1_ProgressChanged;
            bw1.RunWorkerCompleted += bw1_RunWorkerCompleted;
            radioButtonObRabTime.Checked = true;
            Program.ReadListShops();
            // Program.setListShops();
            tabControl1.Visible = false;
            //buttonTest.Visible = false;
            progressBar1.Visible = false;
            label3.Visible = false;

            SetDataGridViewValidating();


            if (Program.listShops != null)
            {
                foreach (mShop h in Program.listShops)
                {
                    listBox1List.Add(new Tuple<string, string>(h.getIdShop().ToString(), h.getAddress()));
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
            //radioButtonIzFile.Checked = true;
            radioButtonLocalDB.Checked = true;

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
            comboBox4.SelectedIndex = 0;
            dataGridViewVarSmen.DataSource = viewVarSmen(false);


            dataGridViewVarSmen.Columns[0].ReadOnly = true;
            dataGridViewVarSmen.Columns[1].ReadOnly = true;


            /*  if (Program.currentShop.minrab.getOtobragenie())
              {
                  tbMinRabCount.Text = Program.currentShop.minrab.getMinCount().ToString();
                  // tbLastHour.Text = Program.currentShop.minrab.getTimeMinRab().ToString();
              }
              else
              {
                  tbMinRabCount.Text = "";
                  // tbLastHour.Text = "";
              }
              // Program.ReadMinRab();

              */

            tbMinRabCount.Text= MinRab.Read(EmployeeType.Cashier, Program.currentShop.getIdShop()).getMinCount().ToString();
            labelMinRabCount.Text = comboBox4.Text + " с времени";

        }


        private void buttonParamOptimiz_Click(object sender, EventArgs e)
        {
            buttonParamOptimiz.BackColor = Color.MistyRose;
            buttonFactors.BackColor = Color.White;
            buttonVariantsSmen.BackColor = Color.White;
            panelParamOptim.BringToFront();
            checkBox1.Checked = Program.currentShop.SortSotr;
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
            progressBar3.Visible = true;
            progressBar3.Value = 0;
            progressBar3.Maximum = 100;
            progressBar3.Step = 1;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (Settings.Default.folder == "")
            {
                openFileDialog.InitialDirectory = "c:\\";
            }
            else
            {
                openFileDialog.InitialDirectory = Settings.Default.folder;
            }
            openFileDialog.Filter = "*|*.xlsx";
            openFileDialog.RestoreDirectory = true;
            string filepath = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog.FileName;
            }

            try
            {

                List<hourSale> hourSales = Helper.FillHourSalesList(filepath, Program.currentShop.getIdShop());
                progressBar3.Value = 50;

                foreach (hourSale hs in hourSales)
                {
                    progressBar3.PerformStep();
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
            catch (Exception ex)
            {
                MessageBox.Show("Произошла критическая ошибка! Использование данных из файла невозможно!" + ex.ToString(), "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar3.Visible = false;
            }
            progressBar3.Visible = false;
        }

        private void radioButtonIzFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIzFile.Checked)
            {
                buttonImportKasOper.Visible = true;
                buttonVygr.Visible = false;
                comboBox2.Visible = false;
                Program.isOffline = true;
            }

        }

        private void radioButtonIzBD_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIzBD.Checked)
            {
                buttonImportKasOper.Visible = false;
                buttonVygr.Visible = true;
                comboBox2.Visible = true;
                Program.isOffline = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Program.ReadConfigShop();
            //MessageBox.Show(listBox1.Text);
            // tabControl1.PerformLayout();
            buttonParamOptimiz.PerformClick();
            tabControl1.SelectTab(tabPage1);
            buttonKassov.PerformClick();

            Program.currentShop = null;
            string[] s = new string[2];
            s = listBox1.Text.Split('_');
            Program.currentShop = new Shop(Int16.Parse(s[0]), s[1]);
            Program.currentShop.setMinRab(Program.ReadMinRabForShop());
            buttonParamOptimiz.PerformClick();
            Program.getListDate(DateTime.Today.Year, false);
            string readPath = Environment.CurrentDirectory + @"\Shops\" + Program.currentShop.getIdShop() + $@"\Calendar{DateTime.Today.AddYears(1).Year}";
            if (File.Exists(readPath))
            {
                Program.getListDate(DateTime.Today.AddYears(1).Year, true);
            }
            Program.currentShop.daysSale.Clear();
            Program.readTSR();
            Program.readFactors(Program.currentShop.getIdShop());
            Program.readVarSmen();
           // Program.ReadMinRab();
            Program.ReadPrilavki();
            Program.ReadSortSotr(); ;
            Program.ReadNormaChas(DateTime.Now.Year);
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
            ForDB.getShopsFromDB(); 
            
            foreach (mShop h in Program.listShops)
            {

                listBox1.Items.Add(h.getIdShop() + "_" + h.getAddress());
            }
            if (Program.listShops.Count > 0)
            {
                MessageBox.Show("Список магазинов обновлен");
            }
            else {
                MessageBox.Show("Ошибка обновления списка магазинов");
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

        private void buttonReadTekschedule_Click(object sender, EventArgs e)
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
            buttonCalendar.Enabled = false;
            Form4 f4 = new Form4(DateTime.Now.Year);
            f4.ShowDialog();
            buttonCalendar.Enabled = true;

        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            try
            {
                ForForecast.initForecasts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

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
            //textBox6.Text = Program.currentShop.prilavki.GetCount().ToString();
            //checkBox1.Checked = Program.currentShop.prilavki.GetNalichie();


        }






        private void Form1_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void buttonPTSR_Click(object sender, EventArgs e)
        {

            Program.WriteTSR();
            // Program.WritePrilavki();
            MessageBox.Show("Данные сохранены");
        }

        private void buttonAplyFactors_Click(object sender, EventArgs e)
        {

            Program.WriteFactors();
            MessageBox.Show("Данные сохранены");
        }

        private void buttonAplyVarSmen_Click(object sender, EventArgs e)
        {
            if (tbMinRabCount.Text == "")
            {
                tbMinRabCount.Text = "1";
            }
            Program.writeVarSmen();
            Program.HandledShops.Add(Program.currentShop.getIdShop());
            UpdateStatusShops();

            Program.WriteMinRab();
            int minRabCount = 0;
            int.TryParse(tbMinRabCount.Text, out minRabCount);

            MinRab.Update(new MinRab(MinRab.getType(comboBox4.Text), minRabCount, Program.currentShop.getIdShop() ));
            MessageBox.Show("Данные сохранены");
        }

        private void buttonMultShops_Click(object sender, EventArgs e)
        {
            panelMultShops.BringToFront();
            Program.shops = new List<Shop>();
            Program.currentShop = null;
            Program.IsMpRezhim = true;
            listBoxMPartShops.Items.Clear();
            Program.currentShop = new Shop(0, "");
            Program.currentShop.setMinRab(Program.ReadMinRabForShop());
            Program.getListDate(DateTime.Today.Year, false);
            string readPath = Environment.CurrentDirectory + @"\Shops\" + Program.currentShop.getIdShopFM() + $@"\Calendar{DateTime.Today.AddYears(1).Year}";
            if (File.Exists(readPath))
            {
                Program.getListDate(DateTime.Today.AddYears(1).Year, true);
            }
            Program.readTSR();
            Program.readFactors(Program.currentShop.getIdShopFM());
            Program.readVarSmen();
            Program.ReadNormaChas(DateTime.Now.Year);
            Program.ReadPrilavki();
            Program.ReadSortSotr();
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
            tabControl1.Visible = false;
            Program.IsMpRezhim = false;
            Program.currentShop = null;
            //Program.shops = new List<Shop>();
        }

        public void CheckMShops() {
            if (Program.shops.Count==0) {
                buttonReadMGraf.BackColor = Color.White;
                return;
            }
            foreach (Shop shop in Program.shops) {
                String s;
                if (!Program.GrafM.TryGetValue(shop.getIdShopFM(), out s)) {
                    buttonReadMGraf.BackColor = Color.Yellow;
                    return;
                }
            }
            buttonReadMGraf.BackColor = Color.Green;
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
                Program.shops.Add(new Shop(int.Parse(aaa[0]), aaa[1]));
            }
            CheckMShops();
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
            String writePath = Environment.CurrentDirectory + "/Shops/" + Program.currentShop.getIdShop() + @"\parametrzoptimization";
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

            DBShop.SaveMixing(Program.currentShop.getIdShop(), checkBox1.Checked);
           /* String writePath2 = Environment.CurrentDirectory + "/Shops/" + Program.currentShop.getIdShop() + @"\SortSotr"; ;
            using (StreamWriter sw = new StreamWriter(writePath2, false, Encoding.Default))
            {

                try
                {
                    sw.Write(checkBox1.Checked.ToString());



                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }*/

            MessageBox.Show("Данные сохранены");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    button14.Visible = true; comboBoxCountSotr.Visible = true; labelCountSotr.Visible = true; if ((Program.currentShop.Semployes != null) && (Program.currentShop.Semployes.Count > 0) && (Program.currentShop.Semployes[0].smens.Count == 0))
                    {
                        button14.BackColor = Color.PaleGreen;
                        checkBox2.Visible = false;
                        comboBoxCountSotr.Items.Clear();
                        comboBoxCountSotr.Items.Add("штатного расписания");
                        comboBoxCountSotr.Items.Add("загруженного графика");
                        comboBoxCountSotr.Items.Add("прогноза продаж");
                    }
                    else
                    {
                        button14.BackColor = Color.DarkGray;
                        checkBox2.Visible = true;
                        comboBoxCountSotr.Items.Clear();
                        comboBoxCountSotr.Items.Add("штатного расписания");
                        comboBoxCountSotr.Items.Add("прогноза продаж");
                    }
                    break;
                case 1: Sotrudniki.CountSotr = "прогноза продаж"; button14.Visible = false; checkBox2.Visible = false; comboBoxCountSotr.Visible = false; labelCountSotr.Visible = false; break;
                case 2: Sotrudniki.CountSotr = "прогноза продаж"; button14.Visible = false; checkBox2.Visible = false; comboBoxCountSotr.Visible = false; labelCountSotr.Visible = false; break;
                case 3: Sotrudniki.CountSotr = "прогноза продаж"; button14.Visible = false; checkBox2.Visible = false; comboBoxCountSotr.Visible = false; labelCountSotr.Visible = false; break;
                case 4:
                    button14.Visible = true; checkBox2.Visible = true; comboBoxCountSotr.Visible = true; labelCountSotr.Visible = true;
                    if ((Program.currentShop.Semployes != null) && (Program.currentShop.Semployes.Count > 0) && (Program.currentShop.Semployes[0].smens.Count > 0))
                    {
                        button14.BackColor = Color.PaleGreen;
                        checkBox2.Visible = false;
                        comboBoxCountSotr.Items.Clear();
                        comboBoxCountSotr.Items.Add("штатного расписания");
                        comboBoxCountSotr.Items.Add("загруженного графика");
                        comboBoxCountSotr.Items.Add("прогноза продаж");
                    }
                    else
                    {
                        button14.BackColor = Color.DarkGray;
                        checkBox2.Visible = true;
                        comboBoxCountSotr.Items.Clear();
                        comboBoxCountSotr.Items.Add("штатного расписания");
                        comboBoxCountSotr.Items.Add("прогноза продаж");
                    }
                    break;
                case -1: button14.Visible = false; checkBox2.Visible = false; comboBoxCountSotr.Visible = false; labelCountSotr.Visible = false; break;



            }

            if ((comboBoxCountSotr.Items.Count > 0) && (Sotrudniki.CountSotr == ""))
            {
                comboBoxCountSotr.SelectedIndex = 0;
                Sotrudniki.CountSotr = comboBoxCountSotr.SelectedItem.ToString();
            }

        }

        public bool CheckSmens() {
            foreach (var sempl in Program.currentShop.Semployes) {
                if (sempl.smens.Count>0) {
                    return true;
                }
            }
            return false;
        }

        private void buttonExport1_Click(object sender, EventArgs e)
        {
            if ((comboBox3.SelectedIndex == 4) && ((!(Program.currentShop.Semployes != null)) || (Program.currentShop.Semployes.Count == 0) || (!CheckSmens())))
            {
                MessageBox.Show("Загрузите график на текущий месяц");
                return;
            }
            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите что отобразить!");
                return;
            }

            if (radioButtonIzBD.Checked)
            {
                // Form3 f3 = new Form3(3);
                // f3.Show(this);
                if (Program.isConnected())
                {
                    Enabled = true;
                    labelStatus2.Text = "режим работы сетевой ";
                    buttonVygr.Visible = true;
                    comboBox2.Visible = true;
                    isConnected = true;
                    StartExportingToExcel();

                }
                else
                {
                   
                    isConnected = false;
                    radioButtonLocalDB.Checked = true;
                    Enabled = true;
                    labelStatus2.Text = "режим работы локальный ";
                    buttonVygr.Visible = false;
                    comboBox2.Visible = false;
                    MessageBox.Show("Ошибка подключения к БД\n Проверьте соединение");
                }

                return;
            }else {
                isConnected = false;
                Enabled = true;
                labelStatus2.Text = "режим работы локальный ";
                StartExportingToExcel();
            }
            return;
        }

        public void StartExportingToExcel()
        {
            saveFileDialog1.DefaultExt = ".xlsx";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "Файл Excel|*.XLSX;*.XLS";
            if (Settings.Default.folder == "")
            {
                saveFileDialog1.InitialDirectory = Environment.CurrentDirectory + @"\Shops\" +
                                                   Program.currentShop.getIdShop();
            }
            else
            {
                saveFileDialog1.InitialDirectory = Settings.Default.folder;
            }
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
                case 4:
                    saveFileDialog1.FileName = "График_" + Program.currentShop.getAddress() + "_" +
                                               Program.getMonths(DateTime.Now.AddMonths(0).Month);

                    Program.TipExporta = 4;
                    break;
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл

            if (radioButtonIzBD.Checked && !Program.isConnected())
            {
                MessageBox.Show("Соединение с базой не установлено! Выберите режим \"из файла\" или подключитесь к базе данных.");
                return;
            } else if (radioButtonIzFile.Checked && !Program.ExistFile)
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
                Program.isLocalDB = radioButtonLocalDB.Checked;
                progressBar1.Maximum = 20;
                progressBar1.Minimum = 0;
                progressBar1.Step = 2;

                ReadTipOptimizacii();
                if (Program.CheckDlinaDnya() && Program.CheckParnSmen())
                {

                    bw.RunWorkerAsync();
                }
                else
                {

                    progressBar1.Visible = false;
                    listBox1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CloseProcessOnError();
                MessageBox.Show(ex.ToString());
            }
        }



        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 2: comboBox3.SelectedIndex = -1; break;
                case 1: buttonParamOptimiz.PerformClick(); break;
                case 0: buttonKassov.PerformClick(); break;
            }
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
                //  Form3 f3 = new Form3(2);
                //  f3.Show(this);
                // this.Enabled = false;
                if (Program.isConnected())
                {
                    Enabled = true;
                    labelStatus2.Text = "режим работы сетевой ";
                    buttonVygr.Visible = true;
                    comboBox2.Visible = true;
                    isConnected = true;
                   StartDiagramForm();

                }
                else
                {
                    isConnected = false;
                    radioButtonIzFile.Checked = true;
                    Enabled = true;
                    labelStatus2.Text = "режим работы локальный ";
                    buttonVygr.Visible = false;
                    comboBox2.Visible = false;
                }
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
                        if (int.Parse(dataGridViewForTSR[e.ColumnIndex, e.RowIndex].Value.ToString()) < 8000)
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

       

       

        

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (Program.shops.Count >0)
            {
                //MessageBox.Show(Program.shops.Count + "");
                //ForExcel.comboBoxMCountPerson1 = comboBoxMCountPerson.SelectedIndex;
                ForExcel.checkBoxMPeremSotr1 = checkBoxMPeremSotr.Checked;
                ForExcel.checkBoxMReadschedule1 = checkBoxMReadschedule.Checked;
                ForExcel.checkBoxMUchetSmen1 = checkBoxMUchetSmen.Checked;

                if (Program.isConnected())
                {
                    CreateZip();
                }
                else
                {
                    //  Form3 f3 = new Form3(1);
                    //  f3.Show(this);
                    //this.Enabled = false;
                    if (Program.isConnected())
                    {
                        ((Form1)this.Owner).Enabled = true;
                        ((Form1)this.Owner).labelStatus2.Text = "режим работы сетевой ";
                        ((Form1)this.Owner).buttonVygr.Visible = true;
                        ((Form1)this.Owner).comboBox2.Visible = true;
                        isConnected = true;
                        ((Form1)this.Owner).isConnected = true;

                       ((Form1)this.Owner).CreateZip();
                           

                       // this.Close();
                    }
                    else
                    {
                        isConnected = false;
                        ((Form1)this.Owner).isConnected = false;
                        ((Form1)this.Owner).radioButtonIzFile.Checked = true;
                        ((Form1)this.Owner).Enabled = true;
                        ((Form1)this.Owner).labelStatus2.Text = "режим работы локальный ";
                        ((Form1)this.Owner).buttonVygr.Visible = false;
                        ((Form1)this.Owner).comboBox2.Visible = false;

                       // this.Close();
                    }
                    return;
                }
            }
            else {
                MessageBox.Show("Не сформирован список магазинов.");
            }

        }


        public void CreateZip()
        {
            saveFileDialog1.DefaultExt = ".zip";
            saveFileDialog1.FileName = "Архив графиков, гистограм по нескольким магазинам";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "Архив|*.zip";
            if (Settings.Default.folder == "")
            {
                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            else
            {
                saveFileDialog1.InitialDirectory = Settings.Default.folder;
            }
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

        private void bg1_ProgressChanged(object sender, ProgressChangedEventArgs e)
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
                MessageBox.Show(ex.ToString());
            }
            if (Directory.Exists(Environment.CurrentDirectory + @"\mult\"))
            {
                foreach (var file in Directory.GetFiles(Environment.CurrentDirectory + @"\mult\"))
                {

                    File.Delete(file);
                }
            }
            int ShopStep = 4;
            if (Program.shops.Count != 0)
            {
                ShopStep = 100 / Program.shops.Count;
            }
            int TaskStep = ShopStep / 4;
            BackgroundWorker bg1 = sender as BackgroundWorker;

            Program.currentShop = new Shop(0, "");
            bg1.ProgressChanged += bg1_ProgressChanged;
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




            // Program.readTSR();

            foreach (Shop shop in Program.shops)
            {
                
                Program.readFactors(shop.getIdShop());
                Program.ReadNormaChas(DateTime.Now.Year);
                Program.currentShop.setIdShopFM(shop.getIdShop());
                Program.getListDate(DateTime.Today.Year, false);
                string readPath = Environment.CurrentDirectory + @"\Shops\" + Program.currentShop.getIdShop() + $@"\Calendar{DateTime.Today.AddYears(1).Year}";
                if (File.Exists(readPath))
                {
                    Program.getListDate(DateTime.Today.AddYears(1).Year, true);
                }
                Program.readTSR();
                Program.readVarSmen(true);

                shop.setMinRab(Program.ReadMinRabForShop());
                Program.currentShop.setMinRab(MinRab.ReadForShop(shop.getIdShop()));
                // Program.currentShop.setIdShop(0);
                Program.currentShop.setAdresShop(shop.getAddress());

                Program.ReadPrilavki();
                if (Program.currentShop.VarSmens.Count == 0)
                {
                    //  VarSmen.CreateVarSmen();
                }

                if (!Directory.Exists(Environment.CurrentDirectory + @"\mult\"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + @"\mult\");
                }


                switch (Program.TipExporta)
                {
                    case 0:
                        {
                            filename = Environment.CurrentDirectory + @"\mult\" + "График_" + Program.currentShop.getAddress() + "_" +
                                               Program.getMonths(DateTime.Now.AddMonths(1).Month) + ".xlsx";
                            try
                            {
                                if (ForExcel.checkBoxMPeremSotr1)
                                {
                                    Program.currentShop.SortSotr = true;
                                }
                                else
                                {
                                    Program.currentShop.SortSotr = false;
                                }
                                if (!ForExcel.checkBoxMReadschedule1)
                                {

                                    bool b = Program.GrafM.TryGetValue(shop.getIdShop(), out Program.file);

                                    if (b)
                                    {

                                        if (ForExcel.checkBoxMUchetSmen1)
                                        {
                                            ForExcel.CreateEmployee();
                                        }
                                        else
                                        {
                                            ForExcel.CreateEmployeeWithVarSmen();
                                        }
                                    }
                                    else
                                    {
                                        b = false;
                                    }
                                }



                                bg1.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Создание прогноза продаж");

                                if (!ForForecast.createPrognoz(false, Program.IsMpRezhim, true, false))
                                {
                                    MessageBox.Show("График не удалось построить. Ошибка в построении прогноза.");
                                    return;
                                }

                                bg1.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Подсчет оптимальной загруженности");

                                Sotrudniki.OptimCountSotr();

                                if (Program.currentShop.Semployes != null)
                                {
                                    if (Program.currentShop.Semployes.Count != 0)
                                    {
                                        Sotrudniki.NewOtrabotal();
                                    }
                                }

                                bg1.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Оптимальная расстановка смен");
                                if (!Sotrudniki.CreateSmens(false, Program.currentShop.employes))
                                {
                                    MessageBox.Show("График не удалось построить для "+ shop.getAddress()+" из-за большой нормы часов в текущем месяце. Все смены у одного из сотрудников 12 часов и не хватает часов до нормы. Перейдите на вкладку производственный календарь и уменьшите текущее значение.");
                                   continue;
                                }

                                if (!Sotrudniki.CheckGrafic13())
                                {
                                    MessageBox.Show("График не удалось построить для " + shop.getAddress() + " из-за большой нормы часов в текущем месяце. Перейдите на вкладку производственный календарь и уменьшите текущее значение.");
                                   continue;
                                }
                                if (!Sotrudniki.CheckGrafic2())
                                {
                                  //  MessageBox.Show("График не удалось построить для " + shop.getAddress() + " из-за выбранных вариантов смен и минимального числа сотрудников. Уменьшите минимальное число сотрудников и используйте другие смены");
                                   // return;
                                }

                                ForExcel.ExportExcel(filename, bg1);

                            }
                            catch (Exception ex)
                            {

                                //  label3.Visible = false;
                                //  progressBar1.Visible = false;
                                MessageBox.Show(ex.ToString());
                                string[] s = new string[2];
                                // s = listBox1.Text.Split('_');
                                //  Program.currentShop = new Shop(Int16.Parse(s[0]), s[1]);
                                //  Program.getListDate(DateTime.Today.Year);
                                Program.readTSR();
                                MessageBox.Show("Расписание не создано");
                                //CloseProcessOnError();
                                //return;
                                // throw ex;
                            }
                            //  System.Drawing.Color color;



                            break;

                        }
                    case 1:
                        {
                            filename = Environment.CurrentDirectory + @"\mult\" + Program.currentShop.getAddress() + ".xlsx";
                            try
                            {
                                bg1.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Создание прогноза продаж");

                                if (!ForForecast.createPrognoz(false, true, true, false))
                                {
                                    MessageBox.Show("График не удалось построить. Ошибка в построении прогноза.");
                                    return;
                                }

                                bg1.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Подсчет оптимальной загруженности");
                                Sotrudniki.OptimCountSotr();


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
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
                            // ObjExcel.DisplayAlerts = false;
                            ObjWorkBook.Saved = true;
                            try
                            {
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Запись в Excel");
                                bg1.ReportProgress(Program.BgProgress += TaskStep);
                                ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookDefault);
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
                            filename = Environment.CurrentDirectory + @"\mult\" + Program.currentShop.getAddress() + ".xlsx";
                            try
                            {
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Создание прогноза продаж");
                                bg1.ReportProgress(Program.BgProgress += TaskStep);

                                if (!ForForecast.createPrognoz(false, true, true, Program.isLocalDB))
                                {
                                    MessageBox.Show("График не удалось построить. Ошибка в построении прогноза.");
                                    return;
                                }

                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Подсчет оптимальной загруженности");
                                bg1.ReportProgress(Program.BgProgress += TaskStep);
                                Sotrudniki.OptimCountSotr();



                            }
                            catch (Exception ex)
                            {
                                CloseProcessOnError();
                                MessageBox.Show(ex.ToString());
                                throw ex;
                            }
                            object misValue = System.Reflection.Missing.Value;

                            Excel.Range excelcells;
                            Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                            ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
                            //ObjWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

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
                            // ObjExcel.DisplayAlerts = false;
                            ObjWorkBook.Saved = true;
                            try
                            {
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Запись в Excel");
                                bg1.ReportProgress(Program.BgProgress += TaskStep);
                                ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookDefault);
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
                    case 3:
                        {
                            filename = Environment.CurrentDirectory + @"\mult\" + "График_" + Program.currentShop.getAddress() + "_" +
                                               Program.getMonths(DateTime.Now.AddMonths(0).Month) + ".xlsx";
                            try
                            {
                                if (ForExcel.checkBoxMPeremSotr1)
                                {
                                    Program.currentShop.SortSotr = true;
                                }
                                else
                                {
                                    Program.currentShop.SortSotr = false;
                                }

                                string fc = Environment.CurrentDirectory + @"\Shops\" + shop.getIdShopFM() + "\\График_" + Program.currentShop.getAddress() + "_" +
                                           Program.getMonths(DateTime.Now.AddMonths(0).Month) + ".xlsx";

                                if (!File.Exists(fc))
                                {

                                    bg1.ReportProgress(0);

                                    fc = Program.file;

                                }
                                else if (!File.Exists(fc))
                                {
                                    bg1.ReportProgress(-1);
                                    continue;
                                }

                                ForExcel.CreateEmployeeAndSmens();





                                bg1.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Создание прогноза продаж");

                                if (!ForForecast.createPrognoz(true, Program.IsMpRezhim, true, Program.isLocalDB))
                                {
                                    MessageBox.Show("График не удалось построить. Ошибка в построении прогноза.");
                                    return;
                                }

                                bg1.ReportProgress(Program.BgProgress += TaskStep);
                                lbProgressMessages.BeginInvoke(new updateLabel3Delegate(ChangeLabel3Text), $"{shop.getAddress()}: Подсчет оптимальной загруженности");

                                Sotrudniki.OptimCountSotr(true);
                                bg1.ReportProgress(6);

                                if (Program.currentShop.Semployes.Count != 0)
                                {
                                    Sotrudniki.NewOtrabotal();
                                    Sotrudniki.StarSmen();
                                }
                                //  
                                bg1.ReportProgress(7);

                                if (!Sotrudniki.CreateSmens(true, Program.currentShop.employes))
                                {

                                    MessageBox.Show("График не удалось построить для " + shop.getAddress() + " из-за большой нормы часов в текущем месяце. Все смены у одного из сотрудников 12 часов и не хватает часов до нормы. Перейдите на вкладку производственный календарь и уменьшите текущее значение.");
                                    continue;
                                }

                                Sotrudniki.CheckDisp();


                                if (!Sotrudniki.CheckGrafic13())
                                {
                                    MessageBox.Show("График не удалось построить для " + shop.getAddress() + " из-за большой нормы часов в текущем месяце. Перейдите на вкладку производственный календарь и уменьшите текущее значение.");
                                    continue;
                                }
                                if (!Sotrudniki.CheckGrafic2())
                                {
                                   // MessageBox.Show("График не удалось построить для " + shop.getAddress() + " из-за выбранных вариантов смен и минимального числа сотрудников. Уменьшите минимальное число сотрудников и используйте другие смены");
                                    //return;
                                }

                                ForExcel.ExportExcel(filename, bg1);

                            }
                            catch (Exception ex)
                            {

                                //  label3.Visible = false;
                                //  progressBar1.Visible = false;
                                MessageBox.Show(ex.ToString());
                                string[] s = new string[2];
                                // s = listBox1.Text.Split('_');
                                //  Program.currentShop = new Shop(Int16.Parse(s[0]), s[1]);
                                //  Program.getListDate(DateTime.Today.Year);
                                Program.readTSR();
                                MessageBox.Show("Расписание не создано");
                                //CloseProcessOnError();
                                //return;
                                // throw ex;
                            }
                            //  System.Drawing.Color color;



                            break;

                        }

                    default:
                        {
                            CloseProcessOnError();
                            break;
                        }
                }
                bg1.ReportProgress(ShopStep * shopCounter);
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

            ZipFile zf = new ZipFile(PathToZip, Encoding.GetEncoding("cp866"));
            zf.AddDirectory(startPath);
            bg1.ReportProgress(100);

            zf.Save(); //Сохраняем архив.

        }

        private void tbKassirCount_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbMinRabCount.Text))
            {
                if (int.Parse(tbMinRabCount.Text) < 1)
                {
                    MessageBox.Show("Меньше 1 нельзя");
                    tbMinRabCount.Text = "";
                    return;
                }

                if (int.Parse(tbMinRabCount.Text) > 5)
                {
                    MessageBox.Show("Не оптимальное количество. Допустимо от 1 до 5.");
                    tbMinRabCount.Text = "";
                    return;
                }

               // int kassirCount;

               /* if (int.TryParse(tbMinRabCount.Text, out kassirCount))
                {
                    Program.currentShop.minrab.setMinCount(kassirCount);

                }*/

            }
            else
            {
               /* Program.currentShop.minrab.setMinCount(1);*/
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
                // Form3 f3 = new Form3();
                // f3.Show(this);
                //this.Enabled = false;
                if (Program.isConnected())
                {
               //     ((Form1)this.Owner).Enabled = true;
                    labelStatus2.Text = "режим работы сетевой ";
                    buttonVygr.Visible = true;
                    comboBox2.Visible = true;
                    isConnected = true;
                    isConnected = true;


                  
                }
                else
                {
                    isConnected = false;
                    
                    radioButtonIzFile.Checked = true;
                    Enabled = true;
                    labelStatus2.Text = "режим работы локальный ";
                    buttonVygr.Visible = false;
                    comboBox2.Visible = false;

                    // this.Close();
                }

                return;
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
                    saveFileDialog1.DefaultExt = ".xlsx";
                    saveFileDialog1.AddExtension = true;
                    saveFileDialog1.Filter = "Файл Excel|*.XLSX;*.XLS";
                    if (Settings.Default.folder == "")
                    {
                        saveFileDialog1.InitialDirectory = Environment.CurrentDirectory + @"\Shops\" + Program.currentShop.getIdShop();
                    }
                    else
                    {
                        saveFileDialog1.InitialDirectory = Settings.Default.folder;
                    }
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
                    Program.currentShop.daysSale = ForForecast.createListDaySale(dt, dt.AddDays(31), Program.currentShop.getIdShop());

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
                    progressBar3.Visible = true;
                    progressBar3.Value = 0;
                    progressBar3.Maximum = Program.currentShop.daysSale.Count;
                    foreach (daySale twd in Program.currentShop.daysSale)
                    {
                        progressBar3.PerformStep();
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

                        ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookDefault);
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
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    progressBar3.Visible = false;
                    buttonRaspisanie.Enabled = true;
                    buttonCalendar.Enabled = true;
                    buttonKassov.Enabled = true;
                    tabControl1.Enabled = true;
                    button_refresh_list_shops.Enabled = true;
                    buttonMultShops.Enabled = true;
                }
            }

        }

        public static string getDolgn(int x)
        {
            switch (x)
            {
                case 0: return "Кассир";
                case 1: return "Продавец";
                case 2: return "Грузчик";
                case 3: return "Гастроном";
                default: return "";
            }
        }

        private void dataGridViewVarSmen_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    Program.currentShop.VarSmens.Find(t => t.getR().ToString() == dataGridViewVarSmen[0, e.RowIndex].Value.ToString() && t.getDolgnost() == getDolgn(comboBox4.SelectedIndex)).setR(int.Parse(dataGridViewVarSmen[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;
                case 1:
                    Program.currentShop.VarSmens.Find(t => t.getR().ToString() == dataGridViewVarSmen[0, e.RowIndex].Value.ToString() && t.getDolgnost() == getDolgn(comboBox4.SelectedIndex)).setV(int.Parse(dataGridViewVarSmen[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;
                case 2:
                    Program.currentShop.VarSmens.Find(t => t.getR().ToString() == dataGridViewVarSmen[0, e.RowIndex].Value.ToString() && t.getDolgnost() == getDolgn(comboBox4.SelectedIndex)).setDeistvie(bool.Parse(dataGridViewVarSmen[e.ColumnIndex, e.RowIndex].Value.ToString()));
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
            Program.getListDate(DateTime.Now.AddYears(1).Year, true);

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
            Program.writeVarSmen();
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
                if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                {
                    Program.currentShop.VarSmens.Add(new VarSmen(int.Parse(textBox3.Text), int.Parse(textBox4.Text), int.Parse(textBox5.Text), false));
                    Program.writeVarSmen();
                    dataGridViewVarSmen.DataSource = viewVarSmen(false);
                }
                else
                {
                    MessageBox.Show("Одно из полей не было заполнено. Смена не будет добавлена");
                }

                label12.Visible = false;
                label13.Visible = false;
                label15.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                Program.addsmena = false;
                button10.Text = "Добавить смену";
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
            /*  String readPath = Environment.CurrentDirectory + "/Shops/" + Program.currentShop.getIdShop() + "/Prilavki";
              if (checkBox1.Checked)
              {
                  // MessageBox.Show("Число продавцов стало зависить от прилавков");

                  Program.currentShop.prilavki.SetNalichie(true);
                  //label19.Visible = true;
                  //textBox6.Visible = true;
              }
              else
              {
                  using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                  {
                      sw.WriteLine(false);
                      Program.HandledShops.Add(Program.currentShop.getIdShop());
                      UpdateStatusShops();
                  }
                  Program.currentShop.prilavki.SetNalichie(false);
                  label19.Visible = false;
                  textBox6.Visible = false;
              }*/
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
            bool error = false;

            if (!String.IsNullOrEmpty(textBox3.Text))
            {
                if (!String.IsNullOrEmpty(textBox4.Text))
                {
                    if (int.Parse(textBox3.Text) < int.Parse(textBox4.Text))
                    {
                        error = true;
                    }
                }

                if (int.Parse(textBox3.Text) > 6 || int.Parse(textBox3.Text) < 1)
                {
                    error = true;
                }
            }

            if (error)
            {
                MessageBox.Show("Меньше 1 и больше 6 нельзя, а количество рабочих смен не должно быть меньше количества выходных");
                textBox3.Text = "";
            }

            return;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            bool error = false;
            if (!String.IsNullOrEmpty(textBox4.Text))
            {
                if (int.Parse(textBox4.Text) < 1)
                {
                    error = true;
                }
                else if (!String.IsNullOrEmpty(textBox3.Text))
                {
                    if (int.Parse(textBox3.Text) < int.Parse(textBox4.Text))
                    {
                        error = true;
                    }
                }
            }

            if (error)
            {
                MessageBox.Show("Меньше 1 нельзя, а количество рабочих смен не должно быть меньше количества выходных.");
                textBox4.Text = "";
            }

            return;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox5.Text) && (int.Parse(textBox5.Text) < 7 || int.Parse(textBox5.Text) > 10))
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
            /*  if (!String.IsNullOrEmpty(tbLastHour.Text))
              {
                  if (int.Parse(textBox6.Text) < 0 || int.Parse(textBox6.Text) > 10)
                  {
                      MessageBox.Show("Введите число продавцов от 1 до 10");
                      textBox6.Text = "";
                      return;
                  }
              }*/
        }

        private void tbLastHour_Leave(object sender, EventArgs e)
        {
            /*  if (!String.IsNullOrEmpty(tbLastHour.Text))
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

              }*/
        }

        private void tbKassirCount_Leave(object sender, EventArgs e)
        {
            if (tbMinRabCount.Text == "")
            {

                tbMinRabCount.Text = "";
            }
        }

        #region Сортировка бокса с магазинами

        private void bAlphabetSort_Click(object sender, EventArgs e)
        {
            bNumberSort.Enabled = true;
            bAlphabetSort.Enabled = false;
            listBox1List.Sort((x1, x2) => x1.Item2.CompareTo(x2.Item2));
            FillListBox(listBox1List, listBox1);
        }

        private void bNumberSort_Click(object sender, EventArgs e)
        {
            bAlphabetSort.Enabled = true;
            bNumberSort.Enabled = false;
            listBox1List.Sort((x1, x2) => x1.Item1.CompareTo(x2.Item1));
            FillListBox(listBox1List, listBox1);
        }

        private void bAlphabetSortM_Click(object sender, EventArgs e)
        {
            bNumberSortM.Enabled = true;
            bAlphabetSortM.Enabled = false;
            listBox1List.Sort((x1, x2) => x1.Item2.CompareTo(x2.Item2));
            FillListBox(listBox1List, listBoxMShops);
        }

        private void bNumberSortM_Click(object sender, EventArgs e)
        {
            bAlphabetSortM.Enabled = true;
            bNumberSortM.Enabled = false;
            listBox1List.Sort((x1, x2) => x1.Item1.CompareTo(x2.Item1));
            FillListBox(listBox1List, listBoxMShops);
        }

        private void FillListBox(List<Tuple<string, string>> listForFilling, System.Windows.Forms.ListBox box)
        {
            box.Items.Clear();
            foreach (var listItem in listForFilling)
            {
                box.Items.Add($"{listItem.Item1}_{listItem.Item2}");
            }
        }

        #endregion

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            /*  textBox6.SelectionStart = 0;
              textBox6.SelectionLength = textBox6.Text.Length;
              textBox6.Focus();*/
        }

        int hoveredIndex = -1;

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                int newHoveredIndex = 0;

                System.Windows.Forms.ListBox senderListBox;
                senderListBox = sender as System.Windows.Forms.ListBox;
                if (senderListBox != null)
                {
                    switch (senderListBox.Name)
                    {
                        case "listBox1":
                            newHoveredIndex = listBox1.IndexFromPoint(e.Location);
                            break;
                        case "listBoxMShops":
                            newHoveredIndex = listBoxMShops.IndexFromPoint(e.Location);
                            break;
                        case "listBoxMPartShops":
                            newHoveredIndex = listBoxMPartShops.IndexFromPoint(e.Location);
                            break;
                    }

                    // If the row has changed since last moving the mouse:
                    if (hoveredIndex != newHoveredIndex)
                    {
                        // Change the variable for the next timw we move the mouse:
                        hoveredIndex = newHoveredIndex;

                        // If over a row showing data (rather than blank space):
                        if (hoveredIndex > -1)
                        {
                            //Set tooltip text for the row now under the mouse:
                            toolTip1.Active = false;
                            toolTip1.SetToolTip(senderListBox, (senderListBox.Items[hoveredIndex]).ToString());
                            toolTip1.Active = true;
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("ERROR: Не удалось показать ToolTip");
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

      

      

        private void dataGridViewVarSmen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panelSingleShop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (Settings.Default.folder == "")
            {
                openFileDialog.InitialDirectory = "c:\\";
            }
            else
            {
                openFileDialog.InitialDirectory = Settings.Default.folder;
            }
            openFileDialog.Filter = "*|*.xlsx";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Program.file = openFileDialog.FileName;


                try
                {
                    progressBar1.Visible = true;
                    progressBar1.Maximum = 100;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;
                    ForExcel.progress = 0;
                    progressBar1.Step = 1;

                    if (comboBox3.SelectedIndex == 0)
                    {
                        if (checkBox2.Checked)
                        {
                            ForExcel.thread1 = new Thread(ForExcel.CreateEmployee);
                        }
                        else
                        {
                            ForExcel.thread1 = new Thread(ForExcel.CreateEmployeeWithVarSmen);
                        }
                    }
                    else
                    {
                        ForExcel.thread1 = new Thread(ForExcel.CreateEmployeeAndSmens);
                    }
                    ForExcel.thread1.Priority = ThreadPriority.Highest;
                    ForExcel.thread1.IsBackground = true;
                    try
                    {
                        ForExcel.thread1.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    timer2.Enabled = true;



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void CheckDone(Thread ts, System.Windows.Forms.Timer t, System.Windows.Forms.Button btn, System.Windows.Forms.ProgressBar pb1, ComboBox cb1)
        {
            pb1.Value = ForExcel.progress;
            if (!ts.IsAlive)
            {
                t.Enabled = false;
                t.Stop();
                Done(btn, pb1, cb1);


            }
            else
            {
                return;
            }

        }

        public static void Done(System.Windows.Forms.Button btn, System.Windows.Forms.ProgressBar pb1, ComboBox cb1)
        {
            if ((Program.currentShop.Semployes.Count != 0) && (!ForExcel.error))
            {
                MessageBox.Show("Чтение завершено успешно");
                btn.BackColor = Color.PaleGreen;
                refreshCountSotr(cb1);
            }
            else
            {
                MessageBox.Show("Ошибка чтения! Файл имеет неверные данные");
                Program.currentShop.Semployes.Clear();
            }
            pb1.Visible = false;
        }

        public static void refreshCountSotr(ComboBox cb)
        {
            if ((Program.currentShop.Semployes != null) && (Program.currentShop.Semployes.Count > 0) && (Program.currentShop.Semployes[0].smens.Count == 0))
            {
                cb.Items.Clear();
                cb.Items.Add("штатного расписания");
                cb.Items.Add("загруженного графика");
                cb.Items.Add("прогноза продаж");
            }
            else if ((Program.currentShop.Semployes != null) && (Program.currentShop.Semployes.Count > 0) && (Program.currentShop.Semployes[0].smens.Count > 0))
            {

                cb.Items.Clear();
                cb.Items.Add("штатного расписания");
                cb.Items.Add("загруженного графика");
                cb.Items.Add("прогноза продаж");
            }
            else
            {

                cb.Items.Clear();
                cb.Items.Add("штатного расписания");
                cb.Items.Add("прогноза продаж");
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            CheckDone(ForExcel.thread1, timer2, button14, progressBar1, comboBoxCountSotr);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewVarSmen.DataSource = viewVarSmen(false);
            labelMinRabCount.Text = comboBox4.Text + " с времени";
            EmployeeType employeeType = MinRab.getType(comboBox4.Text);
            tbMinRabCount.Text = MinRab.Read(employeeType, Program.currentShop.getIdShop()).getMinCount().ToString();
        }

      

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            Program.currentShop.SortSotr = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ForExcel.readVarSmen = checkBox2.Checked;
        }

        private void labelCountSotr_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxCountSotr_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sotrudniki.CountSotr = comboBoxCountSotr.SelectedItem.ToString();
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxMCountPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sotrudniki.CountSotr = comboBoxMCountPerson.SelectedItem.ToString();
        }

        private void checkBoxMPeremSotr_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxMReadschedule_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMReadschedule.Checked == true)
            {
                buttonReadMGraf.Visible = false;
            }
            else
            {
                buttonReadMGraf.Visible = true;
            }
        }

        private void buttonReadMGraf_Click(object sender, EventArgs e)
        {
            if (Program.shops.Count>0) {
                try
                {
                    foreach (Shop shop in Program.shops)
                    {
                        Program.currentShop.setIdShop(shop.getIdShopFM());
                        Program.currentShop.setAdresShop(shop.getAddress());
                        Program.GrafM = new Dictionary<int, string>();
                        string fc = Environment.CurrentDirectory + @"\Shops\" + shop.getIdShopFM() + "\\График_" + Program.currentShop.getAddress() + "_" +
                                                      Program.getMonths(DateTime.Now.AddMonths(0).Month) + ".xlsx";
                        while (!File.Exists(fc))
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.Title = "Выберите график для " + Program.currentShop.getAddress();
                            if (Settings.Default.folder == "")
                            {
                                openFileDialog.InitialDirectory = "c:\\";
                            }
                            else
                            {
                                openFileDialog.InitialDirectory = Settings.Default.folder;
                            }
                            openFileDialog.Filter = "*|*.xlsx";
                            openFileDialog.RestoreDirectory = true;

                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                fc = openFileDialog.FileName;

                            }

                        }

                        Program.GrafM.Add(shop.getIdShop(), fc);

                    }
                    MessageBox.Show("Считывание графиков завершено успешно");
                    buttonReadMGraf.BackColor = Color.Green;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } }
            else {
                MessageBox.Show("Не сформирован список магазинов.");
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void labelMinRabCount_Click(object sender, EventArgs e)
        {

        }
    }
}

