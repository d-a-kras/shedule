using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shedule
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
           
            DateTime d1 = new DateTime(2017,5,1);
            DateTime d2 = new DateTime(2017,5,4);
            Program.createListDaySale(d1,d2);
          
            foreach (daySale ds in Program.currentShop.daysSale) {
                Program.createTemplate(ds);
                
            }

            Program.currentShop.templates[0].createChartTemplate();
          
            Program.currentShop.templates[0].DS.CreateChartDaySale();

            chart1.Series["S1"].Points.DataBindXY(Program.currentShop.templates[0].Chart.X, Program.currentShop.templates[0].Chart.Y);
            chart1.Series["S2"].Points.DataBindXY(Program.currentShop.templates[0].DS.Chart.X, Program.currentShop.templates[0].DS.Chart.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
