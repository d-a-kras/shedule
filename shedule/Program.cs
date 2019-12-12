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
using schedule.Code;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;
using log4net.Config;
using schedule.Models;
using Npgsql;
using shedule.Models;

//DataVisualization.Charting.SeriesChartType.Renko
//Excel.XlChartType.xlLineMarker
//.CornflowerBlue голубой
//.Salmon розовый
// .LightGreen зеленый

namespace schedule
{
 

    static class Program
    {
       // static public List<DataForCalendary> minholidays;
        static public List<Shop> shops;
        static public Dictionary<int, String> GrafM=new Dictionary<int, string>();
        static public int normchas = 0;
        static public bool connect = false;
        internal static bool isLocalDB;

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
            /*
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
            */
            currentShop.SortSotr = DBShop.getMixing(currentShop.getIdShop());

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
               /* if (IsMpRezhim)
                {
                    readPath = Environment.CurrentDirectory + "/Shops/" + currentShop.getIdShopFM() + @"\TSR";
                }*/

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
            Connection activeconnect = Connection.getActiveConnection(Program.currentShop.getIdShop());
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
            //String readPath = Environment.CurrentDirectory + @"\Shops.txt";
            try
            { /*
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
                }*/

                List<DBShop> DBShops = DBShop.getShops();
                if (DBShops.Count > 0)
                {
                    foreach (var shop in DBShops)
                    {
                        Program.listShops.Add(shop.convertMShop());
                    }
                }
                else {
                    Program.listShops = DBShop.ReadFromFile();
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
