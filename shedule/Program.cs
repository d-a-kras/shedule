using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Xml;

namespace shedule
{

    class DataForCalendary{
        DateTime Data;
        int Tip;

        static public bool isHolyday(DateTime mdt) {
            foreach (DateTime dt in Program.holydays) {
               int rez= DateTime.Compare(mdt, dt);
                
                if (rez == 0) return true;
            }
            return false;
        }

        static public bool isPrHolyday(DateTime mdt)
        {
            if (isHolyday(mdt)) return false;
            else
            {
                DateTime next = new DateTime();
                int countD = DateTime.DaysInMonth(mdt.Year, mdt.Month);
                if (mdt.Day != countD)
                {
                    next = new DateTime(mdt.Year, mdt.Month, (mdt.Day + 1));
                }
                else if (mdt.Month != 12) { next = new DateTime(mdt.Year, (mdt.Month + 1), 1); }


                if (isHolyday(next) && next.TimeOfDay.ToString() != "Sanday") { return true; }
                else return false;
            }

        }

       public DataForCalendary(DateTime Дата, int тип) {
            Data = Дата;
            Tip = тип;
        }

       public DataForCalendary(DateTime Дата)
        {
            Data = Дата;
            Tip = 0;
        }

        public DateTime getData() { return this.Data; }
        public int getTip() {
            if (this.Tip == 0) {
                if (this.getData().DayOfWeek.ToString() == "Saturday") return 2;
                else if (this.getData().DayOfWeek.ToString() == "Sunday") return 3;
                else if (isHolyday(this.getData())) return 4;
                else if (isPrHolyday(this.getData())) return 5;
                else return 1; }
            else return this.Tip;
        }
        public string getWeekday() { return this.getData().DayOfWeek.ToString(); }
    }

    enum Timetable
    {

    }

    public class Watch
    {
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



        public hourSale(short idS, DateTime D, string NH, string w, int countCh, int countCl)
        {
            idShop = idS;
            Data = D;
            weekday = w;
            NHour = NH;
            countCheck = countCh;
            countClick = countCl;
         
        }
        public hourSale(short idS, DateTime D, string NH, string w, int countCh, int countCl, double ct)
        {
            idShop = idS;
            Data = D;
            weekday = w;
            NHour = NH;
            countCheck = countCh;
            countClick = countCl;
            countTov = ct;
        }

        public DateTime getData() { return this.Data; }

        public string getWeekday() { return this.weekday; }

        public string getNHour() { return this.NHour; }

        public int getCountClick() { return this.countClick; }

        public int getCountCheck() { return this.countCheck; }

        public int getMinut() { return (this.getCountCheck() * 25 + this.getCountClick() * 2) / 60; }

    }

    static class Program
    {
        
       

        static public int CountSmen;
        static public List<Shop> listShops=new List<Shop>();
        static public List<DataForCalendary> DFCs = new List<DataForCalendary>();
        static public List<hourSale> HSS = new List<hourSale>();
        static public string CountObr = "";

       static public  int[,] CountS = new int[25, 15];
       static public int[,] CountClick = new int[25, 15];
        static public int[,] CountCheck = new int[25, 15];
        static public List<DateTime> holydays = new List<DateTime>();
        static public int[] RD = new int[12];
        static public int[] PHD = new int[12];

        static public void getListDate(int year) {

            Program.ReadCalendarFronXML();
            for (int i = 1; i <= 12; i++) {
                RD[i-1] = 0;
                PHD[i-1] = 0;
                int countDays = DateTime.DaysInMonth(year, i);
                for (int k = 1; k <= countDays; k++) {
                    DataForCalendary dfc = new DataForCalendary(new DateTime(year, i, k));
                    int t=dfc.getTip();
                    if (t == 1) { RD[i-1]++; }
                    if (t == 5) { PHD[i - 1]++; }
                    DFCs.Add(dfc);


                }
            }



        }

        static public void ReadConfigShop(int id) { 


         }
        static public string[] getMonths() {
            String[] months = new String[12];
            months[0] = "Январь";
            months[1] = "Феврвль";
            months[2] = "Март";
            months[3] = "Апрель";
            months[4] = "Май";
            months[5] = "Июнь";
            months[6] = "Июль";
            months[7] = "Август";
            months[8] = "Сентябрь";
            months[9] = "Октябрь";
            months[10] = "Ноябрь";
            months[11] = "Декабрь";
            return months;

        }

        static public void ReadCalendarFronXML()
        {

            XmlDocument xDoc = new XmlDocument();
            String readPath = Environment.CurrentDirectory + @"\Calendars\2017.xml";
            xDoc.Load(readPath);
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            
            // обход всех узлов в корневом элементе
            
                // получаем атрибут name
                if (xRoot.Attributes.Count > 0)
                {

                XmlNode attr = xRoot.LastChild;
                    
                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in attr.ChildNodes)
                    {
                        // если узел - company
                        if (childnode.Name == "day")
                        {
                        //MessageBox.Show(childnode.Attributes.GetNamedItem("d").Value);
                        //MessageBox.Show(xRoot.Attributes.GetNamedItem("year").Value);
                        string d_m = childnode.Attributes.GetNamedItem("d").Value;
                        string[] d_and_m = new string[2];
                          d_and_m  =d_m.Split('.');
                        holydays.Add(new DateTime(Int16.Parse(xRoot.Attributes.GetNamedItem("year").Value), Int16.Parse(d_and_m[0]), Int16.Parse( d_and_m[1])));
                        }
                        
                    }
                    
                
                
            }
        }

        

        static public void Kass() {


            String readPath = Environment.CurrentDirectory + @"\kass.txt";
            try
            {
                int count = File.ReadAllLines(readPath).Length;
                // listShops = new Shop[count];
                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    for (int j = 0; j < 15; j++)
                    {
                        int nd = 1;
                        int d = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 6; break;
                                case 1: nd = 13; break;
                                case 2: nd = 20; break;
                            }
                            CountCheck[nd-1, j] = d;

                        }

                        for (int i = 0; i < 3; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 7; break;
                                case 1: nd = 14; break;
                                case 2: nd = 21; break;
                            }
                            CountCheck[nd-1, j] = d;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 1; break;
                                case 1: nd = 8; break;
                                case 2: nd = 15; break;
                                case 3: nd = 22; break;
                            }
                            CountCheck[nd-1, j] = d;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 2; break;
                                case 1: nd = 9; break;
                                case 2: nd = 16; break;
                                case 3: nd = 23; break;
                            }
                            CountCheck[nd-1, j] = d;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 3; break;
                                case 1: nd = 10; break;
                                case 2: nd = 17; break;
                                case 3: nd = 24; break;
                            }
                            CountCheck[nd-1, j] = d;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 4; break;
                                case 1: nd = 11; break;
                                case 2: nd = 18; break;
                                case 3: nd = 25; break;
                            }
                            CountCheck[nd-1, j] = d;

                        }
                        for (int i = 0; i < 3; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 5; break;
                                case 1: nd = 12; break;
                                case 2: nd = 19; break;
                            }
                            CountCheck[nd-1, j] = d;

                        }
                    }

                    //Количество кликов

                    for (int j = 0; j < 15; j++)
                    {
                        int nd = 1;
                        int d = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 6; break;
                                case 1: nd = 13; break;
                                case 2: nd = 20; break;
                            }
                            CountClick[nd-1, j] = d;

                        }

                        for (int i = 0; i < 3; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 7; break;
                                case 1: nd = 14; break;
                                case 2: nd = 21; break;
                            }
                            CountClick[nd-1, j] = d;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 1; break;
                                case 1: nd = 8; break;
                                case 2: nd = 15; break;
                                case 3: nd = 22; break;
                            }
                            CountClick[nd-1, j] = d;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 2; break;
                                case 1: nd = 9; break;
                                case 2: nd = 16; break;
                                case 3: nd = 23; break;
                            }
                            CountClick[nd-1, j] = d;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 3; break;
                                case 1: nd = 10; break;
                                case 2: nd = 17; break;
                                case 3: nd = 24; break;
                            }
                            CountClick[nd-1, j] = d;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            string line = sr.ReadLine();
                            try
                            {
                                d = Convert.ToInt16(line);
                            }
                            catch (Exception ex) { MessageBox.Show(line+" "+i+" "+j); }
                            switch (i)
                            {
                                case 0: nd = 4; break;
                                case 1: nd = 11; break;
                                case 2: nd = 18; break;
                                case 3: nd = 25; break;
                            }
                            CountClick[nd-1, j] = d;

                        }
                        for (int i = 0; i < 3; i++)
                        {
                            string line = sr.ReadLine();

                            d = Convert.ToInt16(line);

                            switch (i)
                            {
                                case 0: nd = 5; break;
                                case 1: nd = 12; break;
                                case 2: nd = 19; break;
                            }
                            CountClick[nd-1, j] = d;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                setListShops();

            }
        }

        static public void getSm()
        {

            String readPath = Environment.CurrentDirectory + @"\Sm.txt";
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    for (int j = 0; j < 25; j++)
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            CountS[j, i] = Convert.ToInt16(sr.ReadLine());
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                setListShops();

            }
        }

            static public void ReadTekChedule(string fileName) {
           
            
            
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                //Открываем книгу.                                                                                                                                                        
                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(fileName, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                //Выбираем таблицу(лист).
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

                //Очищаем от старого текста окно вывода.
                

                for (int i = 1; i < 101; i++)
                {
                    //Выбираем область таблицы. (в нашем случае просто ячейку)
                    Microsoft.Office.Interop.Excel.Range range = ObjWorkSheet.get_Range(11,11);
                    //Добавляем полученный из ячейки текст.
                   
                    Application.DoEvents();
                }

                //Удаляем приложение (выходим из экселя) - ато будет висеть в процессах!
                ObjExcel.Quit();
            
        }

        static public void ReadOperFromExel(string fileName)
        {

           

            Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            //Открываем книгу.                                                                                                                                                        
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(fileName, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            //Выбираем таблицу(лист).
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
            int x = 3;
            Microsoft.Office.Interop.Excel.Range range = ObjWorkSheet.get_Range(1, x);
            while (range.Text != "") 
            {
               
                string dn = ObjWorkSheet.get_Range(1, x).Text;
                string t = ObjWorkSheet.get_Range(1, x).Text;
                DateTime dt= ObjWorkSheet.get_Range(3, x).Text;
                int cc = ObjWorkSheet.get_Range(5, x).Text;
                int cs = ObjWorkSheet.get_Range(6, x).Text;
                HSS.Add(new hourSale(1, dt, t, dn, cc, cs));
                x++;
                range= ObjWorkSheet.get_Range(1, x);
                Application.DoEvents();
            }

            //Удаляем приложение (выходим из экселя) - ато будет висеть в процессах!
            ObjExcel.Quit();

        }

        

        static public string getStatus() {
            String readPath = Environment.CurrentDirectory + @"\Status.txt";
            try
            {
               
                
                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    
                   
                     CountObr = sr.ReadLine();
                    

                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            return CountObr;
        }

        static public void WriteStatus() {

        }


        static public void ReadListShops() {
            
            String readPath = Environment.CurrentDirectory+@"\Shops.txt";
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

                        short idSh = Convert.ToInt16(line.Substring(0, 2));
                        string Sh = line.Substring(3);

                        listShops.Add( new Shop(idSh, Sh));
                        i++;
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                setListShops();
                
            }
        }



        static public void setListShops() {
          
            Shop h;
            var connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=VShleyev;Password=gjkrjdybr@93";
            string sql = "select * from get_shops() order by КодМагазина";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    listShops.Clear();
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 300;
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        h = new Shop(reader.GetInt16(0), reader.GetString(1));
                        listShops.Add(h);
                        string writePath = Environment.CurrentDirectory + @"\Shops.txt";
                        using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                        {
                            foreach (Shop s in listShops)
                                sw.WriteLine(s.getIdShops() + " " + s.getAddress());
                        }
                    }

                
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show("Ошибка соединения с базой данных");
                   // ReadListShops();
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
