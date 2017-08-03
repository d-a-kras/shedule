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
        private int isThisTypeOfFunction = -1;
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(int typeOfFunction)
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
                ((Form1)this.Owner).buttonVygr.Visible = true;
                ((Form1)this.Owner).comboBox2.Visible = true;
                isConnected = true;
                ((Form1)this.Owner).isConnected = true;
                Program.login = login;
                Program.password = password;

                if (isThisTypeOfFunction == 1)
                {
                    ((Form1)this.Owner).CreateZip();
                }
                if (isThisTypeOfFunction == 2)
                {
                    ((Form1)this.Owner).StartDiagramForm();
                }
                if (isThisTypeOfFunction == 3)
                {
                    ((Form1)this.Owner).StartExportingToExcel();
                }

                this.Close();
            }
            else
            {
                isConnected = false;
                ((Form1)this.Owner).isConnected = false;
                ((Form1)this.Owner).radioButtonIzFile.Checked = true;
                ((Form1)this.Owner).Enabled = true;
                ((Form1)this.Owner).labelStatus2.Text = "режим работы локальный ";
                ((Form1)this.Owner).buttonVygr.Visible = false;
                ((Form1)this.Owner).comboBox2.Visible = false;

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

        private void Form3_Load(object sender, EventArgs e)
        {
            textBoxLogin.Text = Settings.Default.DatabaseLogin;
            textBoxPassword.Text  = Settings.Default.DatabasePassword;
        }
    }
}
