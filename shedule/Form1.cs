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

namespace shedule
{
    public partial class Form1 : Form
    {
        static public int[] chartX;
        static public int[] chartY2;
        static public int[] chartY1;
        static public int Nday=0;

        public void ShowProizvCalendar() {
            foreach (DataForCalendary d in  Program.currentShop.DFCs)
            {
                if(DataForCalendary.isHolyday(d.getData()))
                    monthCalendar1.AddBoldedDate(d.getData());
                if (DataForCalendary.isPrHolyday(d.getData()))
                    monthCalendar1.AddAnnuallyBoldedDate(d.getData());
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

            for (int i = 0; i <4; i++)
            {
                row = dt.NewRow();
                row["Должность"] = Program.currentShop.tsr[i].position;
                row["Количество"] = Program.currentShop.tsr[i].count;
                row["Зарплата"] = Program.currentShop.tsr[i].zarp;
                row["Зарплата за 1/2"] = Program.currentShop.tsr[i].zarp1_2;
                dt.Rows.Add(row);
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
            DataRow row = null;
            //создаём новую строку

            //заполняем строку значениями

            foreach (Factor f in Program.currentShop.factors)
            {
                row = dt.NewRow();
                row["Название"] = f.name;
                row["Текущее значение"] = f.TZnach;
                row["Действует на текущую дату"] = f.Deistvie;
                row["Действует до даты"] = f.DDD;
                row["Новое значение"] = f.NewZnach;
                dt.Rows.Add(row);
            }
            return dt;
        }


        public void getChart()
        {
            
            DateTime d = new DateTime();
            List<hourSale> Hss = new List<hourSale>();
           // MessageBox.Show(Program.HSS[1].getData().Date.ToString());
            // Hss=HSS.FindAll(p => p.getData().Date == );

            int[] TimeObr = new int[15];
            int[] TimeSotr = new int[15];

           
            labelData.Text= (Nday+1) + " марта";
            
            chartX = new int[15];
            chartY2 = new int[15];
            chartY1 = new int[15];

            for (int i = 8, n = 0; i < 23; n++, i++)
            {
                if ((textBoxSpeed.Text == "") || (textBoxTimeClick.Text == "") || (textBoxTimeTell.Text == "")) { MessageBox.Show("Не все данные введены"); break; }
                else
                {
                    TimeObr[n] = (Program.CountCheck[Nday, n] * Int32.Parse(textBoxTimeTell.Text) + Program.CountClick[Nday, n] * Int32.Parse(textBoxTimeClick.Text))/60;
                    TimeSotr[n] = Program.CountS[Nday, n] * Int32.Parse(textBoxSpeed.Text)/60;
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
        //    Program.Connect();
           
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

           float kx =  xmax, ky =  ymax;

            for (int i = 0; i < count; i++)
            {

                System.Drawing.Point point1 = new System.Drawing.Point(Convert.ToInt32(x * kx), Convert.ToInt32(y[i] * ky));
                x++;
                System.Drawing.Point point2 = new System.Drawing.Point(Convert.ToInt32(kx * x), Convert.ToInt32(y[i + 1] * ky));
             //   g.DrawLine(myPen, point1, point2);


            }
        }

        private void button3_Click(object sender, EventArgs e)
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



            chartRange = xlWorkSheet.get_Range("A3", "d5");

            chartPage.SetSourceData(chartRange, misValue);

            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;



            xlWorkBook.SaveAs("csharp.net-informations.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            xlWorkBook.Close(true, misValue, misValue);

            xlApp.Quit();



            releaseObject(xlWorkSheet);

            releaseObject(xlWorkBook);

            releaseObject(xlApp);



            MessageBox.Show("Excel file created , you can find the file c:\\csharp.net-informations.xls");
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
                var connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=VShleyev;Password=gjkrjdybr@93";
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
                            h = new hourSale(reader.GetInt16(0), reader.GetDateTime(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                            hSs.Add(h);

                        }
                       }
                     catch(System.Data.SqlClient.SqlException ex) {
                        MessageBox.Show("Ошибка соединения с базой данных"+ ex);
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
                        hss[i, j] = hSs.FindAll(t => ((t.getWeekday() == Program.collectionweekday[i]) && (t.getNHour() == Program.collectionHours[j])));
                        if (hss[i, j].Count != 0)
                        {
                            foreach (hourSale hh in hss[i, j])
                            {
                                if (hh.getMinut()< min)
                                    min = hh.getMinut();
                                if (hh.getMinut() > max)
                                    max = hh.getMinut();
                                sr += hh.getMinut();
                               // sw.WriteLine(hh.getWeekday()+" "+hh.getNHour()+" "+hh.getCountClick());
                            }
                            sr = sr / (hss[i, j].Count );
                            sw.WriteLine(Program.collectionweekday[i] + " Время=" + Program.collectionHours[j+1] +" Количество данных="+hss[i,j].Count + " среднее=" + sr + " минимум=" + min + " максимум=" + max);
                        }

                    }


                }
                MessageBox.Show("Запись завершена");
            }
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Program.ReadListShops();
            // Program.setListShops();
            tabControl1.Visible = false;
            buttonTest.Visible = false;
           
            if (Program.listShops != null) {
                foreach (Shop h in Program.listShops) {
                  
                    listBox1.Items.Add(h.getIdShop() +"_"+ h.getAddress());
                } }
           textBoxSpeed.Text = 20 + "";
            textBoxTimeTell.Text = 25 + "";
            textBoxTimeClick.Text = 4 + "";
          

            //if (/*Program.isConnect()*/true) {   }
            labelStatus1.Text = "Статус: Обработано " + Program.getStatus() + " магазинов из " + Program.listShops.Count; labelStatus2.Text = " режим работы локальный"; radioButtonIzFile.Checked = true; 
        }

       

        private void buttonFactors_Click(object sender, EventArgs e)
        {
            buttonFactors.BackColor = Color.MistyRose;
            buttonVariantsSmen.BackColor = Color.White;
            buttonParamOptimiz.BackColor = Color.White;
            panelFactors.BringToFront();
            dataGridViewFactors.DataSource = viewFactors(); 
        }

        private void buttonVariantsSmen_Click(object sender, EventArgs e)
        {
            buttonVariantsSmen.BackColor = Color.MistyRose;
            buttonFactors.BackColor = Color.White;
            buttonParamOptimiz.BackColor = Color.White;
            panelDopusVarSmen.BringToFront();
            String readPath = Environment.CurrentDirectory + "/Shops/"+Program.currentShop.getIdShop()+"/Smens.txt";
            try
            {
                Program.CountSmen = File.ReadAllLines(readPath).Length;
            }
            catch (Exception ex) {
                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    sw.WriteLine("");
                    Program.CountSmen = 0;
                }
            }
            switch (Program.CountSmen) {
                case 0: panelSmen1.Visible = true; panelSmen2.Visible = false; panelSmen3.Visible = false; panelSmen4.Visible = false; panelSmen5.Visible = false; break;
                case 1: panelSmen1.Visible = true; panelSmen2.Visible = false; panelSmen3.Visible = false; panelSmen4.Visible = false; panelSmen5.Visible = false; break;
                case 2: panelSmen1.Visible = true; panelSmen2.Visible = true; panelSmen3.Visible = false; panelSmen4.Visible = false; panelSmen5.Visible = false; buttonDelSmen1.Visible = false; break;
                case 3: panelSmen1.Visible = true; panelSmen2.Visible = true; panelSmen3.Visible = true; panelSmen4.Visible = false; panelSmen5.Visible = false; buttonDelSmen1.Visible = false; buttonDelSmen2.Visible = false; break;
                case 4: panelSmen1.Visible = true; panelSmen2.Visible = true; panelSmen3.Visible = true; panelSmen4.Visible = true; panelSmen5.Visible = false; buttonDelSmen3.Visible = false; buttonDelSmen2.Visible = false; buttonDelSmen1.Visible = false; break;
                case 5: panelSmen1.Visible = true; panelSmen2.Visible = true; panelSmen3.Visible = true; panelSmen4.Visible = true; panelSmen5.Visible = true; buttonDelSmen2.Visible = false; buttonDelSmen3.Visible = false; buttonDelSmen4.Visible = false; buttonDelSmen1.Visible = false; break;
                default: MessageBox.Show("Ошибка чтения"); break;
            }
            using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
            {
                try
                {
                    string line;
                    int i = 1;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (Program.CountSmen==1 && line=="") {
                            (this.Controls["panelSingleShop"].Controls["tabControl1"].Controls["tabPage2"].Controls["panelUpravlenie"].Controls["panelDopusVarSmen"].Controls["panelSmen" + i].Controls["textBox" + i + "1"] as System.Windows.Forms.TextBox).Text = "";
                            (this.Controls["panelSingleShop"].Controls["tabControl1"].Controls["tabPage2"].Controls["panelUpravlenie"].Controls["panelDopusVarSmen"].Controls["panelSmen" + i].Controls["textBox" + i + "2"] as System.Windows.Forms.TextBox).Text = "";
                            break;
                        }
                        short R = Convert.ToInt16(line.Substring(0, 1));
                        short V = Convert.ToInt16(line.Substring(1));
                        // MessageBox.Show(R+" "+V);
                        (this.Controls["panelSingleShop"].Controls["tabControl1"].Controls["tabPage2"].Controls["panelUpravlenie"].Controls["panelDopusVarSmen"].Controls["panelSmen" + i].Controls["textBox" + i + "1"] as System.Windows.Forms.TextBox).Text = R.ToString();
                        (this.Controls["panelSingleShop"].Controls["tabControl1"].Controls["tabPage2"].Controls["panelUpravlenie"].Controls["panelDopusVarSmen"].Controls["panelSmen" + i].Controls["textBox" + i + "2"] as System.Windows.Forms.TextBox).Text = V.ToString();
                        i++;

                    }
                }
                catch (Exception ex) {
                    using (StreamWriter sw = new StreamWriter(readPath,false, Encoding.Default)) {
                        sw.WriteLine("");
                    }
                }
            }
            }

        private void buttonParamOptimiz_Click(object sender, EventArgs e)
        {
            buttonParamOptimiz.BackColor = Color.MistyRose;
            buttonFactors.BackColor = Color.White;
            buttonVariantsSmen.BackColor = Color.White;
            panelParamOptim.BringToFront();
            String readPath = Environment.CurrentDirectory + "/Shops/" + Program.currentShop.getIdShop() + "/parametrOptimization.txt"; ;
            try
            {
                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {

                    Program.ParametrOptimization = short.Parse(sr.ReadLine());
                }
            }
                catch (Exception ex) {
                    using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                    {
                        sw.WriteLine("0");
                        Program.ParametrOptimization = 0;
                    }
                }
            
            switch(Program.ParametrOptimization){
                case 0: break;
                case 1: radioButtonMinFondOpl.Select();break;
                case 2: radioButtonMinTime.Select();break;
                case 3: radioButtonObRabTime.Select();break;
                default: Program.ParametrOptimization = 0;break;
            }

        }


        private void buttonRedactir1_Click(object sender, EventArgs e)
        {
            textBox11.ReadOnly = false;
            textBox12.ReadOnly = false;
        }

        private void buttonRedact2_Click(object sender, EventArgs e)
        {
            textBox21.ReadOnly = false;
            textBox22.ReadOnly = false;
        }

        private void buttonRedact3_Click(object sender, EventArgs e)
        {
            textBox31.ReadOnly = false;
            textBox32.ReadOnly = false;
        }

        private void buttonRedact4_Click(object sender, EventArgs e)
        {
            textBox41.ReadOnly = false;
            textBox42.ReadOnly = false;
        }

        private void buttonRedact5_Click(object sender, EventArgs e)
        {
            textBox51.ReadOnly = false;
            textBox52.ReadOnly = false;
        }

        private void buttonDelSmen1_Click(object sender, EventArgs e)
        {
            textBox11.Text = "";
            textBox12.Text = "";
        }

        private void buttonDelSmen2_Click(object sender, EventArgs e)
        {
            panelSmen2.Visible = false;
            buttonDelSmen1.Visible = true;
            Program.CountSmen = 1;
        }

        private void buttonDelSmen3_Click(object sender, EventArgs e)
        {
            panelSmen3.Visible = false;
            buttonDelSmen2.Visible = true;
            Program.CountSmen = 2;
        }

        private void buttonDelSmen4_Click(object sender, EventArgs e)
        {
            panelSmen4.Visible = false;
            buttonDelSmen3.Visible = true;
            Program.CountSmen = 3;
        }

        private void buttonDelSmen5_Click(object sender, EventArgs e)
        {
            panelSmen5.Visible = false;
            buttonDelSmen4.Visible = true;
            buttonAddVarSmen.Visible = true;
            Program.CountSmen = 4;
        }

        private void buttonAddVarSmen_Click(object sender, EventArgs e)
        {
            
            switch (Program.CountSmen) {
                case 1:panelSmen2.Visible = true; buttonDelSmen1.Visible = false; textBox21.Text = ""; textBox22.Text = ""; break;
                case 2: panelSmen3.Visible = true; buttonDelSmen2.Visible = false; textBox31.Text = ""; textBox32.Text = ""; break;
                case 3: panelSmen4.Visible = true; buttonDelSmen3.Visible = false; textBox41.Text = ""; textBox42.Text = ""; break;
                case 4: panelSmen5.Visible = true; buttonDelSmen4.Visible = false; textBox51.Text = ""; textBox52.Text = ""; buttonAddVarSmen.Visible = false; break;

            }
            Program.CountSmen++;
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number==(char)Keys.Back)) { e.Handled = true; }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back)) { e.Handled = true; }
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back)) { e.Handled = true; }
        }

        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back)) { e.Handled = true; }
        }

        private void textBox31_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back)) { e.Handled = true; }
        }

        private void textBox32_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back)) { e.Handled = true; }
        }

        private void textBox41_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back)) { e.Handled = true; }
        }

        private void textBox42_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back)) { e.Handled = true; }
        }

        private void textBox51_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back)) { e.Handled = true; }
        }

        private void textBox52_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == (char)Keys.Back)) { e.Handled = true; }
        }

        private void buttonImportKasOper_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "*|*.xls";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string filepyth = openFileDialog.FileName;
            }

        }

        private void radioButtonIzFile_CheckedChanged(object sender, EventArgs e)
        {
            buttonImportKasOper.Visible = true;
        }

        private void radioButtonIzBD_CheckedChanged(object sender, EventArgs e)
        {
            buttonImportKasOper.Visible = false;
            Form3 f3 = new Form3();
            f3.Show();
            this.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            // Program.ReadConfigShop();
            //MessageBox.Show(listBox1.Text);
            string[] s = new string[2];
            s=listBox1.Text.Split('_');
            Program.currentShop = new Shop(Int16.Parse(s[0]),s[1]);
            Program.getListDate(DateTime.Today.Year);
            tabControl1.Visible = true;
        }



        private void button_refresh_list_shops_Click(object sender, EventArgs e)
        {
            Program.setListShops();
            Program.refreshFoldersShops();
            foreach (Shop h in Program.listShops)
            {

                listBox1.Items.Add(h.getIdShop()+"_"+h.getAddress());
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
            if (Nday  < Program.currentShop.templates.Count)
            {
               /* getChart();
                // 
                chart1.Series["s1"].Points.DataBindXY(chartX, chartY1);
                chart1.Series["s2"].Points.DataBindXY(chartX, chartY2);
                */
                labelData.Text = Program.currentShop.templates[Nday].getData().ToString();
                Program.currentShop.templates[Nday].createChartTemplate();
                Program.currentShop.templates[Nday].DS.CreateChartDaySale();

                chart1.Series["s2"].Points.DataBindXY(Program.currentShop.templates[Nday].Chart.X, Program.currentShop.templates[Nday].Chart.Y);
                chart1.Series["s1"].Points.DataBindXY(Program.currentShop.templates[Nday].DS.Chart.X, Program.currentShop.templates[Nday].DS.Chart.Y);
            }
            else { Nday--; MessageBox.Show("Больше данных нет"); }
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
            Program.speed = int.Parse(textBoxSpeed.Text);
            Nday = 0;
           
            DateTime d1 = new DateTime(2017, 5, 1);
            DateTime d2 = new DateTime(2017, 5, 20);
            Program.createListDaySale(d1, d2);

            foreach (daySale ds in Program.currentShop.daysSale)
            {
                Program.createTemplate(ds);

            }

            Program.currentShop.templates[Nday].createChartTemplate();
           
            Program.currentShop.templates[Nday].DS.CreateChartDaySale();

            chart1.Series["s2"].Points.DataBindXY(Program.currentShop.templates[Nday].Chart.X, Program.currentShop.templates[Nday].Chart.Y);
           chart1.Series["s1"].Points.DataBindXY(Program.currentShop.templates[Nday].DS.Chart.X, Program.currentShop.templates[Nday].DS.Chart.Y);
            /*Program.Kass();
            Program.getSm();
            getChart();
            chart1.Series["s1"].Points.DataBindXY(chartX, chartY1);
            chart1.Series["s2"].Points.DataBindXY(chartX, chartY2);*/

        }

        private void buttonPrevios_Click(object sender, EventArgs e)
        {
            Nday--;
            if (Nday > -1)
            {
               /* getChart();
                // labelData.Text = Program.getData();
                chart1.Series["s1"].Points.DataBindXY(chartX, chartY1);
                chart1.Series["s2"].Points.DataBindXY(chartX, chartY2);*/

                labelData.Text = Program.currentShop.templates[Nday].getData().ToString();
                Program.currentShop.templates[Nday].createChartTemplate();
                Program.currentShop.templates[Nday].DS.CreateChartDaySale();

                chart1.Series["s2"].Points.DataBindXY(Program.currentShop.templates[Nday].Chart.X, Program.currentShop.templates[Nday].Chart.Y);
                chart1.Series["s1"].Points.DataBindXY(Program.currentShop.templates[Nday].DS.Chart.X, Program.currentShop.templates[Nday].DS.Chart.Y);
            }
            else { Nday++; MessageBox.Show("Больше данных нет"); }
        }

        private void buttonReadCalendarFromXML_Click(object sender, EventArgs e)
        {
            Program.ReadCalendarFromXML();
        }

        private void buttonCalendar_Click(object sender, EventArgs e)
        {
            buttonCalendar.BackColor = Color.MistyRose;
            buttonRaspisanie.BackColor = Color.White;
            buttonKassov.BackColor = Color.White;
            panelCalendar.BringToFront();

            
            buttonImportKasOper.Visible = false;
           // ShowProizvCalendar();
            Form4 f4 = new Form4();
            f4.Show();
        }

      


        private void buttonTest_Click(object sender, EventArgs e)
        {
            //Program.isConnected("VShleyev", "gjkrjdyb93");

            Form5 f5 = new Form5();
            f5.Show();
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
            buttonRaspisanie.BackColor = Color.MistyRose;
            buttonCalendar.BackColor = Color.White;
            buttonKassov.BackColor = Color.White;
            panelTRasp.BringToFront();
            dataGridViewForTSR.DataSource = viewTSR();
        }

        public void writeTSR()
        {
            String writePath = Environment.CurrentDirectory + @"\TSR.txt";
            try
            {


                using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                {

                    for (int i = 0; i < 4; i++)
                    {
                        sw.WriteLine(dataGridViewForTSR.Rows[i].Cells["Должность"].Value.ToString());
                        sw.WriteLine(dataGridViewForTSR.Rows[i].Cells["Количество"].Value);
                        sw.WriteLine(dataGridViewForTSR.Rows[i].Cells["Зарплата"].Value);
                        sw.WriteLine(dataGridViewForTSR.Rows[i].Cells["Зарплата за 1/2"].Value);
                    }
                    
                    
                }
                MessageBox.Show("Данные сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

         public void writeFactors()
        {
            String writePath = Environment.CurrentDirectory + @"\factors.txt";
            try
            {


                using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                {

                    for (int i=0; i< Program.currentShop.factors.Count;i++)
                    {
                        sw.WriteLine(dataGridViewFactors.Rows[i].Cells["Должность"].Value);
                        sw.WriteLine(dataGridViewFactors.Rows[i].Cells["Количество"].Value);
                        sw.WriteLine(dataGridViewFactors.Rows[i].Cells["Зарплата"].Value);
                        sw.WriteLine(dataGridViewFactors.Rows[i].Cells["Зарплата за 1/2"].Value);
                    }


                }
                MessageBox.Show("Данные сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        private void Form1_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Form2 formHelp = new Form2();
            formHelp.Show();
        
         }

        private void buttonPTSR_Click(object sender, EventArgs e)
        {
            writeTSR();
        }

        private void buttonAplyFactors_Click(object sender, EventArgs e)
        {
            writeFactors();
        }

        private void buttonAplyVarSmen_Click(object sender, EventArgs e)
        {

        }

        private void buttonMultShops_Click(object sender, EventArgs e)
        {
            panelMultShops.BringToFront();
          
            listBoxMShops.Items.AddRange( listBox1.Items);
        }

        private void buttonSingleShop_Click(object sender, EventArgs e)
        {
            panelSingleShop.BringToFront();
        }

        private void buttonMadd_Click(object sender, EventArgs e)
        {
            string ss = listBoxMShops.Text;
            foreach (string s in listBoxMPartShops.Items) {
                if (listBoxMShops.Text == s) {
                    ss = "";
                }
            }

            
                if (ss!="") {
                    listBoxMPartShops.Items.Add(ss);
                }
            
        }

        private void buttonMdel_Click(object sender, EventArgs e)
        {
            if (listBoxMPartShops.Text != "")
            {
                listBoxMPartShops.Items.Remove(listBoxMPartShops.SelectedItem);
            }
        }

        private void radioButtonMinFondOpl_CheckedChanged(object sender, EventArgs e)
        {
            Program.ParametrOptimization = 1;
        }

        private void radioButtonMinTime_CheckedChanged(object sender, EventArgs e)
        {
            Program.ParametrOptimization = 2;
        }

        private void radioButtonObRabTime_CheckedChanged(object sender, EventArgs e)
        {
            Program.ParametrOptimization = 3;
        }

        private void buttonApplyParamsOptim_Click(object sender, EventArgs e)
        {
            String writePath = Environment.CurrentDirectory + @"\parametrzoptimization.txt";
            using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
            {

                try
                {
                    sw.Write(Program.ParametrOptimization.ToString());
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
        }

        
    }
}
