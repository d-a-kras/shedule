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
        }

        private void bSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Default.DatabaseAddress = tbServerAddress.Text;
                Settings.Default.DatabaseLogin = tbServerLogin.Text;
                Settings.Default.DatabasePassword = tbServerPassword.Text;
                Settings.Default.Save();
                MessageBox.Show("Успешно сохранено!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Не сохранено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
