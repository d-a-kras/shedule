using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SD = System.Data;
using Excel = Microsoft.Office.Interop.Excel;

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
            dataGridView1.DataSource= CreateTable();
            /*dataGridView1.Rows.Add(2);
            dataGridView1.Rows[0].Cells[1].Style.BackColor = System.Drawing.Color.Red;
            dataGridView1.Rows[1].Cells[1].Value = "1";*/
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
            
            Graphics g = pictureBox1.CreateGraphics();
            Pen myPen = new Pen(Color.Blue);
            int count = 10;
            int x = 1;
            float[] y ={ 304, 384, 444, 351, 502, 499, 325, 364, 380, 561, 560};
            float  xmax = count, ymax = 2500;
  
            float kx = pictureBox1.Width / xmax, ky = pictureBox1.Height / ymax;
           
            for (int i = 0; i < count; i++)
            {

                System.Drawing.Point point1 = new System.Drawing.Point(Convert.ToInt32(x * kx), Convert.ToInt32(y[i]*ky));
                x++;
                System.Drawing.Point point2 = new System.Drawing.Point(Convert.ToInt32(kx * x), Convert.ToInt32(y[i+1]*ky));
                g.DrawLine(myPen, point1, point2);
                
               
            }
        }
    }
}
