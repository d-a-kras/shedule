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
using shedule.Models;
using System.Data.Entity;
using System.Data.SQLite;
using shedule.Code;

namespace shedule
{

   
    public partial class fSettings : Form
    {
        Models.ApplicationContext db;
        
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        public BindingList<Connection> DataContext { get; }


        public fSettings()
        {
            InitializeComponent();
            db = new Models.ApplicationContext();
            db.Connections.Load();
            this.DataContext = db.Connections.Local.ToBindingList();
        }

        private void fSettings_Load(object sender, EventArgs e)
        {
           // tbServerAddress.Text = Settings.Default.DatabaseAddress;
           // tbServerLogin.Text = Settings.Default.DatabaseLogin;
           // tbServerPassword.Text = Settings.Default.DatabasePassword;
            label4.Text = Settings.Default.folder;
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            ConnectionReadAll();
        }

        private void bSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
              //  Settings.Default.DatabaseAddress = tbServerAddress.Text;
               // Settings.Default.DatabaseLogin = tbServerLogin.Text;
            //    Settings.Default.DatabasePassword = tbServerPassword.Text;
             //   Settings.Default.folder = label4.Text;
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /**
         Update connection
             */
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridViewDB.SelectedRows.Count > 0)
            {
                int index = dataGridViewDB.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridViewDB[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Connection connection = db.Connections.Find(id);

                DBConnectionForm plForm = new DBConnectionForm(connection);

               /* plForm.SetAddress(connection.server);
                plForm.SetLogin(connection.login);
                plForm.SetPassword(connection.password);*/

                DialogResult result = plForm.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                connection.server = plForm.GetAddress(); ;
                connection.login = plForm.GetLogin();
                connection.password = plForm.GetPassword();

                db.SaveChanges();
                ConnectionReadAll();
                MessageBox.Show("Объект обновлен");

            }
        }

        private void buttonDBAdd_Click(object sender, EventArgs e)
        {
            DBConnectionForm plForm = new DBConnectionForm(new Connection());
            DialogResult result = plForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Connection connection = new Connection();
            connection.server = plForm.GetAddress();
            connection.login = plForm.GetLogin();
            connection.password = plForm.GetPassword();

            db.Connections.Add(connection);
            db.SaveChanges();
            ConnectionReadAll();
            MessageBox.Show("Новое соединение добавлено");
        }

        private void ConnectionReadAll()
        {
            if (!File.Exists(Helper.dbFileName))
                SQLiteConnection.CreateFile(Helper.dbFileName);

            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + Helper.dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;
                
            }
            catch (SQLiteException ex)
            {
                
                MessageBox.Show("Error: " + ex.Message);
            }

            DataTable dTable = new DataTable();
            String sqlQuery;

            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Ошибка локальной БД");
                return;
            }

            try
            {
                sqlQuery = "SELECT * FROM Connections";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    dataGridViewDB.Rows.Clear();

                    for (int i = 0; i < dTable.Rows.Count; i++)
                        dataGridViewDB.Rows.Add(dTable.Rows[i].ItemArray);
                }
                else
                    MessageBox.Show("Database is empty");
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonDBDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewDB.SelectedRows.Count > 0)
            {
                int index = dataGridViewDB.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridViewDB[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Connection connection = db.Connections.Find(id);
                db.Connections.Remove(connection);
                db.SaveChanges();
                ConnectionReadAll();
                MessageBox.Show("Объект удален");
            }
        }

        
    }
}
