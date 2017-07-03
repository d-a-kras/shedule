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
using System.Net.Mail;

namespace shedule
{

    public class DataForCalendary {
        DateTime Data;
        int Tip;
        int TimeBegin;
        int TimeEnd;

        public int getTimeStart() { return this.TimeBegin; }

        public int getTimeEnd() {

            return this.TimeEnd; }

        public void setTimeBaE(int b, int e) {
            this.TimeBegin = b;
            this.TimeEnd = e;

        }

        public int setTip(int t) {
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

        static public int OON(DateTime dfc) {
            DataForCalendary dt = new DataForCalendary(new DateTime(dfc.Year, dfc.Month, 1));

            return (dt.getNWeekday() - 1);
        }

        public int getNiM() {
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

        public DataForCalendary(DateTime Дата, int тип) {
            Data = Дата;
            Tip = тип;
        }

        public DataForCalendary(DateTime Дата)
        {
            Data = Дата;
            Tip = 0;
        }

        public DataForCalendary(DateTime D, int T, int b, int e) {
            Data = D;
            Tip = T;
            TimeBegin = b;
            TimeEnd = e;
        }

        public DateTime getData() { return this.Data; }
        public int getTip()
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

        public Factor(string n, string ot, int TZ, bool D, DateTime ddd, int nz) {
            this.name = n;
            this.otobragenie = ot;
            this.TZnach = TZ;
            this.Deistvie = D;
            this.Data = ddd;
            this.NewZnach = nz;
        }
        
        public string getName(){
        	return this.name;
        }
        public string getOtobragenie(){
        	return this.otobragenie;
        }
        public DateTime getData(){
        	return this.Data;
        }
        
        public int getTZnach(){
        	return this.TZnach;
        }
		 public int getNewZnach(){
        	return this.NewZnach;
        }
        
        public bool getDeistvie(){
        	return this.Deistvie;
        }

        public void setTZnach(int x)
        {
            this.TZnach=x;
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

    public class VarSmen {
        public TipSmen ts;
        bool Deistvie;
        public VarSmen(TipSmen t, bool d) {
            this.ts = t;
            this.Deistvie = d;
        }

        static public void CreateTipSmen() {
            Program.currentShop.VarSmen.Clear();
            foreach (VarSmen vs in Program.currentShop.VarSmenBP) {
                if (vs.Deistvie)
                {
                    switch (vs.ts.getR())
                    {
                        case 5: Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 1)); Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 2)); Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 10)); Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 11)); break;
                        case 4: Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 3)); Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 4)); Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 12)); Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 13)); break;
                        case 2: Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 5)); Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 6)); Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 14)); Program.currentShop.VarSmen.Add(new TipSmen(vs.ts.getR(), vs.ts.getV(), 15)); break;
                    }
                }
               
            }
            Program.currentShop.VarSmen.Add(new TipSmen(0, 0, 0));
        }

        static public void CreateVarSmen()
        {
            Program.currentShop.VarSmenBP.Clear();
            Dictionary<int, bool> p = new Dictionary<int, bool>();
            foreach (TipSmen ts in Program.currentShop.VarSmen)
            {
                
                if (p.ContainsKey(ts.getR()))
                {
                    continue;
                }
                else
                {
                    switch (ts.getR())
                    {

                        case 5: Program.currentShop.VarSmenBP.Add(new VarSmen(ts, true));p.Add(ts.getR(),true); break;
                        case 4: Program.currentShop.VarSmenBP.Add(new VarSmen(ts, true)); p.Add(ts.getR(), true); break;
                        case 2: Program.currentShop.VarSmenBP.Add(new VarSmen(ts, true)); p.Add(ts.getR(), true); break;
                        case 0: p.Add(ts.getR(), true); break;
                    }
                }

            }
           
        }
    }

    public class TipSmen
    {
        int Tip;
        int b;
        int v;
        int DenVych;
        int srvden;
        bool deistvie;

        public void setR(int r)
        {
            this.b=r;
        }

        public void setV(int r)
        {
            this.v = r;
        }

        public void setDeistvie(bool r)
        {
            this.deistvie = r;
        }

        public bool getDeistvie()
        {
           return this.deistvie;
        }

        public int getR() {
            return this.b;
        }
        public int getV()
        {
            return this.v;
        }
        public TipSmen(int byd, int vych, int t)
        {
            this.b = byd;
            this.v = vych;
            this.Tip = t;
        }



        public void setDenVych(int dv)
        {
            this.DenVych = dv;
        }

        public int getSrednee()
        {
            switch (this.Tip)
            {
                case 0: return 6;
                case 5: return 10;
                case 6: return 10;
                case 4: return 11;
                case 3: return 11;
                case 1: return 8;
                case 2: return 8;
                default: return -1;
            }
        }
        public int getTip() {

            if (this.Tip != 0) {
                
                return this.Tip;
            }
            else return 0;
        }



    }

    public class employee
    {

        int IdShop;
        int IdEmployee;
        int status;
        int tip;
        TipSmen TipSm;
        int NormRab;
        int koef = 2;
        string Dolgnost;
        string TipGraf;
        public List<Smena> smens;

        public int getID() {
            return this.IdEmployee;
        }

        public string getTipZan() {
            if (this.TipGraf != "")
            {
                return this.TipGraf;
            }
            else return "Сменный график";

    }

        public string getTipGraph() {
            return this.TipGraf;
        }

        public int GetTip() {
            if (this.tip == 0)
            {
                return this.tip;
            }
            else {
                this.tip= this.TipSm.getTip();
                return this.tip; }
        }
        public employee(int ish, int ie, TipSmen ts, string d, string tgr)
        {
            this.IdShop = ish;
            this.IdEmployee = ie;
            this.TipSm = ts;
            this.Dolgnost = d;
            this.TipGraf = tgr;
            this.smens = new List<Smena>();
        }

        public employee(int ish, int ie, TipSmen ts)
        {
            this.IdShop = ish;
            this.IdEmployee = ie;
            this.TipSm = ts;
            this.smens = new List<Smena>();
        }

        public string GetDolgnost() {
            if (this.Dolgnost != "")
            {
                return this.Dolgnost;
            }
            else {
                switch (this.tip) {
                    case 1: this.Dolgnost = "Кассир 1"; return this.Dolgnost;
                    case 2: this.Dolgnost = "Кассир 1"; return this.Dolgnost;
                    case 3: this.Dolgnost = "Кассир 2"; return this.Dolgnost;
                    case 4: this.Dolgnost = "Кассир 2"; return this.Dolgnost;
                    case 5: this.Dolgnost = "Кассир 3"; return this.Dolgnost;
                    case 6: this.Dolgnost = "Кассир 3"; return this.Dolgnost;
                    case 7: this.Dolgnost = ""; return this.Dolgnost;
                    case 8: this.Dolgnost = ""; return this.Dolgnost;
                    case 9: this.Dolgnost = ""; return this.Dolgnost;
                    case 10: this.Dolgnost = "Продавец 1"; return this.Dolgnost;
                    case 11: this.Dolgnost = "Продавец 2"; return this.Dolgnost;
                    case 12: this.Dolgnost = "Продавец 3"; return this.Dolgnost;
                    case 0: this.Dolgnost = "Кассир 3"; return this.Dolgnost;
                    default: return "";
                }
            }
        }

        public void AddSmena(Smena sm) {
            this.PlusNormRab(sm.getLenght());
            this.smens.Add(sm);
        }

        public int getTip() {
            if (this.tip != 0) {
                return this.tip;
            }
            if (this.TipSm != null)
            {
                return this.TipSm.getTip();
            }
            return 0;
        }

        public int getStatus() {
            return this.status;
        }

        public void setStatus(int s) {
            this.status = s;
        }

        public int getNormRab() {
            int summ = 0;
            if (this.smens.Count!=0) {
                foreach (Smena s in this.smens) {
                    if(s!=null)
                    summ += s.getLenght();
                } }
            this.NormRab = summ;
            return this.NormRab;
        }

        public void setNormRab(int n) {
            this.NormRab = n;
        }
        public void PlusNormRab(int n) {
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

        public int getTip() {
            return this.Tip;
        }

        public WorkingDay(int id, int start, int end) {
            this.idShop = id;
            this.startWorkingDay = start;
            this.endWorkingDay = end;
            this.lss = new List<Smena>();
        }

        public WorkingDay(int id, DateTime D, int start, int end, int t)
        {
            this.idShop = id;
            this.Data = D;
            this.startWorkingDay = start;
            this.endWorkingDay = end;
            this.Tip = t;
            this.lss = new List<Smena>();
        }

        public int getStartWorkingDay() {
            return this.startWorkingDay;
        }

        public int getEndWorkingDay()
        {
            return this.endWorkingDay;
        }

        public void AddSmena(Smena s) {
            this.lss.Add(s);
        }

        public int getIdShop() {
            return this.idShop;
        }

        public DateTime getData() {
            return this.Data;
        }

        public int GetLenghtWorkingDay() {
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

        public ForChart Chart;

        public void PereschetSmen() {
            int K;
            if (Program.currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
            {
                 K = Program.currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
            }
            else
            {
                K = Program.KoefKassira;
            }
            foreach (employee emp in Program.currentShop.employes.FindAll(t=>t.getStatus()==2)) {
                if (emp.smens.Count != 0) {
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

        public string getMonth() {
         //   MessageBox.Show(this.getData().Month+"");
            switch (this.getData().Month) {
               // case : return "";
                default: return "";
            }
        }

        public void CreateSmens2() {

        }

        public void CreateSmens() {
            
            int K;
            if (Program.currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
            {
                 K = Program.currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
            }
            else {  K = Program.KoefKassira; }
            createRazniza();
            this.lss.Clear();
            PereschetSmen();
            int lenght;
            int min = 100;
            int max = -1;
            while (checkRazn()) {
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

                if (sm.getStartSmena()< this.DS.getStartDaySale()) { sm.SetStarnAndLenght(this.DS.getStartDaySale(),sm.getLenght()); }

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

        public TemplateWorkingDay(List<Smena> l, daySale d)
        {
            this.lss = l;
            this.DS = d;
        }
        public TemplateWorkingDay(daySale d)
        {
            this.DS = d;
            this.lss = new List<Smena>();


        }

        public int getCapacity() {
            int cap = 0;
            int K;
            if (Program.currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
            {
               K = Program.currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
            }
            else {  K = Program.KoefKassira; }

            foreach (Smena s in this.lss)
            {
                for (int j = 0, i = this.DS.getStartDaySale(); i < this.DS.getEndDaySale(); i++, j++)
                {
                    if ((i >= s.getStartSmena()) && (i <= s.getEndSmena())) {
                        cap += K;

                    }
                }
            }
            return cap;
        }

        public int getClick() {
            int otc=0;
            foreach (hourSale hs in this.DS.hoursSale ) {
                otc += hs.getCountClick();
            }
            return otc;
        }





        public DateTime getData() {
            return this.DS.getData();
        }

        public void AddSmena(Smena smena)
        {
            this.lss.Add(smena);
        }

        public void M12() {
            foreach (Smena sm in this.lss) {
                if (sm.getLenght() > 12) {
                    lss.Add(new Smena(sm.getIdShop(), sm.getData(), sm.getStartSmena(), 7));
                    lss.Add(new Smena(sm.getIdShop(), sm.getData(), sm.getStartSmena() + 7, sm.getEndSmena()));
                    lss.Remove(sm);
                }
            }
        }

        public void createRazniza() {
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
                            Y[j] +=K;
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
                    if ((i >= s.getStartSmena()) && (i <= s.getEndSmena())) {
                        Y[j] += Program.KoefKassira ;
                        //   MessageBox.Show("Y"+j+" "+Y[j] + "X"+j+" "+X[j]);
                    }
                }
            }
            this.Raznica = new Dictionary<int, int>();
            for (int i = 0; i < this.DS.getLenghtDaySale(); i++) {
         
                    this.Raznica.Add(X[i], Y[i]);
                
            }
            this.Chart = new ForChart(DS.getIdShop(), getData(), X, Y);
        }

        public ForChart getChart() {
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
        DateTime Data;

        public Smena(int id, DateTime dt, int start, int lenght) {
            this.idShop = id;
            this.NStart = start;
            this.Lenght = lenght;
            this.Data = dt;
        }
        static public void giveHoursSdvig(Smena sm1, Smena sm2, int x) {
            sm1.SetStarnAndLenght(sm1.getStartSmena(), sm1.Lenght - x);
            sm2.SetStarnAndLenght(sm2.getStartSmena() - x, sm2.getLenght() + x);
        }

        public void SetStarnAndLenght(int s, int l) {
            this.NStart = s;
            this.Lenght = l;
            this.NEnd = s + l;
        }

        public Smena(int start, int lenght, DateTime dt)
        {
            NStart = start;
            Lenght = lenght;
            Data = dt;
        }
        public int getIdShop() {
            return this.idShop;
        }

        public void addChas(TemplateWorkingDay w) {
            if (this.getEndSmena() == w.DS.getEndDaySale()) { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); }
            else { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); }

        }

        public int getStartSmena()
        {
            return this.NStart;
        }

        public int getEndSmena()
        {
            if (this.NEnd == (this.getStartSmena() + this.getLenght())) {
                return this.NEnd; }
            else return this.getStartSmena() + this.getLenght();
        }
        public int getLenght() {
            return this.Lenght;
        }
        public DateTime getData()
        {
            return this.Data;
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

        public class Shop
    {
        public List<WorkingDay> workingDays { get; set; }
        public List<employee> employes { get; set; }
        public List<TemplateWorkingDay> templates { get; set; }
        public List<daySale> daysSale { get; set; }
        public List<Factor> factors = new List<Factor>();
        public List<DataForCalendary> DFCs = new List<DataForCalendary>();
        public List<TSR> tsr = new List<TSR>();
        public List<daySale> MouthPrognoz = new List<daySale>();
        public List<TemplateWorkingDay> MouthPrognozT = new List<TemplateWorkingDay>();
        public List<TipSmen> VarSmen = new List<TipSmen>();
        public List<VarSmen> VarSmenBP = new List<VarSmen>();

        private int idShop;
        int idFM;
        private String address;
        public int getIdShop() { return idShop; }
        public int getIdShopFM() { return idFM; }
        public string getAddress() { return address; }



        public Shop(int i, string a, int i2)
        {

            this.idShop = i;
            this.address = a;
            this.idFM = i2;
            this.workingDays = new List<WorkingDay>();
            this.daysSale = new List<daySale>();
            this.employes = new List<employee>();
            this.factors = new List<Factor>();
            this.DFCs = new List<DataForCalendary>();
            this.templates = new List<TemplateWorkingDay>();
            Program.newShop();

        }

        public Shop(int i, string a) {
            
            idShop = i;
            address = a;
            this.workingDays = new List<WorkingDay>();
            this.daysSale = new List<daySale>();
            this.employes = new List<employee>();
            this.factors = new List<Factor>();
            this.DFCs = new List<DataForCalendary>();
            this.templates = new List<TemplateWorkingDay>();
            Program.newShop();
           
        }

        public void AddTemplate(TemplateWorkingDay t) {
            this.templates.Add(t);
        }

        public List<WorkingDay> getWorkingDays() {
            return this.workingDays;
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
            this.otobragenie=ot;
            this.count = c;
            this.zarp = z;
            this.zarp1_2 = z1_2;
        }
        
        public string getPosition(){
        	return this.position;
        }
        
        public string getOtobragenie(){
        	return this.otobragenie;
        }
        
        
        public int getCount(){
        	return this.count;
        }

        public void setCount(int x)
        {
             this.count=x;
        }

        public int getZarp(){
        	return this.zarp;
        }

        public void setZarp(int x)
        {
            this.zarp = x;
        }

        public int getZarp1_2(){
        	return this.zarp1_2;
        }

        public void setZarp1_2(int x)
        {
            this.zarp1_2 = x;
        }

        public int getTip(){
        	if(this.tip!=0){
        		return this.tip;
        	}
        	else {
        		switch(this.position){
        			case "kass1": this.tip=1;return this.tip;
        			case "kass2": this.tip=1;return this.tip;
        			case "kass3": this.tip=1;return this.tip;
        			case "prod1": this.tip=1;return this.tip;
        			case "prod2": this.tip=1;return this.tip;
        			case "prod3": this.tip=1;return this.tip;
                    default:  return 0;
        		}
        	}
        }
        
    }


    public class hourSale
    {

        private int idShop;
        private DateTime Data;
        private string weekday;
        private string NHour;
        private int countCheck;
        private int countClick;
        private double countTov;
        private int Minute;


        public void setClick(int c) {
            this.countClick = c;
        }

        public void setCheck(int ch) {
            this.countCheck = ch;
        }

        public hourSale(int idS, DateTime D, string NH, int countCh, int countCl)
        {
            this.idShop = idS;
            this.Data = D;

            this.NHour = NH;
            this.countCheck = countCh;
            this.countClick = countCl;

        }

        public hourSale(int idS, DateTime D, string NH, string dn, int countCh, int countCl)
        {
            this.idShop = idS;
            this.Data = D;
            this.weekday = dn;
            this.NHour = NH;
            this.countCheck = countCh;
            this.countClick = countCl;

        }

        public hourSale(int ids, DateTime D, string NH, int m) {
            this.idShop = ids;
            this.Data = D;
            this.NHour = NH;
            this.Minute = m;
        }

        public hourSale(int idS, DateTime D, string NH, string w, int countCh, int countCl, double ct)
        {
            this.idShop = idS;
            this.Data = D;
            this.weekday = w;
            this.NHour = NH;
            this.countCheck = countCh;
            this.countClick = countCl;
            this.countTov = ct;
        }

        public void setTime(int n)
        {
            this.Minute = n;

        }

        public int getIdShop() {
            return this.idShop;
        }

        public DateTime getData() { return this.Data; }

        public string getWeekday() { return this.weekday; }

        public string getNHour() { return this.NHour; }

        public int getCountClick() { return this.countClick; }

        public int getCountCheck() { return this.countCheck; }

        public int getMinut() {
            if (this.Minute == 0) {
                return (this.getCountCheck() * Program.TimeClick + this.getCountClick() * Program.TimeClick)*100 / Program.KoefKassira; }
            else return this.Minute;
        }


    }

    public class daySale {
        public List<hourSale> hoursSale;
        DateTime Data;
        int idShop;
        int startDaySale;
        int endDaySale;
        int lenghtDaySale;
        int tip;
        int DayWeek;
        public ForChart Chart;
        public ForChart ChartClick;
        public ForChart ChartCheck;


        public int getWeekDay() {

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

        public daySale(int id, DateTime d) {
            this.Data = d;
            this.idShop = id;
            this.hoursSale = new List<hourSale>();
            this.startDaySale = Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == d.ToShortDateString()).getTimeStart();
            // MessageBox.Show("Start "+this.startDaySale);

            this.endDaySale = Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == d.ToShortDateString()).getTimeEnd();
            // MessageBox.Show("END"+this.getData().ToShortDateString() + "");
        }

        public void whatTip() {
            //	Program.currentShop.DFCs.Find(x => x.getData()==d).getTip();

        }

        public int getTip() {
            if (this.tip != 0)
            {
                return this.tip;
            }
            else {
              return  Program.currentShop.DFCs.Find(t => t.getData() == this.getData()).getTip();
            }
        }
        public void setTip(int t) {
            this.tip = t;
        }
        public void Add(hourSale hs) {
            this.hoursSale.Add(hs);
        }

        public int getIdShop() {
            return this.idShop;
        }

        public DateTime getData() {
            return this.Data;
        }

        public int getStartDaySale() {


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

        public void CreateChartDaySale() {
            int[] x = new int[this.hoursSale.Count];
            int[] y = new int[this.hoursSale.Count];
            int i = 0;
            foreach (hourSale hs in this.hoursSale) {

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

    public class PrognDaySale : daySale {
        int tip;
        public List<hourSale> hss;
        public PrognDaySale(int id, DateTime d, int t) : base(id, d) {
        	
            hss = new List<hourSale>();
            this.tip = t;
        }

        public int getTip()
        { return this.tip; }
    }

    static class Program
    {
        static public List<Shop> shops;
        static public int normchas = 0;
        static public bool connect = false;
        static public SqlConnection connection;
         static public int CountSmen;
        static public bool SozdanPrognoz = false;
        static public List<mShop> listShops = new List<mShop>();
        static public int TipOptimizacii=0;
        static public int TipExporta=-1;


        // static public List<hourSale> HSS = new List<hourSale>();
        static public string CountObr = "";

      //  static public int[,] CountS = new int[25, 15];
      //   static public int[,] CountClick = new int[25, 15];
      //   static public int[,] CountCheck = new int[25, 15];
        static public List<DateTime> holydays = new List<DateTime>();
        static public int[] RD = new int[12];
        static public int[] PHD = new int[12];
    
        static public int KoefKassira = 50;
        static public int KoefObr = 20;
        static public int TimeClick = 4;
        static public int TimeRech = 25;
        static public int TimeObrTov=14;

        static public string login="";
        static public string password = "";
        static public int tipDiagram=0;
        
        

        static public Shop currentShop;
        static public short ParametrOptimization;
        static List<hourSale> SaleDay = new List<hourSale>();
        static List<hourSale> Raznica = new List<hourSale>();


        static public  void newShop() {
            RD = new int[12];
            PHD = new int[12];
        }
       
            static public void readTSR()
        {
                       String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\TSR";
           
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                	string [] str= new string[5];
                    string s;

                    while ((s= sr.ReadLine())!=null){

                		str = s.Split('#');
                		currentShop.tsr.Add(new TSR(str[0],str[1],int.Parse(str[2]), int.Parse(str[3]), int.Parse(str[4])));

                	      }


                }
            }
            catch (Exception ex)
            {
            	
            	currentShop.tsr.Add(new TSR("kass1","Кассир 1" ,4, 27000 , 14000 ));
                currentShop.tsr.Add(new TSR("kass2", "Кассир 2",4,25000,13000 ));
                currentShop.tsr.Add(new TSR("kass3", "Кассир 3",3,24000,12000 ));
                currentShop.tsr.Add(new TSR("prod1", "Продавец 1",4,25000,13000));
                currentShop.tsr.Add(new TSR("prod2", "Продавец 2",4,24000,12000 ));
                currentShop.tsr.Add(new TSR("prod3", "Продавец 3",2,23000,23000 ));

                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                   
                    foreach (TSR f in currentShop.tsr)
                    	sw.WriteLine(f.getPosition()+"#"+f.getOtobragenie()+"#"+f.getCount()+"#"+f.getZarp()+"#"+f.getZarp1_2());
                }
               // MessageBox.Show(ex.ToString());

            }

        }
         
        static public void WriteTSR(){
         		 String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\TSR";

            try
            {
                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {

                    foreach (TSR f in currentShop.tsr)
                        sw.WriteLine(f.getPosition() + "#" + f.getOtobragenie() + "#" + f.getCount() + "#" + f.getZarp() + "#" + f.getZarp1_2());
                }
            
            } catch (Exception ex)
            {
            	MessageBox.Show("Ошибка записи");
            }
         }
        
        
            
            
         static public void readFactors()
        {
                       String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\factors";
           
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    string s;

                    string [] str= new string[6];
                	while((s= sr.ReadLine())!=null){

                		str= s.Split('#');
                		currentShop.factors.Add( new Factor(str[0],str[1],int.Parse(str[2]),bool.Parse(str[3]), DateTime.Parse(str[4]), int.Parse(str[5])));

                	      }


                }
            }
            catch (Exception ex)
            {
            	
            	currentShop.factors.Add(new Factor("TimeClick","Время Клика" , 4,true, new DateTime(2100,1,1) , 0 ));
            	currentShop.factors.Add(new Factor("TimeRech","Голосовой интерфейс" , 25,true, new DateTime(2100,1,1) , 0 ));
            	currentShop.factors.Add(new Factor("TimeObrTov","Время на нелиннейные операции" , 14,true, new DateTime(2100,1,1) , 0 ));
                currentShop.factors.Add(new Factor("TimeObrTov", "Позиций в чеке", 5, true, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("KoefKassira","Коэффициент эффективности кассиров (%)" , 50,true, new DateTime(2100,1,1) , 0 ));
            	currentShop.factors.Add(new Factor("KoefObr","Коэффициент эффетивности продавцов (%)" , 20,true, new DateTime(2100,1,1) , 0 ));
            	currentShop.factors.Add(new Factor("Otkr_konkurenta","Открытие конкурента" , 4,false, new DateTime(2100,1,1) , 0 ));
                currentShop.factors.Add(new Factor("zakr_konkurenta", "Закрытие конкурента", 4, false, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("rost_reklam", "Реклама конкурента", 4, false, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("snig_reklam", "Собственная рекламная активность", 4, false, new DateTime(2100, 1, 1), 0));

                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                   
                    foreach (Factor f in currentShop.factors)
                    	sw.WriteLine(f.getName()+"#"+f.getOtobragenie()+"#"+f.getTZnach()+"#"+f.getDeistvie()+"#"+f.getData()+"#"+f.getNewZnach());
                }
               // MessageBox.Show(ex.ToString());

            }

        }
         
         static public void WriteFactors(){
         		 String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\factors";
           
            try
            {
            	 using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                   
                    foreach (Factor f in currentShop.factors)
                    	sw.WriteLine(f.getName()+"#"+f.getOtobragenie()+"#"+f.getTZnach()+"#"+f.getDeistvie()+"#"+f.getData()+"#"+f.getNewZnach());
                }
            } catch (Exception ex)
            {
            	MessageBox.Show("Ошибка записи");
            }
         }
        

        public static void OptimCountSotr()
        {
            if (!SozdanPrognoz) {
                MessageBox.Show("Прогноз не создан");
                return;

            }
            currentShop.employes.Clear();
            int K;
            if (currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
            {
               K = currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
            }
            else {  K = KoefKassira; }
            DateTime dt = DateTime.Today;
          //  normchas = Program.RD[dt.Month ] * 8 - Program.PHD[dt.Month ];
           // MessageBox.Show(normchas + " norm");
             normchas = 184;
            int ob = 0;
            int tc = 0;
            foreach (TemplateWorkingDay t in Program.currentShop.MouthPrognozT)
            {
                tc += t.getClick();
                ob += t.getCapacity();
            }
            
            int CountProd = ((tc * TimeObrTov * 100)/(normchas*3600*KoefObr)); 
            int CountKassirov = ((ob*100 / (normchas*K*3600))) + ParametrOptimization;
            //MessageBox.Show(Program.currentShop.MouthPrognozT.Count+" Количество продавцов=" +CountProd+" Количество кассиров=" + CountKassirov);
            if (CountKassirov < 8) { CountKassirov = 8; }
            if (CountProd < 8) { CountProd = 8; }

            for (int i = 100; i < 104; i++)
            {

                employee e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmen.Find(t => t.getTip() == 10), "Продавец 1", "Сменный график");

                currentShop.employes.Add(e);
                CountProd--;
            }

            for (int i = 104; i < 108; i++)
            {

                employee e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmen.Find(t => t.getTip() == 11), "Продавец 2", "Сменный график");

                currentShop.employes.Add(e);
                CountProd--;
            }

            for (int i = 108; CountProd>0; i++, CountProd--)
            {

                employee e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmen.Find(t => t.getTip() == 12), "Продавец 3", "Сменный график");

                currentShop.employes.Add(e);
                CountProd--;
            }


            for (int i = 0; i < 2; i++)
            {

                employee e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmen.Find(t => t.getTip() == 1), "Кассир 1", "Сменный график");

                currentShop.employes.Add(e);
            }
            for (int i = 2; i < 4; i++)
            {

                employee e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmen.Find(t => t.getTip() == 2), "Кассир 1", "Сменный график");

                currentShop.employes.Add(e);
            }
            for (int i = 4; i < 6; i++)
            {

                employee e = new employee(currentShop.getIdShop(), i, currentShop.VarSmen.Find(t => t.getTip() == 3), "Кассир 2", "Сменный график");

                currentShop.employes.Add(e);
            }

            for (int i = 6; i < 8; i++)
            {

                employee e = new employee(currentShop.getIdShop(), i, currentShop.VarSmen.Find(t => t.getTip() == 4), "Кассир 2", "Сменный график");

                currentShop.employes.Add(e);
            }

            int Ost = CountKassirov - 8;
          //  MessageBox.Show(Ost+"");
            if ((currentShop.VarSmen.Count > 5) && (Ost > 4))
            {


                employee e = new employee(currentShop.getIdShop(), 8, currentShop.VarSmen.Find(t => t.getTip() == 5), "Кассир 3", "Сменный график");

                currentShop.employes.Add(e);
                e = new employee(currentShop.getIdShop(), 9, currentShop.VarSmen.Find(t => t.getTip() == 6), "Кассир 3", "Сменный график");

                currentShop.employes.Add(e);
                Ost -= 2;
                for (int i = 10; Ost == 0; Ost--, i++)
                {
                    e = new employee(currentShop.getIdShop(), i, currentShop.VarSmen.Find(t => t.getTip() == 0), "Кассир 3", "Сменный график");
                    currentShop.employes.Add(e);
                }

            }
            else
            {
                
                for (int i = 8; Ost > 0; Ost--, i++)
                {
                    employee e = new employee(currentShop.getIdShop(), i, currentShop.VarSmen.Find(t => t.getTip() == 0) ,"Кассир 3", "Сменный график");

                    currentShop.employes.Add(e);
                  //  MessageBox.Show("Else");
                }
            }
          //  MessageBox.Show(currentShop.employes.Count.ToString());
        }

        static void Pereshet() {

            foreach (TemplateWorkingDay twd in currentShop.MouthPrognozT) {
                twd.CreateSmens();
                
                
            }

        }

        public static void writeVarSmen()
        {
            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\VarSmen";
           
            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {

                foreach (TipSmen ts in currentShop.VarSmen)
                    sw.WriteLine(ts.getR() + "#" + ts.getV() + "#" + ts.getTip());
            }

        }

        public static void readVarSmen()
        {
            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\VarSmen";
            // String readPath = Environment.CurrentDirectory + "/" + currentShop.getIdShop() + @"\varSmen.txt";
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {


                    string line;
                    string[] s = new string[3];

                    while ((line = sr.ReadLine()) != null)
                    {
                        s = line.Split('#');
                        currentShop.VarSmen.Add(new TipSmen(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2])));
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                currentShop.VarSmen.Add(new TipSmen(5, 2, 1));
                currentShop.VarSmen.Add(new TipSmen(5, 2, 2));
                currentShop.VarSmen.Add(new TipSmen(4, 3, 3));
                currentShop.VarSmen.Add(new TipSmen(4, 3, 4));
                currentShop.VarSmen.Add(new TipSmen(5, 2, 10));
                currentShop.VarSmen.Add(new TipSmen(4, 3, 11));
                currentShop.VarSmen.Add(new TipSmen(2, 2, 12));
                currentShop.VarSmen.Add(new TipSmen(0, 0, 0));

                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {

                    foreach (TipSmen ts in currentShop.VarSmen)
                        sw.WriteLine(ts.getR() + "#" + ts.getV() + "#" + ts.getTip());
                }

            }

        }


        public static bool CreateSmens()
        {
            if (currentShop.MouthPrognozT.Count == 0) {
                
                MessageBox.Show("Ошибка создания прогноза");
                return false;
            }
            List<employee> emplo = currentShop.employes.FindAll((t => (t.getTip() == 1) || (t.getTip() == 2)||(t.getTip() == 3) || (t.getTip() == 4)));
            foreach (employee emp in emplo)
            {
                int dlina = currentShop.VarSmen.Find(x => x.getTip() == emp.getTip()).getSrednee();

                foreach (TemplateWorkingDay wd in currentShop.MouthPrognozT)
                {
                    switch (emp.getTip())
                    {


                        case 1:
                            switch (wd.DS.getTip())
                            {
                                case 1: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                case 2: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                case 3: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                case 4: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                case 5: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                default: break;
                            }; break;
                        case 2:
                            switch (wd.DS.getTip())
                            {
                                case 1: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                case 2: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                case 3: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                case 4: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                case 5: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                default: break;
                            }; break;
                        case 3:
                            switch (wd.DS.getWeekDay())
                            {
                                case 4: emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() != wd.DS.getStartDaySale())); break;
                                case 5: emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() != wd.DS.getStartDaySale())); break;
                                case 6: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                case 7: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;

                                default: break;
                            }; break;
                        case 4:
                            switch (wd.DS.getWeekDay())
                            {
                                case 1: emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() != wd.DS.getStartDaySale())); break;
                                case 2: emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() != wd.DS.getStartDaySale())); break;
                                case 6: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                case 7: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                default: break;
                            }; break;
                        
                    }
                }
                emp.setStatus(2);

            }
            
            
            List<employee> le = new List<employee>();
            le = currentShop.employes.FindAll((t => (t.getTip() == 5) || t.getTip() == 6));
            if (le.Count != 0)
            {
              //  MessageBox.Show("5 & 6");
                foreach (employee emp in le)
                {
                   // Program.Pereshet();
                    foreach (TemplateWorkingDay wd in currentShop.MouthPrognozT)
                    {
                        switch (emp.getTip())
                        {

                            case 5:
                                {
                                    if ((wd.getData().Day % 4 == 1) || (wd.getData().Day % 4 == 2) && (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Count != 0))
                                    {
                                        emp.smens.Add(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss[0]); break;
                                    }
                                    break;
                                }
                            case 6:
                                if ((wd.getData().Day % 4 == 3)||(wd.getData().Day % 2 == 0) && (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Count != 0))
                                {
                                    emp.smens.Add(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss[0]); break;
                                }
                                break;
                            case 7:
                                if ((wd.getData().Day % 4 == 3) || (wd.getData().Day % 2 == 0) && (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Count != 0))
                                {
                                    emp.smens.Add(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss[0]); break;
                                }
                                break;
                            case 8:
                                if ((wd.getData().Day % 4 == 3) || (wd.getData().Day % 2 == 0) && (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Count != 0))
                                {
                                    emp.smens.Add(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss[0]); break;
                                }
                                break;
                            case 9:
                                if ((wd.getData().Day % 4 == 3) || (wd.getData().Day % 2 == 0) && (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Count != 0))
                                {
                                    emp.smens.Add(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss[0]); break;
                                }
                                break;
                        }
                    }


                    emp.setStatus(2);

                }
              
            }
            
            //Pereshet();

            le.Clear();
            le = currentShop.employes.FindAll((t => t.getTip() == 0));
           // MessageBox.Show(le.Count+" le");
            int c = 1;

            foreach (employee emp in le)
            {
              //  MessageBox.Show("0");
                //
                if (le.Count != 0)
                {

                    foreach (TemplateWorkingDay wd in currentShop.MouthPrognozT)
                    {
                        if ((c % le.Count == 0) || (c % le.Count == 1))
                        {
                            Smena s = currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getLenght() > 5) && (t.getLenght() < 9) && (t.getStartSmena() != wd.DS.getStartDaySale() && (t.getEndSmena() != wd.DS.getStartDaySale())));
                            if (s != null)
                            {
                              //  MessageBox.Show(s.getData() + " " + s.getStartSmena() + " " + s.getEndSmena());

                                emp.smens.Add(s);

                                currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Remove(s);
                            }
                            else{
                            	emp.smens.Add(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale()+3, 7));
                            	}
                            //  emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() != wd.DS.getStartDaySale())); break;
                        }
                        else {
                            Smena s = currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find((t=>t.getLenght() < 13) );
                            if (s != null)
                            {
                             //   MessageBox.Show(s.getData() + " " + s.getStartSmena() + " " + s.getEndSmena());

                                emp.smens.Add(s);

                                currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Remove(s);
                            }else{
                            		 emp.smens.Add(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale() + 3, 7));
                            }
                        }
                    c++;
                }
                        
                    
                }
               





                emp.setStatus(2);

            }
            le.Clear();
            le = currentShop.employes.FindAll((t => t.getStatus() == 2));
           
               // MessageBox.Show("2");
                foreach (employee emp in le)
                {

                    int k = 14;
                while (emp.getNormRab() > normchas)
                {
                   // MessageBox.Show("Больше");
                    if (emp.smens.Find(t => t.getLenght() > k) != null)
                    {
                        emp.smens.Remove(emp.smens.Find(t => t.getLenght() > k));
                    }
                    else
                        k--;


                }


                while (emp.getNormRab() < normchas)
                {
                    // MessageBox.Show("Меньше");
                    Smena s = emp.smens.Find(t=>t.getLenght()<6);
                    if (s != null)
                    { s.addChas(currentShop.MouthPrognozT.Find(f => f.DS.getData() == s.getData())); }
                    else
                    {
                        foreach (Smena sm in emp.smens)
                        {
                            if (sm != null)
                                // {
                                sm.addChas(currentShop.MouthPrognozT.Find(f => f.DS.getData() == sm.getData()));
                            // }else { MessageBox.Show(emp.smens.Count+"count  id=" +emp.getID()); }
                            if (emp.getNormRab() == normchas) break;
                        }
                    }

                }

                emp.setStatus(1);
                //MessageBox.Show("status " + emp.getID());
            }

            le.Clear();
            le = currentShop.employes.FindAll((t => (t.getTip() == 10)|| (t.getTip() == 11)|| (t.getTip() == 12)));
           // MessageBox.Show(le.Count+" =le");
            foreach (employee emp in le)
            {
                int dlina = 8;

                foreach (TemplateWorkingDay wd in currentShop.MouthPrognozT)
                {
                    switch (emp.getTip())
                    {


                        case 10:
                          //  MessageBox.Show("10");
                            if (emp.getID() % 2 == 0)
                            {
                                switch (wd.DS.getTip())
                                {
                                    case 1: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 2: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 3: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 4: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 5: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    default: break;
                                };
                            }
                            else{
                                switch (wd.DS.getTip())
                                {
                                    case 1: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 2: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 3: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 4: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 5: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    default: break;
                                }
                    }
                
                        break;
                        case 11:
                          //  MessageBox.Show("11");
                            if (emp.getID() % 2 == 0)
                            {
                                switch (wd.DS.getTip())
                                {
                                    case 2: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 3: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 8: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 5: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 6: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 7: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 9: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    default: break;
                                };
                            }
                            else
                            {
                                switch (wd.DS.getTip())
                                {
                                    case 1: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 4: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 9: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 8: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 2: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 6: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 7: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    default: break;
                                }
                            }

                            break;
                        case 12:
                         //   MessageBox.Show("12");
                            if (emp.getID() % 2 == 0)
                            {
                                switch (wd.DS.getTip())
                                {
                                    case 8: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 3: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 2: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 4: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                
                                    case 1: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 7: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 9: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    default: break;
                                };
                            }
                            else
                            {
                                switch (wd.DS.getTip())
                                {
                                    case 9: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 6: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 1: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 3: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 8: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 2: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                    case 5: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    case 7: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina)); break;
                                    default: break;
                                }
                            }

                            break;

                    };
                            
                              


                    
                }
                emp.setStatus(3);

               

                    int k = 14;
                    while (emp.getNormRab() > normchas)
                    {
                       //  MessageBox.Show("Больше");
                        if (emp.smens.Find(t => t.getLenght() > k) != null)
                        {
                            emp.smens.Remove(emp.smens.Find(t => t.getLenght() > k));
                        }
                        else
                            k--;


                    }


                    while (emp.getNormRab() < normchas)
                    {
                      //   MessageBox.Show("Меньше");
                        foreach (Smena sm in emp.smens)
                        {
                            sm.addChas(currentShop.MouthPrognozT.Find(f => f.DS.getData() == sm.getData()));

                            if (emp.getNormRab() == normchas) break;
                        }

                    }

                    emp.setStatus(1);
                   // MessageBox.Show("status " + emp.getID());
                


            }
            itogChass();
            return true;
 }
        public static void itogChass() {
            foreach (employee e in currentShop.employes) {

                foreach (TemplateWorkingDay twd in currentShop.MouthPrognozT) {
                    if (e.smens.Find(t => t.getData() == twd.getData())!=null) {
                        e.smens.Find(t => t.getData() == twd.getData()).addChas(twd);
                    }
                }
            }
        }


        public static void createPrognoz(){
            currentShop.MouthPrognoz.Clear();
            currentShop.MouthPrognozT.Clear();
        	DateTime ydt= DateTime.Now.AddDays(-1);
            DateTime tdt = DateTime.Today;
            List<PrognDaySale> PDSs= new List<PrognDaySale>();
            DateTime d2 = DateTime.Now.AddDays(-30);

            
        	currentShop.daysSale.Clear();
        	SozdanPrognoz= createListDaySale(d2,ydt);
        	
        	foreach(daySale ds in currentShop.daysSale ){
        		ds.setTip(currentShop.DFCs.Find(x => x.getData().ToShortDateString()==ds.getData().ToShortDateString()).getTip());
        		
        			
        		}

        	for(int i=0; i<10;i++){
                PDSs.Add(new PrognDaySale(currentShop.getIdShop(),tdt, i));
        		foreach(daySale ds in currentShop.daysSale){
        			if(ds.getTip()==i){
        			foreach(hourSale hs in ds.hoursSale ){

        					PDSs.Find(t => t.getTip()==i).hss.Add(hs);
                           

                    }
        		}
        	}
        	
        }
            DateTime fd;

            if (tdt.Month!=12){
        		 fd = new DateTime(tdt.Year,tdt.Month+1,1);
        	}
        	else{
        		 fd = new DateTime(tdt.Year+1,1,1);
        	}
        	
        	
        	foreach(PrognDaySale pds in PDSs){

        		for(int i=7;i<24;i++){
        		    
        			int Sclick=0;
        			int Scheck=0;
                    List<hourSale> h = pds.hss.FindAll(t => t.getNHour() == i.ToString());
                    if (h.Count != 0)
                    {
                        foreach (hourSale hs in h)
                        {
                            Sclick += hs.getCountClick();
                            Scheck += hs.getCountCheck();
                        }
                       
                        pds.hoursSale.Add(new hourSale(currentShop.getIdShop(),h[0].getData(),h[0].getNHour(),Sclick,Scheck));
                    }
        		}
        		
        		
        	}

            
        
                for (int i = 1; i <= DateTime.DaysInMonth(fd.Year, fd.Month); i++) {

                    daySale d = new daySale(currentShop.getIdShop(), new DateTime(fd.Year, fd.Month, i));
                    d.whatTip();
                    d.hoursSale = PDSs.Find(t => t.getTip() == d.getTip()).hoursSale;
                    currentShop.MouthPrognoz.Add(d);

                }
            
        	
        	foreach(daySale ds in currentShop.MouthPrognoz){
        		createPrognozTemplate(ds);
        	}
        
        	
        }

        public static void createPrognoz3()
        {
            currentShop.MouthPrognoz.Clear();
            currentShop.MouthPrognozT.Clear();
            DateTime ydt = DateTime.Now.AddDays(-1);
            DateTime tdt = DateTime.Today;
            List<PrognDaySale> PDSs = new List<PrognDaySale>();
            DateTime d2 = DateTime.Now.AddDays(-30);


            currentShop.daysSale.Clear();
            createListDaySale(d2, ydt);

            foreach (daySale ds in currentShop.daysSale)
            {
                ds.setTip(currentShop.DFCs.Find(x => x.getData().ToShortDateString() == ds.getData().ToShortDateString()).getTip());


            }

            for (int i = 0; i < 10; i++)
            {
                PDSs.Add(new PrognDaySale(currentShop.getIdShop(), tdt, i));
                foreach (daySale ds in currentShop.daysSale)
                {
                    if (ds.getTip() == i)
                    {
                        foreach (hourSale hs in ds.hoursSale)
                        {

                            PDSs.Find(t => t.getTip() == i).hss.Add(hs);


                        }
                    }
                }

            }
            


            foreach (PrognDaySale pds in PDSs)
            {

                for (int i = 7; i < 24; i++)
                {

                    int Sclick = 0;
                    int Scheck = 0;
                    List<hourSale> h = pds.hss.FindAll(t => t.getNHour() == i.ToString());
                    if (h.Count != 0)
                    {
                        foreach (hourSale hs in h)
                        {
                            Sclick += hs.getCountClick();
                            Scheck += hs.getCountCheck();
                        }

                        pds.hoursSale.Add(new hourSale(currentShop.getIdShop(), h[0].getData(), h[0].getNHour(), Sclick, Scheck));
                    }
                }


            }

            DateTime fd1;
            DateTime fd2;
            DateTime fd3;

            if (tdt.Month < 10)
            {
                fd1 = new DateTime(tdt.Year, tdt.Month + 1, 1);
                fd2 = new DateTime(tdt.Year, tdt.Month + 1, 1);
                fd3 = new DateTime(tdt.Year, tdt.Month + 1, 1);
            }
            else
            {   if (tdt.Month == 10)
                {
                    fd1 = new DateTime(tdt.Year, 11, 1);
                    fd2 = new DateTime(tdt.Year, 12, 1);
                    fd3 = new DateTime(tdt.Year+1, 1, 1);
                }
                else if (tdt.Month == 11)
                {
                    fd1 = new DateTime(tdt.Year, 12, 1);
                    fd2 = new DateTime(tdt.Year+1, 1, 1);
                    fd3 = new DateTime(tdt.Year+1, 2, 1);
                }
                else if (tdt.Month == 12)
                {
                    fd1 = new DateTime(tdt.Year+1, 1, 1);
                    fd2 = new DateTime(tdt.Year+1, 2, 1);
                    fd3 = new DateTime(tdt.Year+1, 3, 1);
                }
                else {

                    fd1 = new DateTime(tdt.Year, tdt.Month + 1, 1);
                    fd2 = new DateTime(tdt.Year, tdt.Month + 1, 1);
                    fd3 = new DateTime(tdt.Year, tdt.Month + 1, 1);
                    MessageBox.Show("Ошибка построения прогноза");
                }
            }

            for (int i = 1; i <= DateTime.DaysInMonth(fd1.Year, fd1.Month); i++)
            {

                daySale d = new daySale(currentShop.getIdShop(), new DateTime(fd1.Year, fd1.Month, i));
                d.whatTip();
                d.hoursSale = PDSs.Find(t => t.getTip() == d.getTip()).hoursSale;
                currentShop.MouthPrognoz.Add(d);

            }

            for (int i = 1; i <= DateTime.DaysInMonth(fd2.Year, fd2.Month); i++)
            {

                daySale d = new daySale(currentShop.getIdShop(), new DateTime(fd2.Year, fd2.Month, i));
                d.whatTip();
                d.hoursSale = PDSs.Find(t => t.getTip() == d.getTip()).hoursSale;
                currentShop.MouthPrognoz.Add(d);

            }
            for (int i = 1; i <= DateTime.DaysInMonth(fd3.Year, fd3.Month); i++)
            {

                daySale d = new daySale(currentShop.getIdShop(), new DateTime(fd3.Year, fd3.Month, i));
                d.whatTip();
                d.hoursSale = PDSs.Find(t => t.getTip() == d.getTip()).hoursSale;
                currentShop.MouthPrognoz.Add(d);

            }



            foreach (daySale ds in currentShop.MouthPrognoz)
            {
                createPrognozTemplate(ds);
            }


        }




        static public bool createListDaySale( DateTime n, DateTime k){
			
            var connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=VShleyev;Password=gjkrjdybr@93";
            string s1 = n.Year + "/" + n.Day + "/" + n.Month;
            string s2=k.Year+"/" + k.Day+"/" +k.Month;
            string sql;
            if (currentShop.getIdShop() == 0) {sql = "select * from dbo.get_StatisticByShopsDayHour('" + Program.currentShop.getIdShopFM() + "', '" + s1 + "', '" + s2 + " 23:59:00')"; }
            else {  sql = "select * from dbo.get_StatisticByShopsDayHour('" + Program.currentShop.getIdShop() + "', '" + s1 + "', '" + s2 + " 23:59:00')"; }
            //string sql = "select * from dbo.get_StatisticByShopsDayHour('301','17/01/01', '2017/01/20 23:59:00')";
            //string sql = "select * from dbo.get_StatisticByShopsDayHour('103','2017/05/01', '2017/15/09 23:59:00')";
            //MessageBox.Show(sql);
			List<hourSale> hss =new List<hourSale>();
			daySale ds;
			
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 300;
                    SqlDataReader reader = command.ExecuteReader();
                    

                    while (reader.Read())
                    {
                        hourSale h = new hourSale(reader.GetInt16(0), reader.GetDateTime(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                        
						hss.Add(h);

                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show("Ошибка соединения с базой данных " + ex.Message);
                    return false;
                }
                
            }
			//посчитать количество дней 
			 TimeSpan ts = k - n;
            
			DateTime d = n;

			for(int i=0;i<=ts.Days+1;i++){
				
				 ds = new daySale(Program.currentShop.getIdShop(),d);
				currentShop.daysSale.Add(ds);
				d = d.AddDays(1.0d);
			}
           // MessageBox.Show("Количество дней по ts " + ts.Days.ToString());
          //  MessageBox.Show("Количество часов "+hss.Count.ToString());
			foreach(hourSale hs in hss){


                currentShop.daysSale.Find(x => x.getData().ToShortDateString() == hs.getData().ToShortDateString()).Add(hs);
				
			}


            return true;
        } 
		
		/*static List<ForChart> createSaleForChart(){
			
			
			foreach(daySale ds in currentShop.daysSale){
				foreach(hourSale hs in ds.hoursSale){
					
				}
			}
			
		}*/
		
		static void readTemplateForShop(){
			 String readPath = Environment.CurrentDirectory + "/Shops"+currentShop.getIdShop()+"/Templates.txt";
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {

                    string line;
                    string[] s = new string[4];
					TemplateWorkingDay twd;
                    while ((line = sr.ReadLine()) != null) {
                        daySale wd = new daySale(int.Parse(sr.ReadLine()), DateTime.Parse(sr.ReadLine()));
						twd=new TemplateWorkingDay(wd);
						while((line = sr.ReadLine()) != "*****"){
							s = line.Split('#');
							Smena sm= new Smena(int.Parse(s[0]),DateTime.Parse(s[1]) ,int.Parse(s[2]),int.Parse(s[3]));
							twd.AddSmena(sm);
						}
                        currentShop.AddTemplate(twd);
                        
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
		}

        static void writeTemplateForShop() {
            String readPath = Environment.CurrentDirectory + "/Shops" + currentShop.getIdShop() + "/Templates.txt";
            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {
                foreach (TemplateWorkingDay t in currentShop.templates) {
                    sw.WriteLine(t.DS.getIdShop());
                    sw.WriteLine(t.getData());
                    sw.WriteLine(t.DS.getStartDaySale());
                    sw.WriteLine(t.DS.getEndDaySale());
                   
                    foreach (Smena s in t.lss) {
                        sw.WriteLine(t.DS.getIdShop() + "#" + t.getData() + "#" + s.getStartSmena() + "#" + s.getLenght());
                    }
                    sw.WriteLine("*****");
                }
            } }

        static public List<hourSale> createDaySale(int idShop, DateTime dt)
        {
            var connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=VShleyev;Password=gjkrjdybr@93";
            string sql = "select * from dbo.get_StatisticByShopsDayHour('301', '2017/01/02', '2017/01/04 23:59:00')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 300;
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        hourSale h = new hourSale(reader.GetInt16(0), reader.GetDateTime(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                        SaleDay.Add(h);

                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show("Ошибка соединения с базой данных" + ex);
                }
            
          
                return Raznica = SaleDay;
            }
        }

        static public Smena OptimRec(DateTime data)
        {

            int lenght;
            int min = 100;
            int max = -1;
            foreach (hourSale hs in Raznica)
            {
                if (hs.getMinut() > 0)
                {
                    if (min > int.Parse(hs.getNHour()))
                    {
                        min = int.Parse(hs.getNHour());

                    }
                    if (max < int.Parse(hs.getNHour()))
                    {
                        max = int.Parse(hs.getNHour());
                    }
                }
            }
            lenght = max - min;
          // MessageBox.Show("Nachalo="+min+ " Dlina="+lenght );
            return new Smena(min, lenght, data);
        }

       static public Smena addRecl(Smena sm)
        {
            int K;
            if (currentShop.factors.Find(t => t.getName() == "KoefKassira")!=null)
            {
                 K = currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
            }
            else {  K = KoefKassira; }
            int EndSmena = sm.getStartSmena()+sm.getLenght()+1;
           
            for (int i = sm.getStartSmena(); i < EndSmena; i++)
            {

                hourSale temp = Raznica.Find(x => x.getNHour() == i.ToString());
               // MessageBox.Show("temp1="+temp.getNHour()+" "+temp.getMinut());
                if (!(temp == null))
                {
                    int t=temp.getMinut() - K;
                    Raznica.Add(new hourSale(currentShop.getIdShop(), sm.getData(), i.ToString(), t));
                   // MessageBox.Show("Count Razniza=" + Raznica.Count);
                    Raznica.Remove(temp);
                   // MessageBox.Show("temp2=" + temp.getNHour()+" " + t+" Count Razniza=" + Raznica.Count);
                }
                else { Raznica.Add( new hourSale(currentShop.getIdShop(),sm.getData(),i.ToString(),0)); }
                
                
               // MessageBox.Show(t.ToString());
               // Raznica.Remove(Raznica.Find(x => x.getNHour() == i.ToString())) ;
               // Raznica.Add(new hourSale(temp.getIdShop(), temp.getData(), temp.getNHour(), t));

            }
            return sm;
        }


        static bool checkGraph()
        {
           // MessageBox.Show("Razniza="+Raznica.Count.ToString());
            foreach (hourSale hs in Raznica)
            {
              // MessageBox.Show("разница="+ hs.getNHour()+" " + hs.getMinut().ToString());

                if (hs.getMinut() > 0)
                {
                   // MessageBox.Show("true");
                    return true;
                   
                }



            } 

            return false;
               
            
        }

        static public void createTemplate( daySale ds)
        {
            //createDaySale(id, data);
            //DateTime data2= data.AddDays(10.0d);
            int id = currentShop.getIdShop();
            //createListDaySale(data,data2);
            
            Raznica = new List<hourSale> (ds.hoursSale);
            
           
            TemplateWorkingDay twd = new TemplateWorkingDay(ds);
            twd.AddSmena(addRecl(new Smena(twd.DS.getStartDaySale(), twd.DS.getLenghtDaySale(), ds.getData())));
            twd.AddSmena(addRecl(new Smena(twd.DS.getStartDaySale(), twd.DS.getLenghtDaySale(), ds.getData())));
            while (checkGraph())
            {
                
                twd.AddSmena(addRecl(OptimRec(ds.getData())));
             //   MessageBox.Show("Количество смен="+twd.lss.Count);
                
            }

            currentShop.templates.Add(twd);
            //MessageBox.Show("Шаблон магазина" + id + "за дату" + ds.getData() + "создан");
          
         
        }

        static public void createPrognozTemplate(daySale ds)
        {
            //createDaySale(id, data);
            //DateTime data2= data.AddDays(10.0d);
            int id = currentShop.getIdShop();
            //createListDaySale(data,data2);

            Raznica = new List<hourSale>(ds.hoursSale);


            TemplateWorkingDay twd = new TemplateWorkingDay(ds);
            twd.AddSmena(addRecl(new Smena(twd.DS.getStartDaySale(), twd.DS.getLenghtDaySale(), ds.getData())));
            twd.AddSmena(addRecl(new Smena(twd.DS.getStartDaySale(), twd.DS.getLenghtDaySale(), ds.getData())));
            while (checkGraph())
            {

                twd.AddSmena(addRecl(OptimRec(ds.getData())));
                //   MessageBox.Show("Количество смен="+twd.lss.Count);

            }

            currentShop.MouthPrognozT.Add(twd);
            //MessageBox.Show("Шаблон магазина" + id + "за дату" + ds.getData() + "создан");


        }

        static public void refreshFoldersShops() {
            foreach (mShop shop in Program.listShops) {
                string pyth= Environment.CurrentDirectory +"/Shops/"+ shop.getIdShop().ToString();
                if (!Directory.Exists(pyth)) {
                    Directory.CreateDirectory(pyth);
                }

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
            currentShop.DFCs.Clear();
            string readPath = Environment.CurrentDirectory+ @"\Shops\"+currentShop.getIdShop() +@"\Calendar";
           // MessageBox.Show(readPath);
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    string line;
                    string[] s = new string[4];
                   
                    while ((line = sr.ReadLine()) != null) {
                        s = line.Split('#');
                        DataForCalendary d = new DataForCalendary(DateTime.Parse( s[0]),int.Parse(s[1]),int.Parse(s[2]),int.Parse(s[3]));
                       // MessageBox.Show(DateTime.Parse(s[0]).Month.ToString());
                        switch (int.Parse(s[1])) {
                            case 1: RD[DateTime.Parse(s[0]).Month-1]++; break;
                            case 2: RD[DateTime.Parse(s[0]).Month-1]++; break;
                            case 3: RD[DateTime.Parse(s[0]).Month-1]++; break;
                            case 4: RD[DateTime.Parse(s[0]).Month-1]++; break;
                            case 5: RD[DateTime.Parse(s[0]).Month-1]++; break;
                          
                            case 9: PHD[DateTime.Parse(s[0]).Month-1]++; break;
                       
                        }
                        
                        currentShop.DFCs.Add(d);
                    }
                 //   MessageBox.Show("DFCs.Add 1");
                }

                

            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
                
                Program.ReadCalendarFromXML(DateTime.Today.Year );
                for (int i = 1; i <= 12; i++)
                {
                    RD[i - 1] = 0;
                    PHD[i - 1] = 0;
                    int countDays = DateTime.DaysInMonth(year, i);
                    for (int k = 1; k <= countDays; k++)
                    {
                        DataForCalendary dfc = new DataForCalendary(new DateTime(year, i, k));
                        int t = dfc.getTip();
                        if ((t == 1)||(t==2)||(t==3)||(t==4)||(t==5)) { RD[i - 1]++; }
                        if (t == 9) { PHD[i - 1]++; }

                        if (currentShop.DFCs.Find(x =>x.getData()==dfc.getData())!=null) {
                            
                        }else currentShop.DFCs.Add(dfc);

                    }
                }
              //  MessageBox.Show("DFCs.Add ex");
                foreach (DataForCalendary dfc in currentShop.DFCs)
                {
                    switch (dfc.getTip())
                    {
                        case 1: dfc.setTimeBaE(8, 23); break;
                        case 2: dfc.setTimeBaE(8, 23); break;
                        case 3: dfc.setTimeBaE(8, 23); break;
                        case 4: dfc.setTimeBaE(8, 23); break;
                        case 5: dfc.setTimeBaE(8, 23); break;
                        case 6: dfc.setTimeBaE(9, 22); break;
                        case 7: dfc.setTimeBaE(9, 22); break;
                        case 8: dfc.setTimeBaE(9, 22); break;
                        case 9: dfc.setTimeBaE(9, 22); break;
                        case 10: dfc.setTimeBaE(9, 22); break;
                        default: dfc.setTimeBaE(0, 0); break;
                    }
                }

                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                   
                    foreach (DataForCalendary d in currentShop.DFCs)
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
            months[1] = "Февраль";
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


        static public string getMonths(int m)
        {
            switch (m) {

                case 1: return "Январь";
                case 2:
                    return  "Февраль";
                case 3:
                    return  "Март";
                case 4:
                    return  "Апрель";
                case 5:
                    return  "Май";
                case 6:
                    return  "Июнь";
                case 7:
                    return  "Июль";
                case 8:
                    return "Август";
                case 9:
                    return  "Сентябрь";
                case 10:
                    return "Октябрь";
                case 11:
                    return "Ноябрь";
                case 12: return "Декабрь";
                default: return"";
        }
           

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

        static public void ReadCalendarFromXML(int years)
        {

            XmlDocument xDoc = new XmlDocument();
            String readPath = Environment.CurrentDirectory + @"\Calendars\"+years+".xml";
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
                    if ((childnode.Name == "day" )&&(childnode.Attributes.GetNamedItem("t").Value=="1"))
                    {
                        //MessageBox.Show(childnode.Attributes.GetNamedItem("d").Value);
                        //MessageBox.Show(xRoot.Attributes.GetNamedItem("year").Value);
                        string d_m = childnode.Attributes.GetNamedItem("d").Value;
                        string[] d_and_m = new string[2];
                        d_and_m = d_m.Split('.');
                        currentShop.DFCs.Add(new DataForCalendary(new DateTime(Int16.Parse(xRoot.Attributes.GetNamedItem("year").Value), Int16.Parse(d_and_m[0]), Int16.Parse(d_and_m[1])),8));
                    }
                    
                     if ((childnode.Name == "day" )&&(childnode.Attributes.GetNamedItem("t").Value=="2"))
                    {
                        //MessageBox.Show(childnode.Attributes.GetNamedItem("d").Value);
                        //MessageBox.Show(xRoot.Attributes.GetNamedItem("year").Value);
                        string d_m = childnode.Attributes.GetNamedItem("d").Value;
                        string[] d_and_m = new string[2];
                        d_and_m = d_m.Split('.');
                        currentShop.DFCs.Add(new DataForCalendary(new DateTime(Int16.Parse(xRoot.Attributes.GetNamedItem("year").Value), Int16.Parse(d_and_m[0]), Int16.Parse(d_and_m[1])),9));
                    }

                }



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
               // HSS.Add(new hourSale(1, dt, t, dn, cc, cs));
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

                        listShops.Add(new mShop(idSh, Sh));
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

            mShop h;
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
                       // MessageBox.Show(reader.GetInt16(0)+" "+ reader.GetString(1));
                        h = new mShop(reader.GetInt16(0), reader.GetString(1));
                        listShops.Add(h);
                        string writePath = Environment.CurrentDirectory + @"\Shops.txt";
                        using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                        {
                            foreach (mShop s in listShops)
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

        public static bool isConnected(string l, string p)
        {
            var connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=" + l + ";Password=" + p;

          //  connectionString = "Data Source=CENTRUMSRV;Persist Security Info=True;User ID=VShleyev;Password=gjkrjdybr@93";
            using (connection = new SqlConnection(connectionString))
            {
               try
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open) { connect= true; return connect; }
                    else { connect= false; return connect; }

                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    
                    MessageBox.Show(ex.Message);
                    connect = false;
                    return connect;
                }
            }

        
    }
       /* public static void Http(int year){
        	try{
        		string content;
        	using (var request = new HttpRequest())
				{
  				  request.UserAgent = Http.ChromeUserAgent();

   					 // Отправляем запрос.
  				  HttpResponse response = request.Get("http://xmlcalendar.ru/data/ru/"+year+"/calendar.xml");
   					 // Принимаем тело сообщения в виде строки.
   							content = response.ToString();
					}
        	 using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    
        
                        sw.Write( content);
                }
        	
        	
        	}
                catch (SmtpException ex)
                    {
                     Console.WriteLine("Произошла ошибка при работе с HTTP-сервером: {0}", ex.Message);

                      switch (ex.Status)
                      {
                        case HttpExceptionStatus.Other:
                      Console.WriteLine("Неизвестная ошибка");
                     break;

                      case HttpExceptionStatus.ProtocolError:
                       Console.WriteLine("Код состояния: {0}", (int)ex.HttpStatusCode);
                       break;

        case HttpExceptionStatus.ConnectFailure:
            Console.WriteLine("Не удалось соединиться с HTTP-сервером.");
            break;

        case HttpExceptionStatus.SendFailure:
            Console.WriteLine("Не удалось отправить запрос HTTP-серверу.");
            break;

        case HttpExceptionStatus.ReceiveFailure:
            Console.WriteLine("Не удалось загрузить ответ от HTTP-сервера.");
            break;
    }
}


        }*/
            

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
