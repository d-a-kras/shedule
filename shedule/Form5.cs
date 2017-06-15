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
            DateTime dt = new DateTime(2017,06,03);
            TemplateWorkingDay t = new TemplateWorkingDay(dt);
            t.cre
            chart1.Series["s1"].Points.DataBindXY(, chartY1);
            chart1.Series["s2"].Points.DataBindXY(chartX, chartY2);
        }
    }
}
