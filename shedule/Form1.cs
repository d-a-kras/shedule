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
        private SD.DataTable CreateTable()
        {
            //создаём таблицу
            SD.DataTable dt = new SD.DataTable("Friends");
            //создаём три колонки
            DataColumn colID = new DataColumn("ID", typeof(Int32));
            DataColumn colName = new DataColumn("Name", typeof(String));
            DataColumn colAge = new DataColumn("Age", typeof(Int32));
            //добавляем колонки в таблицу
            dt.Columns.Add(colID);
            dt.Columns.Add(colName);
            dt.Columns.Add(colAge);
            DataRow row = null;
            //создаём новую строку
            row = dt.NewRow();
            //заполняем строку значениями
            row["ID"] = 1;
            row["Name"] = "Vanya";
            row["Age"] = 45;
            //добавляем строку в таблицу
            dt.Rows.Add(row);
            //создаём ещё одну запись в таблице
            row = dt.NewRow();
            row["ID"] = 2;
            row["Name"] = "Vasya";
            row["Age"] = 35;
            dt.Rows.Add(row);
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
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                workSheet.Cells[rowExcel, "A"] = dataGridView1.Rows[i].Cells["ID"].Value;
                workSheet.Cells[rowExcel, "B"] = dataGridView1.Rows[i].Cells["Name"].Value;
                workSheet.Cells[rowExcel, "C"] = dataGridView1.Rows[i].Cells["Age"].Value;
                ++rowExcel;
            }
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
            if (Program.isConnect()) { labelStatus1.Text = "Статус: режим работы сетевой "; radioButtonIzBD.Checked = true; }
            else { labelStatus1.Text = "Статус: режим работы локальный";radioButtonIzFile.Checked = true; }
            Program.ReadListShops();
            tabControl1.TabPages[4].Hide();
            foreach (Shop h in Program.listShops) {
                
                listBox1.Items.Add(h.getAddress());
            }
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
    }
}
