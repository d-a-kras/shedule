﻿using System;
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
using shedule.Code;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;
using log4net.Config;
using shedule.Models;
using Npgsql;

//DataVisualization.Charting.SeriesChartType.Renko
//Excel.XlChartType.xlLineMarker
//.CornflowerBlue голубой
//.Salmon розовый
// .LightGreen зеленый

namespace shedule
{
    public class MinPers{
        public int Ncount;
       public  int Tcount;
        public MinPers(int n, int t) {
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

        public string getDolgnost() {
            return this.dolgnost;
        }

        public void setDolgnost(string d) {
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
        public Dictionary<DateTime,int> statusDays;

        public int getTipSmen()
        {
            return this.tipsmen;
        }
        public void setTipSmen(int ts)
        {
             this.tipsmen=ts;
        }

        public int getOtdih() {
            return this.otdih;
        }
        public int getStatusDay(DateTime dt)
        {
            int status=0;


            if (!this.statusDays.TryGetValue(dt, out status)) { status = 0; }
            
            
             return status;
            
        }

        public void otrabDay(DateTime dt)
        {
            
            this.statusDays.Add(dt,1);
            
        }
        public int getOtdihInHolyday()
        {
            return this.otdihinholyday;
        }
        public void setOtdihInHolyday(int ot)
        {
             this.otdihinholyday=ot;
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
            int m= otr % (VS.getR() + VS.getV());

            if (m >= VS.getR()) {
                this.otrabotal = m - (VS.getR() + VS.getV());
                    }
            else {
                this.otrabotal = m;
            }
            this.otdih = 0;
        }



        public employee(int ish, int ie,  string d, string tgr, int o)
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

        public void setOtrabotal(int s) {
          
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
            this.minKassUtr.Ncount = minRabs.First(t=>t.getEmployeeType()==EmployeeType.Cashier).getMinCount();
            this.minProdUtr.Ncount = minRabs.First(t => t.getEmployeeType() == EmployeeType.Seller).getMinCount();
            this.minKassVech.Ncount = minRabs.First(t => t.getEmployeeType() == EmployeeType.Cashier).getMinCount();
            int prodvech= minRabs.First(t => t.getEmployeeType() == EmployeeType.Seller).getMinCount();
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

        public void NM() {
            this.minGruzUtr = new MinPers(0,0);
       this.minGruzVech = new MinPers(0,0);
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





        public DateTime getData(int d=0)    
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
            this.Tip=t;
        }

        public void addChas(TemplateWorkingDay w)
        {
            if (this.getLenght() >= (w.DS.getLenghtDaySale() - 1)){return;}
            if ((this.getEndSmena() == w.DS.getEndDaySale()) && (this.getStartSmena() == w.DS.getStartDaySale())) { return; }
            if (this.Tip!=2) { 
             if (this.getEndSmena() == w.DS.getEndDaySale()) { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); return; }
                else if (this.getStartSmena() == w.DS.getStartDaySale()) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); return; }
                else if (this.getEndSmena() < w.DS.getEndDaySale()) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); return; }
                else if (this.getStartSmena() == (w.DS.getStartDaySale() + 1)) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); return; }


                else { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); } }
            else {
                if (this.getEndSmena() == w.DS.getEndDaySale()) { this.SetStarnAndLenght(this.getStartSmena() - 2, this.getLenght()+1);

                    if (this.getStartSmena() <= w.DS.getStartDaySale()) {

                        this.SetStarnAndLenght(this.getStartSmena() + 2, this.getLenght());

                    } return; }
                else if (this.getStartSmena() == w.DS.getStartDaySale()) { this.SetStarnAndLenght(this.getStartSmena()+2, this.getLenght()+1 );
                    if (this.getEndSmena() >= w.DS.getEndDaySale())
                    {

                        this.SetStarnAndLenght(this.getStartSmena() - 2, this.getLenght());

                    }
                    return; }
                else if (this.getEndSmena() > w.DS.getEndDaySale() - 2) { this.SetStarnAndLenght(this.getStartSmena()-1, this.getLenght()+1 ); return; }
                else if (this.getStartSmena() < (w.DS.getStartDaySale() + 2)) { this.SetStarnAndLenght(this.getStartSmena() +1, this.getLenght()+1 ); return; }
                else if (this.getEndSmena() < w.DS.getEndDaySale()-2) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); return; }
                else if (this.getStartSmena() > (w.DS.getStartDaySale() + 2)) { this.SetStarnAndLenght(this.getStartSmena()-1, this.getLenght() + 1); return; }
                else if ((this.getEndSmena() < w.DS.getEndDaySale())&&(this.F2)) { this.SetStarnAndLenght(this.getStartSmena(), this.getLenght() + 1); return; }
                else if ((this.getStartSmena() > w.DS.getStartDaySale())&&(this.F2)) { this.SetStarnAndLenght(this.getStartSmena() - 1, this.getLenght() + 1); return; }

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
            else {
                if (this.getEndSmena() >= w.DS.getEndDaySale()){
                    this.SetStarnAndLenght(this.getStartSmena(), w.DS.getEndDaySale()- w.DS.getStartDaySale()-1);
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

    public class Shop
    {
        public List<WorkingDay> workingDays { get; set; }
        public List<DataForCalendary> holidayDays
        {
            get { return DFCs.Where(x => x.Tip == 8 || x.Tip == 9).ToList(); }
        }
        public List<NormaChas> NormaChasov = new List<NormaChas>();
        public List<employee> employes { get; set; }
        public List<employee> Semployes { get; set; }
        public List<TemplateWorkingDay> templates { get; set; }
        public List<daySale> daysSale { get; set; }
        public List<Factor> factors = new List<Factor>();
        public List<DataForCalendary> DFCs = new List<DataForCalendary>();
        public List<TSR> tsr = new List<TSR>();
        public List<TSR> tsrBG = new List<TSR>();
        public List<daySale> MouthPrognoz = new List<daySale>();
        public List<TemplateWorkingDay> MouthPrognozT = new List<TemplateWorkingDay>();
        public List<VarSmen> VarSmens = new List<VarSmen>();
        public List<MinRab> ListMinRab;
        public int RaznChas;
        public bool SortSotr = false;
        // public int countPrilavok = 0;
        public Prilavki prilavki;

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

        public Shop(int i, string a)
        {

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
       /* public void setMinRab(MinRab mr) { this.minrab = mr; }*/
        public void setMinRab(List<MinRab> listMinRab) { this.ListMinRab = listMinRab; }
        public void AddTemplate(TemplateWorkingDay t)
        {
            this.templates.Add(t);
        }

        public List<WorkingDay> getWorkingDays()
        {
            return this.workingDays;
        }

        public void setIdShop(int x)
        {
            this.idShop = x;
        }

        public void setIdShopFM(int x)
        {
            this.idFM = x;
        }

        public void setAdresShop(string x)
        {
            this.address = x;
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

        public double getCountTov()
        {
            return this.countTov;
        }

        public void setClick(int c)
        {
            this.countClick = c;
        }

        public void setCheck(int ch)
        {
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

        public hourSale(int ids, DateTime D, string NH, int m)
        {
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

        public int getIdShop()
        {
            return this.idShop;
        }

        public DateTime getData() { return this.Data; }

        public string getWeekday() { return this.weekday; }

        public string getNHour() { return this.NHour; }

        public int getCountClick() { return this.countClick; }

        public int getCountCheck() { return this.countCheck; }

        public int getMinut()
        {
            if (this.Minute == 0)
            {
                return (this.getCountCheck() * Program.TimeRech + this.getCountClick() * Program.TimeClick) * Program.KoefKassira / 100;
            }
            else return this.Minute;
        }

        public int getTime()
        {
            return (this.getCountCheck() * Program.TimeClick + this.getCountClick() * Program.TimeClick);
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
            if (Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == d.ToShortDateString())!=null) {
                this.startDaySale = Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == d.ToShortDateString()).getTimeStart();
                // MessageBox.Show("Start "+this.startDaySale);

                this.endDaySale = Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == d.ToShortDateString()).getTimeEnd(); }
            // MessageBox.Show("END"+this.getData().ToShortDateString() + "");
        }

        public void whatTip()
        {
            this.setTip(	Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString()==this.Data.ToShortDateString()).getTip());

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

        public DateTime getData(int d=0)
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
        int tip;
        public List<hourSale> hss;
        public PrognDaySale(int id, DateTime d, int t) : base(id, d)
        {

            hss = new List<hourSale>();
            this.tip = t;
        }

        public int getTip()
        { return this.tip; }
    }

    static class Program
    {
       // static public List<DataForCalendary> minholidays;
        static public List<Shop> shops;
        static public Dictionary<int, String> GrafM=new Dictionary<int, string>();
        static public int normchas = 0;
        static public bool connect = false;

        // static public bool SozdanPrognoz = false;
        static public List<mShop> listShops = new List<mShop>();
        static public int TipOptimizacii = 0;
       // static public bool revertSotr = false;
        static public int TipExporta = -1;
        static public int zakr_konkurenta = 4;
        static public int otkr_konkurenta = 4;
        static public int rost_reklam = 4;
        static public int snig_reklam = 4;
        static public int progress = 0;
        static public bool ExistFile = false;
        public static bool IsMpRezhim = false;
        public static bool isOffline = false;

        public static int BgProgress = 0;

        public static int MinKassirCount = 2;
        public static int LastHourInInterval = -1;

        // static public List<hourSale> HSS = new List<hourSale>();
        static public string CountObr = "";

        //  static public int[,] CountS = new int[25, 15];
        //   static public int[,] CountClick = new int[25, 15];
        //   static public int[,] CountCheck = new int[25, 15];
        static public List<DateTime> holydays = new List<DateTime>();
        static public int[] RD = new int[13];
        static public int[] PHD = new int[13];

        static public int KoefKassira = 100;
        static public int KoefObr = 100;
        static public int TimeClick = 4;
        static public int TimeRech = 25;
        static public int TimeObrTov = 14;

        static public string file = "";
        static public string login = "VShleyev";
        static public string password = "gjkrjdybr@93";
        static public int tipDiagram = 0;
        static public bool TSRTG = true;
        static public bool exit = true;
        static public bool addsmena = false;

        static public Shop currentShop;
        static public short ParametrOptimization;
        static List<hourSale> SaleDay = new List<hourSale>();
        static List<hourSale> Raznica = new List<hourSale>();
        public static int errorNum = 0;
        public static int maxErrorNum = 2;

        public static bool isProcessing = false;

        //#region ConnectionSettings

        //public static string databaseAddress = "";
        //public static string databaseLogin = "";
        //public static string databasePassword = "";

        //#endregion

        public static HashSet<int> HandledShops = new HashSet<int>();

        static public void WritePrilavki()
        {
            /*Program.currentShop.minrab.setOtobragenie(true);*/
            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\Prilavki";
            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {
                sw.Write(Program.currentShop.prilavki.GetNalichie() + "_" + Program.currentShop.prilavki.GetCount());
                Program.HandledShops.Add(Program.currentShop.getIdShop());

            }
        }

        static public void ReadNormaChas(int Year)
        {
            currentShop.NormaChasov.RemoveAll(t => t.getYear() == Year);
            String readPath = Environment.CurrentDirectory + @"\NormaChas"+Year;
            if (!Directory.Exists(Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop()))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop());

            }


            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    string[] str = new string[3];
                    string s;
                    int i = 0;
                    while ((s = sr.ReadLine()) != null)
                    {

                        str = s.Split('_');
                        currentShop.NormaChasov.Add( new NormaChas(int.Parse(str[0]),int.Parse(str[1]), int.Parse(str[2])));
                        i++;
                    }


                }

            }
            catch
            {
                for (int i = 1; i < 13; i++)
                {
                    
                    currentShop.NormaChasov.Add( new NormaChas(Year,i, Program.RD[i-1] * 8 - Program.PHD[i-1]));

                }
                currentShop.NormaChasov.Add(new NormaChas(Year, 13, 136));




                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    foreach (NormaChas nc in currentShop.NormaChasov.FindAll(t=>t.getYear()==Year))
                    {

                        sw.WriteLine(nc.getYear()+"_"+ nc.getMonth() + "_" + nc.getNormChas());

                    }


                }
                // MessageBox.Show(ex.ToString());

            }

        }

        static public void ReadPrilavki()
        {
            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\Prilavki";
            if (!Directory.Exists(Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop()))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop());

            }


            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    string[] str = new string[2];
                    string s;

                    while ((s = sr.ReadLine()) != null)
                    {

                        str = s.Split('_');
                        currentShop.prilavki = new Prilavki(bool.Parse(str[0]), int.Parse(str[1]));

                    }


                }

            }
            catch
            {

                currentShop.prilavki = new Prilavki(false, 0);




                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    sw.Write(currentShop.prilavki.GetNalichie() + "_" + currentShop.prilavki.GetCount());

                }
                // MessageBox.Show(ex.ToString());

            }


        }

        static public void ReadSortSotr()
        {
            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\SortSotr";
            if (!Directory.Exists(Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop()))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop());

            }


            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {

                    currentShop.SortSotr = bool.Parse(sr.ReadLine());

                    


                }

            }
            catch
            {

                currentShop.SortSotr = false;




                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    sw.Write(currentShop.SortSotr);

                }
                // MessageBox.Show(ex.ToString());

            }


        }

        static public void WriteNormChas(int year)
        {

            String readPath = Environment.CurrentDirectory + @"\NormaChas"+year;

            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {
                foreach (NormaChas nc in currentShop.NormaChasov.FindAll(t=>t.getYear()==year))
                {

                    sw.WriteLine(nc.getYear()+"_"+nc.getMonth() + "_" + nc.getNormChas());

                }


            }
        }

        static public void WriteMinRab()
        {
           /* Program.currentShop.minrab.setOtobragenie(true);
            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\MinRab";
            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {
                sw.Write(Program.currentShop.minrab.getMinCount() + "_" + Program.currentShop.minrab.getTimeMinRab() + "_" + Program.currentShop.minrab.getOtobragenie());
                Program.HandledShops.Add(Program.currentShop.getIdShop());
            }*/
        }

        static public void ReadParametrOptimizacii()
        {
            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\parametroptimizacii";

            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {

                    string s;

                    while ((s = sr.ReadLine()) != null)
                    {
                        short m = short.Parse(s);
                        if ((m > 0) && (m < 4))
                        {
                            Program.ParametrOptimization = m;
                        }
                        else { Program.ParametrOptimization = 1; }
                    }


                }

            }
            catch
            {





                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    sw.Write("1");

                }
                // MessageBox.Show(ex.ToString());

            }


        }

        static public MinRab ReadMinRab()
        {


            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop().ToString() + @"\MinRab";
            if (IsMpRezhim)
            {
                readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShopFM().ToString() + @"\MinRab";

            }

            if (!Directory.Exists(Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop().ToString()))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop().ToString());

            }



            MinRab mr = null;
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    string[] str = new string[2];
                    string s;

                    while ((s = sr.ReadLine()) != null)
                    {

                        str = s.Split('_');
                        mr = new MinRab(int.Parse(str[0]), int.Parse(str[1]), bool.Parse(str[2]));

                    }


                }

            }
            catch
            {

                mr = new MinRab(1, 9, false);




                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    sw.Write(mr.getMinCount() + "_" + mr.getTimeMinRab() + "_" + "false");

                }
                // MessageBox.Show(ex.ToString());

            }

            return mr;
        }

        static public List<MinRab> ReadMinRabForShop()
        {

            List<MinRab> lmr;
            if (IsMpRezhim)
            {
                lmr = MinRab.ReadForShop(currentShop.getIdShopFM());
            }
            else {
                lmr = MinRab.ReadForShop(currentShop.getIdShop());
            }

            return lmr;
        }




        static public void newShop()
        {
            RD = new int[13];
            PHD = new int[13];
        }

        static public void readTSR()
        {

            String readPath;
            if (TSRTG)
            {
                currentShop.tsr.Clear();
                readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\TSR";
                if (IsMpRezhim)
                {
                    readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShopFM() + @"\TSR";
                }

                try
                {


                    using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                    {
                        string[] str = new string[5];
                        string s;

                        while ((s = sr.ReadLine()) != null)
                        {

                            str = s.Split('#');
                            currentShop.tsr.Add(new TSR(str[0], str[1], int.Parse(str[2]), int.Parse(str[3]), int.Parse(str[4])));

                        }


                    }

                }
                catch
                {

                    currentShop.tsr.Add(new TSR("kass", "Кассир", 4, 27000, 14000));
                    currentShop.tsr.Add(new TSR("prod", "Продавец", 4, 25000, 13000));
                    currentShop.tsr.Add(new TSR("gruz", "Грузчик", 2, 25000, 13000));
                    currentShop.tsr.Add(new TSR("gastr", "Гастроном", 2, 25000, 13000));

                    using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                    {

                        foreach (TSR f in currentShop.tsr)
                            sw.WriteLine(f.getPosition() + "#" + f.getOtobragenie() + "#" + f.getCount() + "#" + f.getZarp() + "#" + f.getZarp1_2());
                    }
                }
            }

            else
            {
                currentShop.tsrBG.Clear();
                readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\TSRBG";
                try
                {


                    using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                    {
                        string[] str = new string[5];
                        string s;

                        while ((s = sr.ReadLine()) != null)
                        {

                            str = s.Split('#');
                            currentShop.tsrBG.Add(new TSR(str[0], str[1], int.Parse(str[2]), int.Parse(str[3]), int.Parse(str[4])));

                        }


                    }
                }
                catch
                {

                    currentShop.tsr.Add(new TSR("kass", "Кассир", 4, 27000, 14000));
                    currentShop.tsr.Add(new TSR("prod", "Продавец", 4, 25000, 13000));
                    currentShop.tsr.Add(new TSR("gruz", "Грузчик", 2, 25000, 13000));
                    currentShop.tsr.Add(new TSR("gastr", "Гастроном", 2, 25000, 13000));


                    using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                    {

                        foreach (TSR f in currentShop.tsrBG)
                            sw.WriteLine(f.getPosition() + "#" + f.getOtobragenie() + "#" + f.getCount() + "#" + f.getZarp() + "#" + f.getZarp1_2());
                    }
                }
            }


            // MessageBox.Show(ex.ToString());



        }

        static public void WriteTSR()
        {
            String readPath;
            if (TSRTG)
            {

                readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\TSR";



                try
                {
                    using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                    {

                        foreach (TSR f in currentShop.tsr)
                            sw.WriteLine(f.getPosition() + "#" + f.getOtobragenie() + "#" + f.getCount() + "#" + f.getZarp() + "#" + f.getZarp1_2());
                    }
                    Program.HandledShops.Add(Program.currentShop.getIdShop());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка записи " + ex.Message);
                }
            }
            else
            {

                readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\TSRBG";



                try
                {
                    using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                    {

                        foreach (TSR f in currentShop.tsrBG)
                            sw.WriteLine(f.getPosition() + "#" + f.getOtobragenie() + "#" + f.getCount() + "#" + f.getZarp() + "#" + f.getZarp1_2());
                    }
                    Program.HandledShops.Add(Program.currentShop.getIdShop());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка записи " + ex.Message);
                }

            }


        }




        static public void readFactors(int id)
        {

            String readPath = Environment.CurrentDirectory + "/Shops/" + id + @"\factors";

            try
            {
                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    string s;

                    string[] str = new string[6];
                    while ((s = sr.ReadLine()) != null)
                    {

                        str = s.Split('#');
                        currentShop.factors.Add(new Factor(str[0], str[1], int.Parse(str[2]), bool.Parse(str[3]), DateTime.Parse(str[4]), int.Parse(str[5])));

                    }
                }
            }
            catch
            {
                currentShop.factors.Add(new Factor("TimeClick", "Время Клика", 4, true, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("TimeRech", "Голосовой интерфейс", 25, true, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("TimeObrTov", "Время на нелиннейные операции", 14, true, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("PozicVCheke", "Позиций в чеке", 5, true, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("KoefKassira", "Коэффициент эффективности кассиров (%)", 40, true, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("KoefObr", "Коэффициент эффетивности продавцов (%)", 60, true, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("Otkr_konkurenta", "Открытие конкурента (%)", 4, false, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("zakr_konkurenta", "Закрытие конкурента (%)", 4, false, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("snig_reklam", "Реклама конкурента (%)", 2, false, new DateTime(2100, 1, 1), 0));
                currentShop.factors.Add(new Factor("rost_reklam", "Собственная рекламная активность (%)", 2, false, new DateTime(2100, 1, 1), 0));

                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    foreach (Factor f in currentShop.factors)
                        sw.WriteLine(f.getName() + "#" + f.getOtobragenie() + "#" + f.getTZnach() + "#" + f.getDeistvie() + "#" + f.getData() + "#" + f.getNewZnach());
                }
                // MessageBox.Show(ex.ToString());
            }

            Helper.CheckFactorsActuality();
        }


        public static void WriteFactors()
        {
            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\factors";

            try
            {
                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {

                    foreach (Factor f in currentShop.factors)
                        sw.WriteLine(f.getName() + "#" + f.getOtobragenie() + "#" + f.getTZnach() + "#" + f.getDeistvie() + "#" + f.getData() + "#" + f.getNewZnach());
                }
                Program.HandledShops.Add(Program.currentShop.getIdShop());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи " + ex.Message);
            }
        }


        //  int CountProd = ((tc * TimeObrTov * 100) / (normchas* 3600 * KoefObr));
        // MessageBox.Show("Param " + ParametrOptimization + "Count Kass " + CountKassirov);


        //    employee e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmen.Find(t => t.getTip() == 1), "Кассир 1", "Сменный график");
        //    currentShop.employes.Add(e);



        static void Pereshet()
        {

            foreach (TemplateWorkingDay twd in currentShop.MouthPrognozT)
            {
                twd.CreateSmens();


            }

        }

        public static void writeVarSmen()
        {
            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\VarSmen2";

            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {

                foreach (VarSmen vs in currentShop.VarSmens)
                    sw.WriteLine(vs.getR() + "#" + vs.getV() + "#" + vs.getDeistvie()+"#"+vs.getDolgnost());
            }
            Program.HandledShops.Add(Program.currentShop.getIdShop());

        }

        public static void readVarSmen(bool m=false)
        {
            currentShop.VarSmens = new List<VarSmen>();
            String readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShop() + @"\VarSmen2";
            if (m) {
                readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShopFM() + @"\VarSmen2";
            }
           

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
                        currentShop.VarSmens.Add(new VarSmen(int.Parse(s[0]), int.Parse(s[1]), bool.Parse(s[2]), s[3] ));
                    }
                }
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
                currentShop.VarSmens.Add(new VarSmen(5, 2, false, "Кассир"));
                currentShop.VarSmens.Add(new VarSmen(2, 2, true, "Кассир"));
                currentShop.VarSmens.Add(new VarSmen(3, 3, true, "Кассир"));
                currentShop.VarSmens.Add(new VarSmen(4, 3, false, "Кассир"));
                currentShop.VarSmens.Add(new VarSmen(6, 1, false, "Кассир"));
                currentShop.VarSmens.Add(new VarSmen(5, 2, false, "Продавец"));
                currentShop.VarSmens.Add(new VarSmen(2, 2, true, "Продавец"));
                currentShop.VarSmens.Add(new VarSmen(3, 3, true, "Продавец"));
                currentShop.VarSmens.Add(new VarSmen(4, 3, false, "Продавец"));
                currentShop.VarSmens.Add(new VarSmen(6, 1, false, "Продавец"));
                currentShop.VarSmens.Add(new VarSmen(5, 2, false, "Гастроном"));
                currentShop.VarSmens.Add(new VarSmen(2, 2, true, "Гастроном"));
                currentShop.VarSmens.Add(new VarSmen(3, 3, true, "Гастроном"));
                currentShop.VarSmens.Add(new VarSmen(4, 3, false, "Гастроном"));
                currentShop.VarSmens.Add(new VarSmen(6, 1, false, "Гастроном"));
                currentShop.VarSmens.Add(new VarSmen(5, 2, false, "Грузчик"));
                currentShop.VarSmens.Add(new VarSmen(2, 2, true, "Грузчик"));
                currentShop.VarSmens.Add(new VarSmen(3, 3, true, "Грузчик"));
                currentShop.VarSmens.Add(new VarSmen(4, 3, false, "Грузчик"));
                currentShop.VarSmens.Add(new VarSmen(6, 1, false, "Грузчик"));

                //  case 1: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() +2 ), 10));  } break;

            };





            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {

                foreach (VarSmen vs in currentShop.VarSmens)
                    sw.WriteLine(vs.getR() + "#" + vs.getV() + "#" + vs.getDeistvie()+"#"+vs.getDolgnost());
            }

        }






        public static void itogChass()
        {
            foreach (employee e in currentShop.employes)
            {

                foreach (TemplateWorkingDay twd in currentShop.MouthPrognozT)
                {
                    if (e.smens.Find(t => t.getData() == twd.getData()) != null)
                    {
                        e.smens.Find(t => t.getData() == twd.getData()).obedChas(twd);
                    }
                }
            }
        }
        public static void itogChass2()
        {
            foreach (employee e in currentShop.employes)
            {

                foreach (TemplateWorkingDay twd in currentShop.MouthPrognozT.FindAll(t=>t.DS.getData().Day>DateTime.Now.Day))
                {
                    if (e.smens.Find(t => t.getData().Date == twd.getData().Date) != null)
                    {
                        e.smens.Find(t => t.getData().Date == twd.getData().Date).obedChas(twd);
                    }
                }
            }
        }
        public static bool CheckParnSmen()
        {
            List<VarSmen> lvs = Program.currentShop.VarSmens.FindAll(t => t.getDeistvie() == true);
            List<TSR> LGruz = Program.currentShop.tsr.FindAll(t => t.getPosition() == "gruz");
            List<TSR> LGastr = Program.currentShop.tsr.FindAll(t => t.getTip() == 4);
            int CountGruz = LGruz.Sum(o => o.getCount());
            int CountGastr = LGastr.Sum(o => o.getCount());
            if (((lvs.Find(t => t.getR() == 2) != null) && ((lvs.Find(t => t.getR() == 3)) != null) && (lvs.Count == 2)) && ((CountGruz % 2 != 0) || (CountGastr % 2 != 0)))
            {
                MessageBox.Show("Выбраны только смены 2/2 и 3/3 и нечетное число грузчиков или гастрономов. Добавьте дополнительно варианты смен или сделайте число сотрудников четным");
                return false;
            }
            if (((lvs.Find(t => t.getR() == 2) != null) || (lvs.Find(t => t.getR() == 3) != null)) && (lvs.Count == 1) && ((CountGruz % 2 != 0) || (CountGastr % 2 != 0)))
            {
                MessageBox.Show("Выбрана только смена 2/2 или 3/3 и нечетное число грузчиков или гастрономов. Добавьте дополнительно вариаты смен или сделайте число сотрудников четным");
                return false;
            }

            if (((lvs.Find(t => t.getR() == 5) != null) || (lvs.Find(t => t.getR() == 6) != null))) { }
            else
            {

                if ((CountGruz % 2 != 0) || (CountGastr % 2 != 0))
                {
                    MessageBox.Show("Не выбраны смены 5/2 или 6/1 и нечетное число грузчиков или гастрономов. Добавьте дополнительно варианты смен или сделайте число сотрудников четным");
                    return false;
                }



            }

            return true;
        }

        public static bool CheckDlinaDnya()
        {
            int min = 14;

            List<DataForCalendary> ldfc = new List<DataForCalendary>();
            Program.currentShop.DFCs = Program.currentShop.DFCs.FindAll(t => t.getTimeStart() != 0);
                ldfc =Program.currentShop.DFCs.FindAll(t => t.getMonth() == DateTime.Now.AddMonths(1).Month);
            foreach (DataForCalendary ds in ldfc)
            {
                if (ds.getLenght() < min)
                {
                    min = ds.getLenght();
                }
            }

            List<VarSmen> lvs = Program.currentShop.VarSmens.FindAll(t => t.getDeistvie() == true);
            foreach (VarSmen vs in lvs)
            {
                if (min < vs.getDlina())
                {

                    MessageBox.Show("Расписание не создано из-за выбранных вариантов смен при слишком короткой длине работы магазина. Используйте смены 5/2 и 6/1, или увеличьте время работы магазина.");
                    return false;
                }
            }
            return true;
        }

        public static bool CheckSrokaFactors()
        {
            bool flag = false;
            foreach (Factor f in currentShop.factors)
            {
                if (f.getData() < DateTime.Today)
                {
                    f.setTZnach(f.getNewZnach());
                    f.setData(new DateTime(2100, 1, 1));
                    f.setNewZnach(0);
                    flag = true;
                }
            }
            if (flag)
            {
                WriteFactors();
            }
            return flag;
        }

        public static void CheckDeistvFactors()
        {
            foreach (Factor f in currentShop.factors)
            {

                switch (f.getName())
                {
                    case "otkr_konkurenta": { if (f.getDeistvie()) { otkr_konkurenta = f.getTZnach(); } else { otkr_konkurenta = 0; }; break; }
                    case "zakr_konkurenta": if (f.getDeistvie()) { zakr_konkurenta = f.getTZnach(); } else { zakr_konkurenta = 0; } break;
                    case "rost_reklam": if (f.getDeistvie()) { rost_reklam = f.getTZnach(); } else { rost_reklam = 0; } break;
                    case "snig_reklam": if (f.getDeistvie()) { snig_reklam = f.getTZnach(); } else { snig_reklam = 0; } break;
                }
            }

        }




        public static bool createPrognoz( bool current, bool isMp, bool first  )
        {
            CheckDeistvFactors();
            currentShop.MouthPrognoz.Clear();
            currentShop.MouthPrognozT.Clear();
            DateTime ydt = DateTime.Now.AddDays(-1);
            DateTime tdt = DateTime.Today;
            List<PrognDaySale> PDSs = new List<PrognDaySale>();
            DateTime d2 = DateTime.Now.AddDays(-30);

            if ((first)||(currentShop.daysSale.Count==0)) {
                //   if (connect)
                //  {
                if (!isOffline)
                {
                    currentShop.daysSale.Clear();
                    if (isMp)
                    {
                        createListDaySale(d2, ydt, currentShop.getIdShopFM());
                    }
                    else
                    {
                        createListDaySale(d2, ydt, currentShop.getIdShop());
                    }
                }

                if ((isMp) && (isOffline))
                {
                    createListDaySale(d2, ydt, currentShop.getIdShopFM());
                }
            }

            //     }
            //   else if(ExistFile) {
            //        SozdanPrognoz = ExistFile;

            //  }
            //    else
            //   {
            //   throw new Exception("Загрузите данные из файла или установите соединение с БД");
            //      }


            bool pr = false;
            foreach (daySale ds in currentShop.daysSale)
            {

                ds.setTip(ds.getTip());
                if (ds.getTip() == 8 || ds.getTip() == 9) { pr = true; }

            }

           
               
                    Helper.readDays8and9(DateTime.Now.Year);
              
            

            for (int i = 1; i < 10; i++)
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


                        Sclick = (int)Math.Ceiling((double)((Sclick / h.Count) * ((100 + (float)otkr_konkurenta) / 100) * ((100 - (float)zakr_konkurenta) / 100) * ((100 + (float)rost_reklam) / 100) * ((100 + (float)snig_reklam) / 100)));
                        Scheck = (int)Math.Ceiling((double)((Scheck / h.Count) * ((100 + (float)otkr_konkurenta) / 100) * ((100 - (float)zakr_konkurenta) / 100) * ((100 + (float)rost_reklam) / 100) * ((100 + (float)snig_reklam) / 100)));
                        pds.hoursSale.Add(new hourSale(currentShop.getIdShop(), h[0].getData(), h[0].getNHour(), Scheck, Sclick));
                        //MessageBox.Show(Scheck+" ");
                    }
                }


            }

            DateTime fd;

            int dim;
            daySale d;
            if (current) {
               
                fd = new DateTime(tdt.Year , tdt.Month , tdt.AddDays(1).Day);
                dim = DateTime.DaysInMonth(fd.Year, fd.Month);

            } else {
                fd = new DateTime(tdt.AddMonths(1).Year, tdt.AddMonths(1).Month,1);
                dim = DateTime.DaysInMonth(fd.Year, fd.Month);
            }

            for (int i = fd.Day; i <= dim; i++)
            {
                try
                {
                    d = new daySale(currentShop.getIdShop(), new DateTime(fd.Year, fd.Month, i));
                    d.whatTip();
                    d.hoursSale = PDSs.Find(t => t.getTip() == d.getTip()).hoursSale;
                    currentShop.MouthPrognoz.Add(d);
                }
                catch
                {
                    //нужно чтоб вылазило сообщение о том что даты в календаре нет
                    MessageBox.Show($"Даты {i}.{fd.Month}.{fd.Year} нет в календаре!");
                    return false;
                }

            }



            foreach (daySale ds in currentShop.MouthPrognoz)
            {
                createPrognozTemplate(ds);
            }

            return true;
        }

        public static void createPrognoz3()
        {
            currentShop.MouthPrognoz.Clear();
            currentShop.MouthPrognozT.Clear();
            DateTime ydt = DateTime.Now.AddDays(-1);
            DateTime tdt = DateTime.Today;
            List<PrognDaySale> PDSs = new List<PrognDaySale>();
            DateTime d2 = DateTime.Now.AddDays(-30);

            if (!isOffline) {
                currentShop.daysSale.Clear();
                createListDaySale(d2, ydt, currentShop.getIdShop());
            }

            foreach (daySale ds in currentShop.daysSale)
            {
                ds.setTip(currentShop.DFCs.Find(x => x.getData().ToShortDateString() == ds.getData().ToShortDateString()).getTip());


            }

            

            Helper.readDays8and9(DateTime.Now.Year);

            

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


                        Sclick = (int)Math.Ceiling((double)((Sclick / h.Count) * ((100 + (float)otkr_konkurenta) / 100) * ((100 - (float)zakr_konkurenta) / 100) * ((100 + (float)rost_reklam) / 100) * ((100 + (float)snig_reklam) / 100)));
                        Scheck = (int)Math.Ceiling((double)((Scheck / h.Count) * ((100 + (float)otkr_konkurenta) / 100) * ((100 - (float)zakr_konkurenta) / 100) * ((100 + (float)rost_reklam) / 100) * ((100 + (float)snig_reklam) / 100)));
                        pds.hoursSale.Add(new hourSale(currentShop.getIdShop(), h[0].getData(), h[0].getNHour(), Scheck, Sclick));
                        //MessageBox.Show(Scheck+" ");
                    }
                }


            }

            DateTime[] fd = new DateTime[3];
            // DateTime fd2;
            // DateTime fd3;

            if (tdt.Month < 10)
            {
                fd[0] = new DateTime(tdt.Year, tdt.Month + 1, 1);
                fd[1] = new DateTime(tdt.Year, tdt.Month + 2, 1);
                fd[2] = new DateTime(tdt.Year, tdt.Month + 3, 1);
            }
            else
            {
                if (tdt.Month == 10)
                {
                    fd[0] = new DateTime(tdt.Year, 11, 1);
                    fd[1] = new DateTime(tdt.Year, 12, 1);
                    fd[2] = new DateTime(tdt.Year + 1, 1, 1);
                }
                else if (tdt.Month == 11)
                {
                    fd[0] = new DateTime(tdt.Year, 12, 1);
                    fd[1] = new DateTime(tdt.Year + 1, 1, 1);
                    fd[2] = new DateTime(tdt.Year + 1, 2, 1);
                }
                else if (tdt.Month == 12)
                {
                    fd[0] = new DateTime(tdt.Year + 1, 1, 1);
                    fd[1] = new DateTime(tdt.Year + 1, 2, 1);
                    fd[2] = new DateTime(tdt.Year + 1, 3, 1);
                }
                else
                {

                    fd[0] = new DateTime(tdt.Year, tdt.Month + 1, 1);
                    fd[1] = new DateTime(tdt.Year, tdt.Month + 1, 1);
                    fd[2] = new DateTime(tdt.Year, tdt.Month + 1, 1);
                    MessageBox.Show("Ошибка построения прогноза");
                }
            }

            for (int j = 0; j < 3; j++)
            {
                for (int i = 1; i <= DateTime.DaysInMonth(fd[j].Year, fd[j].Month); i++)
                {
                    try
                    {
                        daySale d = new daySale(currentShop.getIdShop(), new DateTime(fd[j].Year, fd[j].Month, i));
                        d.whatTip();
                        d.hoursSale = PDSs.Find(t => t.getTip() == d.getTip()).hoursSale;
                        currentShop.MouthPrognoz.Add(d);
                    }
                    catch(Exception ex) {
                        Logger.Error(ex.ToString());
                        MessageBox.Show($"Даты {i}.{fd[j].Month}.{fd[j].Year} нет в календаре!");
                    }
                }
            }



            foreach (daySale ds in currentShop.MouthPrognoz)
            {
                createPrognozTemplate(ds);
            }


        }




        public static void createListDaySale(DateTime n, DateTime k, int id)
        {
            Connection activeconnect = Connection.getActiveConnection();
            var connectionString = Connection.getConnectionString(activeconnect);
            string s1 = n.Year + "/" + Helper.NumberToString(n.Month) + "/" + Helper.NumberToString(n.Day);
            string s2 = k.Year + "/" + Helper.NumberToString(k.Month) + "/" + Helper.NumberToString(k.Day);
            string sql;
            if (currentShop.getIdShop() == 0) {
                id = Program.currentShop.getIdShopFM();
            }

            sql = ForDB.getSQL_statisticbyshopsdayhour(id,s1,s2, Connection.getSheme(activeconnect));  // "select * from "+ Connection.getSheme(activeconnect) + "get_StatisticByShopsDayHour('" + id + "', '" + s1 + "', '" + s2 + " 23:59:00')"; 

            currentShop.daysSale = new List<daySale>();
            List<hourSale> hss = new List<hourSale>();
            daySale ds;

            int countAttemption = 0;
            while (hss.Count == 0 && countAttemption < 2)
            {
                countAttemption++;
                hss = ForDB.getHourFromDB(connectionString, sql, currentShop.getIdShop());

                if (hss.Count > 1) countAttemption = 2;
            }

            if (hss.Count < 2 && Constants.IsThrowExceptionOnNullResult)
            {
                countAttemption = 0;
                MessageBox.Show("Ошибка соединения с базой данных ");
                throw new Exception("Соединение с базой нестабильно, данные не были получены.");
            }

            countAttemption = 0;


            if (hss.Count > 200)
            {
                //посчитать количество дней 
                TimeSpan ts = k - n;

                DateTime d = n;

                for (int i = 0; i <= ts.Days + 1; i++)
                {

                    ds = new daySale(Program.currentShop.getIdShop(), d);
                    currentShop.daysSale.Add(ds);
                    d = d.AddDays(1.0d);
                }
                // MessageBox.Show("Количество дней по ts " + ts.Days.ToString());
                //  MessageBox.Show("Количество часов "+hss.Count.ToString());
                foreach (hourSale hs in hss)
                {
                    currentShop.daysSale.Find(x => x.getData().ToShortDateString() == hs.getData().ToShortDateString()).Add(hs);
                }

                //using (StreamWriter sm = new StreamWriter(@"D:\Users\tailer_d\Desktop\test\test.txt"))
                //{
                //    foreach (var s in results)
                //    {
                //        sm.WriteLine(s);
                //    }
                //}


            }
            else
            {
                if (hss.Count > 0)
                {
                    string max = hss.Max(t => t.getData()).ToShortDateString();

                    MessageBox.Show("Данных недостаточно. Последняя запись в базу данных " + max);
                }
                else {
                    MessageBox.Show("Из базы данных не вернулись значения за прошлый месяц");
                }
                Form6 f6 = new Form6();
                f6.ShowDialog();
                var newid = f6.newId;
                if (++errorNum <= maxErrorNum)
                {
                    createListDaySale(n, k, newid);
                }
                else
                {
                    throw new Exception("Соединение с базой нестабильно, выгрузка невозможна.");
                }

            }
        }

        /*static List<ForChart> createSaleForChart(){


            foreach(daySale ds in currentShop.daysSale){
                foreach(hourSale hs in ds.hoursSale){

                }
            }

        }*/

        static void readTemplateForShop()
        {
            String readPath = Environment.CurrentDirectory + "/Shops" + currentShop.getIdShop() + "/Templates.txt";
            try
            {


                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {

                    string line;
                    string[] s = new string[4];
                    TemplateWorkingDay twd;
                    while ((line = sr.ReadLine()) != null)
                    {
                        daySale wd = new daySale(int.Parse(sr.ReadLine()), DateTime.Parse(sr.ReadLine()));
                        twd = new TemplateWorkingDay(wd);
                        while ((line = sr.ReadLine()) != "*****")
                        {
                            s = line.Split('#');
                            Smena sm = new Smena(int.Parse(s[0]), DateTime.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
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

        static void writeTemplateForShop()
        {
            String readPath = Environment.CurrentDirectory + "/Shops" + currentShop.getIdShop() + "/Templates.txt";
            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {
                foreach (TemplateWorkingDay t in currentShop.templates)
                {
                    sw.WriteLine(t.DS.getIdShop());
                    sw.WriteLine(t.getData());
                    sw.WriteLine(t.DS.getStartDaySale());
                    sw.WriteLine(t.DS.getEndDaySale());

                    foreach (Smena s in t.lss)
                    {
                        sw.WriteLine(t.DS.getIdShop() + "#" + t.getData() + "#" + s.getStartSmena() + "#" + s.getLenght());
                    }
                    sw.WriteLine("*****");
                }
                Program.HandledShops.Add(Program.currentShop.getIdShop());
            }
        }

        static public List<hourSale> createDaySale(int idShop, DateTime dt)
        {
            //var connectionString = $"Data Source={Settings.Default.DatabaseAddress};Persist Security Info=True;User ID={Program.login};Password={Program.password}";
            Connection activeconnect = Connection.getActiveConnection();
            var connectionString = Connection.getConnectionString(activeconnect);
            string s1 = dt.Year + "/" + dt.Day + "/" + dt.Month;
            string s2 = dt.Year + "/" + dt.Day + "/" + dt.Month;
            //string s1 = "2018/8/3";
            //string s2 = "2018/8/3";
            List< hourSale> lhs=new List<hourSale>();
            string sql;
            sql = "select * from "+ Connection.getSheme(activeconnect) + "get_StatisticByShopsDayHour('" + idShop + "', '" + s1 + "', '" + s2 + " 23:59:00')";

            int countAttemption = 0;
            int countRecords = 0;
            while (countRecords == 0 && countAttemption < 2)
            {
                countAttemption++;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.CommandTimeout = 3000;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            hourSale h = new hourSale(reader.GetInt16(0), reader.GetDateTime(1), reader.GetString(2),
                                reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetDouble(6));
                            lhs.Add(h);
                            countRecords++;

                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        MessageBox.Show("Ошибка соединения с базой данных" + ex);
                    }
                }
                if (countRecords > 2) countAttemption = 2;
            }

            if (countRecords < 2 && Constants.IsThrowExceptionOnNullResult)
            {
                countRecords = 0;
                countAttemption = 0;
                throw new Exception("Соединение с базой нестабильно, данные не были получены.");
            }

            countRecords = 0;
            countAttemption = 0;


            return lhs;
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
            if (currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
            {
                K = currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
            }
            else { K = KoefKassira; }
            int EndSmena = sm.getStartSmena() + sm.getLenght() + 1;

            for (int i = sm.getStartSmena(); i < EndSmena; i++)
            {

                hourSale temp = Raznica.Find(x => x.getNHour() == i.ToString());
                // MessageBox.Show("temp1="+temp.getNHour()+" "+temp.getMinut());
                if (!(temp == null))
                {
                    int t = temp.getMinut() - K * 3;
                    Raznica.Add(new hourSale(currentShop.getIdShop(), sm.getData(), i.ToString(), t));
                    // MessageBox.Show("Count Razniza=" + Raznica.Count);
                    Raznica.Remove(temp);
                    // MessageBox.Show("temp2=" + temp.getNHour()+" " + t+" Count Razniza=" + Raznica.Count);
                }
                else { Raznica.Add(new hourSale(currentShop.getIdShop(), sm.getData(), i.ToString(), 0)); }


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

        static public void createTemplate(daySale ds)
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
            int i = 0;
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





        static private void CreateXML()
        {
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

        static public void getListDate(int year, bool next)
        {
            RD= new int[13];
            try
            {
                DateTime.Parse($"01-01-{year}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                throw new Exception($"Значение {year} недопустимо в качестве года!");
            }

            if (!next) {
                currentShop.DFCs.Clear();
            }
            string readPath = Environment.CurrentDirectory + @"\Shops\" + currentShop.getIdShop() + $@"\Calendar{year}";
            if (Program.IsMpRezhim) { readPath = Environment.CurrentDirectory + @"\Shops\" + currentShop.getIdShopFM() + $@"\Calendar{year}"; }

            // MessageBox.Show(readPath);
            try
            {
                using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                {
                    string line;
                    string[] s = new string[4];

                    while ((line = sr.ReadLine()) != null)
                    {
                        s = line.Split('#');
                        DataForCalendary d = new DataForCalendary(DateTime.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
                        // MessageBox.Show(DateTime.Parse(s[0]).Month.ToString());
                        switch (int.Parse(s[1]))
                        {
                            case 1: RD[DateTime.Parse(s[0]).Month - 1]++; break;
                            case 2: RD[DateTime.Parse(s[0]).Month - 1]++; break;
                            case 3: RD[DateTime.Parse(s[0]).Month - 1]++; break;
                            case 4: RD[DateTime.Parse(s[0]).Month - 1]++; break;
                            case 5: RD[DateTime.Parse(s[0]).Month - 1]++; break;

                            case 9: PHD[DateTime.Parse(s[0]).Month - 1]++; break;

                        }

                        currentShop.DFCs.Add(d);
                    }
                    //   MessageBox.Show("DFCs.Add 1");
                }



            }
            catch
            {
                //  MessageBox.Show(ex.Message);
                try
                {
                    Program.ReadCalendarFromXML(year);
                }
                catch {
                    MessageBox.Show("Отсутствует файл календаря на "+year+" год");
                }

                for (int i = 0; i < 12; i++)
                {
                    RD[i ] = 0;
                    PHD[i ] = 0;
                    DateTime dt = new DateTime(year,1,1);
                    int countDays = DateTime.DaysInMonth(dt.AddMonths(i).Year, dt.AddMonths(i).Month);
                    for (int k = 1; k <= countDays; k++)
                    {
                        DataForCalendary dfc = new DataForCalendary(new DateTime(year, i+1, k));
                        int t = dfc.getTip();
                        if ((t == 1) || (t == 2) || (t == 3) || (t == 4) || (t == 5)) { RD[i ]++; }
                        if (t == 9) { PHD[i ]++; }

                        if (currentShop.DFCs.Find(x => x.getData() == dfc.getData()) != null)
                        {

                        }
                        else currentShop.DFCs.Add(dfc);

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
                        case 6: dfc.setTimeBaE(9, 23); break;
                        case 7: dfc.setTimeBaE(9, 23); break;
                        case 8: dfc.setTimeBaE(9, 23); break;
                        case 9: dfc.setTimeBaE(9, 23); break;
                        case 10: dfc.setTimeBaE(9, 23); break;
                        default: dfc.setTimeBaE(8, 23); break;
                    }
                }

                string directoryPath = readPath.Split(new string[] { "\\" }, StringSplitOptions.None)
                    .Reverse()
                    .Skip(1)
                    .Reverse()
                    .Aggregate((prev, current) => prev + "\\" + current);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    foreach (DataForCalendary d in currentShop.DFCs)
                        sw.WriteLine(d.getData() + "#" + d.getTip() + "#" + d.getTimeStart() + "#" + d.getTimeEnd());
                }

            }
        }

        static public void ReadConfigShop(int id)
        {


        }
        public static string[] getMonths()
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
            switch (m)
            {

                case 1: return "Январь";
                case 2:
                    return "Февраль";
                case 3:
                    return "Март";
                case 4:
                    return "Апрель";
                case 5:
                    return "Май";
                case 6:
                    return "Июнь";
                case 7:
                    return "Июль";
                case 8:
                    return "Август";
                case 9:
                    return "Сентябрь";
                case 10:
                    return "Октябрь";
                case 11:
                    return "Ноябрь";
                case 12: return "Декабрь";
                default: return "";
            }


        }

        static public string getMonthInString(int nm)
        {

            switch (nm)
            {
                case 1: return "Января";
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

        public static void ReadCalendarFromXML(int years)
        {

            XmlDocument xDoc = new XmlDocument();
            String readPath = Environment.CurrentDirectory + @"\Calendars\" + years + ".xml";
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
                    if ((childnode.Name == "day") && (childnode.Attributes.GetNamedItem("t").Value == "1"))
                    {
                        //MessageBox.Show(childnode.Attributes.GetNamedItem("d").Value);
                        //MessageBox.Show(xRoot.Attributes.GetNamedItem("year").Value);
                        string d_m = childnode.Attributes.GetNamedItem("d").Value;
                        string[] d_and_m = new string[2];
                        d_and_m = d_m.Split('.');
                        currentShop.DFCs.Add(new DataForCalendary(new DateTime(Int16.Parse(xRoot.Attributes.GetNamedItem("year").Value), Int16.Parse(d_and_m[0]), Int16.Parse(d_and_m[1])), 8));
                    }

                    if ((childnode.Name == "day") && (childnode.Attributes.GetNamedItem("t").Value == "2"))
                    {
                        //MessageBox.Show(childnode.Attributes.GetNamedItem("d").Value);
                        //MessageBox.Show(xRoot.Attributes.GetNamedItem("year").Value);
                        string d_m = childnode.Attributes.GetNamedItem("d").Value;
                        string[] d_and_m = new string[2];
                        d_and_m = d_m.Split('.');
                        currentShop.DFCs.Add(new DataForCalendary(new DateTime(Int16.Parse(xRoot.Attributes.GetNamedItem("year").Value), Int16.Parse(d_and_m[0]), Int16.Parse(d_and_m[1])), 9));
                    }

                }



            }
        }



        static public void R()
        {

            //Helper.readDays8and9();

            Helper.CreateHolidaysForAllShops(DateTime.Now.Year);
            


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
                        s = line.Split('_');
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
                ForDB.getShopsFromDB();
            }
        }

        public static bool isConnected()
        {
            Program.connect = ForDB.isConnected();
            return Program.connect;
        }



        public static bool isConnect() { return connect; }

       
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

        static public string[] collectionHours = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };






        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {
            try
            {
                Logger.InitLogger();
               if  (!File.Exists(Environment.CurrentDirectory + "/Calendars/" + (DateTime.Now.Year+1).ToString()+".xml")) {
                    Helper.CheckAndDownloadNextYearCalendar();
                    }
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Произошла ошибка! " + ex.ToString());
            }





        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            try
            {
                //  Helper.KillExcels();
            }
            catch { }

        }

    }

}
