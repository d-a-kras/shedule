using schedule;
using schedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Models
{
    [Serializable]
    public class ForChart
    {
        public int[] X;
        public int[] Y;
        public DateTime Data;
        public int idShop;

        public ForChart(int id, DateTime d, int[] x, int[] y)
        {
            this.X = x;
            this.Y = y;
            this.Data = d;
            this.idShop = id;
        }
    }

    public class Smena
    {
        int idShop;
        int NStart;
        int NEnd;
        int Lenght;
        bool zanyta;
        DateTime Data;
        int Tip;
        public bool F2;

        public void Zanyta()
        {
            this.zanyta = true;
        }

        public bool isZanyta()
        {
            if (this.zanyta == true)
            {
                return true;
            }
            else { return false; }
        }

        public Smena(int id, DateTime dt, int start, int lenght)
        {
            this.idShop = id;
            this.NStart = start;
            this.Lenght = lenght;
            this.Data = dt;
            this.zanyta = false;
            this.Tip = 0;
            this.F2 = false;
        }
        static public void giveHoursSdvig(Smena sm1, Smena sm2, int x)
        {
            sm1.SetStarnAndLenght(sm1.getStartSmena(), sm1.Lenght - x);
            sm2.SetStarnAndLenght(sm2.getStartSmena() - x, sm2.getLenght() + x);
        }

        public void SetStarnAndLenght(int s, int l)
        {
            this.NStart = s;
            this.Lenght = l;
            this.NEnd = s + l;
        }

        public Smena(int start, int lenght, DateTime dt)
        {
            NStart = start;
            Lenght = lenght;
            Data = dt;
            this.Tip = 0;
        }
        public int getIdShop()
        {
            return this.idShop;
        }

        public int getTip()
        {
            return this.Tip;
        }

        public void setTip(int t)
        {
            this.Tip = t;
        }

        public void addChas(TemplateWorkingDay w)
        {
            if (this.getLenght() >= (w.DS.getLenghtDaySale() - 1)) { return; }
            if ((this.getEndSmena() == w.DS.getEndDaySale()) && (this.getStartSmena() == w.DS.getStartDaySale())) { return; }
            if (this.Tip != 2)
            {
                if (this.getEndSmena() == w.DS.getEndDaySale()) { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); return; }
                else if (this.getStartSmena() == w.DS.getStartDaySale()) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); return; }
                else if (this.getEndSmena() < w.DS.getEndDaySale()) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); return; }
                else if (this.getStartSmena() == (w.DS.getStartDaySale() + 1)) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); return; }


                else { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); }
            }
            else
            {
                if (this.getEndSmena() == w.DS.getEndDaySale())
                {
                    this.SetStarnAndLenght(this.getStartSmena() - 2, this.getLenght() + 1);

                    if (this.getStartSmena() <= w.DS.getStartDaySale())
                    {

                        this.SetStarnAndLenght(this.getStartSmena() + 2, this.getLenght());

                    }
                    return;
                }
                else if (this.getStartSmena() == w.DS.getStartDaySale())
                {
                    this.SetStarnAndLenght(this.getStartSmena() + 2, this.getLenght() + 1);
                    if (this.getEndSmena() >= w.DS.getEndDaySale())
                    {

                        this.SetStarnAndLenght(this.getStartSmena() - 2, this.getLenght());

                    }
                    return;
                }
                else if (this.getEndSmena() > w.DS.getEndDaySale() - 2) { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); return; }
                else if (this.getStartSmena() < (w.DS.getStartDaySale() + 2)) { this.SetStarnAndLenght(this.getStartSmena() + 1, this.getLenght() + 1); return; }
                else if (this.getEndSmena() < w.DS.getEndDaySale() - 2) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); return; }
                else if (this.getStartSmena() > (w.DS.getStartDaySale() + 2)) { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); return; }
                else if ((this.getEndSmena() < w.DS.getEndDaySale()) && (this.F2)) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); return; }
                else if ((this.getStartSmena() > w.DS.getStartDaySale()) && (this.F2)) { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); return; }

            }

        }

        public void addChas2(TemplateWorkingDay w)
        {
            if (this.getLenght() >= (w.DS.getLenghtDaySale() - 1)) { return; };
            if ((this.getEndSmena() == w.DS.getEndDaySale()) && (this.getStartSmena() == w.DS.getStartDaySale())) { return; }
            else if (this.getEndSmena() == w.DS.getEndDaySale()) { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); }
            else if (this.getStartSmena() == w.DS.getStartDaySale()) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); }
            else if (this.getStartSmena() == (w.DS.getStartDaySale() + 1)) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); }

            else { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); }

        }

        public void obedChas(TemplateWorkingDay w)
        {
            if ((this.getEndSmena() == w.DS.getEndDaySale()) && (this.getStartSmena() == w.DS.getStartDaySale())) { return; }
            else if (this.getEndSmena() == w.DS.getEndDaySale()) { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); }
            else if (this.getStartSmena() == w.DS.getStartDaySale()) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); }

            else { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); }

        }

        public void delChas(TemplateWorkingDay w)
        {
            if (this.Tip != 12)
            {
                if ((this.getEndSmena() == w.DS.getEndDaySale()) && (this.getStartSmena() == w.DS.getStartDaySale())) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() - 1); }
                else if (this.getEndSmena() == w.DS.getEndDaySale()) { this.SetStarnAndLenght(this.getStartSmena() + 1, this.getLenght() - 1); }
                else if (this.getStartSmena() == w.DS.getStartDaySale()) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() - 1); }
                else { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() - 1); }
            }
            else
            {
                if (this.getEndSmena() >= w.DS.getEndDaySale())
                {
                    this.SetStarnAndLenght(this.getStartSmena(), w.DS.getEndDaySale() - w.DS.getStartDaySale() - 1);
                }
                if ((this.getEndSmena() == w.DS.getEndDaySale()) && (this.getStartSmena() == w.DS.getStartDaySale())) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() - 1); this.setTip(13); }
                else if (this.getEndSmena() == w.DS.getEndDaySale()) { this.SetStarnAndLenght(this.getStartSmena() + 1, this.getLenght() - 1); this.setTip(13); }
                else if (this.getStartSmena() == w.DS.getStartDaySale()) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() - 1); this.setTip(13); }
                else { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() - 1); this.setTip(13); }
            }
        }



        public int getStartSmena()
        {
            return this.NStart;
        }

        public int getEndSmena()
        {
            if (this.NEnd == (this.getStartSmena() + this.getLenght()))
            {
                return this.NEnd;
            }
            else return this.getStartSmena() + this.getLenght();
        }
        public int getLenght()
        {
            return this.Lenght;
        }
        public DateTime getData()
        {
            return this.Data;
        }

        public int getDataTip()
        {
            int i = 0;
            // MessageBox.Show(this.getWeekday());
            switch (this.getData().DayOfWeek.ToString())
            {
                case "Monday": i = 1; break;
                case "Tuesday": i = 2; break;
                case "Wednesday": i = 3; break;
                case "Thursday": i = 4; break;
                case "Friday": i = 5; break;
                case "Saturday": i = 6; break;
                case "Sunday": i = 7; break;
                default: i = 8; break;
            }
            return i;
        }
    }

    public class mShop
    {
        private int idShop;
        private String address;
        public int getIdShop() { return idShop; }
        public string getAddress() { return address; }
        public mShop(int i, string a)
        {

            idShop = i;
            address = a;


        }
    }

    public class Prilavki
    {
        bool nalichie;
        int count;
        public Prilavki(bool n, int c)
        {
            this.nalichie = n;
            this.count = c;
        }

        public bool GetNalichie()
        {
            return this.nalichie;
        }

        public int GetCount()
        {
            return this.count;
        }

        public void SetNalichie(bool n)
        {
            this.nalichie = n;
        }

        public void SetCount(int c)
        {
            this.count = c;
        }
    }


    public class TSR
    {
        public string position;
        public string otobragenie;
        int tip;
        public int count;
        public int zarp;
        public int zarp1_2;

        public TSR(string p, string ot, int c, int z, int z1_2)
        {
            this.position = p;
            this.otobragenie = ot;
            this.count = c;
            this.zarp = z;
            this.zarp1_2 = z1_2;
        }

        public string getPosition()
        {
            return this.position;
        }

        public string getOtobragenie()
        {
            return this.otobragenie;
        }


        public int getCount()
        {
            return this.count;
        }

        public void setCount(int x)
        {
            this.count = x;
        }

        public int getZarp()
        {
            return this.zarp;
        }

        public void setZarp(int x)
        {
            this.zarp = x;
        }

        public int getZarp1_2()
        {
            return this.zarp1_2;
        }

        public void setZarp1_2(int x)
        {
            this.zarp1_2 = x;
        }

        public int getTip()
        {
            if (this.tip != 0)
            {
                return this.tip;
            }
            else
            {
                switch (this.position)
                {
                    case "kass1": this.tip = 1; return this.tip;
                    case "kass": this.tip = 1; return this.tip;
                    case "kass2": this.tip = 1; return this.tip;
                    case "kass3": this.tip = 1; return this.tip;
                    case "prod1": this.tip = 2; return this.tip;
                    case "prod": this.tip = 2; return this.tip;
                    case "prod2": this.tip = 2; return this.tip;
                    case "prod3": this.tip = 2; return this.tip;
                    case "gruz": this.tip = 3; return this.tip;
                    case "gastr": this.tip = 4; return this.tip;
                    default: return 0;
                }
            }
        }

    }

    [Serializable]
    public class daySale
    {
        public List<hourSale> hoursSale;
        DateTime Data;
        int idShop;
        int startDaySale;
        int endDaySale;
        int lenghtDaySale;
        int tip;

        public ForChart Chart;
        public ForChart ChartClick;
        public ForChart ChartCheck;


        public int getWeekDay()
        {

            int i = 0;
            // MessageBox.Show(this.getWeekday());
            switch (this.getData().DayOfWeek.ToString())
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

        public string getWeekDay2()
        {



            switch (this.getData().DayOfWeek.ToString())
            {
                case "Monday": return "Пн";
                case "Tuesday": return "Вт";
                case "Wednesday": return "Ср";
                case "Thursday": return "Чт";
                case "Friday": return "Пт";
                case "Saturday": return "Сб";
                case "Sunday": return "Вс";
                default: return "";
            }


        }

        public string getWeekDay3()
        {



            switch (this.getData().DayOfWeek.ToString())
            {
                case "Monday": return "понедельник";
                case "Tuesday": return "вторник";
                case "Wednesday": return "среда";
                case "Thursday": return "четверг";
                case "Friday": return "пятница";
                case "Saturday": return "суббота";
                case "Sunday": return "воскресенье";
                default: return "";
            }


        }


        public daySale(int id, DateTime d, int tipOfDay)
        {
            Data = d;
            idShop = id;
            hoursSale = new List<hourSale>();
            startDaySale = Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == d.ToShortDateString()).getTimeStart();
            endDaySale = Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == d.ToShortDateString()).getTimeEnd();
            tip = tipOfDay;
        }

        public daySale(int id, DateTime d)
        {
            this.Data = d;
            this.idShop = id;
            this.hoursSale = new List<hourSale>();
            if (Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == d.ToShortDateString()) != null)
            {
                this.startDaySale = Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == d.ToShortDateString()).getTimeStart();
                // MessageBox.Show("Start "+this.startDaySale);

                this.endDaySale = Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == d.ToShortDateString()).getTimeEnd();
            }
            // MessageBox.Show("END"+this.getData().ToShortDateString() + "");
        }

        public void whatTip()
        {
            this.setTip(Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == this.Data.ToShortDateString()).getTip());

        }

        public int getTip()
        {

            if (this.tip == 0)
            {
                if (Program.currentShop.DFCs.Find(t => t.getData().ToShortDateString() == this.getData().ToShortDateString()) != null)
                {
                    return Program.currentShop.DFCs.Find(t => t.getData().ToShortDateString() == this.getData().ToShortDateString()).getTip();
                }
                else
                {
                    switch (this.getData().DayOfWeek.ToString())
                    {
                        case "Monday": return 1; ;
                        case "Tuesday": return 2;
                        case "Wednesday": return 3;
                        case "Thursday": return 4;
                        case "Friday": return 5;
                        case "Saturday": return 6; ;
                        case "Sunday": return 7;
                        default: return 0;
                    }

                }
            }
            else return this.tip;
        }
        public void setTip(int t)
        {
            this.tip = t;
        }
        public void Add(hourSale hs)
        {
            this.hoursSale.Add(hs);
        }

        public int getIdShop()
        {
            return this.idShop;
        }

        public DateTime getData(int d = 0)
        {
            return this.Data.AddDays(d);
        }

        public int getStartDaySale()
        {


            return this.startDaySale;
        }

        public int getEndDaySale()
        {

            return this.endDaySale;

        }

        public int getLenghtDaySale()
        {
            //MessageBox.Show("lenght="+(this.getEndDaySale() - this.getStartDaySale()));
            return this.lenghtDaySale = this.getEndDaySale() - this.getStartDaySale();
        }

        public void CreateChartDaySale()
        {
            int[] x = new int[this.hoursSale.Count];
            int[] y = new int[this.hoursSale.Count];
            int i = 0;
            foreach (hourSale hs in this.hoursSale)
            {

                x[i] = int.Parse(hs.getNHour());
                //  MessageBox.Show(x[i]+"xi");
                y[i] = hs.getMinut();
                // MessageBox.Show(y[i] + "yi");
                i++;
            }
            this.Chart = new ForChart(getIdShop(), getData(), x, y);
        }

        public void CreateChartDaySaleClick()
        {
            int[] x = new int[this.hoursSale.Count];
            int[] y = new int[this.hoursSale.Count];
            int i = 0;
            foreach (hourSale hs in this.hoursSale)
            {

                x[i] = int.Parse(hs.getNHour());
                //  MessageBox.Show(x[i]+"xi");
                y[i] = hs.getCountClick();
                // MessageBox.Show(y[i] + "yi");
                i++;
            }
            this.ChartClick = new ForChart(getIdShop(), getData(), x, y);
        }

        public void CreateChartDaySaleCheck()
        {
            int[] x = new int[this.hoursSale.Count];
            int[] y = new int[this.hoursSale.Count];
            int i = 0;
            foreach (hourSale hs in this.hoursSale)
            {

                x[i] = int.Parse(hs.getNHour());
                //  MessageBox.Show(x[i]+"xi");
                y[i] = hs.getCountCheck();
                // MessageBox.Show(y[i] + "yi");
                i++;
            }
            this.ChartCheck = new ForChart(getIdShop(), getData(), x, y);
        }
    }

    public class PrognDaySale : daySale
    {
        private int tip;
        public List<hourSale> hss;
        public PrognDaySale(int id, DateTime d, int t) : base(id, d)
        {

            hss = new List<hourSale>();
            this.tip = t;
        }

        public new int getTip()
        { return this.tip; }
    }
    public class MinPers
    {
        public int Ncount;
        public int Tcount;
        public MinPers(int n, int t)
        {
            this.Ncount = n;
            this.Tcount = t;
        }

    }

    public class NormaChas
    {
        int Year;
        int NMonth;
        int CountChas;


        public void setCountChas(int c)
        {
            this.CountChas = c;
        }

        public NormaChas(int yy, int NM, int C)
        {
            this.Year = yy;
            this.NMonth = NM;
            this.CountChas = C;
        }

        public int getNormChas()
        {
            return this.CountChas;
        }

        public int getYear()
        {
            return this.Year;
        }

        public int getMonth()
        {
            return this.NMonth;
        }

        public bool CheckNorma()
        {
            if (this.CountChas > 176)
            {
                return false;
            }
            else return true;
        }

    }

    public class DataForCalendary
    {
        DateTime Data;
        public int Tip;
        int TimeBegin;
        int TimeEnd;

        public int getMonth() { return this.Data.Month; }

        public int getTimeStart() { return this.TimeBegin; }

        public int getTimeEnd()
        {

            return this.TimeEnd;
        }

        public int getLenght()
        {

            return (this.TimeEnd - this.getTimeStart());
        }

        public void setTimeBaE(int b, int e)
        {
            this.TimeBegin = b;
            this.TimeEnd = e;

        }

        public int setTip(int t)
        {
            return this.Tip = t;
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

        static public int OON(DateTime dfc)
        {
            DataForCalendary dt = new DataForCalendary(new DateTime(dfc.Year, dfc.Month, 1));

            return (dt.getNWeekday() - 1);
        }

        public int getNiM()
        {
            return this.Data.Day;
        }

        public int getNM()
        {
            return this.Data.Month;
        }

        /*static public bool isHolyday(DateTime mdt) {
            foreach (DateTime dt in Program.holydays) {
                int rez = DateTime.Compare(mdt, dt);

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

        }*/

        public DataForCalendary(DateTime Дата, int тип)
        {
            Data = Дата;
            Tip = тип;
        }

        public DataForCalendary(DateTime Дата)
        {
            Data = Дата;
            Tip = 0;
        }

        public DataForCalendary(DateTime D, int T, int b, int e)
        {
            Data = D;
            Tip = T;
            TimeBegin = b;
            TimeEnd = e;
        }

        public DateTime getData() { return this.Data; }

        public int gettip()
        {

            if (this.Tip == 0)
            {

                switch (this.getData().DayOfWeek.ToString())
                {
                    case "Monday": return 1; ;
                    case "Tuesday": return 2;
                    case "Wednesday": return 3;
                    case "Thursday": return 4;
                    case "Friday": return 5;
                    case "Saturday": return 6; ;
                    case "Sunday": return 7;
                    default: return 0;
                }


            }
            else return this.Tip;
        }



        public int getTip()
        {

            if (this.Tip == 0)
            {
                if (Program.currentShop.DFCs.Find(t => t.getData().ToShortDateString() == this.getData().ToShortDateString()) != null)
                {
                    return Program.currentShop.DFCs.Find(t => t.getData() == this.getData()).gettip();
                }
                else
                {
                    switch (this.getData().DayOfWeek.ToString())
                    {
                        case "Monday": return 1; ;
                        case "Tuesday": return 2;
                        case "Wednesday": return 3;
                        case "Thursday": return 4;
                        case "Friday": return 5;
                        case "Saturday": return 6; ;
                        case "Sunday": return 7;
                        default: return 0;
                    }

                }
            }
            else return this.Tip;
        }
        public string getWeekday() { return this.getData().DayOfWeek.ToString(); }
    }


    public class Factor
    {
        public string name;
        public string otobragenie;
        public int TZnach;
        public bool Deistvie;
        public DateTime Data;
        public int NewZnach;

        public Factor(string n, string ot, int TZ, bool D, DateTime ddd, int nz)
        {
            this.name = n;
            this.otobragenie = ot;
            this.TZnach = TZ;
            this.Deistvie = D;
            this.Data = ddd;
            this.NewZnach = nz;
        }

        public string getName()
        {
            return this.name;
        }
        public string getOtobragenie()
        {
            return this.otobragenie;
        }
        public DateTime getData()
        {
            return this.Data;
        }

        public int getTZnach()
        {
            return this.TZnach;
        }
        public int getNewZnach()
        {
            return this.NewZnach;
        }

        public bool getDeistvie()
        {
            return this.Deistvie;
        }

        public void setTZnach(int x)
        {
            this.TZnach = x;
        }

        public void setNewZnach(int x)
        {
            this.NewZnach = x;
        }

        public void setDeistvie(bool x)
        {
            this.Deistvie = x;
        }

        public void setData(DateTime x)
        {
            this.Data = x;
        }
    }

    public class VarSmen
    {

        bool Deistvie;
        int r;
        int v;
        int dlina;
        string dolgnost;

        public int getDlina()
        {
            if (this.dlina != 0)
            {
                return this.dlina;
            }
            else
            {
                switch (this.getR())
                {
                    case 3: this.dlina = 8; return this.dlina;
                    case 5: this.dlina = 6; return this.dlina;
                    case 2: this.dlina = 8; return this.dlina;
                    case 4: this.dlina = 8; return this.dlina;
                    case 6: this.dlina = 6; return this.dlina;
                    default: this.dlina = 6; return this.dlina;
                }


            }


        }

        public VarSmen(int rab, int vyh, bool d, string dolgnost1)
        {
            this.r = rab;
            this.v = vyh;
            this.Deistvie = d;
            this.dolgnost = dolgnost1;
        }

        public VarSmen(int rab, int vyh, int dl, bool d)
        {
            this.r = rab;
            this.v = vyh;
            this.dlina = dl;
            this.Deistvie = d;
        }

        public string getDolgnost()
        {
            return this.dolgnost;
        }

        public void setDolgnost(string d)
        {
            this.dolgnost = d;
        }

        public int getR()
        {
            return this.r;
        }


        public int getV()
        {
            return this.v;
        }

        public bool getDeistvie()
        {
            return this.Deistvie;
        }

        public void setR(int rr)
        {
            this.r = rr;
        }


        public void setV(int vv)
        {
            this.v = vv;
        }

        public void setDeistvie(bool b)
        {
            this.Deistvie = b;
        }

    }



    public class employee
    {
        int otrabotal;
        public int TipTekSmen;
        int IdShop;
        int IdEmployee;
        public int IdEmployee2;
        int status;
        int tip;
        VarSmen VS;
        int NormRab;
        string Dolgnost;
        string TipGraf;
        int otdih;
        int otdihinholyday;
        int tipsmen;
        public bool praz = false;

        public List<Smena> smens;
        public Dictionary<DateTime, int> statusDays;

        public int getTipSmen()
        {
            return this.tipsmen;
        }
        public void setTipSmen(int ts)
        {
            this.tipsmen = ts;
        }

        public int getOtdih()
        {
            return this.otdih;
        }
        public int getStatusDay(DateTime dt)
        {
            int status = 0;


            if (!this.statusDays.TryGetValue(dt, out status)) { status = 0; }


            return status;

        }

        public void otrabDay(DateTime dt)
        {

            this.statusDays.Add(dt, 1);

        }
        public int getOtdihInHolyday()
        {
            return this.otdihinholyday;
        }
        public void setOtdihInHolyday(int ot)
        {
            this.otdihinholyday = ot;
        }

        public void OtrabotalDay()
        {
            this.otrabotal += 1;
            if (this.otrabotal == this.VS.getR())
            {
                this.otrabotal = (-1) * this.VS.getV();
            }

        }
        public VarSmen getVS()
        {
            return this.VS;
        }
        public void setVS(VarSmen sm)
        {
            this.VS = sm;
        }
        public int getOtrabotal()
        {
            return this.otrabotal;
        }

        public int getID()
        {
            return this.IdEmployee;
        }

        public string getTipZan()
        {
            if (this.TipGraf != "")
            {
                return this.TipGraf;
            }
            else return "Сменный график";

        }

        public string getTipGraph()
        {
            return this.TipGraf;
        }

        public int GetTip()
        {
            if (this.tip != 0)
            {
                return this.tip;
            }
            else
            {
                switch (this.getID() / 100)
                {
                    case 0: this.tip = 1; return this.tip;
                    case 1: this.tip = 2; return this.tip;
                    case 2: this.tip = 3; return this.tip;
                    case 3: this.tip = 4; return this.tip;
                    default: return -1;
                }

            }
        }
        public employee(int ish, int ie, VarSmen vs, int otr, string d, string tgr)
        {
            this.IdShop = ish;
            this.tipsmen = 0;
            this.IdEmployee = ie;

            this.VS = vs;
            this.Dolgnost = d;
            this.TipGraf = tgr;
            this.otdihinholyday = 0;
            this.smens = new List<Smena>();
            this.statusDays = new Dictionary<DateTime, int>();
            int m = otr % (VS.getR() + VS.getV());

            if (m >= VS.getR())
            {
                this.otrabotal = m - (VS.getR() + VS.getV());
            }
            else
            {
                this.otrabotal = m;
            }
            this.otdih = 0;
        }



        public employee(int ish, int ie, string d, string tgr, int o)
        {
            this.IdShop = ish;
            this.IdEmployee = ie;

            this.tipsmen = 0;
            this.Dolgnost = d;
            this.TipGraf = tgr;
            this.otdihinholyday = 0;
            this.smens = new List<Smena>();
            this.otdih = o;
            this.statusDays = new Dictionary<DateTime, int>();


        }

        public employee(int ish, int ie, VarSmen vs, string d, string tgr, int o)
        {
            this.IdShop = ish;
            this.IdEmployee = ie;

            this.tipsmen = 0;
            this.VS = vs;
            this.Dolgnost = d;
            this.TipGraf = tgr;
            this.otdihinholyday = 0;
            this.smens = new List<Smena>();
            this.otdih = o;
            this.statusDays = new Dictionary<DateTime, int>();

        }

        public void setOtrabotal(int s)
        {

            this.otrabotal = s;
        }

        public string GetDolgnost()
        {
            if (this.Dolgnost != "")
            {
                return this.Dolgnost;
            }
            else
            {
                switch (this.tip)
                {
                    case 1: this.Dolgnost = "Кассир"; return this.Dolgnost;
                    case 2: this.Dolgnost = "Продавец"; return this.Dolgnost;
                    case 3: this.Dolgnost = "Грузчик"; return this.Dolgnost;
                    case 4: this.Dolgnost = "Гастроном"; return this.Dolgnost;

                    default: return "";
                }
            }
        }

        public void AddSmena(Smena sm)
        {
            this.PlusNormRab(sm.getLenght());

            this.smens.Add(sm);
        }




        public int getStatus()
        {
            return this.status;
        }

        public void setStatus(int s)
        {
            this.status = s;
        }

        public int getNormRab()
        {
            int summ = 0;
            if (this.smens.Count != 0)
            {
                foreach (Smena s in this.smens)
                {
                    if (s != null)
                        summ += s.getLenght();
                }
            }
            this.NormRab = summ;
            return this.NormRab;
        }

        public void setNormRab(int n)
        {
            this.NormRab = n;
        }
        public void PlusNormRab(int n)
        {
            this.NormRab += n;
        }
    }


    public class WorkingDay
    {

        int idShop;
        int startWorkingDay;
        int endWorkingDay;
        int Lenght;
        DateTime Data;
        public List<Smena> lss;
        int LenghtWorkingDay;
        int Tip;

        public int getTip()
        {
            return this.Tip;
        }



        public WorkingDay(int id, DateTime D, int start, int end)
        {
            this.idShop = id;
            this.Data = D;
            this.startWorkingDay = start;
            this.endWorkingDay = end;

            this.lss = new List<Smena>();
        }

        public int getStartWorkingDay()
        {
            return this.startWorkingDay;
        }

        public int getEndWorkingDay()
        {
            return this.endWorkingDay;
        }

        public void AddSmena(Smena s)
        {
            this.lss.Add(s);
        }

        public int getIdShop()
        {
            return this.idShop;
        }

        public DateTime getData()
        {
            return this.Data;
        }

        public int GetLenghtWorkingDay()
        {
            return this.Lenght;
        }

        public ForChart createChartTemplate()
        {

            int[] X = new int[LenghtWorkingDay];
            int[] Y = new int[LenghtWorkingDay];
            for (int j = 0, i = this.getStartWorkingDay(); i < getEndWorkingDay(); i++, j++)
            {
                X[j] = i;
            }
            foreach (Smena s in lss)
            {
                for (int i = 0; i < s.getLenght(); i++)
                {
                    Y[i]++;
                }
            }
            return new ForChart(this.getIdShop(), this.getData(), X, Y);
        }

        public int getWeekDay()
        {

            int i = 0;
            // MessageBox.Show(this.getWeekday());
            switch (this.getData().DayOfWeek.ToString())
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

    }

    public class TemplateWorkingDay
    {

        public daySale DS;
        public List<Smena> lss;
        public Dictionary<int, int> Raznica;
        public MinPers minGruzUtr;
        public MinPers minGruzVech;
        public MinPers minKassUtr;
        public MinPers minKassVech;
        public MinPers minProdUtr;
        public MinPers minProdVech;
        public MinPers minGastrUtr;
        public MinPers minGastrVech;


        public ForChart Chart;

        public void setMinCountSotr(int m)
        {
            this.minGruzUtr.Ncount = 1;
            this.minGruzVech.Ncount = 1;
            this.minKassUtr.Ncount = m;
            this.minProdUtr.Ncount = m;
            this.minKassVech.Ncount = m;
            if (m > 1)
            {
                this.minProdVech.Ncount = m - 1;
            }
            else
            {
                this.minProdVech.Ncount = 1;
            }
            this.minGastrUtr.Ncount = 1;
            this.minGastrVech.Ncount = 1;

        }

        public void setMinCountSotr(List<MinRab> minRabs)
        {
            this.minGruzUtr.Ncount = minRabs.First(t => t.getEmployeeType() == EmployeeType.Porter).getMinCount(); ;
            this.minGruzVech.Ncount = 1;
            this.minKassUtr.Ncount = minRabs.First(t => t.getEmployeeType() == EmployeeType.Cashier).getMinCount();
            this.minProdUtr.Ncount = minRabs.First(t => t.getEmployeeType() == EmployeeType.Seller).getMinCount();
            this.minKassVech.Ncount = minRabs.First(t => t.getEmployeeType() == EmployeeType.Cashier).getMinCount();
            int prodvech = minRabs.First(t => t.getEmployeeType() == EmployeeType.Seller).getMinCount();
            if (prodvech > 1)
            {
                this.minProdVech.Ncount = prodvech - 1;
            }
            else
            {
                this.minProdVech.Ncount = 1;
            }
            this.minGastrUtr.Ncount = 1;
            this.minGastrVech.Ncount = 1;

        }

        public void mMinCountKassUtr()
        {
            this.minKassUtr.Ncount -= 1;
        }

        public void mMinCountProdUtr()
        {
            this.minProdUtr.Ncount -= 1;
        }

        public void mMinCountKassVech()
        {
            this.minKassVech.Ncount -= 1;
        }
        public void mMinCountProdVech()
        {
            this.minProdVech.Ncount -= 1;
        }

        public void PereschetSmen()
        {
            int K;
            if (Program.currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
            {
                K = Program.currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
            }
            else
            {
                K = Program.KoefKassira;
            }
            foreach (employee emp in Program.currentShop.employes.FindAll(t => t.getStatus() == 2))
            {
                if (emp.smens.Count != 0)
                {
                    // if(emp.getNormRab()!=Program.normchas)
                    //MessageBox.Show(emp.getID()+" "+emp.smens.Count);
                    List<Smena> ls = emp.smens.FindAll(t => t.getData() == this.getData());
                    foreach (Smena s in ls)
                    {
                        if (s.getStartSmena() < this.DS.getStartDaySale()) { s.SetStarnAndLenght(this.DS.getStartDaySale(), s.getLenght()); }
                        for (int i = s.getStartSmena(); i < s.getEndSmena(); i++)
                        {
                            Raznica[i] = Raznica[i] - K;
                        }
                    }
                }

            }

        }

        public string getMonth()
        {
            //   MessageBox.Show(this.getData().Month+"");
            switch (this.getData().Month)
            {
                // case : return "";
                default: return "";
            }
        }

        public void CreateSmens2()
        {

        }

        public void CreateSmens()
        {

            int K;
            if (Program.currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
            {
                K = Program.currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
            }
            else { K = Program.KoefKassira; }
            createRazniza();
            this.lss.Clear();
            PereschetSmen();
            int lenght;
            int min = 100;
            int max = -1;
            while (checkRazn())
            {
                foreach (int k in this.Raznica.Keys)
                {
                    if (k > 0)
                    {
                        if (min > k)
                        {
                            min = k;

                        }
                        if (max < k)
                        {
                            max = k;
                        }
                    }
                }
                lenght = max - min;

                Smena sm = new Smena(min, lenght, this.DS.getData());
                this.lss.Add(sm);
                int EndSmena = sm.getStartSmena() + sm.getLenght() + 1;

                if (sm.getStartSmena() < this.DS.getStartDaySale()) { sm.SetStarnAndLenght(this.DS.getStartDaySale(), sm.getLenght()); }

                for (int i = sm.getStartSmena(); i < EndSmena; i++)
                {

                    this.Raznica[i] = this.Raznica[i] - K;


                }
            }
        }




        public bool checkRazn()
        {

            foreach (int k in this.Raznica.Keys)
            {
                if (Raznica[k] > 0)
                {
                    return true;
                }

            }

            return false;

        }


        public string GetWeekDay()
        {
            switch (this.DS.getData().DayOfWeek.ToString())
            {
                case "Monday": return "Понедельник";
                case "Tuesday": return "Вторник";
                case "Wednesday": return "Среда";
                case "Thursday": return "Четверг";
                case "Friday": return "Пятница";
                case "Saturday": return "Суббота";
                case "Sunday": return "Воскресенье";
                default: return "";
            }
        }

        public void NM()
        {
            this.minGruzUtr = new MinPers(0, 0);
            this.minGruzVech = new MinPers(0, 0);
            this.minKassUtr = new MinPers(0, 0);
            this.minKassVech = new MinPers(0, 0);
            this.minProdUtr = new MinPers(0, 0);
            this.minProdVech = new MinPers(0, 0);
            this.minGastrUtr = new MinPers(0, 0);
            this.minGastrVech = new MinPers(0, 0);
        }

        public TemplateWorkingDay(List<Smena> l, daySale d)
        {
            this.lss = l;
            this.DS = d;
            NM();
            setMinCountSotr(Program.currentShop.ListMinRab);


        }
        public TemplateWorkingDay(daySale d)
        {
            this.DS = d;
            this.lss = new List<Smena>();
            NM();
            setMinCountSotr(Program.currentShop.ListMinRab);


        }

        public int getCapacityPCC()
        {
            int s = 0;
            foreach (hourSale hs in this.DS.hoursSale)
            {
                s += hs.getTime();
            }
            return s;
        }

        public int getCapacity()
        {
            int cap = 0;
            int K;
            if (Program.currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
            {
                K = Program.currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
            }
            else { K = Program.KoefKassira; }

            foreach (Smena s in this.lss)
            {
                for (int j = 0, i = this.DS.getStartDaySale(); i < this.DS.getEndDaySale(); i++, j++)
                {
                    if ((i >= s.getStartSmena()) && (i <= s.getEndSmena()))
                    {
                        cap += K;

                    }
                }
            }
            return cap;
        }

        public int getClick()
        {
            int otc = 0;
            foreach (hourSale hs in this.DS.hoursSale)
            {
                otc += hs.getCountClick();
            }
            return otc;
        }





        public DateTime getData(int d = 0)
        {
            return this.DS.getData(d);
        }

        public void AddSmena(Smena smena)
        {
            this.lss.Add(smena);
        }

        public void M12()
        {
            foreach (Smena sm in this.lss)
            {
                if (sm.getLenght() > 12)
                {
                    lss.Add(new Smena(sm.getIdShop(), sm.getData(), sm.getStartSmena(), 7));
                    lss.Add(new Smena(sm.getIdShop(), sm.getData(), sm.getStartSmena() + 7, sm.getEndSmena()));
                    lss.Remove(sm);
                }
            }
        }

        public void createRazniza()
        {
            int K;

            if (Program.currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
            {
                K = Program.currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();

            }
            else { K = Program.KoefKassira; }

            if (this.Raznica == null)
            {
                int[] X = new int[this.DS.getLenghtDaySale()];
                int[] Y = new int[this.DS.getLenghtDaySale()];

                for (int j = 0, i = this.DS.getStartDaySale(); i < this.DS.getEndDaySale(); i++, j++)
                {
                    X[j] = i;
                }
                foreach (Smena s in lss)
                {
                    for (int j = 0, i = this.DS.getStartDaySale(); i < this.DS.getEndDaySale(); i++, j++)
                    {
                        if ((i >= s.getStartSmena()) && (i <= s.getEndSmena()))
                        {
                            Y[j] += K;
                            //   MessageBox.Show("Y"+j+" "+Y[j] + "X"+j+" "+X[j]);
                        }
                    }
                }
                this.Raznica = new Dictionary<int, int>();
                for (int i = 0; i < this.DS.getLenghtDaySale(); i++)
                {

                    this.Raznica.Add(X[i], Y[i]);

                }
            }
        }


        public void createChartTemplate()
        {

            int[] X = new int[this.DS.getLenghtDaySale()];
            int[] Y = new int[this.DS.getLenghtDaySale()];

            for (int j = 0, i = this.DS.getStartDaySale(); i < this.DS.getEndDaySale(); i++, j++)
            {
                X[j] = i;
            }
            foreach (Smena s in lss)
            {
                for (int j = 0, i = this.DS.getStartDaySale(); i < this.DS.getEndDaySale(); i++, j++)
                {
                    if ((i >= s.getStartSmena()) && (i <= s.getEndSmena()))
                    {
                        Y[j] += Program.KoefKassira;
                        //   MessageBox.Show("Y"+j+" "+Y[j] + "X"+j+" "+X[j]);
                    }
                }
            }
            this.Raznica = new Dictionary<int, int>();
            for (int i = 0; i < this.DS.getLenghtDaySale(); i++)
            {

                this.Raznica.Add(X[i], Y[i]);

            }
            this.Chart = new ForChart(DS.getIdShop(), getData(), X, Y);
        }

        public ForChart getChart()
        {
            return this.Chart;
        }

        static bool isEqual(TemplateWorkingDay a, TemplateWorkingDay b)
        {
            if (a.DS.getLenghtDaySale() == b.DS.getLenghtDaySale())
            {
                ForChart A = a.getChart();
                ForChart B = b.getChart();
                for (int i = 0; i < a.DS.getLenghtDaySale(); i++)
                {
                    if (A.X != B.Y) { return false; }
                }
                return true;
            }
            else return false;
        }
    }

}
