using schedule.Code;
using shedule.Models;
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
        private int Mixing;

        public DBShop(int id, string address, int connection) {
            this.Address = address;
            this.ShopId = id;
            this.ConnectionId = connection;
            this.Mixing = 0;
            this.ConnectionId = Connection.getActiveConnection().Id;
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

        public int mixing
        {
            get { return Mixing; }
            set
            {
                Mixing = value;
                OnPropertyChanged("Mixing");
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

                foreach (var shop in listShop)
                {
                    int shopId = shop.getIdShop();
                    DBShop dBShop = db.DBShops.FirstOrDefault(t => t.shopid == shopId);
                    if (dBShop!=null) {
                        db.DBShops.Remove(dBShop); 
                    }
                    db.DBShops.Add(DBShop.convertDBShop(shop));
                }
                db.SaveChanges();
                Program.listShops.Clear();
                List<DBShop> dBShops = db.DBShops.ToList();
                foreach (var sh in dBShops) {
                    var h = new mShop(sh.shopid, sh.address);
                    Program.listShops.Add(h);
                }
                Program.listShops = Program.listShops.OrderBy(t => t.getIdShop()).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }

        public static void SaveMixing(int shopId, int isMixing)
        {
            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.DBShops.Load();
                BindingList<DBShop> DataContext = db.DBShops.Local.ToBindingList();
                DBShop dBShop = db.DBShops.First(t => t.shopid == shopId);
                dBShop.mixing = isMixing;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }

        public static int getMixing(int shopId)
        {
            int mix=0;
            try
            {
                
                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.DBShops.Load();
                BindingList<DBShop> DataContext = db.DBShops.Local.ToBindingList();
                mix=db.DBShops.First(t => t.shopid == shopId).mixing;
               
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
            return mix;
        }


        public mShop convertMShop() {
            return new mShop(this.shopid,this.address);
        }

        public static DBShop convertDBShop(mShop shop)
        {
            
            DBShop newshop=new DBShop(shop.getIdShop(), shop.getAddress(), Connection.getActiveConnection(shop.getIdShop()).Id);
            if (shop.getAddress().IndexOf("SET10") > -1)
            {
                newshop.connectionId = 2;
            }
            
            return newshop;
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
