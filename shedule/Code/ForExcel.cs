using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;
using LinqToExcel;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace shedule.Code
{
    class ForExcel
    {
        public static Thread thread1;
        public static int progress = 0;
        public static bool error = false;
        public static bool readVarSmen = true;
        public static int comboBoxMCountPerson1 = 0;
        public static bool checkBoxMPeremSotr1 = false;
        public static bool checkBoxMReadShedule1 = false;
        public static bool checkBoxMUchetSmen1 = false;
       // public static string NameExcel;

        public static void  ExportExcel(string filename, BackgroundWorker bg) {

           /* try
            {
                foreach (Process proc in Process.GetProcessesByName("EXCEL"))
                {
                    proc.Dispose();
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.ToString());
            }
            */
            System.Drawing.Color color;
            Microsoft.Office.Interop.Excel.Range excelcells;

            try
            {
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
                ObjWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
               

                
                ObjWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[2];
                ObjWorkSheet.Name = "Часы";

                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                bg.ReportProgress(10);
                ObjWorkSheet.Name = "График";

                excelcells = ObjWorkSheet.get_Range("A1", "AK100");
                excelcells.Font.Size = 10;
                excelcells.NumberFormat = "@";
               // bg.ReportProgress(10);
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;

                excelcells = ObjWorkSheet.get_Range("E3", "F100");
                excelcells.NumberFormat = "General";

                int i = 7;
                foreach (daySale twd in Program.currentShop.MouthPrognoz)
                {

                    ObjWorkSheet.Cells[1, i] = twd.getWeekDay2();
                    if ((twd.getWeekDay2()=="Сб")|| (twd.getWeekDay2() == "Вс")) {
                        excelcells = ObjWorkSheet.get_Range(RetutnI(i)+"1", RetutnI(i)+"100");
                        excelcells.Font.ColorIndex = 3;
                    }
                    ObjWorkSheet.Cells[2, i] = twd.getData().Day;
                    i++;
                }
                //Microsoft.Office.Interop.Excel.Range excelcells2 = ObjWorkSheet.get_Range("A3", "AK50");
                //  excelcells2.ColumnWidth = Program.currentShop.getAddress().Length;
                //  bg.ReportProgress(12);

                bg.ReportProgress(12);
                excelcells = ObjWorkSheet.get_Range("D1", "D2");
                excelcells.WrapText = true;
                excelcells = ObjWorkSheet.get_Range("E1", "E2");
                excelcells.WrapText = true;
                excelcells = ObjWorkSheet.get_Range("F1", "F2");
                excelcells.WrapText = true;
                excelcells = ObjWorkSheet.get_Range("B1", "B100");
                excelcells.ColumnWidth = 9.29;

                excelcells = ObjWorkSheet.get_Range("C1", "C100");
                excelcells.ColumnWidth = 7.57;
                excelcells = ObjWorkSheet.get_Range("D1", "D100");
                excelcells.ColumnWidth = 14;
                excelcells = ObjWorkSheet.get_Range("E1", "E100");
                excelcells.ColumnWidth = 7.57;
                excelcells = ObjWorkSheet.get_Range("F1", "AL100");
                excelcells.ColumnWidth = 5.43;
                excelcells = ObjWorkSheet.get_Range("A1", "F2");
                excelcells.RowHeight = 15;
                ObjWorkSheet.Columns[1].ColumnWidth = Program.currentShop.getAddress().Length;

                ObjWorkSheet.Cells[2, 1] = "Адрес";
                excelcells = ObjWorkSheet.get_Range("A1", "A2");
                excelcells.Merge(Type.Missing);
               
                ObjWorkSheet.Cells[2, 2] = "Должность";
                excelcells = ObjWorkSheet.get_Range("B1", "B2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 3] = "График";
                ObjWorkSheet.Cells[2, 4] = "Тип занятости";
                excelcells = ObjWorkSheet.get_Range("C1", "C2");
                excelcells.Merge(Type.Missing);
              //  ObjWorkSheet.Cells[2, 4] = "Должность";
                excelcells = ObjWorkSheet.get_Range("D1", "D2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 5] = "Общ.кол-во час.";
                excelcells = ObjWorkSheet.get_Range("E1", "E2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 6] = "Кол-во смен";
                excelcells = ObjWorkSheet.get_Range("F1", "F2");
                excelcells.Merge(Type.Missing);

                // MessageBox.Show(Program.currentShop.employes.Count+" count");
                int j = 3;
              
                foreach (employee emp in Program.currentShop.employes)
                {
                    switch (emp.getID() / 100)
                    {
                        case 0: color = System.Drawing.Color.LightSkyBlue; break;
                        case 1: color = System.Drawing.Color.LightGreen; break;
                        case 2: color = System.Drawing.Color.DarkSeaGreen; break;
                        case 3: color = System.Drawing.Color.PaleGoldenrod; break;

                        default: color = System.Drawing.Color.White; break;

                    }
                    try
                    {
                        ObjWorkSheet.Cells[j, 1] = Program.currentShop.getAddress();
                        ObjWorkSheet.Cells[j, 1].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 2] = emp.GetDolgnost();
                        ObjWorkSheet.Cells[j, 2].Interior.Color = color;


                        ObjWorkSheet.Cells[j, 3].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 3] = emp.getVS().getR() + "/" + emp.getVS().getV();
                        ObjWorkSheet.Cells[j, 4].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 4] = emp.getTipZan();
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                        continue;
                    }
                    i = 7;
                    foreach (daySale twd in Program.currentShop.MouthPrognoz)
                    {
                        //  MessageBox.Show(emp.getTip() + "");
                       
                        ObjWorkSheet.Cells[j, i].Interior.Color = color;

                        if ((emp.smens.Find(t => t.getData().Date == twd.getData().Date) != null) && (emp.smens.Count != 0))
                        {
                            // MessageBox.Show(emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + " - " + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena());

                            ObjWorkSheet.Cells[j, i] =
                                 emp.smens.Find(t => t.getData().Date == twd.getData().Date).getStartSmena() + " - " +
                                 emp.smens.Find(t => t.getData().Date == twd.getData().Date).getEndSmena();
                        }
                        //else
                        // { ObjWorkSheet.Cells[emp.getID() + 4, i] = emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + "-" + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena(); }
                        i++;
                    }

                       
                        /*if (Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()) != null)
                        {
                            ObjWorkSheet.Cells[j, 4] =
                                Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()).getZarp();
                        }*/
                        excelcells = ObjWorkSheet.get_Range("E" + j, "E" + j);
                        excelcells.FormulaLocal = $"=СУММ(Часы!G{j}:Часы!AL{j})";
                    ObjWorkSheet.Cells[j, 5].Interior.Color = color;
                    //  ObjWorkSheet.Cells[j, 5].Interior.Color = System.Drawing.Color.LightSkyBlue; ;
                    excelcells = ObjWorkSheet.get_Range("F" + j, "F" + j);
                    excelcells.FormulaLocal = $"=СЧЕТ(Часы!G{j}:Часы!AK{j})";
                    ObjWorkSheet.Cells[j, 6].Interior.Color = color;

                    

                    j++;

                }


                excelcells = ObjWorkSheet.get_Range("A1", "AL2");
                excelcells.Cells.Font.Bold = true;

                excelcells = ObjWorkSheet.get_Range("A1", RetutnI(Program.currentShop.MouthPrognoz.Count + 6) + (Program.currentShop.employes.Count+2).ToString());
                excelcells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


                bg.ReportProgress(14);


                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[2];
                
                //ObjWorkSheet.Name = "Часы";

                excelcells = ObjWorkSheet.get_Range("A1", "AL100");
                excelcells.Font.Size = 10;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                excelcells.NumberFormat = "General";
                excelcells = ObjWorkSheet.get_Range("C1", "C100");
                excelcells.NumberFormat = "@";
               
                i = 7;
                foreach (daySale twd in Program.currentShop.MouthPrognoz)
                {

                    ObjWorkSheet.Cells[1, i] = twd.getWeekDay2();
                    if ((twd.getWeekDay2() == "Сб") || (twd.getWeekDay2() == "Вс"))
                    {
                        excelcells = ObjWorkSheet.get_Range(RetutnI(i) + "1", RetutnI(i) + "100");
                        excelcells.Font.ColorIndex = 3;
                    }
                    ObjWorkSheet.Cells[2, i] = twd.getData().Day;
                    i++;
                }
                // Microsoft.Office.Interop.Excel.Range excelcells3 = ObjWorkSheet.get_Range("A3", "AL50");
                //excelcells3.ColumnWidth = Program.currentShop.getAddress().Length;
               // ExcelWorkSheet.Range.Select("B2", "C6");
               
                bg.ReportProgress(16);

                excelcells = ObjWorkSheet.get_Range("D1", "D2");
                excelcells.WrapText = true;
                excelcells = ObjWorkSheet.get_Range("E1", "E2");
                excelcells.WrapText = true;
                excelcells = ObjWorkSheet.get_Range("F1", "F2");
                excelcells.WrapText = true;
                excelcells = ObjWorkSheet.get_Range("B1", "B100");
                excelcells.ColumnWidth = 9.29;
                excelcells = ObjWorkSheet.get_Range("C1", "C100");
                excelcells.ColumnWidth = 7.57;
                excelcells = ObjWorkSheet.get_Range("D1", "D100");
                excelcells.ColumnWidth = 14;
                excelcells = ObjWorkSheet.get_Range("E1", "E100");
                excelcells.ColumnWidth = 7.57;
                excelcells = ObjWorkSheet.get_Range("F1", "AL100");
                excelcells.ColumnWidth = 5.43;
                excelcells = ObjWorkSheet.get_Range("A1", "F2");
                excelcells.RowHeight = 15;
                ObjWorkSheet.Columns[1].ColumnWidth = Program.currentShop.getAddress().Length;



                ObjWorkSheet.Cells[2, 1] = "Адрес";
                excelcells = ObjWorkSheet.get_Range("A1", "A2");
                excelcells.Merge(Type.Missing);

                ObjWorkSheet.Cells[2, 2] = "Должность";
                excelcells = ObjWorkSheet.get_Range("B1", "B2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 3] = "График";
               // ObjWorkSheet.Cells[2, 2] = "Должность";
                excelcells = ObjWorkSheet.get_Range("C1", "C2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 4] = "Тип занятости";
                excelcells = ObjWorkSheet.get_Range("D1", "D2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 5] = "Общ.кол-во час.";
                excelcells = ObjWorkSheet.get_Range("E1", "E2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 6] = "Кол-во смен";
                excelcells = ObjWorkSheet.get_Range("F1", "F2");
                excelcells.Merge(Type.Missing);


                // MessageBox.Show(Program.currentShop.employes.Count+" count");
                j = 3;
                foreach (employee emp in Program.currentShop.employes)
                {

                    i = 7;
                    foreach (daySale twd in Program.currentShop.MouthPrognoz)
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
                     
                       // if ((emp.smens.Find(t => t.getData().Date == twd.getData().Date) != null) && (emp.smens.Count != 0))
                        //{
                            String c = "График!" + RetutnI(i) + j;

                            excelcells = ObjWorkSheet.get_Range(RetutnI(i) + j, RetutnI(i) + j);
                            String f = $"=Если({ c  }=\"\";\"\";ПСТР({ c };Поиск(\"-\";{ c })+1;ДЛСТР({ c })-ПОИСК(\"-\";{ c }))-ПСТР({ c };1;ПОИСК(\"-\";{ c })-1)-1)";
                            excelcells.FormulaLocal = f;

                    //    }
                        //else
                        // { ObjWorkSheet.Cells[emp.getID() + 4, i] = emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + "-" + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena(); }
                        i++;

                        ObjWorkSheet.Cells[j, 1] = Program.currentShop.getAddress();
                        ObjWorkSheet.Cells[j, 1].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 2] = emp.GetDolgnost();
                        ObjWorkSheet.Cells[j, 2].Interior.Color = color;
                       
                        
                        ObjWorkSheet.Cells[j, 3] = emp.getVS().getR() + "/" + emp.getVS().getV();
                        ObjWorkSheet.Cells[j, 3].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 4].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 4] = emp.getTipZan();
                        /*
                        if (Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()) != null)
                        {
                            ObjWorkSheet.Cells[j, 4] =
                                Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()).getZarp();
                        }*/
                        excelcells = ObjWorkSheet.get_Range("E" + j, "E" + j);
                        excelcells.FormulaLocal = $"=СУММ(G{j}:AL{j}";
                        ObjWorkSheet.Cells[j, 5].Interior.Color = color;
                        // ObjWorkSheet.Cells[j, 4].Interior.Color = System.Drawing.Color.LightSkyBlue; ;
                        excelcells = ObjWorkSheet.get_Range("F" + j, "F" + j);
                        excelcells.FormulaLocal = $"=СЧЕТ(Часы!G{j}:Часы!AL{j}";
                        ObjWorkSheet.Cells[j, 6].Interior.Color = color;


                    }

                    j++;

                }

                excelcells = ObjWorkSheet.get_Range("A1", RetutnI(Program.currentShop.MouthPrognoz.Count + 6) + (Program.currentShop.employes.Count+2).ToString());
                excelcells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                excelcells = ObjWorkSheet.get_Range("A1", "AL2");
                excelcells.Cells.Font.Bold = true;



                // 3 list begin


                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[3];
                ObjWorkSheet.Name = "Смены";

              

                //ObjWorkSheet.Name = "Часы";

                excelcells = ObjWorkSheet.get_Range("A1", "AL100");
                excelcells.Font.Size = 10;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                excelcells.NumberFormat = "General";
                excelcells = ObjWorkSheet.get_Range("C1", "C100");
                excelcells.NumberFormat = "@";

                i = 7;
                foreach (daySale twd in Program.currentShop.MouthPrognoz)
                {

                    ObjWorkSheet.Cells[1, i] = twd.getWeekDay2();
                    if ((twd.getWeekDay2() == "Сб") || (twd.getWeekDay2() == "Вс"))
                    {
                        excelcells = ObjWorkSheet.get_Range(RetutnI(i) + "1", RetutnI(i) + "100");
                        excelcells.Font.ColorIndex = 3;
                    }
                    ObjWorkSheet.Cells[2, i] = twd.getData().Day;
                    i++;
                }
                // Microsoft.Office.Interop.Excel.Range excelcells3 = ObjWorkSheet.get_Range("A3", "AL50");
                //excelcells3.ColumnWidth = Program.currentShop.getAddress().Length;
                // ExcelWorkSheet.Range.Select("B2", "C6");

                bg.ReportProgress(16);

                excelcells = ObjWorkSheet.get_Range("D1", "D2");
                excelcells.WrapText = true;
                excelcells = ObjWorkSheet.get_Range("E1", "E2");
                excelcells.WrapText = true;
                excelcells = ObjWorkSheet.get_Range("F1", "F2");
                excelcells.WrapText = true;
                excelcells = ObjWorkSheet.get_Range("B1", "B100");
                excelcells.ColumnWidth = 9.29;
                excelcells = ObjWorkSheet.get_Range("C1", "C100");
                excelcells.ColumnWidth = 7.57;
                excelcells = ObjWorkSheet.get_Range("D1", "D100");
                excelcells.ColumnWidth = 14;
                excelcells = ObjWorkSheet.get_Range("E1", "E100");
                excelcells.ColumnWidth = 7.57;
                excelcells = ObjWorkSheet.get_Range("F1", "AL100");
                excelcells.ColumnWidth = 5.43;
                excelcells = ObjWorkSheet.get_Range("A1", "F2");
                excelcells.RowHeight = 15;
                ObjWorkSheet.Columns[1].ColumnWidth = Program.currentShop.getAddress().Length;



                ObjWorkSheet.Cells[2, 1] = "Адрес";
                excelcells = ObjWorkSheet.get_Range("A1", "A2");
                excelcells.Merge(Type.Missing);

                ObjWorkSheet.Cells[2, 2] = "Должность";
                excelcells = ObjWorkSheet.get_Range("B1", "B2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 3] = "График";
                // ObjWorkSheet.Cells[2, 2] = "Должность";
                excelcells = ObjWorkSheet.get_Range("C1", "C2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 4] = "Тип занятости";
                excelcells = ObjWorkSheet.get_Range("D1", "D2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 5] = "Общ.кол-во час.";
                excelcells = ObjWorkSheet.get_Range("E1", "E2");
                excelcells.Merge(Type.Missing);
                ObjWorkSheet.Cells[2, 6] = "Кол-во смен";
                excelcells = ObjWorkSheet.get_Range("F1", "F2");
                excelcells.Merge(Type.Missing);


                // MessageBox.Show(Program.currentShop.employes.Count+" count");
                 j = 3;

                foreach (employee emp in Program.currentShop.employes)
                {
                    switch (emp.getID() / 100)
                    {
                        case 0: color = System.Drawing.Color.LightSkyBlue; break;
                        case 1: color = System.Drawing.Color.LightGreen; break;
                        case 2: color = System.Drawing.Color.DarkSeaGreen; break;
                        case 3: color = System.Drawing.Color.PaleGoldenrod; break;

                        default: color = System.Drawing.Color.White; break;

                    }
                    try
                    {
                        ObjWorkSheet.Cells[j, 1] = Program.currentShop.getAddress();
                        ObjWorkSheet.Cells[j, 1].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 2] = emp.GetDolgnost();
                        ObjWorkSheet.Cells[j, 2].Interior.Color = color;


                        ObjWorkSheet.Cells[j, 3].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 3] = emp.getVS().getR() + "/" + emp.getVS().getV();
                        ObjWorkSheet.Cells[j, 4].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 4] = emp.getTipZan();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        continue;
                    }
                    i = 7;
                    foreach (daySale twd in Program.currentShop.MouthPrognoz)
                    {
                        //  MessageBox.Show(emp.getTip() + "");

                        ObjWorkSheet.Cells[j, i].Interior.Color = color;

                        if ((emp.smens.Find(t => t.getData().Date == twd.getData().Date) != null) && (emp.smens.Count != 0))
                        {
                            // MessageBox.Show(emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + " - " + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena());

                            ObjWorkSheet.Cells[j, i] =
                                 emp.smens.Find(t => t.getData().Date == twd.getData().Date).getTip();
                                
                        }
                        //else
                        // { ObjWorkSheet.Cells[emp.getID() + 4, i] = emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + "-" + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena(); }
                        i++;
                    }


                    /*if (Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()) != null)
                    {
                        ObjWorkSheet.Cells[j, 4] =
                            Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()).getZarp();
                    }*/
                    



                    j++;

                }

                excelcells = ObjWorkSheet.get_Range("A1", RetutnI(Program.currentShop.MouthPrognoz.Count + 6) + (Program.currentShop.employes.Count + 2).ToString());
                excelcells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                excelcells = ObjWorkSheet.get_Range("A1", "AL2");
                excelcells.Cells.Font.Bold = true;

                ObjWorkSheet.Visible= XlSheetVisibility.xlSheetHidden; ;
                //3 list end

                ObjExcel.Visible = false;
                ObjExcel.UserControl = true;
               // ObjExcel.DisplayAlerts = true;
                ObjWorkBook.Saved = true;

                ObjWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookDefault);
                Program.HandledShops.Add(Program.currentShop.getIdShop());
                // ObjWorkBook.SaveAs(filename);

                ObjWorkBook.Close();

                ObjExcel.Quit();
                //   MessageBox.Show("Расписание создано");

            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка на внешней утилите: EXCEL" + ex);

                // ObjWorkBook.Close(0);
                //ObjExcel.Quit();
            }
        }

        public static string RetutnI(int i) {
            switch (i) {
                case 1: return "A";
                case 2: return "B";
                case 3: return "C";
                case 4: return "D";
                case 5: return "E";
                case 6: return "F";
                case 7: return "G";
                case 8: return "H";
                case 9: return "I";
                case 10: return "J";
                case 11: return "K";
                case 12: return "L";
                case 13: return "M";
                case 14: return "N";
                case 15: return "O";
                case 16: return "P";
                case 17: return "Q";
                case 18: return "R";
                case 19: return "S";
                case 20: return "T";
                case 21: return "U";
                case 22: return "V";
                case 23: return "W";
                case 24: return "X";
                case 25: return "Y";
                case 26: return "Z";
                case 27: return "AA";
                case 28: return "AB";
                case 29: return "AC";
                case 30: return "AD";
                case 31: return "AE";
                case 32: return "AF";
                case 33: return "AG";
                case 34: return "AH";
                case 35: return "AI";
                case 36: return "AJ";
                case 37: return "AK";
                default:return "";


            }
        }

        public static void CreateEmployee()
        {
          
          
            
            string filepath = Program.file;
            Program.currentShop.Semployes = new List<employee>();
            if (File.Exists(filepath))
            {

                {
                    //Создаём приложение.
                    Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                    //Открываем книгу.                                                                                                                                                        
                    Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(filepath, 0, true);// 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                    //Выбираем таблицу(лист).
                    Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                    ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                    for (int i=3;i<100;i++) {
                        progress = i;
                        if (ObjWorkSheet.Cells[i, 2].Text!="") {
                            int ind = returnIndex(ObjWorkSheet.Cells[i, 2].Text);
                            if ((ind != -1)&& getOtdih(i, ObjWorkSheet, getDenM(i, ObjWorkSheet))<5) {
                                employee e = new employee(Program.currentShop.getIdShop(), ind, ObjWorkSheet.Cells[i, 2].Text, "Сменный график", getOtdih(i, ObjWorkSheet,getDenM(i, ObjWorkSheet)));
                                Program.currentShop.Semployes.Add(e);
                            }
                        }

                    }

                    ObjExcel.Visible = false;
                    ObjExcel.UserControl = true;
                   // ObjExcel.DisplayAlerts = false;
                    ObjWorkBook.Saved = true;

                  

                    ObjWorkBook.Close();
                    ObjExcel.Quit();
                }
                

               
            }
           
        }

        public static void CreateEmployeeWithVarSmen()
        {


            error = false;
            string filepath = Program.file;
            Program.currentShop.Semployes = new List<employee>();
            if (File.Exists(filepath))
            {

                {
                    //Создаём приложение.
                    Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                    //Открываем книгу.                                                                                                                                                        
                    Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(filepath, 0, true);// 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                    //Выбираем таблицу(лист).
                    Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                    ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                    for (int i = 3; i < 100; i++)
                    {
                        progress = i;
                        if (ObjWorkSheet.Cells[i, 2].Text != "")
                        {
                            int ind = returnIndex(ObjWorkSheet.Cells[i, 2].Text);
                            if ((ind != -1) && getOtdih(i, ObjWorkSheet, getDenM(i, ObjWorkSheet)) < 5)
                            {
                                string dolgnost = ObjWorkSheet.Cells[i, 2].Text;
                                string svs = ObjWorkSheet.Cells[i, 3].Text;
                                string[] msvs = new string[2];
                                msvs = svs.Split('/');
                                int rabdn = 0;
                                try
                                {
                                    rabdn = int.Parse(msvs[0]);
                                }
                                catch {
                                    MessageBox.Show("В файле существует сотрудник с несуществующим вариантом смен");
                                    error = true;
                                    return;
                                }
                                VarSmen vs = Program.currentShop.VarSmens.Find(t=> (t.getR()==rabdn)&&(t.getDolgnost()==dolgnost));
                                if ((vs != null)&&(vs.getDeistvie())) {


                                    employee e = new employee(Program.currentShop.getIdShop(), ind, vs, dolgnost , "Сменный график", getOtdih(i, ObjWorkSheet, getDenM(i, ObjWorkSheet)));
                                    Program.currentShop.Semployes.Add(e);
                                } else {
                                    MessageBox.Show("В файле существует сотрудник с невыбранным вариантом смен: "+vs.getDolgnost()+" "+vs.getR()+"/"+vs.getV());
                                    error = true;
                                    return ; 
                                }
                            }
                        }

                    }

                    ObjExcel.Visible = false;
                    ObjExcel.UserControl = true;
                   ObjExcel.DisplayAlerts = false;
                    ObjWorkBook.Saved = true;
                    ObjExcel.DisplayAlerts = true;


                    ObjWorkBook.Close();
                    ObjExcel.Quit();
                }



            }
           // return true;
        }

        public static void CreateEmployeeAndSmens()
        {
            error = false;
            string filepath = Program.file;
            Program.currentShop.Semployes = new List<employee>();
            if (File.Exists(filepath))
            {

                {
                    //Создаём приложение.
                    Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                    //Открываем книгу.                                                                                                                                                        
                    Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(filepath, 0, true);// 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                    //Выбираем таблицу(лист).
                    Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                    ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                    for (int i = 3; i < 100; i++)
                    {
                        progress = i;
                        if (ObjWorkSheet.Cells[i, 2].Text != "")
                        {
                            bool rvs = false;
                            string dolgnost = ObjWorkSheet.Cells[i, 2].Text;
                            string svs = ObjWorkSheet.Cells[i, 3].Text;
                            string[] msvs = new string[2];
                            msvs = svs.Split('/');
                            int rabdn = 0;
                            try
                            {
                                rabdn = int.Parse(msvs[0]);
                            }
                            catch
                            {
                                MessageBox.Show("В файле существует сотрудник с несуществующим вариантом смен");
                                error = true;
                                return;
                            }
                            VarSmen vs = Program.currentShop.VarSmens.Find(t => (t.getR() == rabdn) && (t.getDolgnost() == dolgnost));
                            if ((vs != null) && (vs.getDeistvie()))
                            {

                                rvs = true;
                                //employee e = new employee(Program.currentShop.getIdShop(), ind, vs, dolgnost, "Сменный график", getOtdih(i, ObjWorkSheet, getDenM(i, ObjWorkSheet)));
                                //Program.currentShop.Semployes.Add(e);
                            }
                            else
                            {
                                MessageBox.Show("В файле существует сотрудник с невыбранным вариантом смен: " + vs.getDolgnost() + " " + vs.getR() + "/" + vs.getV());
                                error = true;
                                return;
                            }



                            int ind = returnIndex(ObjWorkSheet.Cells[i, 2].Text);
                            if ((ind != -1)&& getOtdih(i, ObjWorkSheet, DateTime.Now.AddDays(1).Day + 5) <5)
                            {

                                int nd = DateTime.Now.AddDays(1).Day + 5;
                                employee e = new employee(Program.currentShop.getIdShop(), ind, ObjWorkSheet.Cells[i, 2].Text, "Сменный график", getOtdih(i, ObjWorkSheet, nd ));
                                if (rvs) {
                                    e.setVS(vs);
                                }
                                Program.currentShop.Semployes.Add(e);
                                for (int k=0-nd+7,j=7;k<=0;k++,j++) {
                                    try
                                    {
                                        string s = ObjWorkSheet.Cells[i, j].Text;
                                        string[] sm = new string[2];
                                        sm = s.Split('-');
                                        int l = int.Parse(sm[1]) - int.Parse(sm[0]);
                                        e.AddSmena(new Smena(Program.currentShop.getIdShop(),DateTime.Now.AddDays(k),int.Parse(sm[0]),l));
                                    }
                                    catch {
                                    }
                                }
                            }
                        }

                    }

                    ObjExcel.Visible = false;
                    ObjExcel.UserControl = true;
                    ObjExcel.DisplayAlerts = false;
                    ObjWorkBook.Saved = true;
                    ObjExcel.DisplayAlerts = true;


                    ObjWorkBook.Close();
                    ObjExcel.Quit();
                }


            }

        }

        public static int returnIndex(string d) {
           
            int i = -1;
            try
            {
               List<employee> le= Program.currentShop.Semployes.FindAll(t => t.GetDolgnost() == d);
                i = le.Max(t => t.getID())+1;
            }
            catch {
               
                    switch (d)
                    {
                        case "Кассир": i = 0; break;
                        case "Продавец": i = 100; break;
                        case "Грузчик": i = 200; break;
                        case "Гастроном": i = 300; break;
                        default: Program.currentShop.Semployes.Clear(); break;
                    }
                }
              
            
            return i;
        }

        public static int getDenM(int stroka, Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet) {
            int den = 25;
            for (int j = den; j < 50; j++)
            {
                if (ObjWorkSheet.Cells[1, j].Text == "")
                {
                    den = j - 1;
                    break;
                }
            }
            return den;
        }

        public static int getOtdih(int stroka, Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet, int den) {
            int o = 0;
          
           

            for (int i = 0; i < 7; i++, den--) {
                if ((ObjWorkSheet.Cells[stroka, den].Text!="")&&(o>0)) {
                    break;
                };
                if ((ObjWorkSheet.Cells[stroka, den].Text == "") && (o < 0))
                {
                    break;
                };

                if (ObjWorkSheet.Cells[stroka, den].Text != ""){
                    o--;
                }
                if (ObjWorkSheet.Cells[stroka, den].Text == "")
                {
                    o++;
                }
               
            }


            

            return o;
        }
    }
}
