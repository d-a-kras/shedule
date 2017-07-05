﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shedule
{
    public partial class Form4 : Form
    {


        Label[] ld;
        List<DataForCalendary> ldfc = new List<DataForCalendary>();

        public Form4()
        {
            InitializeComponent();
        }


        private void createCalendar() {

         ld = new Label[Program.currentShop.DFCs.Count];
        int m, i, j;
            int k = 0;
          

            foreach (DataForCalendary d in Program.currentShop.DFCs)
            {

                i = d.getNWeekday()-1;


               ld[k] = new Label();
                ld[k].Click += new System.EventHandler(ld_Click);

                ld[k].Text=d.getNiM().ToString();
                switch (d.getTip()) {
                    case 1: ld[k].BackColor = Color.White;break;
                    case 2: ld[k].BackColor = Color.White; break;
                    case 3: ld[k].BackColor = Color.White; break;
                    case 4: ld[k].BackColor = Color.White; break;
                    case 5: ld[k].BackColor = Color.White; break;
                    case 6: ld[k].BackColor = Color.Orange; break;
                    case 7: ld[k].BackColor = Color.Orange; break;
                    case 8: ld[k].BackColor = Color.Red; break;
                    case 9: ld[k].BackColor = Color.Yellow; break;
                }

                j =(d.getNiM()+ DataForCalendary.OON(d.getData())-1)/7;
                
                m = d.getNM();
                (this.Controls["tableLayoutPanel" + m] as System.Windows.Forms.TableLayoutPanel).Controls.Add(ld[k], i, j);
                ld[k].Tag = d.getData().Day+" "+d.getData().Month+" "+d.getData().Year+";"+d.getTimeStart()+";"+d.getTimeEnd();
                k++;
 
            }
                    
                   
                
            
        }

        private void ld_Click(object sender, EventArgs e)
        {
            
            string s1;
            if (sender is Label)
                s1 = (sender as Label).Tag.ToString();
            else s1 = "0;0;0";
          //  MessageBox.Show(s1 +"");
            string[] s = new string[3];
            s=s1.Split(';');
            string[] d = new string[3];
            d = s[0].Split(' ');

            labelData.Text = d[0]+Program.getMonthInString( int.Parse( d[1]))+d[2];

            textBoxStart.Text = s[1];
            textBoxEnd.Text = s[2];
            if ((comboBox1.SelectedIndex == 4) && (Form.ModifierKeys == Keys.Control))
            {
                ldfc.Add(new DataForCalendary(new DateTime(int.Parse(d[2]), int.Parse(d[1]), int.Parse(d[0]))));
            }
        }

        private static DataTable CreateTable()
        {
            //создаём таблицу
            string[] months = Program.getMonths();
            DataTable dt = new DataTable("norm");
            //создаём три колонки
            DataColumn Mounth = new DataColumn("месяц", typeof(string));

            DataColumn colCountDayInMonth = new DataColumn("количество дней в месяце", typeof(Int16));
            DataColumn colCountDayRab = new DataColumn("количество рабочих дней", typeof(Int16));
            DataColumn colCountDayVuh = new DataColumn("количество выходных дней", typeof(Int16));
            DataColumn normCh = new DataColumn("норма часов", typeof(Int16));

            //добавляем колонки в таблицу
            dt.Columns.Add(Mounth);
            dt.Columns.Add(colCountDayInMonth);
            dt.Columns.Add(colCountDayRab);
            dt.Columns.Add(colCountDayVuh);
            dt.Columns.Add(normCh);
            DataRow row = null;
            //создаём новую строку

            //заполняем строку значениями

            for (int i = 1; i <= 12; i++)
            {
                row = dt.NewRow();
                row["месяц"] = months[i - 1];
                row["количество дней в месяце"] = DateTime.DaysInMonth(DateTime.Today.Year, i);
                row["количество рабочих дней"] = Program.RD[i - 1];
                row["количество выходных дней"] = DateTime.DaysInMonth(DateTime.Today.Year, i) - Program.RD[i - 1];
                row["норма часов"] = Program.RD[i - 1] * 8 - Program.PHD[i - 1];
                dt.Rows.Add(row);
            }
            return dt;
        }


        private void Form4_Load(object sender, EventArgs e)

        {
            textBoxEnd.Enabled = false;
            textBoxStart.Enabled = false;
            
            dataGridViewCalendar.DataSource = CreateTable();
            createCalendar();
        }

        private void buttonAddCalendary_Click(object sender, EventArgs e)
        {
            string readPath = Environment.CurrentDirectory + @"\Shops\" + Program.currentShop.getIdShop() + @"\Calendar.txt";
            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {

                foreach (DataForCalendary d in Program.currentShop.DFCs)
                    sw.WriteLine(d.getData() + "#" + d.getTip() + "#" + d.getTimeStart() + "#" + d.getTimeEnd());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(comboBox1.SelectedItem.ToString());
            switch (comboBox1.SelectedIndex){
                case 0: textBoxStart.Text= Program.currentShop.DFCs.Find(t => (t.getTip() == 1)).getTimeStart().ToString(); textBoxEnd.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString(); ldfc.Clear(); ldfc = Program.currentShop.DFCs.FindAll(t => (t.getTip() == 1) || (t.getTip() == 2) || (t.getTip() == 3) || (t.getTip() == 4) || (t.getTip() == 5));  break;
                case 1: textBoxStart.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 7)).getTimeStart().ToString(); textBoxEnd.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString(); ldfc.Clear(); ldfc = Program.currentShop.DFCs.FindAll(t => (t.getTip() == 6) || (t.getTip() == 7)) ;  break;
                case 2: textBoxStart.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 8)).getTimeStart().ToString(); textBoxEnd.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString(); ldfc.Clear(); ldfc = Program.currentShop.DFCs.FindAll(t => (t.getTip() == 8));  break;
                case 3: textBoxStart.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 9)).getTimeStart().ToString(); textBoxEnd.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString(); ldfc.Clear(); ldfc = Program.currentShop.DFCs.FindAll(t => (t.getTip() == 9));  break;
                case 4: textBoxStart.Text = ""; textBoxEnd.Text = ""; break;
                default: break;
            }
        }

        public void RedactirStartEndDay()
        {
            if (ldfc.Count != 0)
            {
                foreach (DataForCalendary dfc in ldfc)
                {
                    dfc.setTimeBaE(int.Parse(textBoxStart.Text), int.Parse(textBoxEnd.Text));

                }
            }
            else {
                MessageBox.Show("Даты не выбраны");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Редактировать") {

                button1.Text = "Применить";
                textBoxEnd.Enabled = true;
                textBoxStart.Enabled = true;
            }
            else{
                button1.Text = "Редактировать";
                textBoxEnd.Enabled = false;
                textBoxStart.Enabled = false;
                RedactirStartEndDay();
            }
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            //e.KeyCode
        }
    }
}