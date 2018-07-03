using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shedule.Code
{



    public class Sotrudniki
    {
        static public bool EZanyt(DateTime wd, List<employee> le)
        {
            foreach (employee e in le)
            {
                if (e.getStatusDay(wd) != 1)
                {
                    return false;
                }
            }
            return true;
        }

        public static string CountSotr = "";
        static public bool CheckGrafic13()
        {

            foreach (employee emp in Program.currentShop.employes)
            {
                foreach (Smena sm in emp.smens)
                    if (sm.getLenght() > 13)
                    {


                        return false;
                    }



            }


            return true;

        }

        static public bool CheckGrafic2()
        {

            foreach (TemplateWorkingDay mp in Program.currentShop.MouthPrognozT)
            {
                if ((mp.minKassUtr.Tcount < 1) || (mp.minKassVech.Tcount < 1) || ((mp.minProdUtr.Tcount < 1)))
                {
                    // MessageBox.Show(mp.DS.getData().ToShortDateString());
                    return false;
                }



            }


            return true;

        }

        static public bool CheckGrafic()
        {
            List<Smena> lss = new List<Smena>();
            // WorkingDay wd; 
            foreach (TemplateWorkingDay mp in Program.currentShop.MouthPrognozT)
            {
                if ((mp.minKassUtr.Tcount < 1) || (mp.minProdUtr.Tcount < 1) || (mp.minKassVech.Tcount < 1) || (mp.minProdVech.Tcount < 1)) { return false; }
                /*   wd  = new WorkingDay(Program.currentShop.getIdShop(), mp.getData(),mp.DS.getStartDaySale(),mp.DS.getEndDaySale());
                     foreach (employee emp in Program.currentShop.employes) {
                         if (emp.smens.Find(x=>x.getData()==mp.getData())!=null) {
                             wd.AddSmena(emp.smens.Find(x => x.getData() == mp.getData()));
                            }
                     }
                     Program.currentShop.workingDays.Add(wd);
                    */

            }


            return true;

        }

        static public int shiftSm(ref int i, ref bool f1, List<VarSmen> lvs)
        {
            i++;
            if (i == lvs.Count - 1)
            {

                f1 = true;

            }
            if (i == lvs.Count)
            {

                i = 0;

            }

            return i;

        }
        /*
        static public int shiftSm2(ref int i, ref bool f1)
        {
            i++;
            if (i == Program.currentShop.VarSmens.FindAll(t => (t.getDeistvie() == true)&&(!((t.getR() == 2) || (t.getR() == 3)))).Count)
            {
                i = 0;
                f1 = true;

            }

            return i;

        }

        static public int shiftSm3(ref int i, ref bool f1)
        {
            i++;
            if (i == Program.currentShop.VarSmens.FindAll(t => (t.getDeistvie() == true) && (!((t.getR() == 4) || (t.getR() == 2) || (t.getR() == 3)))).Count)
            {
                i = 0;
                f1 = true;

            }

            return i;

        }*/

        static public int shiftProd(ref int i)
        {
            i++;
            if (i == Program.currentShop.tsr.FindAll(t => t.getTip() == 2).Count)
                i = 0;


            return i;

        }
        static public int shiftKass(ref int i)
        {
            i++;
            if (i == Program.currentShop.tsr.FindAll(t => t.getTip() == 1).Count)
                i = 0;


            return i;

        }

        static public int shiftGruz(ref int i)
        {
            i++;
            if (i == Program.currentShop.tsr.FindAll(t => t.getPosition() == "gruz").Count)
                i = 0;


            return i;

        }

        static public int shiftGastr(ref int i)
        {
            i++;
            if (i == Program.currentShop.tsr.FindAll(t => t.getPosition() == "gastr").Count)
                i = 0;


            return i;

        }



        public static void OptimCountSotr(bool current = false)
        {
            int loop = 0;
            bool flag = false;
            int nvs = -1;
            int nprod = -1;
            int ngastr = -1;
            int ngruz = -1;
            int nkass = -1;
            int kassCount = Program.MinKassirCount;

            List<VarSmen> DVS0 = Program.currentShop.VarSmens.FindAll(t => t.getDolgnost() == "Грузчик");

            List<VarSmen> DVS = DVS0.FindAll(t => t.getDeistvie() == true);
            if (DVS.Count == 0)
            {
                Exception ex = new Exception("Не выбрана ни одна смена у грузчиков");
                MessageBox.Show("Не выбрана ни одна смена у грузчиков");
                return;
            }
            List<VarSmen> DVS23 = DVS.FindAll(t => ((t.getR() == 2) || (t.getR() == 3)));
            List<VarSmen> DVS2 = DVS.FindAll(t => !((t.getR() == 2) || (t.getR() == 3)));
            List<VarSmen> DVS3 = DVS.FindAll(t => !((t.getR() == 2) || (t.getR() == 3) || (t.getR() == 4)));

            DVS2.Sort(delegate (VarSmen s1, VarSmen s2)
            { return s2.getR().CompareTo(s1.getR()); });

            if (DVS.Count != 0)
            {


                employee e;
                if (Program.currentShop.MouthPrognozT.Count == 0)
                {
                    //MessageBox.Show("Прогноз не создан");
                    return;

                }
                Program.currentShop.employes.Clear();
                int K, KP, Tobrtov;
                if (Program.currentShop.factors.Find(t => t.getName() == "TimeObrTov") != null)
                {
                    Tobrtov = Program.currentShop.factors.Find(t => t.getName() == "TimeObrTov").getTZnach();
                }
                else { Tobrtov = 14; }
                if (Program.currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
                {
                    K = Program.currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
                }
                else { K = Program.KoefKassira; }
                if (Program.currentShop.factors.Find(t => t.getName() == "KoefObr") != null)
                {
                    KP = Program.currentShop.factors.Find(t => t.getName() == "KoefObr").getTZnach();
                }
                else { KP = Program.KoefKassira; }
                DateTime dt = DateTime.Today;
                if (current)
                {
                    Program.normchas = Program.currentShop.NormaChasov.Find(t => t.getMonth() == dt.Month).getNormChas();
                }
                else
                {
                    Program.normchas = Program.currentShop.NormaChasov.Find(t => t.getMonth() == dt.Month + 1).getNormChas();
                }


                int ob = 0;
                int tc = 0;
                foreach (TemplateWorkingDay t in Program.currentShop.MouthPrognozT)
                {
                    tc += t.getClick();
                    ob += t.getCapacityPCC();
                }

                List<TSR> LGruz = Program.currentShop.tsr.FindAll(t => t.getPosition() == "gruz");

                List<TSR> LProd = Program.currentShop.tsr.FindAll(t => t.getTip() == 2);
                List<TSR> LKass = Program.currentShop.tsr.FindAll(t => t.getTip() == 1);
                List<TSR> LGastr = Program.currentShop.tsr.FindAll(t => t.getPosition() == "gastr");
                int CountProd = 0;
                int CountKassirov = 0;
                int CountGastr = 0;
                int CountGruz = 0;

                switch (CountSotr)
                {
                    case "штатного расписания":
                        CountGastr = LGastr.Sum(o => o.getCount());
                        CountGruz = LGruz.Sum(o => o.getCount());
                        CountProd = LProd.Sum(o => o.getCount());
                        CountKassirov = LKass.Sum(o => o.getCount());
                        break;
                    case "загруженного графика":
                        if (Program.currentShop.Semployes.Count > 0)
                        {
                            CountGastr = Program.currentShop.Semployes.FindAll(t => t.GetDolgnost() == "Гастроном").Count;
                            CountGruz = Program.currentShop.Semployes.FindAll(t => t.GetDolgnost() == "Грузчик").Count;
                            CountProd = Program.currentShop.Semployes.FindAll(t => t.GetDolgnost() == "Продавец").Count;
                            CountKassirov = Program.currentShop.Semployes.FindAll(t => t.GetDolgnost() == "Кассир").Count;
                        }
                        else
                        {
                            CountGastr = LGastr.Sum(o => o.getCount());
                            CountGruz = LGruz.Sum(o => o.getCount());
                            CountProd = LProd.Sum(o => o.getCount());
                            CountKassirov = LKass.Sum(o => o.getCount());
                        }

                        break;
                    case "прогноза продаж":
                        CountGastr = LGastr.Sum(o => o.getCount());
                        CountGruz = LGruz.Sum(o => o.getCount());
                        if (current)
                        {
                            int kf = Program.normchas / DateTime.DaysInMonth(Program.currentShop.MouthPrognozT[0].getData().Year, Program.currentShop.MouthPrognozT[0].getData().Month) * Program.currentShop.MouthPrognozT.Count;
                            CountProd = (int)Math.Ceiling((tc * Tobrtov * 60) / (double)(kf * 3600 * KP));
                            CountKassirov = (int)Math.Ceiling(ob / (double)(kf * K * 36)) + Program.ParametrOptimization;
                        }
                        else
                        {
                            CountProd = (int)Math.Ceiling((tc * Tobrtov * 60) / (double)(Program.normchas * 3600 * KP));
                            CountKassirov = (int)Math.Ceiling(ob / (double)(Program.normchas * K * 36)) + Program.ParametrOptimization;

                        }
                        break;
                    default:
                        CountGastr = LGastr.Sum(o => o.getCount());
                        CountGruz = LGruz.Sum(o => o.getCount());
                        CountProd = LProd.Sum(o => o.getCount());
                        CountKassirov = LKass.Sum(o => o.getCount());
                        break;
                }






                bool flagB = true;
                if (CountKassirov <= 3) { CountKassirov = 3; flagB = false; }
                if (CountProd < 2) { CountProd = 2; }


                bool fflag = true;
                bool flagg2 = false;
                bool flagg3 = false;
                bool flaggastr2 = false;
                bool flaggastr3 = false;
                bool flagp2 = false;
                bool flagp3 = false;
                bool flag2 = false;
                bool flag3 = false;
                bool f56gruz = false;
                bool f56gastr = false;





                flag = true;
                loop = 0;
                for (int i = 200; CountGruz > 0; CountGruz--, i++)
                {
                    if (flag)
                    {

                        flagg2 = false;
                        flagg3 = false;


                    }
                    if (((DVS.Find(t => t.getR() == 2) != null) && (!flagg2) && (CountGruz > 1)) || (loop > 1000))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -1, LGruz[shiftGruz(ref ngruz)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountGruz--; i++;

                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 1, LGruz[ngruz].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);

                        flagg2 = true;
                        continue;
                    }
                    if (((DVS.Find(t => t.getR() == 3) != null) && (!flagg3) && (CountGruz > 1)) || (loop > 1000))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -1, LGruz[shiftGruz(ref ngruz)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountGruz--; i++;

                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 2, LGruz[ngruz].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);

                        flagg3 = true;
                        continue;
                    }
                    if ((DVS3.Count != 0 && CountGruz == 1) || ((DVS3.Count != 0) && f56gruz))
                    {

                        DateTime fd = new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1);
                        DataForCalendary d = new DataForCalendary(fd);
                        int otrab = (d.getNWeekday()) - 1;


                        e = new employee(Program.currentShop.getIdShop(), i, DVS3[shiftSm(ref nvs, ref flag, DVS3)], otrab, LGruz[shiftGruz(ref ngruz)].getOtobragenie(), "Сменный график");

                        Program.currentShop.employes.Add(e);
                    }
                    else
                    {
                        loop++;

                        i--;
                        CountGruz++;
                        flag = true;
                        if (DVS23.Count == 0)
                        {
                            f56gruz = true;
                        }
                    }
                }

                DVS0 = Program.currentShop.VarSmens.FindAll(t => t.getDolgnost() == "Продавец");

                DVS = DVS0.FindAll(t => t.getDeistvie() == true);
                if (DVS.Count == 0)
                {
                    Exception ex = new Exception("Не выбрана ни одна смена у продавцов");
                    MessageBox.Show("Не выбрана ни одна смена у продавцов");
                    return;
                }
                DVS2 = DVS.FindAll(t => !((t.getR() == 2) || (t.getR() == 3)));
                DVS3 = DVS.FindAll(t => !((t.getR() == 2) || (t.getR() == 3) || (t.getR() == 4)));
                DVS23 = DVS.FindAll(t => ((t.getR() == 2) || (t.getR() == 3)));


                int otrPr = 0;
                nvs = -1;
                flag = true;
                fflag = false;
                loop = 0;
                for (int i = 100; CountProd > 0; CountProd--, i++)
                {


                    if (flag)
                    {


                        flagp2 = false;
                        flagp3 = false;

                        flag = false;

                    }
                    if (((DVS.Find(t => t.getR() == 2) != null) && (!flagp2) && (CountProd > 1)) || (loop > 1000))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -1, LProd[shiftProd(ref nprod)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountProd--; i++;
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 1, LProd[nprod].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        flagp2 = true;
                        continue;
                    }
                    if (((DVS.Find(t => t.getR() == 3) != null) && (!flagp3) && (CountProd > 1)) || (loop > 1000))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -2, LProd[shiftProd(ref nprod)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountProd--; i++;
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 1, LProd[nprod].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        flagp3 = true;
                        continue;
                    }

                    if ((DVS2.Count != 0) && (!flag))
                    {
                        if ((nvs == 0) && (DVS2.Count == 2) && (CountProd == 2)) { otrPr = 2; }
                        if ((nvs == 1) && (DVS2.Count == 2) && (CountProd == 1)) { otrPr = -2; }
                        if (!fflag)
                        {
                            DateTime fd = new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1);
                            DataForCalendary d = new DataForCalendary(fd);
                            otrPr = (d.getNWeekday()) - 1;
                            fflag = true;
                        }
                        e = new employee(Program.currentShop.getIdShop(), i, DVS2[shiftSm(ref nvs, ref flag, DVS2)], otrPr, LProd[shiftProd(ref nprod)].getOtobragenie(), "Сменный график");
                        otrPr += 2;
                        Program.currentShop.employes.Add(e);
                    }
                    else
                    {
                        loop++;
                        i--;
                        CountProd++;
                        flag = true;
                    }
                }

                DVS0 = Program.currentShop.VarSmens.FindAll(t => t.getDolgnost() == "Гастроном");
                DVS = DVS0.FindAll(t => t.getDeistvie() == true);
                if (DVS.Count == 0)
                {
                    Exception ex = new Exception("Не выбрана ни одна смена у гастрономов");
                    MessageBox.Show("Не выбрана ни одна смена у гастрономов");
                    return;
                }
                DVS2 = DVS.FindAll(t => !((t.getR() == 2) || (t.getR() == 3)));
                DVS3 = DVS.FindAll(t => !((t.getR() == 2) || (t.getR() == 3) || (t.getR() == 4)));
                DVS23 = DVS.FindAll(t => ((t.getR() == 2) || (t.getR() == 3)));

                nvs = -1;
                flag = true;
                loop = 0;
                for (int i = 300; CountGastr > 0; CountGastr--, i++)
                {
                    if (flag)
                    {


                        flaggastr2 = false;
                        flaggastr3 = false;

                        flag = false;

                    }
                    if (((DVS.Find(t => t.getR() == 2) != null) && (!flaggastr2) && (CountGastr > 1)) || (loop > 1000))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -1, LGastr[shiftGastr(ref ngastr)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountGastr--; i++;

                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 1, LGastr[ngastr].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);

                        flaggastr2 = true;
                        continue;
                    }
                    if (((DVS.Find(t => t.getR() == 3) != null) && (!flaggastr3) && (CountGastr > 1)) || (loop > 1000))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -2, LGastr[shiftGastr(ref ngastr)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountGastr--; i++;

                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 1, LGastr[ngastr].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);

                        flaggastr3 = true;
                        continue;
                    }

                    if ((DVS3.Count != 0 && CountGastr == 1) || ((DVS3.Count != 0) && f56gastr))
                    {
                        DateTime fd = new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1);
                        DataForCalendary d = new DataForCalendary(fd);
                        int otrab = d.getNWeekday() - 1;
                        e = new employee(Program.currentShop.getIdShop(), i, DVS3[shiftSm(ref nvs, ref flag, DVS3)], otrab, LGastr[shiftGastr(ref ngastr)].getOtobragenie(), "Сменный график");

                        Program.currentShop.employes.Add(e);
                    }
                    else
                    {
                        loop++;
                        i--;
                        CountGastr++;
                        flag = true;
                        if (DVS23.Count == 0)
                        {
                            f56gastr = true;
                        }
                    }
                }

                DVS0 = Program.currentShop.VarSmens.FindAll(t => t.getDolgnost() == "Кассир");
                DVS = DVS0.FindAll(t => t.getDeistvie() == true);
                if (DVS.Count == 0)
                {
                    Exception ex = new Exception();
                    MessageBox.Show("Не выбрана ни одна смена у кассиров");
                    return;
                }
                DVS2 = DVS.FindAll(t => !((t.getR() == 2) || (t.getR() == 3)));
                DVS3 = DVS.FindAll(t => !((t.getR() == 2) || (t.getR() == 3) || (t.getR() == 4)));

                int otrKass = 0;
                nvs = -1;
                flag = true;
                fflag = false;
                loop = 0;
                for (int i = 0; CountKassirov > 0; CountKassirov--, i++)
                {

                    if (flag)
                    {


                        flag2 = false;
                        flag3 = false;

                        flag = false;

                    }

                    if (((DVS.Find(t => t.getR() == 2) != null) && (!flag2) && (flagB) && (CountKassirov > 1)) || (loop > 1000))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -1, LKass[shiftKass(ref nkass)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountKassirov--; i++;
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 1, LKass[nkass].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        flag2 = true;
                        continue;
                    }
                    if (((DVS.Find(t => t.getR() == 3) != null) && (!flag3) && (CountKassirov > 1)) || (loop > 1000))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -2, LKass[shiftKass(ref nkass)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountKassirov--; i++;
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 1, LKass[nkass].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        flag3 = true;
                        continue;
                    }

                    if ((DVS2.Count != 0) && (!flag))
                    {
                        if ((nvs == 0) && (DVS2.Count == 2) && (CountKassirov == 2)) { otrKass = 2; }
                        if ((nvs == 1) && (DVS2.Count == 2) && (CountKassirov == 1)) { otrKass = -2; }
                        if (!fflag)
                        {
                            DateTime fd = new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1);
                            DataForCalendary d = new DataForCalendary(fd);
                            otrKass = (d.getNWeekday()) - 1;
                            fflag = true;
                        }
                        e = new employee(Program.currentShop.getIdShop(), i, DVS2[shiftSm(ref nvs, ref flag, DVS2)], otrKass, LKass[shiftKass(ref nkass)].getOtobragenie(), "Сменный график");
                        otrKass += 2;


                        Program.currentShop.employes.Add(e);
                    }
                    else
                    {
                        i--;
                        CountKassirov++;
                        flag = true;
                        flagB = true;
                        loop++;
                    }
                }


            }
            else
            {
                // throw new Exception("Недостаточное количество смен");
                MessageBox.Show("Недостаточное количество смен");
            }
        }



        static public void NewOtrabotal()
        {



            foreach (employee e in Program.currentShop.Semployes)
            {
                if (Program.currentShop.employes.Find(t => t.getID() == e.getID()) != null)
                {
                    if ((ForExcel.readVarSmen) && (e.getVS() != null))
                    {
                        Program.currentShop.employes.Find(t => t.getID() == e.getID()).setVS(e.getVS());
                    }
                    int o = e.getOtdih();
                    if (o < 0)
                    {
                        o = (-1) * o;
                        if (o >= Program.currentShop.employes.Find(t => t.getID() == e.getID()).getVS().getR())
                        {
                            o = (-1) * Program.currentShop.employes.Find(t => t.getID() == e.getID()).getVS().getV();
                        }
                        e.setOtrabotal(o);

                    }
                    else
                    {
                        o = o - Program.currentShop.employes.Find(t => t.getID() == e.getID()).getVS().getV();
                        if (o > 0)
                        {
                            o = 0;
                        }
                        e.setOtrabotal(o);
                    }
                    Program.currentShop.employes.Find(t => t.getID() == e.getID()).setOtrabotal(e.getOtrabotal());

                }
                else
                {
                    if ((e.getVS().getR() == 2) || (e.getVS().getR() == 3))
                    {
                        if (Program.currentShop.VarSmens.Find(t => (t.getDeistvie() == true) && (t.getDolgnost() == e.GetDolgnost()) && (t.getR() > t.getV())) != null)
                        {
                            e.setVS(Program.currentShop.VarSmens.Find(t => (t.getDeistvie() == true) && (t.getDolgnost() == e.GetDolgnost()) && (t.getR() > t.getV())));
                        }
                    }
                }
            }
            //   if ((Program.currentShop.employes.Count - Program.currentShop.Semployes.Count)>0) {
            foreach (employee e in Program.currentShop.employes)
            {
                if (Program.currentShop.Semployes.Find(t => t.getID() == e.getID()) != null)
                {

                }
                else
                {
                    if ((e.getVS().getR() == 2) || (e.getVS().getR() == 3))
                    {
                        if (Program.currentShop.VarSmens.Find(t => (t.getDeistvie() == true) && (t.getDolgnost() == e.GetDolgnost()) && (t.getR() > t.getV())) != null)
                        {
                            e.setVS(Program.currentShop.VarSmens.Find(t => (t.getDeistvie() == true) && (t.getDolgnost() == e.GetDolgnost()) && (t.getR() > t.getV())));
                        }
                    }
                }
                //    }
            }
        }

        static public void StarSmen()
        {
            foreach (employee e in Program.currentShop.Semployes)
            {
                if (Program.currentShop.employes.Find(t => t.getID() == e.getID()) != null)
                {
                    Program.currentShop.employes.Find(t => t.getID() == e.getID()).smens.AddRange(e.smens);
                }
            }
        }

        static public bool CreateSmens(bool current, List<employee> PLE)
        {
            Smena sm;
            bool sort = false;
            bool count;
            int sdvig = 2;


            List<employee> emplo = PLE.FindAll((t => (t.getStatus() == 0) && (t.GetTip() == 1)));
            emplo.Sort(delegate (employee s1, employee s2)
            { return s2.getVS().getR().CompareTo(s1.getVS().getR()); });

            count = false;
            if (emplo.Count > 4) { count = true; }

            int minnku = Program.currentShop.MouthPrognozT.Min(t => t.minKassUtr.Ncount);
            int minnkv = Program.currentShop.MouthPrognozT.Min(t => t.minKassVech.Ncount);


            foreach (TemplateWorkingDay wd in Program.currentShop.MouthPrognozT)
            {
                sdvig = 2;
                int ostatok = -1;
                int start = wd.DS.getStartDaySale() + 1;
                wd.lss = wd.lss.DistinctBy(p => p.getStartSmena()).ToList();
                wd.lss = wd.lss.FindAll(p => p.getStartSmena() > wd.DS.getStartDaySale()).ToList();
                wd.lss = wd.lss.FindAll(p => p.getLenght() > 6).ToList();

                if (count)
                {
                    int ck = 0;
                    if (wd.DS.hoursSale.Find(t => t.getNHour() == wd.DS.getStartDaySale().ToString()) != null)
                    {
                        if (Program.ParametrOptimization == 0)
                        {
                            ck = (int)Math.Round(((wd.DS.hoursSale.Find(t => t.getNHour() == (wd.DS.getStartDaySale() + 1).ToString()).getCountCheck()) / (double)60));
                        }
                        else
                        {
                            ck = (int)Math.Ceiling(((wd.DS.hoursSale.Find(t => t.getNHour() == (wd.DS.getStartDaySale() + 1).ToString()).getCountCheck()) / (double)60));

                        }
                    }
                    if (ck > wd.minKassUtr.Ncount)
                    {
                        wd.minKassUtr.Ncount = ck;

                    }

                    int ck2 = 0;
                    int ps = wd.DS.getEndDaySale() - 1;
                    if (wd.DS.hoursSale.Find(t => t.getNHour() == ps.ToString()) != null)
                    {
                        if (Program.ParametrOptimization == 0)
                        {
                            ck2 = (int)Math.Round(((wd.DS.hoursSale.Find(t => t.getNHour() == (wd.DS.getEndDaySale() - 2).ToString()).getCountCheck()) / (double)60));
                        }
                        else
                        {
                            ck2 = (int)Math.Ceiling(((wd.DS.hoursSale.Find(t => t.getNHour() == (wd.DS.getEndDaySale() - 2).ToString()).getCountCheck()) / (double)60));

                        }
                    }

                    if (ck2 > wd.minKassVech.Ncount)
                    {

                        wd.minKassVech.Ncount = ck2;
                    }
                    if (Program.currentShop.SortSotr)
                    {
                        if (sort)
                        {
                            emplo.Sort(delegate (employee s1, employee s2)
                            { if (s1.getVS().getR() == s2.getVS().getR()) { return s1.getID().CompareTo(s2.getID()); } else { return 0; } });
                            sort = false;
                        }
                        else
                        {
                            emplo.Sort(delegate (employee s1, employee s2)
                            { if (s1.getVS().getR() == s2.getVS().getR()) { return s2.getID().CompareTo(s1.getID()); } else { return 0; } });

                            sort = true;
                        }
                    }
                }


                if (Program.currentShop.SortSotr)
                {
                    if (wd.getData().Day == 15)
                    {
                        emplo.Sort(delegate (employee s1, employee s2)
                        { return s2.getID().CompareTo(s1.getID()); });
                    }
                }
                if (wd.DS.getTip() == 8)
                {
                    //emplo = emplo.OrderBy(u => u.getVS().getR()).ThenByDescending(u => u.getOtdihInHolyday()).ToList();
                }

                //проверки пройдены выше
                foreach (var emp in emplo.FindAll(t => t.getOtrabotal() < 0))
                {
                    emp.otrabDay(wd.getData());
                    emp.OtrabotalDay();

                }
                bool kassutr = true;
                bool kassvech = true;
                while (!EZanyt(wd.getData(), emplo))
                {


                    var e = emplo.FindAll(t => (t.getOtrabotal() >= 0 && t.TipTekSmen == 0));

                    if (e.Count()>0) {
                        var eu = emplo.FindAll(t => t.getOtrabotal() >= 0 && t.TipTekSmen == 1);
                        var ev = emplo.FindAll(t => t.getOtrabotal() >= 0 && t.TipTekSmen == 3);

                        int nu = minnku - eu.Count();
                        int nv = minnkv - ev.Count();
                        /*int nd = e.Count() - nu - nv;
                        while (nd < 0)
                        {
                            nu--;
                            nv--;
                            nd = e.Count() - nu - nv;
                        }*/

                        while ((nu > 0 || nv > 0) && (e.Count() > 0))
                        {
                            if (nu > 0)
                            {
                                var e1 = e.Find(t => (t.getVS().getR() == 2 || t.getVS().getR() == 3) && t.TipTekSmen == 0);

                                if (e1 == null)
                                {
                                    e1 = e.Find(t => t.TipTekSmen == 0);
                                }

                                e1.TipTekSmen = 1;
                                nu--;
                            }

                            if (nv > 0)
                            {
                                var e1 = e.Find(t => (t.getVS().getR() == 2 || t.getVS().getR() == 3) && t.TipTekSmen == 0);

                                if (e1 == null)
                                {
                                    e1 = e.Find(t => t.TipTekSmen == 0);

                                }
                                e1.TipTekSmen = 3;
                                nv--;
                            }
                        }




                        foreach (var e2 in e.FindAll(t => t.getOtrabotal() >= 0 && t.TipTekSmen == 0)) {
                            e2.TipTekSmen = 2;
                        }
                    }


                    /*

                   if (((emp.getVS().getR() == 4) || (emp.getVS().getR() == 5) || (emp.getVS().getR() == 6)) && (wd.DS.getTip() == 8) && (emp.getOtrabotal() >= 0))
                     {

                         if (emp.getOtdihInHolyday()==0) {
                             emp.setOtdihInHolyday(1);
                                }

                         if ((wd.minKassUtr.Ncount == 0)&&(wd.minKassVech.Ncount==0)) {

                             List<employee> les = Program.currentShop.employes.FindAll(t => (t.getOtdihInHolyday() != 0 && t.GetDolgnost() == "Кассир"));
                             int max =les.Max(t=>t.getOtdihInHolyday());
                             int over = les.Sum(t => t.getOtdihInHolyday()) / les.Count();
                          //   if ((emp.getOtdihInHolyday() < max)||(max==over)) {
                                 emp.OtrabotalDay();
                                 emp.setOtdihInHolyday(emp.getOtdihInHolyday() + 1);
                                 continue;
                           //  }
                         }
                     }
                     */



                    if ((((wd.minKassUtr.Ncount > 0) && (wd.minKassVech.Tcount > 0)) || (wd.minKassUtr.Tcount == 0)))
                    {
                        int tts = 1;
                        employee emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.TipTekSmen == 1) && ((t.getVS().getR() == 2) || (t.getVS().getR() == 3))));
                        if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.TipTekSmen == 1)));
                        }
                        if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.getTipSmen() == 4)));
                            tts = 4;
                        }
                        if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.TipTekSmen == 2)));
                            if (emp != null)
                            {
                                emp.setTipSmen(4);
                                tts = 4;
                            }

                        }

                        if (emp != null)
                        {
                            int dlina = emp.getVS().getDlina();
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            sm.setTip(tts);
                            emp.AddSmena(sm);
                            sm.Zanyta();
                            emp.TipTekSmen = 1;
                            wd.minKassUtr.Ncount--;
                            wd.minKassUtr.Tcount++;
                            emp.otrabDay(wd.getData());
                            emp.OtrabotalDay();
                            kassutr = true;
                        }
                        else
                        {
                            kassutr = false;
                        }
                    }
                    else { kassutr = false; }

                    if ((wd.minKassVech.Ncount > 0))
                    {
                        int tts = 3;
                        employee emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.TipTekSmen == 3) && ((t.getVS().getR() == 2) || (t.getVS().getR() == 3))));
                        if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.TipTekSmen == 3)));
                        }
                        if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.getTipSmen() == 5)));
                            tts = 5;
                        }
                        if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.TipTekSmen == 2)));
                            if (emp != null)
                            {
                                emp.setTipSmen(5);
                                tts = 5;
                            }
                        }
                        if (emp != null)
                        {
                            int dlina = emp.getVS().getDlina();
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), wd.DS.getEndDaySale() - dlina, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            sm.setTip(tts);
                            emp.AddSmena(sm);
                            sm.Zanyta();
                            emp.TipTekSmen = 3;
                            wd.minKassVech.Ncount--;
                            wd.minKassVech.Tcount++;
                            emp.otrabDay(wd.getData());
                            emp.OtrabotalDay();
                            kassvech = true;
                        }
                        else
                        {
                            kassvech = false;
                        }
                    }
                    else { kassvech = false; }

                    if (kassutr || kassvech) { continue; }

                    if (wd.minKassUtr.Ncount > 0)
                    {
                        employee emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0)&& (t.getTipSmen() != 5) && ((t.getVS().getR() == 2) || (t.getVS().getR() == 3))));
                        if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && ((t.getVS().getR() == 2) || (t.getVS().getR() == 3))));
                        }
                            if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0)));
                        }
                        
                        if (emp != null)
                        {
                            int dlina = emp.getVS().getDlina();
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            sm.setTip(6);
                            emp.AddSmena(sm);
                            sm.Zanyta();
                            emp.TipTekSmen = 1;
                            wd.minKassUtr.Ncount--;
                            wd.minKassUtr.Tcount++;
                            emp.otrabDay(wd.getData());
                            emp.OtrabotalDay();
                        }
                    }

                    if (wd.minKassVech.Ncount > 0) {

                        employee emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getTipSmen() != 4)&&(t.getOtrabotal() >= 0) && ((t.getVS().getR() == 2) || (t.getVS().getR() == 3))));
                        if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && ((t.getVS().getR() == 2) || (t.getVS().getR() == 3))));
                        }
                            if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0)));
                        }
                        
                        if (emp != null)
                        {
                            int dlina = emp.getVS().getDlina();
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), wd.DS.getEndDaySale() - dlina, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            sm.setTip(7);
                            emp.AddSmena(sm);
                            sm.Zanyta();
                            emp.TipTekSmen = 3;
                            wd.minKassVech.Ncount--;
                            wd.minKassVech.Tcount++;
                            emp.otrabDay(wd.getData());
                            emp.OtrabotalDay();
                        }
                      }


                    if (wd.DS.getTip() == 8)
                    {
                        if (wd.lss.FindAll(t =>(t.isZanyta()==false)).Count()>0) {
                            employee emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.TipTekSmen == 2)));
                            if (emp == null)
                            {
                                emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && ((t.TipTekSmen == 7) || (t.TipTekSmen == 6))));
                            }
                            if (emp == null)
                            {
                                emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && ((t.TipTekSmen == 4) || (t.TipTekSmen == 5))));
                            }
                            if (emp == null)
                            {
                                emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && ((t.TipTekSmen == 4) || (t.TipTekSmen == 5))));
                            }
                            if (emp == null)
                            {
                                emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) ));
                            }
                            if (emp != null)
                            {
                                int dlina = emp.getVS().getDlina();
                                Smena s = wd.lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale()) && (!t.isZanyta()) && (t.getStartSmena() != (start + 1)));
                                if (s != null)
                                {
                                    start = s.getStartSmena();
                                    if (s.getLenght() > dlina)
                                    {
                                        ostatok = s.getEndSmena();
                                    }
                                    s.Zanyta();
                                    s.setTip(2);
                                    emp.AddSmena(s);

                                    emp.TipTekSmen = 2;
                                    emp.otrabDay(wd.getData());
                                    emp.OtrabotalDay();
                                }
                            }
                        }

                        else {
                            break;
                        }
                    }
                    else {
                        employee emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0)&&(t.TipTekSmen==1)));
                        if (emp!=null) {
                            int dlina = emp.getVS().getDlina();
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            sm.setTip(2);
                            emp.AddSmena(sm);
                            sm.Zanyta();
                            emp.TipTekSmen = 1;
                            wd.minKassUtr.Ncount--;
                            wd.minKassUtr.Tcount++;
                            emp.otrabDay(wd.getData());
                            emp.OtrabotalDay();
                        }
                        emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.TipTekSmen == 3)));
                        if (emp != null)
                        {
                            int dlina = emp.getVS().getDlina();
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), wd.DS.getEndDaySale() - dlina, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            sm.setTip(2);
                            emp.AddSmena(sm);
                            sm.Zanyta();
                            emp.TipTekSmen = 3;
                            wd.minKassVech.Ncount--;
                            wd.minKassVech.Tcount++;
                            emp.otrabDay(wd.getData());
                            emp.OtrabotalDay();
                        }
                         emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0)));
                         
                        if (emp != null)
                        {
                            int dlina = emp.getVS().getDlina();
                            if (ostatok > 0)
                            {
                                start = ostatok - dlina;
                                ostatok = -1;
                            }

                            else if (wd.lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale()) && (!t.isZanyta()) && (t.getStartSmena() != (start + 1))) != null)
                            {
                                Smena s = wd.lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale()) && (!t.isZanyta()) && (t.getStartSmena() != (start + 1)));
                                start = s.getStartSmena();
                                if (s.getLenght() > dlina)
                                {
                                    ostatok = s.getEndSmena();
                                }
                                s.Zanyta();
                            }
                            else if (wd.lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale()) && (t.getStartSmena() != start)) != null)
                            {

                                Smena s = wd.lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale()));
                                start = s.getStartSmena() + sdvig;
                                sdvig += 2;
                                s.Zanyta();
                            }
                            Smena sm1 = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm1.getEndSmena() >= wd.DS.getEndDaySale())
                            {
                                sm1.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina);
                            }
                            sm1.setTip(2);
                            emp.AddSmena(sm1);

                            emp.TipTekSmen = 2;
                            emp.otrabDay(wd.getData());
                            emp.OtrabotalDay();
                        }
                    }

                  



                }


            }




            foreach (employee emp in emplo)
                emp.setStatus(1);

            emplo.Clear();
            sort = false;


            //Продавцы
            emplo = PLE.FindAll(t => (t.getStatus() == 0) && (t.GetTip() == 2));
            count = false;
            if (emplo.Count > 6) { count = true; }


            foreach (TemplateWorkingDay wd in Program.currentShop.MouthPrognozT)
            {

                if (count)
                {
                    int ck = 0;
                    if (wd.DS.hoursSale.Find(t => t.getNHour() == wd.DS.getStartDaySale().ToString()) != null)
                    {
                        if (Program.ParametrOptimization == 0)
                        {
                            ck = (int)Math.Round((wd.DS.hoursSale.Find(t => t.getNHour() == wd.DS.getStartDaySale().ToString()).getCountCheck() / (double)60));
                        }
                        else
                        {
                            ck = (int)Math.Ceiling((wd.DS.hoursSale.Find(t => t.getNHour() == wd.DS.getStartDaySale().ToString()).getCountCheck() / (double)60));
                        }
                    }
                    if (ck > wd.minProdUtr.Ncount)
                    {
                        wd.minProdUtr.Ncount = ck;

                    }

                    int ck2 = 0;
                    int ps = wd.DS.getEndDaySale() - 1;
                    if (wd.DS.hoursSale.Find(t => t.getNHour() == ps.ToString()) != null)
                    {
                        if (Program.ParametrOptimization == 0)
                        {
                            ck2 = (int)Math.Round(wd.DS.hoursSale.Find(t => t.getNHour() == "21").getCountCheck() / (double)60);

                        }
                        else
                        {
                            ck2 = (int)Math.Ceiling(wd.DS.hoursSale.Find(t => t.getNHour() == "21").getCountCheck() / (double)60);
                        }
                    }
                    if (ck2 > wd.minProdVech.Ncount)
                    {

                        wd.minProdVech.Ncount = ck2;
                    }
                    if (Program.currentShop.SortSotr)
                    {
                        if (sort)
                        {
                            emplo.Sort(delegate (employee s1, employee s2)
                            { return s1.getID().CompareTo(s2.getID()); });
                            sort = false;
                        }
                        else
                        {
                            emplo.Sort(delegate (employee s1, employee s2)
                            { return s2.getID().CompareTo(s1.getID()); });
                            sort = true;
                        }
                    }
                }

                int start = wd.DS.getStartDaySale();
                if (Program.currentShop.SortSotr)
                {
                    if (wd.getData().Day == 15)
                    {
                        emplo.Sort(delegate (employee s1, employee s2)
                        { return s2.getID().CompareTo(s1.getID()); });
                    }
                }

                if (wd.DS.getTip() == 8)
                {
                    // emplo =  emplo.OrderBy(u => u.getVS().getR()).ThenByDescending(u => u.getOtdihInHolyday()).ToList();
                }

                foreach (var emp in emplo.FindAll(t => t.getOtrabotal() < 0))
                {
                    emp.otrabDay(wd.getData());
                    emp.OtrabotalDay();
                }
                while (!EZanyt(wd.getData(), emplo))
                {




                    /*    if (((emp.getVS().getR() == 4) || (emp.getVS().getR() == 5) || (emp.getVS().getR() == 6)) && (wd.DS.getTip() == 8)&&(emp.getOtrabotal()>=0))
                        {

                            if (emp.getOtdihInHolyday() == 0)
                            {
                                emp.setOtdihInHolyday(1);
                            }

                            if ((wd.minProdUtr.Ncount == 0) && (wd.minProdVech.Ncount == 0))
                            {

                                List<employee> les = Program.currentShop.employes.FindAll(t => (t.getOtdihInHolyday() != 0 && t.GetDolgnost() == "Продавец"));
                                int max = les.Max(t => t.getOtdihInHolyday());
                                int over = les.Sum(t => t.getOtdihInHolyday()) / les.Count();
                               // if ((emp.getOtdihInHolyday() < max) || (max == over))
                               // {
                                    emp.OtrabotalDay();
                                    emp.setOtdihInHolyday(emp.getOtdihInHolyday() + 1);
                                    continue;
                              //  }
                            }
                        }*/

                    if (Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() != start) && (!t.isZanyta())) != null)
                    {
                        start = Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale()) && (!t.isZanyta()) && (t.getStartSmena() != start)).getStartSmena() - 1;
                    }
                    else if (Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() != start)) != null)
                    {
                        start = Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale())).getStartSmena() - 1;
                    }






                    if (((wd.minProdUtr.Ncount > 0) && (wd.minProdVech.Tcount > 0)) || (wd.minProdUtr.Tcount == 0))
                    {

                        employee emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.TipTekSmen == 1)));
                        if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0)));
                        }
                        int dlina = emp.getVS().getDlina();



                        start = wd.DS.getStartDaySale();
                        sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                        if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                        emp.AddSmena(sm);
                        sm.Zanyta();
                        emp.TipTekSmen = 1;
                        wd.minProdUtr.Ncount--;
                        wd.minProdUtr.Tcount++;
                        emp.otrabDay(wd.getData());
                        emp.OtrabotalDay();


                    }

                    else if ((wd.minProdVech.Ncount > 0))
                    {
                        employee emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0) && (t.TipTekSmen == 3)));
                        if (emp == null)
                        {
                            emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0)));
                        }
                        int dlina = emp.getVS().getDlina();

                        start = wd.DS.getEndDaySale() - dlina;

                        if (emp.getOtrabotal() >= 0)
                        {
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            emp.AddSmena(sm);
                            sm.Zanyta();
                            emp.TipTekSmen = 3;
                            wd.minProdVech.Ncount--;
                            wd.minProdVech.Tcount++;

                        }

                        emp.otrabDay(wd.getData());
                        emp.OtrabotalDay();

                    }

                    else
                    {
                        employee emp = emplo.Find(t => ((t.getStatusDay(wd.getData()) != 1) && (t.getOtrabotal() >= 0)));

                        int dlina = emp.getVS().getDlina();
                        if (emp.getOtrabotal() >= 0)
                        {

                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            emp.AddSmena(sm);
                            sm.Zanyta();
                            emp.TipTekSmen = 2;

                        }
                        emp.otrabDay(wd.getData());
                        emp.OtrabotalDay();

                    }
                }

            }
            foreach (employee emp in emplo)
                emp.setStatus(1);
            //гастрономы
            emplo.Clear();
            sort = false;
            emplo = PLE.FindAll(t => (t.getStatus() == 0) && (t.GetTip() == 4));
            count = false;
            if (emplo.Count > 6) { count = true; }
            foreach (TemplateWorkingDay wd in Program.currentShop.MouthPrognozT)
            {

                if ((sort) && (count))
                {
                    emplo.Sort(delegate (employee s1, employee s2)
                    { return s1.getID().CompareTo(s2.getID()); });
                    sort = false;
                }
                else
                {
                    emplo.Sort(delegate (employee s1, employee s2)
                    { return s2.getID().CompareTo(s1.getID()); });
                    sort = true;
                }
                foreach (employee emp in emplo)
                {
                    int dlina = emp.getVS().getDlina();


                    int start = wd.DS.getStartDaySale();
                    if (Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale())) != null)
                    {
                        start = Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale())).getStartSmena();
                    }
                    else
                    {

                    }

                    if (((emp.getVS().getR() == 4) || (emp.getVS().getR() == 5) || (emp.getVS().getR() == 6)) && (wd.DS.getTip() == 9))
                    {
                        // dlina -= 1;
                    }



                    else
                    {

                        if (((wd.minGastrUtr.Ncount > 0) && (wd.minGastrVech.Tcount > 0)) || (wd.minGastrUtr.Tcount == 0))
                        {


                            if (emp.getOtrabotal() >= 0)
                            {
                                start = wd.DS.getStartDaySale();
                                sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                                if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                                emp.AddSmena(sm);

                                emp.TipTekSmen = 2;
                                wd.minGastrUtr.Ncount--;
                                wd.minGastrUtr.Tcount++;

                            }

                        }

                        else if ((wd.minGastrVech.Ncount > 0))
                        {
                            start = wd.DS.getEndDaySale() - dlina;

                            if (emp.getOtrabotal() >= 0)
                            {
                                sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                                if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                                emp.AddSmena(sm);

                                emp.TipTekSmen = 3;
                                wd.minGastrVech.Ncount--;
                                wd.minGastrVech.Tcount++;

                            }

                        }

                        else if (emp.getOtrabotal() >= 0)
                        {

                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            emp.AddSmena(sm);

                            emp.TipTekSmen = 1;

                        }
                        emp.OtrabotalDay();
                    }
                }

            }
            foreach (employee emp in emplo)
                emp.setStatus(1);
            //грузчики
            emplo.Clear();
            sort = false;
            emplo = PLE.FindAll(t => (t.getStatus() == 0) && (t.GetTip() == 3));
            count = false;
            if (emplo.Count > 6) { count = true; }
            foreach (TemplateWorkingDay wd in Program.currentShop.MouthPrognozT)
            {
                if ((sort) && (count))
                {
                    emplo.Sort(delegate (employee s1, employee s2)
                    { return s1.getID().CompareTo(s2.getID()); });
                    sort = false;
                }
                else
                {
                    emplo.Sort(delegate (employee s1, employee s2)
                    { return s2.getID().CompareTo(s1.getID()); });
                    sort = true;
                }
                foreach (employee emp in emplo)
                {

                    int dlina = emp.getVS().getDlina();
                    if ((wd.DS.getTip() == 6) || (wd.DS.getTip() == 7))
                    {
                        dlina = emp.getVS().getDlina() - 1;
                    }


                    int start = wd.DS.getStartDaySale();
                    if (wd.lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale())) != null)
                    {
                        start = Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale())).getStartSmena();
                    }
                    else
                    {

                    }
                    if ((wd.minGruzUtr.Ncount > 0))
                    {


                        if (emp.getOtrabotal() >= 0)
                        {
                            start = wd.DS.getStartDaySale();
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            emp.AddSmena(sm);

                            emp.TipTekSmen = 2;
                            wd.minGruzUtr.Ncount--;
                            wd.minGruzUtr.Tcount++;

                        }

                    }

                    else if ((wd.minGruzVech.Ncount > 0))
                    {
                        if (wd.DS.getEndDaySale() >= 22)
                        {

                            start = 23 - dlina;
                        }
                        else
                        {
                            start = wd.DS.getEndDaySale() - dlina;
                        }


                        if (emp.getOtrabotal() >= 0)
                        {
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina - 1);
                            //if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(23 - dlina, dlina); }
                            emp.AddSmena(sm);

                            emp.TipTekSmen = 3;
                            wd.minGruzVech.Ncount--;
                            wd.minGruzVech.Tcount++;

                        }

                    }

                    else if (emp.getOtrabotal() >= 0)
                    {

                        sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina - 1);
                        if (sm.getEndSmena() > 22) { sm.SetStarnAndLenght(23 - dlina, dlina); }
                        emp.AddSmena(sm);

                        emp.TipTekSmen = 1;

                    }
                    emp.OtrabotalDay();
                    foreach (Smena sm1 in emp.smens)
                        if (sm1.getLenght() > 12)
                        {
                            int x = 0;


                        }
                }

            }
            foreach (employee emp in emplo)
                emp.setStatus(1);

            emplo = PLE.FindAll(t => (t.getStatus() == 0));
            foreach (employee emp in emplo)
            {
                int dlina = emp.getVS().getDlina();



                foreach (TemplateWorkingDay wd in Program.currentShop.MouthPrognozT)
                {
                    int start = Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() > wd.DS.getStartDaySale()).getStartSmena();

                    if ((emp.TipTekSmen == 3))
                    {


                        if (emp.getOtrabotal() >= 0)
                        {
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            emp.AddSmena(sm);

                            emp.TipTekSmen = 2;

                        }

                    }

                    else if (emp.TipTekSmen == 1)
                    {
                        start = wd.DS.getEndDaySale() - dlina;

                        if (emp.getOtrabotal() >= 0)
                        {
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            emp.AddSmena(sm);
                            emp.TipTekSmen = 3;

                        }

                    }

                    else if (emp.getOtrabotal() >= 0)
                    {
                        start = wd.DS.getStartDaySale();
                        sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                        if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                        emp.AddSmena(sm);

                        emp.TipTekSmen = 1;

                    }
                    emp.OtrabotalDay();
                }
                emp.setStatus(1);
            }


            emplo.Clear();
            emplo = PLE.FindAll(t => t.getStatus() == 1);
            // MessageBox.Show("2");
            foreach (employee emp in emplo)
            {
                int dop = 0;
                if ((current) && (Program.currentShop.Semployes.Find(t => t.getID() == emp.getID()) != null))
                {
                    dop = Program.currentShop.Semployes.Find(t => t.getID() == emp.getID()).smens.Count();
                }

                while (emp.getNormRab() > Program.normchas + dop)
                {

                    if ((emp.smens.Find(t => t.getLenght() > 12) != null) && (!current))
                    {
                        Smena s = emp.smens.Find(t => t.getLenght() > 12);
                        if (((current) && (s.getData() > DateTime.Now)) || (!current))
                        {
                            s.delChas(Program.currentShop.MouthPrognozT.Find(f => f.DS.getData() == s.getData()));
                        }

                    }
                    else
                    {
                        foreach (Smena sm1 in emp.smens)
                        {
                            if (current)
                            {
                                if (sm1.getData().Day <= DateTime.Now.Day)
                                {
                                    continue;
                                }
                            }
                            if (sm1 != null)
                                // {
                                sm1.delChas(Program.currentShop.MouthPrognozT.Find(f => f.DS.getData().Date == sm1.getData().Date));
                            // }else { MessageBox.Show(emp.smens.Count+"count  id=" +emp.getID()); }
                            if (emp.getNormRab() == Program.normchas) break;
                        }
                    }

                }


                int x = 0;
                int vh = 0;
                while (emp.getNormRab() < (Program.normchas + dop))
                {

                    // MessageBox.Show("Меньше");
                    if ((current) && (!(Program.currentShop.Semployes.Find(t => t.getID() == emp.getID()) != null)))
                    {
                        break; ;
                    }

                    Smena s = emp.smens.Find(t => t.getLenght() < 6);
                    if (s != null)
                    {
                        var mpd = Program.currentShop.MouthPrognozT.Find(f => f.DS.getData() == s.getData());
                        if ((mpd.DS.getTip() != 6) && (mpd.DS.getTip() != 7) && (mpd.DS.getTip() != 8) && (mpd.DS.getTip() != 9) || (vh > 100))
                        {
                            if (emp.GetTip() == 3)
                            {
                                s.addChas2(mpd);
                            }
                            else
                            {
                                s.addChas(mpd);
                            }
                        }
                        vh++;
                    }
                    else
                    {


                        foreach (Smena sm1 in emp.smens)
                        {
                            if (current)
                            {
                                if (sm1.getData().Day <= DateTime.Now.Day)
                                {
                                    continue;
                                }
                            }
                            if ((sm1 != null) && (sm1.getLenght() < 12))
                            {
                                var mpd = Program.currentShop.MouthPrognozT.Find(f => f.DS.getData() == sm1.getData());
                                if ((mpd.DS.getTip() != 6) && (mpd.DS.getTip() != 7) && (mpd.DS.getTip() != 8) && current && (mpd.DS.getTip() != 9) || (x > emp.smens.Count))
                                {
                                    if (emp.GetTip() == 3)
                                    {
                                        sm1.addChas2(mpd);
                                    }
                                    else
                                    {
                                        sm1.addChas(mpd);
                                    }
                                    
                                }
                                else if ((mpd.DS.getTip() != 6) && (mpd.DS.getTip() != 7) && (mpd.DS.getTip() != 8) && (mpd.DS.getTip() != 9) || (x > emp.smens.FindAll(t => t.getData().Day > DateTime.Now.Day).Count))
                                {
                                    if (emp.GetTip() == 3)
                                    {
                                        sm1.addChas2(mpd);
                                    }
                                    else
                                    {
                                        sm1.addChas(mpd);
                                    }
                                }
                                x++;
                            }
                            else
                            {
                                x++;
                            }
                            if ((x == 5 * emp.smens.Count) && (!current))
                            {
                                return false;
                            }
                            if ((x == 5 * emp.smens.FindAll(t => t.getData().Day > DateTime.Now.Day).Count) && (current))
                            {
                                return false;
                            }
                            // }else { MessageBox.Show(emp.smens.Count+"count  id=" +emp.getID()); }
                            if (emp.getNormRab() == (Program.normchas + dop)) break;
                        }
                    }

                }
                emp.setStatus(2);
            }
            if (current)
            {
                Program.itogChass2();
            }
            else
            {
                Program.itogChass();
            }

            return true;
        }


        /*    public static bool CreateSmens()
             {
                 if (Program.currentShop.MouthPrognozT.Count == 0)
                 {


                     return false;
                 }
                 List<employee> emplo = Program.currentShop.employes.FindAll((t => (t.getTip() == 1) || (t.getTip() == 2) || (t.getTip() == 3) || (t.getTip() == 4)));
                 foreach (employee emp in emplo)
                 {
                     int dlina = currentShop.VarSmens.Find(x => x.getTip() == emp.getTip()).getSrednee();



                     foreach (TemplateWorkingDay wd in currentShop.MouthPrognozT)
                     {
                         switch (emp.getTip())
                         {
                             case 1:
                                 switch (wd.DS.getTip())
                                 {
                                     case 1: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), wd.DS.getStartDaySale())); break;
                                     case 2: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), wd.DS.getStartDaySale())); break;
                                     case 3: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), wd.DS.getStartDaySale())); break;
                                     case 4: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), wd.DS.getStartDaySale())); break;
                                     case 5: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), wd.DS.getStartDaySale(), wd.DS.getStartDaySale())); break;
                                     default: break;
                                 }; break;
                             case 2:
                                 switch (wd.DS.getTip())
                                 {
                                     case 1:
                                         emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                     case 2:
                                         emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                     case 3:
                                         emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                     case 4:
                                         emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                     case 5: emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - dlina), dlina)); break;
                                     default: break;
                                 }; break;
                             case 3:
                                 //  MessageBox.Show(" TipDn" + wd.DS.getTip() + " Count= " + currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.FindAll(t => t.getStartSmena() != wd.DS.getStartDaySale()).Count);
                                 switch (wd.DS.getTip())
                                 {

                                     case 1: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() +2 ), 10));  } break;
                                     case 2: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     //case 3: emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getEndSmena() != wd.DS.getEndDaySale())); break;
                                     // case 4: emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() != wd.DS.getStartDaySale())); break;
                                     case 5: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 9: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 8: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 6: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 7: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;

                                     default: break;
                                 }; break;
                             case 4:
                                 //  MessageBox.Show("Count= " + currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.FindAll(t => t.getStartSmena() != wd.DS.getStartDaySale()).Count);
                                 switch (wd.DS.getTip())
                                 {
                                     case 8: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 9: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 4: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 5: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 1: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 2: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 6: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
                                     case 7: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getStartDaySale() + 2), 10)); } break;
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
                                     if ((wd.getData().Day % 4 == 3) || (wd.getData().Day % 2 == 0) && (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Count != 0))
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


                 foreach (employee emp in le)
                 {

                     int dbv = 0;
                     foreach (TemplateWorkingDay wd in currentShop.MouthPrognozT)
                     {
                         if (dbv < 6)
                         {
                             switch (wd.DS.getTip())
                             {
                                 case 8: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); dbv++; } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - 10), 10)); dbv++; } break;
                                 case 9: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); dbv++; } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - 10), 10)); dbv++; } break;
                                 case 2: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); dbv++; } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - 10), 10)); dbv++; } break;
                                 case 4: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); dbv++; } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - 10), 10)); dbv++; } break;
                                 case 6: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); dbv++; } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - 10), 10)); dbv++; } break;
                                 case 1: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); dbv++; } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - 10), 10)); dbv++; } break;
                                 case 3: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); dbv++; } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - 10), 10)); dbv++; } break;
                                 case 5: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); dbv++; } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - 10), 10)); dbv++; } break;
                                 case 7: if (currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab) != null) { emp.AddSmena(currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() >= currentShop.TimeMinRab)); dbv++; } else { emp.AddSmena(new Smena(currentShop.getIdShop(), wd.getData(), (wd.DS.getEndDaySale() - 10), 10)); dbv++; } break;

                                 default: break;
                             }; 
                         }
                         else {
                             dbv = 0;
                             continue;
                         }

                     }
                     emp.setStatus(2);


                 }


                     emp.setStatus(1);
                     //MessageBox.Show("status " + emp.getID());
                 }

                 le.Clear();
                 le = currentShop.employes.FindAll((t => (t.getTip() == 10) || (t.getTip() == 11) || (t.getTip() == 12)));
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
                                 else
                                 {
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
             }*/

        public static void CheckDisp()
        {







            /*       List<employee> le=Program.currentShop.employes.FindAll(t=>t.smens.Count==0);
               if (le.Count>0) {
                   Program.createPrognoz(false, false, false);
                   CreateSmens(false, le);
               }*/


            foreach (employee e in Program.currentShop.Semployes)
            {
                if (Program.currentShop.employes.Find(t => t.getID() == e.getID()) != null)
                {

                }
                else
                {
                    Program.currentShop.employes.Add(e);
                }
            }


            DateTime fd = DateTime.Now;
            daySale d;
            Program.currentShop.MouthPrognoz = new List<daySale>();
            for (int i = 1; i <= DateTime.DaysInMonth(fd.Year, fd.Month); i++)
            {

                d = new daySale(Program.currentShop.getIdShop(), new DateTime(fd.Year, fd.Month, i));
                //   d.whatTip();

                Program.currentShop.MouthPrognoz.Add(d);

            }

        }
    }

}


