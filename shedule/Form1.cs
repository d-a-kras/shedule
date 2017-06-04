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

namespace shedule
{
    public partial class Form1 : Form
    {
        static public int[] chartX;
        static public int[] chartY2;
        static public int[] chartY1;
        static public int Nday=0;

        public void ShowProizvCalendar() {
            foreach (DataForCalendary d in Program.DFCs)
            {
                if(DataForCalendary.isHolyday(d.getData()))
                    monthCalendar1.AddBoldedDate(d.getData());
                if (DataForCalendary.isPrHolyday(d.getData()))
                    monthCalendar1.AddAnnuallyBoldedDate(d.getData());
            }
            
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

        private SD.DataTable CreateTable()
        {
            //создаём таблицу
            string[] months = Program.getMonths();
            SD.DataTable dt = new SD.DataTable("norm");
            //создаём три колонки
            DataColumn Mounth = new DataColumn("M", typeof(string));
            
            DataColumn colCountDayInMonth = new DataColumn("CDIM", typeof(Int16));
            DataColumn colCountDayRab = new DataColumn("CDR", typeof(Int16));
            DataColumn colCountDayVuh = new DataColumn("CDV", typeof(Int16));
            DataColumn normCh = new DataColumn("NC", typeof(Int16));

            //добавляем колонки в таблицу
            dt.Columns.Add(Mounth);
            dt.Columns.Add(colCountDayInMonth);
            dt.Columns.Add(colCountDayRab);
            dt.Columns.Add(colCountDayVuh);
            dt.Columns.Add(normCh);
            DataRow row = null;
            //создаём новую строку
            
            //заполняем строку значениями
            
            for (int i = 1; i <= 12; i++) {
                row = dt.NewRow();
                row["M"] = months[i-1];
                row["CDIM"] = DateTime.DaysInMonth(DateTime.Today.Year, i);
                row["CDR"] = Program.RD[i - 1];
                row["CDV"] = DateTime.DaysInMonth(DateTime.Today.Year, i) - Program.RD[i - 1]; 
                row["NC"] = Program.RD[i - 1]*8-Program.PHD[i-1];
                dt.Rows.Add(row);
            }
            return dt;
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
          /* for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                workSheet.Cells[rowExcel, "A"] = dataGridView1.Rows[i].Cells["ID"].Value;
                workSheet.Cells[rowExcel, "B"] = dataGridView1.Rows[i].Cells["Name"].Value;
                workSheet.Cells[rowExcel, "C"] = dataGridView1.Rows[i].Cells["Age"].Value;
                ++rowExcel;
            }*/
            workSheet.SaveAs("MyFile.xls");
            exApp.Quit();
        }

        public Form1()
        {
            InitializeComponent();
            Program.Connect();
           
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
            tabControl1.TabPages[4].Hide();
            if (Program.listShops!=null) {
                foreach (Shop h in Program.listShops) {

                    listBox1.Items.Add(h.getAddress());
                } }
            textBoxSpeed.Text = 1000 + "";
            textBoxTimeTell.Text = 25 + "";
            textBoxTimeClick.Text = 4 + "";

            if (Program.isConnect()) { labelStatus1.Text = "Статус: Обработано " + Program.getStatus() + " магазинов из " + Program.listShops.Count; labelStatus2.Text="режим работы сетевой "  ; radioButtonIzBD.Checked = true; }
            else { labelStatus1.Text = "Статус: Обработано " + Program.getStatus() + " магазинов из " + Program.listShops.Count; labelStatus2.Text = " режим работы локальный"; radioButtonIzFile.Checked = true; }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Form2 formHelp = new Form2();
            formHelp.Show();
        }

        private void buttonFactors_Click(object sender, EventArgs e)
        {
            panelFactors.BringToFront();
        }

        private void buttonVariantsSmen_Click(object sender, EventArgs e)
        {
            panelDopusVarSmen.BringToFront();
            String readPath = Environment.CurrentDirectory + @"\Smens.txt"; ;
            Program.CountSmen = File.ReadAllLines(readPath).Length;
            switch (Program.CountSmen) {
                case 1: panelSmen1.Visible = true; panelSmen2.Visible = false; panelSmen3.Visible = false; panelSmen4.Visible = false; panelSmen5.Visible = false; break;
                case 2: panelSmen1.Visible = true; panelSmen2.Visible = true; panelSmen3.Visible = false; panelSmen4.Visible = false; panelSmen5.Visible = false; buttonDelSmen1.Visible = false; break;
                case 3: panelSmen1.Visible = true; panelSmen2.Visible = true; panelSmen3.Visible = true; panelSmen4.Visible = false; panelSmen5.Visible = false; buttonDelSmen1.Visible = false; buttonDelSmen2.Visible = false; break;
                case 4: panelSmen1.Visible = true; panelSmen2.Visible = true; panelSmen3.Visible = true; panelSmen4.Visible = true; panelSmen5.Visible = false; buttonDelSmen3.Visible = false; buttonDelSmen2.Visible = false; buttonDelSmen1.Visible = false; break;
                case 5: panelSmen1.Visible = true; panelSmen2.Visible = true; panelSmen3.Visible = true; panelSmen4.Visible = true; panelSmen5.Visible = true; buttonDelSmen2.Visible = false; buttonDelSmen3.Visible = false; buttonDelSmen4.Visible = false; buttonDelSmen1.Visible = false; break;
                default: MessageBox.Show("Ошибка чтения"); break;
            }
            using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
            {
                string line;
                int i = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    short R = Convert.ToInt16(line.Substring(0,1));
                    short V = Convert.ToInt16(line.Substring(1));
                    // MessageBox.Show(R+" "+V);
                    (this.Controls["tabControl1"].Controls["tabPage2"].Controls["panelUpravlenie"].Controls["panelDopusVarSmen"].Controls["panelSmen" + i].Controls["textBox" + i + "1"] as System.Windows.Forms.TextBox).Text = R.ToString();
                    (this.Controls["tabControl1"].Controls["tabPage2"].Controls["panelUpravlenie"].Controls["panelDopusVarSmen"].Controls["panelSmen" + i].Controls["textBox" + i + "2"] as System.Windows.Forms.TextBox).Text = V.ToString();
                    i++;

                }
            }
            }

        private void buttonParamOptimiz_Click(object sender, EventArgs e)
        {
            panelParamOptim.BringToFront();
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
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Program.ReadConfigShop(listBox1.SelectedIndex);
        }

        private void button_refresh_list_shops_Click(object sender, EventArgs e)
        {
            Program.setListShops();
            foreach (Shop h in Program.listShops)
            {

                listBox1.Items.Add(h.getAddress());
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
            if (Nday  < 25)
            {
                getChart();
                // labelData.Text = Program.getData();
                chart1.Series["s1"].Points.DataBindXY(chartX, chartY1);
                chart1.Series["s2"].Points.DataBindXY(chartX, chartY2);
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
            //Program.ReadOperFromExel(Environment.CurrentDirectory + @"\P.xls");
            Nday = 0;
            Program.Kass();
            Program.getSm();
            getChart();
            chart1.Series["s1"].Points.DataBindXY(chartX, chartY1);
            chart1.Series["s2"].Points.DataBindXY(chartX, chartY2);

        }

        private void buttonPrevios_Click(object sender, EventArgs e)
        {
            Nday--;
            if (Nday > -1)
            {
                getChart();
                // labelData.Text = Program.getData();
                chart1.Series["s1"].Points.DataBindXY(chartX, chartY1);
                chart1.Series["s2"].Points.DataBindXY(chartX, chartY2);
            }
            else { Nday++; MessageBox.Show("Больше данных нет"); }
        }

        private void buttonReadCalendarFromXML_Click(object sender, EventArgs e)
        {
            Program.ReadCalendarFronXML();
        }

        private void buttonCalendar_Click(object sender, EventArgs e)
        {
            panelCalendar.BringToFront();
            Program.getListDate(DateTime.Today.Year);
            ShowProizvCalendar();
            dataGridViewCalendar.DataSource = CreateTable();

        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            DateTime f23 = new DateTime(2017, 5, 9);
            DataForCalendary f = new DataForCalendary(f23);
            MessageBox.Show(DataForCalendary.isHolyday(f23).ToString());

            MessageBox.Show(f.getTip().ToString());
            Program.getListDate(2017);
            for (int i = 1; i < 30; i++)
            {
                 f23 = new DateTime(2017, 5, i);
                f = new DataForCalendary(f23);

                MessageBox.Show(f.getTip().ToString());
                // MessageBox.Show( DataForCalendary.isHolyday(f23).ToString());
            }
        }

        private void buttonKassov_Click(object sender, EventArgs e)
        {
            panelKassOper.BringToFront();
            
        }

        
        
    }
}
