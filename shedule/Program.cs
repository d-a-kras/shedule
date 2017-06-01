using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace shedule
{

    enum Position
    {
        cashier,
        seller,
        loader
    }

    public class Shop
    {
        private short idShop;
        private String address;
        public short getIdShops() { return idShop; }
        public string getAddress() { return address; }
        public Shop(short i, string a) {
            idShop = i;
            address = a;
        }
    }


    public class hourSale
    {
        private short idShop;
        private DateTime Data;
        private string weekday;
        private string NHour;
        private int countCheck;
        private int countClick;
        private double countTov;



        public hourSale(short idS, DateTime D, string NH, string w, int countCh, int countCl, double countT)
        {
            idShop = idS;
            Data = D;
            weekday = w;
            NHour = NH;
            countCheck = countCh;
            countClick = countCl;
            countTov = countT;
        }

        public string getWeekday() { return this.weekday; }

        public String getNHour() { return this.NHour; }

        public int getCountClick() { return this.countClick; }

        public int getCountCheck() { return this.countCheck; }

        public int getMinut() { return (this.getCountCheck() * 25 + this.getCountClick() * 2) / 60; }

    }

    static class Program
    {
        static public int CountSmen;
        static public Shop[] listShops;
        static public void ReadListShops() {
            
            String readPath = Environment.CurrentDirectory+@"\Shops.txt";
          
            int count = File.ReadAllLines(readPath).Length;
            listShops = new Shop[count];
            using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
            {
                string line;
                int i = 0;
                while ((line=sr.ReadLine())!=null) {
                    
                    short idSh = Convert.ToInt16( line.Substring(0, 2));
                    string Sh = line.Substring(3);
                    
                    listShops[i] = new Shop(idSh, Sh);
                    i++;
                }
            }
        }
            
           

       static SqlConnection connection;

        public static bool isConnect() {
           return connection.State == System.Data.ConnectionState.Open ? true: false; 

        }
        static public void Connect() {
            var connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=VShleyev;Password=gjkrjdybr@93";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                   
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    
                }
            }
        }

        static public string[] collectionweekday = { "понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресенье" };

        static public string[] collectionHours = {"0","1","2","3","4","5","6","7", "8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23" };

        enum Timetable {

        }

        public class Watch {
            int coming;
            int departure;
            DateTime date;
            int IdEmployee;
                
        }

        public class employee
        {
            int IdEmployee;
            Position position;
            String name;
            int CountHours;
            Timetable timetable;
            List<Watch> Wathes;

            public employee(int x1, int y1, String z1)
            {
                
            }
        }

        
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        
    }



   
}
