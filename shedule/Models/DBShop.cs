using schedule.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schedule.Models
{
    public class DBShop : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private int ShopId;
        private string Address;
        private int ConnectionId;

        public DBShop(int id, string address, int connection) {
            this.Address = address;
            this.ShopId = id;
            this.ConnectionId = connection;
        }
        public DBShop()
        {

        }



        public int shopid
        {
            get { return ShopId; }
            set
            {
                ShopId = value;
                OnPropertyChanged("ShopId");
            }
        }
        public string address
        {
            get { return Address; }
            set
            {
                Address = value;
                OnPropertyChanged("Address");
            }
        }

        public int connectionId
        {
            get { return ConnectionId; }
            set
            {
                ConnectionId = value;
                OnPropertyChanged("ConnectionId");
            }
        }

        public static List<DBShop> getShops()
        {
            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.DBShops.Load();
                BindingList<DBShop> DataContext = db.DBShops.Local.ToBindingList();
                List<DBShop> shops = db.DBShops.ToList();

                return shops;


            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return new List<DBShop>();
            }
        }


        public static void setShops(List<mShop> listShop)
        {
            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.DBShops.Load();
                BindingList<DBShop> DataContext = db.DBShops.Local.ToBindingList();
                List<DBShop> dBShops = db.DBShops.ToList();
                foreach (var shop in dBShops) {
                    db.DBShops.Remove(shop);
                }
                foreach (var shop in listShop)
                {
                    db.DBShops.Add(DBShop.convertDBShop(shop));
                }
                db.SaveChanges();


            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }


        public mShop convertMShop() {
            return new mShop(this.shopid,this.address);
        }

        public static DBShop convertDBShop(mShop shop)
        {
            return new DBShop(shop.getIdShop(), shop.getAddress(), Connection.getActiveConnection(Program.currentShop.getIdShop()).Id );
        }

        public static int getConnection(int Id)
        {
            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.DBShops.Load();
                BindingList<DBShop> DataContext = db.DBShops.Local.ToBindingList();
                int ConnectionId = db.DBShops.First(t=>t.Id==Id).connectionId;

                return ConnectionId;


            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return 0;
            }
        }

        public static List<mShop> ReadFromFile()
        {
            String readPath = Environment.CurrentDirectory + @"\Shops.txt";
            List<mShop> listshops = new List<mShop>();
            try
            { 
                int count = File.ReadAllLines(readPath).Length;
                // listShops = new Shop[count];
                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    string line;
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] s = new string[2];
                        s = line.Split('_');
                        int idSh = Convert.ToInt16(s[0]);
                        string Sh = s[1];

                        listshops.Add(new mShop(idSh, Sh));
                        i++;
                    }
                }

                setShops(listshops);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
            return listshops;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
