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
        private bool isConnected = false;
        private bool isThisTypeOfFunction = false;
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(bool typeOfFunction)
        {
            isThisTypeOfFunction = typeOfFunction;
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            if (Program.isConnected(login, password))
            {
                ((Form1)this.Owner).Enabled = true;
                ((Form1)this.Owner).labelStatus2.Text = "режим работы сетевой ";
                isConnected = true;
                ((Form1)this.Owner).isConnected = true;

                if (isThisTypeOfFunction)
                {
                    ((Form1)this.Owner).CreateZip();
                }

                this.Close();
            }
            else
            {
                ((Form1)this.Owner).radioButtonIzFile.Checked = true;
                ((Form1)this.Owner).Enabled = true;
                this.Close();
            }


        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isConnected)
            {
                ((Form1)this.Owner).radioButtonIzFile.Checked = true;
            }
            
            ((Form1)this.Owner).Enabled = true;
        }
    }
}
