using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace shedule
{
    public partial class fSettings : Form
    {
        public fSettings()
        {
            InitializeComponent();
        }

        private void fSettings_Load(object sender, EventArgs e)
        {
            tbServerAddress.Text = Settings.Default.DatabaseAddress;
            tbServerLogin.Text = Settings.Default.DatabaseLogin;
            tbServerPassword.Text = Settings.Default.DatabasePassword;
            label4.Text = Settings.Default.folder;
        }

        private void bSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Default.DatabaseAddress = tbServerAddress.Text;
                Settings.Default.DatabaseLogin = tbServerLogin.Text;
                Settings.Default.DatabasePassword = tbServerPassword.Text;
                Settings.Default.folder = label4.Text;
                Settings.Default.Save();
                MessageBox.Show("Успешно сохранено!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Не сохранено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.R();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                label4.Text = folderBrowserDialog1.SelectedPath;
                Settings.Default.folder= folderBrowserDialog1.SelectedPath;
            }
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Shop shop = Program.currentShop;
            string fc = Environment.CurrentDirectory + @"\Shops\" + shop.getIdShop() + "\\текущий график.xlsx";
          
            if (!File.Exists(fc))
            {
                MessageBox.Show(File.Exists(fc).ToString());
                fc = Settings.Default.folder + @"\" + shop.getIdShop() + ".xlsx";

            }
            else if (!File.Exists(fc))
            {
                MessageBox.Show("Файл с текущим графиком для магазина " + shop.getIdShop() + "  не найден");
            }
            else
            {
                Program.file = fc;
                MessageBox.Show("Файл с текущим графиком для магазина " + shop.getIdShop() + "  найден"+ Program.file);
            }












        }
    }
}
