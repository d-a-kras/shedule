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
    public partial class Form6 : Form
    {
        public int newId;
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string[] s = new string[2];
            s = listBox1.Text.Split('_');
            if (this.Owner != null)
            {
                if (this.Owner is Form1)
                {
                    ((Form1)this.Owner).Enabled = true;
                }
            }

            this.Close();
            if (s[0] != "") { 
            newId = int.Parse(s[0]);
        }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            
            if (Program.listShops != null)
            {
                foreach (mShop h in Program.listShops)
                {

                    listBox1.Items.Add(h.getIdShop() + "_" + h.getAddress());
                }
            }

        }
    }
}
