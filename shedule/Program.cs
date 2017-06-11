using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Xml;
using SD = System.Data;
//using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml.Linq;

namespace shedule
{

    class DataForCalendary{
        DateTime Data;
        int Tip;
        int TimeBegin;
        int TimeEnd;

        public int getTimeStart() {  return this.TimeBegin; }

        public int getTimeEnd() { return this.TimeEnd; }

        public void setTimeBaE(int b, int e) {
            this.TimeBegin = b;
            this.TimeEnd = e;

        }

        public int getNWeekday()
        {
            int i = 0;
           // MessageBox.Show(this.getWeekday());
            switch (this.getWeekday())
            {
                case "Monday": i = 1; break;
                case "Tuesday": i = 2; break;
                case "Wednesday": i = 3; break;
                case "Thursday": i = 4; break;
                case "Friday": i = 5; break;
                case "Saturday": i = 6; break;
                case "Sunday": i = 7; break;
                default: i = -1; break;
            }
            return i;
        }

        static public int OON(DateTime dfc) {
            DataForCalendary dt = new DataForCalendary(new DateTime(dfc.Year,dfc.Month,1));
            
            return (dt.getNWeekday()-1);
        }

    public int getNiM() {
            return this.Data.Day;
        }

        public int getNM()
        {
            return this.Data.Month;
        }
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

        public DataForCalendary(DateTime D,int T,int b,int e) {
            Data = D;
            Tip = T;
            TimeBegin = b;
            TimeEnd = e;
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

    public class Factor
    {
       public string name;
       public  int TZnach;
       public  bool Deistvie;
       public  DateTime DDD;
       public  int NewZnach;

        public Factor(string n, int TZ, bool D, DateTime ddd, int nz) {
            name = n;
            TZnach = TZ;
            Deistvie = D;
            DDD = ddd;
            NewZnach = nz;
        }

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
        private int idShop;
        private String address;
        public int getIdShop() { return idShop; }
        public string getAddress() { return address; }

        public Shop(int i, string a) {
            idShop = i;
            address = a;
        }
    }

    public class TSR
    {
        public string position;
        public int count;
        public int zarp;
        public int zarp1_2;

        public TSR(string p, int c, int z, int z1_2)
        {
            this.position = p;
            this.count = c;
            this.zarp = z;
            this.zarp1_2 = z1_2;
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

        static public bool connect= false;
        static public SqlConnection connection;
        static public int CountSmen;
        static public List<Shop> listShops = new List<Shop>();
        static public List<DataForCalendary> DFCs = new List<DataForCalendary>();
        static public List<hourSale> HSS = new List<hourSale>();
        static public string CountObr = "";

        static public int[,] CountS = new int[25, 15];
        static public int[,] CountClick = new int[25, 15];
        static public int[,] CountCheck = new int[25, 15];
        static public List<DateTime> holydays = new List<DateTime>();
        static public int[] RD = new int[12];
        static public int[] PHD = new int[12];
        static public TSR[] tsr = new TSR[4];
        static public List<Factor> factors = new List<Factor>();
        static public Shop currentShop;
        static public short ParametrOptimization;

        static public void refreshFoldersShops() {
            foreach (Shop shop in Program.listShops) {
                string pyth= Environment.CurrentDirectory +"/Shops/"+ shop.getIdShop().ToString();
                if (!Directory.Exists(pyth)) {
                    Directory.CreateDirectory(pyth);
                }

            }
        }

        static public void readTSR()
        {
            String readPath = Environment.CurrentDirectory + @"\TSR.txt";
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {

                    for (int i = 0; i < 4; i++)
                    {
                        tsr[i] = new TSR(sr.ReadLine(), int.Parse(sr.ReadLine()), int.Parse(sr.ReadLine()), int.Parse(sr.ReadLine()));
                        
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                for (int i = 0; i < 4; i++)
                {
                    tsr[i] = new TSR("", 0, 0, 0);

                }
            }

        }

        static public void readFactors()
        {
            String readPath = Environment.CurrentDirectory + @"\factors.txt";
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {

                    foreach(Factor f in factors)
                    {
                        factors.Add( new Factor(sr.ReadLine(), int.Parse(sr.ReadLine()), bool.Parse(sr.ReadLine()), DateTime.Parse(sr.ReadLine()), int.Parse(sr.ReadLine())));

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        static private void CreateXML() {
            String readPath = Environment.CurrentDirectory + "TSR";
            XDocument xdoc = new XDocument();
            // создаем первый элемент
            XElement iphone6 = new XElement("phone");
            // создаем атрибут
            XAttribute iphoneNameAttr = new XAttribute("name", "iPhone 6");
            XElement iphoneCompanyElem = new XElement("company", "Apple");
            XElement iphonePriceElem = new XElement("price", "40000");
            // добавляем атрибут и элементы в первый элемент
            iphone6.Add(iphoneNameAttr);
            iphone6.Add(iphoneCompanyElem);
            iphone6.Add(iphonePriceElem);
      
            XElement phones = new XElement("phones");
            // добавляем в корневой элемент
            phones.Add(iphone6);
            // добавляем корневой элемент в документ
            xdoc.Add(phones);
            //сохраняем документ
            xdoc.Save("phones.xml");
        }



       static private DataTable CreateTable()
        {
            //создаём таблицу
            string[] months = Program.getMonths();
            DataTable dt = new DataTable("norm");
            
            DataColumn Mounth = new DataColumn("Должность", typeof(string));
            DataColumn colCountDayInMonth = new DataColumn("Количество", typeof(string));
            DataColumn colCountDayRab = new DataColumn("Зарплата", typeof(Int16));
            DataColumn colCountDayVuh = new DataColumn("Зарплата за 1/2", typeof(Int16));

            //добавляем колонки в таблицу
            dt.Columns.Add(Mounth);
            dt.Columns.Add(colCountDayInMonth);
            dt.Columns.Add(colCountDayRab);
            dt.Columns.Add(colCountDayVuh);
            DataRow row = null;
            

            for (int i = 1; i <= 12; i++)
            {
                row = dt.NewRow();
                row["Должность"] = months[i - 1];
                row["Количество"] = DateTime.DaysInMonth(DateTime.Today.Year, i);
                row["Зарплата"] = Program.RD[i - 1];
                row["Зарплата за 1/2"] = DateTime.DaysInMonth(DateTime.Today.Year, i) - Program.RD[i - 1];
                dt.Rows.Add(row);
            }
            return dt;
        }

        static public void getListDate(int year)
        {

            String readPath = Environment.CurrentDirectory+ "/Shops/"+Program.currentShop.getIdShop() +"/CalendarSmen.txt";
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    string line;
                    string[] s = new string[4];
                    while ((line = sr.ReadLine()) != null) {
                        s = line.Split('#');
                        DataForCalendary d = new DataForCalendary(DateTime.Parse( s[0]),int.Parse(s[1]),int.Parse(s[2]),int.Parse(s[3]));
                        Program.DFCs.Add(d);
                    }
                }

            }
            catch (Exception ex)
            {
                Program.ReadCalendarFromXML();
                for (int i = 1; i <= 12; i++)
                {
                    RD[i - 1] = 0;
                    PHD[i - 1] = 0;
                    int countDays = DateTime.DaysInMonth(year, i);
                    for (int k = 1; k <= countDays; k++)
                    {
                        DataForCalendary dfc = new DataForCalendary(new DateTime(year, i, k));
                        int t = dfc.getTip();
                        if (t == 1) { RD[i - 1]++; }
                        if (t == 5) { PHD[i - 1]++; }
                        DFCs.Add(dfc);


                    }
                }
                foreach (DataForCalendary dfc in DFCs)
                {
                    switch (dfc.getTip())
                    {
                        case 1: dfc.setTimeBaE(7, 23); break;
                        case 2: dfc.setTimeBaE(9, 22); break;
                        case 3: dfc.setTimeBaE(9, 22); break;
                        case 4: dfc.setTimeBaE(9, 22); break;
                        case 5: dfc.setTimeBaE(9, 22); break;
                        default: dfc.setTimeBaE(0, 0); break;
                    }
                }

                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    foreach (DataForCalendary d in DFCs)
                        sw.WriteLine(d.getData()+"#"+d.getTip()+"#"+d.getTimeStart()+"#"+d.getTimeEnd() );
                }

            }
            



        }

        static public void ReadConfigShop(int id)
        {


        }
        static public string[] getMonths()
        {
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

        static public string getMonthInString(int nm) {
       
            switch (nm) {
                case 1 : return "Января"; 
                case 2: return "Февраля"; 
                case 3: return "Марта"; 
                case 4: return "Апреля"; 
                case 5: return "Мая"; 
                case 6: return "Июня";
                case 7: return "Июля";
                case 8: return "Августа"; 
                case 9: return "Сентбря"; 
                case 10: return "Октября";
                case 11: return "Ноября";
                case 12: return "Декабря";
                default: return "Ошибка чтения даты";
            }
        }

        static public void ReadCalendarFromXML()
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
                        d_and_m = d_m.Split('.');
                        holydays.Add(new DateTime(Int16.Parse(xRoot.Attributes.GetNamedItem("year").Value), Int16.Parse(d_and_m[0]), Int16.Parse(d_and_m[1])));
                    }

                }



            }
        }



        static public void Kass()
        {


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
                            CountCheck[nd - 1, j] = d;

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
                            CountCheck[nd - 1, j] = d;

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
                            CountCheck[nd - 1, j] = d;

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
                            CountCheck[nd - 1, j] = d;

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
                            CountCheck[nd - 1, j] = d;

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
                            CountCheck[nd - 1, j] = d;

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
                            CountCheck[nd - 1, j] = d;

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
                            CountClick[nd - 1, j] = d;

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
                            CountClick[nd - 1, j] = d;

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
                            CountClick[nd - 1, j] = d;

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
                            CountClick[nd - 1, j] = d;

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
                            CountClick[nd - 1, j] = d;

                        }
                        for (int i = 0; i < 4; i++)
                        {
                            string line = sr.ReadLine();
                            try
                            {
                                d = Convert.ToInt16(line);
                            }
                            catch (Exception ex) { MessageBox.Show(line + " " + i + " " + j); }
                            switch (i)
                            {
                                case 0: nd = 4; break;
                                case 1: nd = 11; break;
                                case 2: nd = 18; break;
                                case 3: nd = 25; break;
                            }
                            CountClick[nd - 1, j] = d;

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
                            CountClick[nd - 1, j] = d;

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

        static public void ReadTekChedule(string fileName)
        {



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
                Microsoft.Office.Interop.Excel.Range range = ObjWorkSheet.get_Range(11, 11);
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
                DateTime dt = ObjWorkSheet.get_Range(3, x).Text;
                int cc = ObjWorkSheet.get_Range(5, x).Text;
                int cs = ObjWorkSheet.get_Range(6, x).Text;
                HSS.Add(new hourSale(1, dt, t, dn, cc, cs));
                x++;
                range = ObjWorkSheet.get_Range(1, x);
                Application.DoEvents();
            }

            //Удаляем приложение (выходим из экселя) - ато будет висеть в процессах!
            ObjExcel.Quit();

        }



        static public string getStatus()
        {
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


        static public void WriteStatus()
        {

        }


        static public void ReadListShops()
        {

            String readPath = Environment.CurrentDirectory + @"\Shops.txt";
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
                        s=line.Split('_');
                        int idSh = Convert.ToInt16(s[0]);
                        string Sh = s[1];

                        listShops.Add(new Shop(idSh, Sh));
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                setListShops();

            }
        }



        static public void setListShops()
        {

            Shop h;
            var connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=VShleyev;Password=gjkrjdybr@93";
            string sql = "select * from get_shops() order by КодМагазина";

            using ( connection = new SqlConnection(connectionString))
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
                                sw.WriteLine(s.getIdShop() + "_" + s.getAddress());
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

        public static bool isConnect() { return connect; }

        public static void isConnected(string l, string p)
        {
            var connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=" + l + ";Password=" + p;

          //  connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=VShleyev;Password=gjkrjdybr@93";
            using (connection = new SqlConnection(connectionString))
            {
               try
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open) { connect= true; }
                    else { connect= false; }

                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    
                    MessageBox.Show(ex.Message);
                    connect = false;
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
