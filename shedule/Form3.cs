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
            if (Program.isConnected(login,password))
            {
                ((Form1)this.Owner).Enabled = true;
                ((Form1)this.Owner).labelStatus2.Text = "режим работы сетевой ";
                this.Close();
                
               

            }
            else {
                ((Form1)this.Owner).Enabled = true;
                this.Close();
                
            }

            
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
