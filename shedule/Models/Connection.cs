using shedule.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Models
{


    public class Connection : INotifyPropertyChanged
    {
        private string Server;
        private string Login;
        private string Password;
        private string Sheme;
        private bool isActive;
        private string NameDB;

        public int Id { get; set; }

        public string server
        {
            get { return Server; }
            set
            {
                Server = value;
                OnPropertyChanged("Server");
            }
        }

        public string nameDB
        {
            get { return NameDB; }
            set
            {
                NameDB = value;
                OnPropertyChanged("NameDB");
            }
        }

        public string sheme
        {
            get { return Sheme; }
            set
            {
                Sheme = value;
                OnPropertyChanged("Sheme");
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                OnPropertyChanged("isActive");
            }
        }

        public string login
        {
            get { return Login; }
            set
            {
                Login = value;
                OnPropertyChanged("Login");
            }
        }
        public string password
        {
            get { return Password; }
            set
            {
                Password = value;
                OnPropertyChanged("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static string getConnectionString() {
            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.Connections.Load();
                BindingList<Connection> DataContext = db.Connections.Local.ToBindingList();
                Connection con = db.Connections.First(t => t.IsActive==true);

                // String connectionString= $"Data Source="+con.Server+";Persist Security Info=True;User ID="+con.Login+";Password="+con.Password;
                // String connectionString = $"Provider=PostgreSQL OLE DB Provider;Data Source=" + con.Server + ";location=" + con.NameDB + ";User ID=" + con.Login + ";password=" + con.Password + ";timeout=1000;";
                String connectionString = $"Host=" + con.Server + ";Port=5432;Database=" + con.NameDB + ";Username=" + con.Login + ";Password=" + con.Password;
                return connectionString;


            }
            catch (Exception ex)
            {
                String str = ex.ToString();
                Logger.Error(ex.ToString());
                return "";
            }
        }

        public static string getSheme(Connection con)
        {
            string sheme = "";
            if (con.sheme != null && con.sheme != "") {
                sheme = con.sheme+".";
            }
            return sheme;
        }
        public static string getConnectionString(Connection con)
        {
            try
            {
                String connectionString = $"Host=" + con.Server + ";Port=5432;Database=" + con.NameDB + ";Username=" + con.Login + ";Password=" + con.Password;
                //String connectionString = $"Provider=PostgreSQL OLE DB Provider;Data Source=" + con.Server + ";location="+ con.NameDB + ";User ID="+ con.Login +";password="+ con.Password + ";timeout=1000;";
                //String connectionString = $"Data Source=" + con.Server + ";Persist Security Info=True;User ID=" + con.Login + ";Password=" + con.Password;
                return connectionString;


            }
            catch (Exception ex)
            {
                String str = ex.ToString();
                Logger.Error(ex.ToString());
                return "";
            }
        }

        public static Connection getActiveConnection()
        {
            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.Connections.Load();
                BindingList<Connection> DataContext = db.Connections.Local.ToBindingList();
                int connectionId = 0;
                if (Program.currentShop!=null) {
                    int shopid = Program.currentShop.getIdShop();
                    DBShop shop = db.DBShops.First(t=>t.shopid==shopid);
                    connectionId=shop.connectionId;
                }
                Connection con;
                if (connectionId==0) {
                    con = db.Connections.First(t => t.IsActive == true);
                }
                else {
                    con = db.Connections.Find(connectionId);
                }
                return con;


            }
            catch (Exception ex)
            {
                String str = ex.ToString();
                Logger.Error(ex.ToString());
                return new Connection();
            }
        }

        

    }
}
