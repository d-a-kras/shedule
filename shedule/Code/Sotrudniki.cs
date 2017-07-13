using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Code
{


 public  class Sotrudniki
    {
        static public int shiftSm(ref int i)
        {
            i++;
            if (i == Program.currentShop.VarSmens.FindAll(t => t.getDeistvie() == true).Count)
                i = 0;


            return i;

        }
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
            if (i == Program.currentShop.tsr.FindAll(t => t.getPosition() == "gastronom").Count)
                i = 0;


            return i;

        }

        public static void OptimCountSotr()
        {
            int nvs = -1;
            int nprod = -1;
            int ngastr = -1;
            int ngruz = -1;
            int nkass = -1;
            int kassCount = Program.MinKassirCount;
            List<VarSmen> DVS = Program.currentShop.VarSmens.FindAll(t => t.getDeistvie() == true);
            employee e;
            if (Program.currentShop.MouthPrognozT.Count==0)
            {
                //MessageBox.Show("Прогноз не создан");
                return;

            }
            Program.currentShop.employes.Clear();
            int K, KP;
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
            Program.normchas = Program.RD[dt.Month] * 8 - Program.PHD[dt.Month];

            int ob = 0;
            int tc = 0;
            foreach (TemplateWorkingDay t in Program.currentShop.MouthPrognozT)
            {
                tc += t.getClick();
                ob += t.getCapacityPCC();
            }

            List<TSR> LGruz = Program.currentShop.tsr.FindAll(t => t.getPosition() == "gruz");
            List<TSR> LGastr = Program.currentShop.tsr.FindAll(t => t.getPosition() == "gastronom");
            List<TSR> LProd = Program.currentShop.tsr.FindAll(t => t.getTip() == 2);
            List<TSR> LKass = Program.currentShop.tsr.FindAll(t => t.getTip() == 1);
            

            //((tc * TimeObrTov * 100) / (normchas * 3600 * KoefObr));
            int CountProd = LProd.Sum(o => o.getCount());
            int CountGastr = LGastr.Sum(o => o.getCount());
            int CountGruz = LGruz.Sum(o => o.getCount());
            int CountKassirov = ((ob * K / (Program.normchas*6  * 3600))) + Program.ParametrOptimization;

            if (CountKassirov < 4) { CountKassirov = 4; }
            if (CountProd < 4) { CountProd = 4; }
            if (CountGruz < 2) { CountGruz = 2; }

            bool flaggastr2 = false;
            bool flaggastr3 = false;


            for (int i = 300; CountGastr > 0; CountGastr--, i++)
            {

                if ((Program.currentShop.VarSmens.Find(t => t.getR() == 2) != null) && (!flaggastr2))
                {
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -2, LGastr[shiftGastr(ref ngastr)].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    CountGastr--; i++;
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 0, LGastr[ngastr].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    continue;
                }
                if ((Program.currentShop.VarSmens.Find(t => t.getR() == 3) != null) && (!flaggastr3))
                {
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -3, LGastr[shiftGastr(ref ngastr)].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    CountGastr--; i++;
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 0, LGastr[ngastr].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    continue;
                }

                e = new employee(Program.currentShop.getIdShop(), i, DVS[shiftSm(ref nvs)], i, LGastr[shiftGastr(ref ngastr)].getOtobragenie(), "Сменный график");

                Program.currentShop.employes.Add(e);

            }

            bool flagg2 = false;
            bool flagg3 = false;


            for (int i = 200; CountGruz > 0; CountGruz--, i++)
            {

                if ((Program.currentShop.VarSmens.Find(t => t.getR() == 2) != null) && (!flagg2))
                {
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -2, LGruz[shiftGruz(ref ngruz)].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    CountGruz--; i++;
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 0, LGruz[ngruz].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    continue;
                }
                if ((Program.currentShop.VarSmens.Find(t => t.getR() == 3) != null) && (!flagg3))
                {
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -3, LGruz[shiftGruz(ref ngruz)].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    CountGruz--; i++;
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 0, LGruz[ngruz].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    continue;
                }

                e = new employee(Program.currentShop.getIdShop(), i, DVS[shiftSm(ref nvs)], i, LGruz[shiftGruz(ref ngruz)].getOtobragenie(), "Сменный график");

                Program.currentShop.employes.Add(e);

            }

            bool flagp2 = false;
            bool flagp3 = false;


            for (int i = 100; CountProd > 0; CountProd--, i++)
            {

                if ((Program.currentShop.VarSmens.Find(t => t.getR() == 2) != null) && (!flagp2))
                {
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -2, LProd[shiftProd(ref nprod)].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    CountProd--; i++;
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 0, LProd[ nprod].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    continue;
                }
                if ((Program.currentShop.VarSmens.Find(t => t.getR() == 3) != null) && (!flagp3))
                {
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -3, LProd[shiftProd(ref nprod)].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    CountProd--; i++;
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 0, LProd[nprod].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    continue;
                }

                e = new employee(Program.currentShop.getIdShop(), i, DVS[shiftSm(ref nvs)], i,LProd[shiftProd(ref nprod)].getOtobragenie(), "Сменный график");

                Program.currentShop.employes.Add(e);

            }

            bool flag2 = false;
            bool flag3 = false;


            for (int i = 0; CountKassirov > 0; CountKassirov--, i++)
            {
                if ((Program.currentShop.VarSmens.Find(t => t.getR() == 2) != null)&&(!flag2))
                {
                     e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -2, LKass[shiftKass(ref nkass)].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    CountKassirov--; i++;
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 0, LKass[ nkass].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    continue;
                }
                if ((Program.currentShop.VarSmens.Find(t => t.getR() == 3) != null) && (!flag3))
                {
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -3, LKass[shiftKass(ref nkass)].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    CountKassirov--; i++;
                    e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 0, LKass[ nkass].getOtobragenie(), "Сменный график");
                    Program.currentShop.employes.Add(e);
                    continue;
                }
                 e = new employee(Program.currentShop.getIdShop(), i, DVS[shiftSm(ref nvs)], i,LKass[shiftKass(ref nkass)].getOtobragenie(), "Сменный график");

                Program.currentShop.employes.Add(e);

            }
        }


       static public void CreateSmens()
        {
            List<employee> emplo = Program.currentShop.employes.FindAll((t => (t.getStatus() == 0) && (t.GetTip() == 1)));
            foreach (employee emp in emplo)
            {
                int dlina = emp.getVS().getDlina();



                foreach (TemplateWorkingDay wd in Program.currentShop.MouthPrognozT)
                {
                    int start = Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() > wd.DS.getStartDaySale()) && (!t.isZanyta())).getStartSmena();


                    if (((emp.getVS().getR() == 4) || (emp.getVS().getR() == 5) || (emp.getVS().getR() == 6)) && (wd.DS.getTip() == 8))
                    {

                        emp.OtrabotalDay();
                    }
                    else if(((emp.getVS().getR() == 4) || (emp.getVS().getR() == 5) || (emp.getVS().getR() == 6)) && (wd.DS.getTip() == 9)) {
                        dlina -= 1;
                    }
                    else
                    {
                        if ((wd.minSotrUtr > 0) && (emp.TipTekSmen != 1))
                        {
                            start = wd.DS.getStartDaySale();

                            if (emp.getOtrabotal() >= 0)
                            {

                                emp.AddSmena(new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina));

                                emp.TipTekSmen = 1;
                                wd.minSotrUtr--;
                            }

                        }

                        else if ((wd.minSotrVech > 0) && (emp.TipTekSmen == 1))
                        {
                            start = wd.DS.getEndDaySale() - dlina;

                            if (emp.getOtrabotal() >= 0)
                            {
                                emp.AddSmena(new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina));

                                emp.TipTekSmen = 3;
                                wd.minSotrVech--;
                            }

                        }

                        else if (emp.getOtrabotal() >= 0)
                        {
                            emp.AddSmena(new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina));

                            emp.TipTekSmen = 2;

                        }


                        emp.OtrabotalDay();

                    }
                }
                emp.setStatus(1);
            }

            emplo.Clear();
            emplo = Program.currentShop.employes.FindAll(t => (t.getStatus() == 0) && (t.GetTip() == 2));
            foreach (employee emp in emplo)
            {
                int dlina = emp.getVS().getDlina();



                foreach (TemplateWorkingDay wd in Program.currentShop.MouthPrognozT)
                {
                    int start = Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => t.getStartSmena() > wd.DS.getStartDaySale()).getStartSmena();

                    if ((emp.TipTekSmen == 3))
                    {


                        if (emp.getOtrabotal() >=0)
                        {
                            emp.AddSmena(new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina));
                           
                            emp.TipTekSmen = 2;

                        }

                    }

                    else if (emp.TipTekSmen == 1)
                    {
                        start = wd.DS.getEndDaySale() - dlina;

                        if (emp.getOtrabotal() >=0)
                        {
                            emp.AddSmena(new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina));
                          
                            emp.TipTekSmen = 3;

                        }

                    }

                    else if (emp.getOtrabotal() >=0)
                    {
                        start = wd.DS.getStartDaySale();
                        emp.AddSmena(new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina));
                       
                        emp.TipTekSmen = 1;

                    }
                    emp.OtrabotalDay();
                }
                emp.setStatus(1);
            }


            emplo = Program.currentShop.employes.FindAll(t => (t.getStatus() == 0));
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
                            emp.AddSmena(new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina));

                            emp.TipTekSmen = 2;

                        }

                    }

                    else if (emp.TipTekSmen == 1)
                    {
                        start = wd.DS.getEndDaySale() - dlina;

                        if (emp.getOtrabotal() >= 0)
                        {
                            emp.AddSmena(new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina));

                            emp.TipTekSmen = 3;

                        }

                    }

                    else if (emp.getOtrabotal() >= 0)
                    {
                        start = wd.DS.getStartDaySale();
                        emp.AddSmena(new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina));

                        emp.TipTekSmen = 1;

                    }
                    emp.OtrabotalDay();
                }
                emp.setStatus(1);
            }


            emplo.Clear();
            emplo = Program.currentShop.employes.FindAll(t => t.getStatus() == 1);
            // MessageBox.Show("2");
            foreach (employee emp in emplo)
            {


                while (emp.getNormRab() > Program.normchas)
                {

                    Smena s = emp.smens.Find(t => t.getLenght() > 12);
                    if (s != null)
                    { s.delChas(Program.currentShop.MouthPrognozT.Find(f => f.DS.getData() == s.getData())); }
                    else
                    {
                        foreach (Smena sm in emp.smens)
                        {
                            if (sm != null)
                                // {
                                sm.delChas(Program.currentShop.MouthPrognozT.Find(f => f.DS.getData() == sm.getData()));
                            // }else { MessageBox.Show(emp.smens.Count+"count  id=" +emp.getID()); }
                            if (emp.getNormRab() == Program.normchas) break;
                        }
                    }

                }


                while (emp.getNormRab() < Program.normchas)
                {
                    // MessageBox.Show("Меньше");
                    Smena s = emp.smens.Find(t => t.getLenght() < 6);
                    if (s != null)
                    { s.addChas(Program.currentShop.MouthPrognozT.Find(f => f.DS.getData() == s.getData())); }
                    else
                    {
                        foreach (Smena sm in emp.smens)
                        {
                            if (sm != null)
                                // {
                                sm.addChas(Program.currentShop.MouthPrognozT.Find(f => f.DS.getData() == sm.getData()));
                            // }else { MessageBox.Show(emp.smens.Count+"count  id=" +emp.getID()); }
                            if (emp.getNormRab() == Program.normchas) break;
                        }
                    }

                }
                emp.setStatus(2);
            }
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

        
    }
}
