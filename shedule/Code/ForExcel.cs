using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;
using LinqToExcel;
using System.Threading;
using System.ComponentModel;

namespace shedule.Code
{
    class ForExcel
    {
        public static Thread thread1;
        public static int progress = 0;

        public static void  ExportExcel(string filename, BackgroundWorker bg) {

            System.Drawing.Color color;
            Microsoft.Office.Interop.Excel.Range excelcells;

            try
            {
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

                ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
                ObjWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[2];
                ObjWorkSheet.Name = "Часы";
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                bg.ReportProgress(10);
                ObjWorkSheet.Name = "График";

                excelcells = ObjWorkSheet.get_Range("F3", "AK50");
                excelcells.Font.Size = 10;
                excelcells.NumberFormat = "@";
               // bg.ReportProgress(10);
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                int i = 6;
                foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                {

                    ObjWorkSheet.Cells[1, i] = twd.GetWeekDay();
                    ObjWorkSheet.Cells[2, i] = twd.getData().Day;
                    i++;
                }
                //Microsoft.Office.Interop.Excel.Range excelcells2 = ObjWorkSheet.get_Range("A3", "AK50");
                //  excelcells2.ColumnWidth = Program.currentShop.getAddress().Length;
                //  bg.ReportProgress(12);

                bg.ReportProgress(12);
                ObjWorkSheet.Cells[2, 1] = "Адрес";

                ObjWorkSheet.Cells[2, 2] = "Должность";
                ObjWorkSheet.Cells[2, 3] = "Тип занятости";
                
                ObjWorkSheet.Cells[2, 4] = "Общее число часов";
                ObjWorkSheet.Cells[2, 5] = "Количество смен";

                // MessageBox.Show(Program.currentShop.employes.Count+" count");
                int j = 3;
                foreach (employee emp in Program.currentShop.employes)
                {

                    i = 6;
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
                        /*if (Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()) != null)
                        {
                            ObjWorkSheet.Cells[j, 4] =
                                Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()).getZarp();
                        }*/
                        excelcells = ObjWorkSheet.get_Range("D" + j, "D" + j);
                        excelcells.FormulaLocal = $"=СУММ(Часы!F{j}:Часы!AK{j}";

                        ObjWorkSheet.Cells[j, 4].Interior.Color = System.Drawing.Color.LightSkyBlue; ;
                        ObjWorkSheet.Cells[j, 5] = emp.smens.Count;
                        ObjWorkSheet.Cells[j, 5].Interior.Color = color;

                    }

                    j++;

                }
                bg.ReportProgress(14);


                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[2];

                //ObjWorkSheet.Name = "Часы";

                excelcells = ObjWorkSheet.get_Range("F3", "AL50");
               // excelcells.Font.Size = 10;
              excelcells.NumberFormat = "General";
                //excelcells.NumberFormat = "@";
                // excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                //  excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                i = 6;
                foreach (TemplateWorkingDay twd in Program.currentShop.MouthPrognozT)
                {

                    ObjWorkSheet.Cells[1, i] = twd.GetWeekDay();
                    ObjWorkSheet.Cells[2, i] = twd.getData().Day;
                    i++;
                }
               // Microsoft.Office.Interop.Excel.Range excelcells3 = ObjWorkSheet.get_Range("A3", "AL50");
                //excelcells3.ColumnWidth = Program.currentShop.getAddress().Length;

                bg.ReportProgress(16);


                ObjWorkSheet.Cells[2, 1] = "Адрес";

                ObjWorkSheet.Cells[2, 2] = "Должность";
                ObjWorkSheet.Cells[2, 3] = "Тип занятости";
               
                ObjWorkSheet.Cells[2, 4] = "Общее число часов";
                ObjWorkSheet.Cells[2, 5] = "Количество смен";

                // MessageBox.Show(Program.currentShop.employes.Count+" count");
                j = 3;
                foreach (employee emp in Program.currentShop.employes)
                {

                    i = 6;
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
                            String c = "График!" + RetutnI(i) + j;

                            excelcells = ObjWorkSheet.get_Range(RetutnI(i) + j, RetutnI(i) + j);
                            String f = $"=Если({ c  }=\"\";\"\";ПСТР({ c };Поиск(\"-\";{ c })+1;ДЛСТР({ c })-ПОИСК(\"-\";{ c }))-ПСТР({ c };1;ПОИСК(\"-\";{ c })-1)-1)";
                            excelcells.FormulaLocal = f;

                        }
                        //else
                        // { ObjWorkSheet.Cells[emp.getID() + 4, i] = emp.smens.Find(t => t.getData() == twd.getData()).getStartSmena() + "-" + emp.smens.Find(t => t.getData() == twd.getData()).getEndSmena(); }
                        i++;

                        ObjWorkSheet.Cells[j, 1] = Program.currentShop.getAddress();
                        ObjWorkSheet.Cells[j, 1].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 2] = emp.GetDolgnost();
                        ObjWorkSheet.Cells[j, 2].Interior.Color = color;
                        ObjWorkSheet.Cells[j, 3] = emp.getTipZan();
                        /*
                        if (Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()) != null)
                        {
                            ObjWorkSheet.Cells[j, 4] =
                                Program.currentShop.tsr.Find(t => t.getOtobragenie() == emp.GetDolgnost()).getZarp();
                        }*/
                        excelcells = ObjWorkSheet.get_Range("D" + j, "D" + j);
                        excelcells.FormulaLocal = $"=СУММ(F{j}:AK{j}";
                      
                        ObjWorkSheet.Cells[j, 4].Interior.Color = System.Drawing.Color.LightSkyBlue; ;
                        ObjWorkSheet.Cells[j, 5] = emp.smens.Count;
                        ObjWorkSheet.Cells[j, 5].Interior.Color = color;


                    }

                    j++;

                }


                ObjExcel.Visible = false;
                ObjExcel.UserControl = true;
                ObjExcel.DisplayAlerts = false;
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
              //  MessageBox.Show("Ошибка на внешней утилите: EXCEL" + ex);

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
                            if (ind != -1) {
                                employee e = new employee(Program.currentShop.getIdShop(), ind, ObjWorkSheet.Cells[i, 2].Text, "Сменный график", getOtdih(i, ObjWorkSheet,getDenM(i, ObjWorkSheet)));
                                Program.currentShop.Semployes.Add(e);
                            }
                        }

                    }

                    ObjExcel.Visible = false;
                    ObjExcel.UserControl = true;
                    ObjExcel.DisplayAlerts = false;
                    ObjWorkBook.Saved = true;

                  

                    ObjWorkBook.Close();
                    ObjExcel.Quit();
                }
                

               
            }
           
        }

        public static void CreateEmployeeAndSmens()
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

                    for (int i = 3; i < 100; i++)
                    {
                        progress = i;
                        if (ObjWorkSheet.Cells[i, 2].Text != "")
                        {
                            int ind = returnIndex(ObjWorkSheet.Cells[i, 2].Text);
                            if (ind != -1)
                            {
                                int nd = DateTime.Now.AddDays(1).Day + 4;
                                employee e = new employee(Program.currentShop.getIdShop(), ind, ObjWorkSheet.Cells[i, 2].Text, "Сменный график", getOtdih(i, ObjWorkSheet, nd ));
                                Program.currentShop.Semployes.Add(e);
                                for (int k=0-nd+6,j=6;k<=0;k++,j++) {
                                    try
                                    {
                                        string s = ObjWorkSheet.Cells[i, j].Text;
                                        string[] sm = new string[2];
                                        sm = s.Split('-');
                                        int l = int.Parse(sm[1]) - int.Parse(sm[0])-1;
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
