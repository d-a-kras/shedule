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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            if (Program.isConnect())
            {
                this.Close();
              //  Form1.labelStatus1.Text = "Статус: Обработано " + Program.getStatus() + " магазинов из " + Program.listShops.Count; Form1.labelStatus2.Text = "режим работы сетевой ";

            }
            else {
                this.Close();
                
            }

            
        }
    }
}
