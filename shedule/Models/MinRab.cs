using schedule.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schedule.Models
{
    public class MinRab  : INotifyPropertyChanged
    {
        int MinCount;
        int Time;
        bool Otobragenie;
        EmployeeType EmployeeType;
        int ShopId;

        static private SQLiteConnection m_dbConn;
        static private SQLiteCommand m_sqlCmd;

        public int Id { get; set; }

        public int minCount
        {
            get { return MinCount; }
            set
            {
                MinCount = value;
                OnPropertyChanged("MinCount");
            }
        }
        public int time
        {
            get { return Time; }
            set
            {
                Time = value;
                OnPropertyChanged("Time");
            }
        }


        public bool otobragenie
        {
            get { return Otobragenie; }
            set
            {
                Otobragenie = value;
                OnPropertyChanged("Otobragenie");
            }
        }

        public int shopId
        {
            get { return ShopId; }
            set
            {
                ShopId = value;
                OnPropertyChanged("ShopId");
            }
        }

        public int employeetype
        {
            get { return (int)this.EmployeeType; }
            set
            {
                EmployeeType = (EmployeeType)Enum.GetValues(typeof(EmployeeType)).GetValue(value);
                OnPropertyChanged("EmployeeType");
            }
        }

        public MinRab(int mc, int t, bool o)
        {
            this.MinCount = mc;
            this.Time = t;
            this.Otobragenie = o;
        }

        public MinRab()
        {
        }

        public MinRab(EmployeeType employeeType, int count, int shopId)
        {
            this.EmployeeType = employeeType;
            this.setMinCount(count);
            this.shopId = shopId;
        }

        public MinRab(EmployeeType employeeType, int count, int shopId,int time,bool otobragenie)
        {
            this.EmployeeType = employeeType;
            this.setMinCount(count);
            this.ShopId = shopId;
            this.Time = time;
            this.otobragenie = otobragenie;
        }

        public int getMinCount()
        {
            return this.MinCount;
        }

        public int getTimeMinRab()
        {
            return this.Time;
        }

        public EmployeeType getEmployeeType()
        {
            return this.EmployeeType;
        }
        public bool getOtobragenie()
        {
            return this.Otobragenie;
        }

        public void setOtobragenie(bool b)
        {
            this.Otobragenie = b;
        }

        public void setMinCount(int mc)
        {
            this.MinCount = mc;
        }

        public void setTime(int mt)
        {
            this.Time = mt;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static void ReadAll()
        {
            string dbFileName = Helper.dbFileName;
            if (!File.Exists(dbFileName))
                SQLiteConnection.CreateFile(dbFileName);

            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

            }
            catch (SQLiteException ex)
            {
                Logger.Error(ex.ToString());
               
            }

            DataTable dTable = new DataTable();
            String sqlQuery;

            if (m_dbConn.State != ConnectionState.Open)
            {

                return;
            }

            try
            {
                sqlQuery = "SELECT * FROM MinRab";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                   // for (int i = 0; i < dTable.Rows.Count; i++)
                       // dataGridViewDB.Rows.Add(dTable.Rows[i].ItemArray);
                }


            }
            catch (SQLiteException ex)
            {
                Logger.Error(ex.ToString());
            }
        }

        public static MinRab Read(EmployeeType employeeType, int shopId)
        {

            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.MinRab.Load();
                BindingList<MinRab> DataContext = db.MinRab.Local.ToBindingList();
                int empType = (int)employeeType;
                MinRab mr = db.MinRab.First(t => t.employeetype == empType && t.shopId==shopId);
                return mr;


            }
            catch (Exception ex)
            {
                String str = ex.ToString();
                Logger.Error(ex.ToString());
                MinRab newmr = new MinRab(employeeType,1,Program.currentShop.getIdShop(),9,true);
                Create(newmr);
                return newmr;
            }
            
        }

        public static List<MinRab> ReadForShop(int shopId)
        {

            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.MinRab.Load();
                BindingList<MinRab> DataContext = db.MinRab.Local.ToBindingList();
                List<MinRab> mr = db.MinRab.Where(t => t.shopId == shopId).ToList();
                if (mr.Count < 4)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        MinRab minRab = db.MinRab.FirstOrDefault(t => t.shopId == shopId && t.employeetype == i);
                        if (minRab==null) { 
                        MinRab newmr = new MinRab((EmployeeType)i, 1, Program.currentShop.getIdShop(), 9, true);
                        Create(newmr); 
                      }
                    }
                     mr = db.MinRab.Where(t => t.shopId == shopId).ToList();
                }
                return mr;


            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return new List<MinRab>();
            }

        }

        public static int Update(MinRab minRab)
        {
           

            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.MinRab.Load();
                BindingList<MinRab> DataContext = db.MinRab.Local.ToBindingList();

                MinRab mr = db.MinRab.First(t=>t.employeetype == minRab.employeetype && t.shopId== minRab.shopId);
                mr.minCount = minRab.getMinCount();
                mr.time= minRab.getTimeMinRab();

                db.SaveChanges();


            }
            catch (Exception ex)
            {
                String str = ex.ToString();
                Logger.Error(ex.ToString());
            }

            return 0;
        }


         public static void Create(MinRab minRab)
            {
            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.MinRab.Load();
                BindingList<MinRab> DataContext = db.MinRab.Local.ToBindingList();
                db.MinRab.Add(minRab);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                String str = ex.ToString();
                Logger.Error(ex.ToString());
            }

        }

        public static void CreateAllForShop(int shopId)
        {
            for (int i=1;i<5;i++) {
                Create(new MinRab());
            }
        }

        public static EmployeeType getType(string typeStr)
        {
            switch (typeStr) {
                case "кассиров": return EmployeeType.Cashier;
                case "продавцов": return EmployeeType.Seller;
                case "грузчиков": return EmployeeType.Porter;
                case "гастрономов": return EmployeeType.Gastronome;
                default: return EmployeeType.NoName;
            }

        }
        
    }

}
