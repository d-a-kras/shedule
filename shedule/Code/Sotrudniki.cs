using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Code
{


 public  class Sotrudniki
    {

        static public bool CheckGrafic13()
        {
            
            foreach (employee emp in Program.currentShop.employes)
            {
                foreach(Smena sm in emp.smens)
                if (sm.getLenght()>12) { return false; }
              
                   

            }


            return true;

        }

        static public bool CheckGrafic2()
        {

            foreach (TemplateWorkingDay mp in Program.currentShop.MouthPrognozT)
            {
                if ((mp.minKassUtr== Program.currentShop.minrab.getMinCount()) ||(mp.minKassVech == Program.currentShop.minrab.getMinCount())||(mp.minProdVech == Program.currentShop.minrab.getMinCount())||((mp.minProdUtr == Program.currentShop.minrab.getMinCount()))) { return false; }



            }


            return true;

        }

        static public bool CheckGrafic() {
            List<Smena> lss = new List<Smena>();
           // WorkingDay wd; 
            foreach (TemplateWorkingDay mp in Program.currentShop.MouthPrognozT){
                if ((mp.minKassUtr>0)||(mp.minProdUtr>0)||(mp.minKassVech>0)||(mp.minProdVech>0)) { return false; }
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

        static public int shiftSm(ref int i,ref bool f1)
        {
            i++;
            if (i == Program.currentShop.VarSmens.FindAll(t => t.getDeistvie() == true).Count)
            {
                i = 0;
                f1 = true;
               
            }

            return i;

        }

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

       

        public static void OptimCountSotr()
        {
            bool flag = false;
            int nvs = -1;
            int nprod = -1;
           
            int ngruz = -1;
            int nkass = -1;
            int kassCount = Program.MinKassirCount;
           
                List<VarSmen> DVS = Program.currentShop.VarSmens.FindAll(t => t.getDeistvie() == true);
            List<VarSmen> DVS2 = DVS.FindAll(t =>!(( t.getR()==2)||(t.getR()==3)));
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
                Program.normchas = Program.currentShop.NormaChasov[dt.Month].getNormChas();
                
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
                int CountProd=0;
            
                    CountProd = (int)Math.Round((double)((tc * Tobrtov * 60) / (Program.normchas * 3600 * KP)))+Program.currentShop.prilavki.GetCount();
                
                
               
               
                int CountGruz = LGruz.Sum(o => o.getCount());
                int CountKassirov = (int)Math.Round((double)(ob / (Program.normchas * K *36 ))) + Program.ParametrOptimization;
                
                if (CountKassirov < 4) { CountKassirov = 4; }
                if (CountProd < 4) { CountProd = 4; }
                if (CountGruz < 2) { CountGruz = 2; }

              
                bool flagg2 = false;
                bool flagg3 = false;
                bool flagp2 = false;
                bool flagp3 = false;
                bool flag2 = false;
                bool flag3 = false;


               

            


                for (int i = 200; CountGruz > 0; CountGruz--, i++)
                {
                    if (flag)
                    {
                       
                        flagg2 = false;
                        flagg3 = false;
                        

                    }
                    if ((DVS.Find(t => t.getR() == 2) != null) && (!flagg2))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -1, LGruz[shiftGruz(ref ngruz)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountGruz--; i++;
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 1, LGruz[ngruz].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        continue;
                    }
                    if ((DVS.Find(t => t.getR() == 3) != null) && (!flagg3))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -1, LGruz[shiftGruz(ref ngruz)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountGruz--; i++;
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 2, LGruz[ngruz].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        continue;
                    }
                    if (DVS2.Count != 0)
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, DVS2[shiftSm2(ref nvs, ref flag)], i, LGruz[shiftGruz(ref ngruz)].getOtobragenie(), "Сменный график");

                        Program.currentShop.employes.Add(e);
                    }
                    else {
                        i--;
                        CountGruz++;
                        flag = true; 
                    }
                }



                for (int i = 100; CountProd > 0; CountProd--, i++)
                {
                    if (flag)
                    {
                       
                      
                        flagp2 = false;
                        flagp3 = false;
                      
                        flag = false;

                    }
                    if ((DVS.Find(t => t.getR() == 2) != null) && (!flagp2))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -1, LProd[shiftProd(ref nprod)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountProd--; i++;
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 1, LProd[nprod].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        flagp2 = true;
                        continue;
                    }
                    if ((DVS.Find(t => t.getR() == 3) != null) && (!flagp3))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -2, LProd[shiftProd(ref nprod)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountProd--; i++;
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 1, LProd[nprod].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        flagp3 = true;
                        continue;
                    }

                    if (DVS2.Count != 0)
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, DVS2[shiftSm2(ref nvs, ref flag)], i, LProd[shiftProd(ref nprod)].getOtobragenie(), "Сменный график");

                        Program.currentShop.employes.Add(e);
                    }
                    else
                    {
                        i--;
                        CountProd++;
                        flag = true;
                    }
                }

                

                for (int i = 0; CountKassirov > 0; CountKassirov--, i++)
                {
                    if (flag)
                    {


                          flag2 = false;
                         flag3 = false;

                        flag = false;

                    }

                    if ((DVS.Find(t => t.getR() == 2) != null) && (!flag2))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), -1, LKass[shiftKass(ref nkass)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountKassirov--; i++;
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 2), 1, LKass[ nkass].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        flag2 = true;
                        continue;
                    }
                    if ((DVS.Find(t => t.getR() == 3) != null) && (!flag3))
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), -2, LKass[shiftKass(ref nkass)].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        CountKassirov--; i++;
                        e = new employee(Program.currentShop.getIdShop(), i, Program.currentShop.VarSmens.Find(t => t.getR() == 3), 1, LKass[nkass].getOtobragenie(), "Сменный график");
                        Program.currentShop.employes.Add(e);
                        flag3 = true;
                        continue;
                    }

                    if (DVS2.Count != 0)
                    {
                        e = new employee(Program.currentShop.getIdShop(), i, DVS2[shiftSm2(ref nvs, ref flag)], i, LKass[shiftKass(ref nkass)].getOtobragenie(), "Сменный график");

                       
                        Program.currentShop.employes.Add(e);
                    }
                    else
                    {
                        i--;
                        CountKassirov++;
                        flag = true;
                        
                    }
                }
            }
            else { 
               throw new Exception("Недостаточное количество смен");
            }
        }


       static public void CreateSmens()
        {
            Smena sm;
            
            List<employee> emplo = Program.currentShop.employes.FindAll((t => (t.getStatus() == 0) && (t.GetTip() == 1)));
            foreach (employee emp in emplo)
            {
                int dlina = emp.getVS().getDlina();



                foreach (TemplateWorkingDay wd in Program.currentShop.MouthPrognozT)
                {
                    int start = Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() >= wd.TimePrih) && (!t.isZanyta())).getStartSmena();
                    if (((emp.getVS().getR() == 4) || (emp.getVS().getR() == 5) || (emp.getVS().getR() == 6)) && (wd.DS.getTip() == 9))
                    {
                        dlina -= 1;
                    }

                    if (((emp.getVS().getR() == 4) || (emp.getVS().getR() == 5) || (emp.getVS().getR() == 6)) && (wd.DS.getTip() == 8))
                    {

                        emp.OtrabotalDay();
                    }
                    if (((emp.getVS().getR() == 5) || (emp.getVS().getR() == 6)) && (wd.DS.getTip() == 7))
                    {

                        emp.OtrabotalDay();
                    }
                    if ((emp.getVS().getR() == 5)  && (wd.DS.getTip() == 6))
                    {

                        emp.OtrabotalDay();
                    }

                    else
                    {
                        if ((wd.minKassUtr > 0) && (emp.TipTekSmen != 3))
                        {
                            start = wd.DS.getStartDaySale();

                            if (emp.getOtrabotal() >= 0)
                            {
                                sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                                if (sm.getEndSmena()>wd.DS.getEndDaySale()) { sm.SetStarnAndLenght( wd.DS.getEndDaySale() - dlina, dlina); }
                                emp.AddSmena(sm);

                                emp.TipTekSmen = 1;
                                wd.minKassUtr--;
                            }

                        }

                        else if ((wd.minKassVech > 0) )
                        {
                            start = wd.DS.getEndDaySale() - dlina;

                            if (emp.getOtrabotal() >= 0)
                            {
                                sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                                if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                                emp.AddSmena(sm);

                                emp.TipTekSmen = 3;
                                wd.minKassVech--;
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
                    int start = Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() >= wd.TimePrih) && (!t.isZanyta())).getStartSmena();

                    if ((emp.TipTekSmen != 3)&&(wd.minProdUtr>0))
                    {


                        if (emp.getOtrabotal() >=0)
                        {
                            start = wd.DS.getStartDaySale();
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            emp.AddSmena(sm);

                            emp.TipTekSmen = 2;
                            wd.minProdUtr--;

                        }

                    }

                    else if ( (wd.minProdVech > 0))
                    {
                        start = wd.DS.getEndDaySale() - dlina;

                        if (emp.getOtrabotal() >=0)
                        {
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            emp.AddSmena(sm);

                            emp.TipTekSmen = 3;
                            wd.minProdVech--;

                        }

                    }

                    else if (emp.getOtrabotal() >=0)
                    {

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
            emplo = Program.currentShop.employes.FindAll(t => (t.getStatus() == 0) && (t.GetTip() == 3));
            foreach (employee emp in emplo)
            {
                int dlina = emp.getVS().getDlina();



                foreach (TemplateWorkingDay wd in Program.currentShop.MouthPrognozT)
                {
                    int start = Program.currentShop.MouthPrognozT.Find(t => t.getData() == wd.getData()).lss.Find(t => (t.getStartSmena() >= wd.TimePrih) && (!t.isZanyta())).getStartSmena();

                    if ( (wd.minGruzUtr > 0))
                    {


                        if (emp.getOtrabotal() >= 0)
                        {
                            start = wd.DS.getStartDaySale();
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            emp.AddSmena(sm);

                            emp.TipTekSmen = 2;
                            wd.minGruzUtr--;

                        }

                    }

                    else if ((wd.minGruzVech > 0))
                    {
                        start = wd.DS.getEndDaySale() - dlina;

                        if (emp.getOtrabotal() >= 0)
                        {
                            sm = new Smena(Program.currentShop.getIdShop(), wd.getData(), start, dlina);
                            if (sm.getEndSmena() > wd.DS.getEndDaySale()) { sm.SetStarnAndLenght(wd.DS.getEndDaySale() - dlina, dlina); }
                            emp.AddSmena(sm);

                            emp.TipTekSmen = 3;
                            wd.minGruzVech--;

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
            emplo = Program.currentShop.employes.FindAll(t => t.getStatus() == 1);
            // MessageBox.Show("2");
            foreach (employee emp in emplo)
            {


                while (emp.getNormRab() > Program.normchas)
                {

                    Smena s = emp.smens.Find(t => t.getLenght() > 13);
                    if (s != null)
                    { s.delChas(Program.currentShop.MouthPrognozT.Find(f => f.DS.getData() == s.getData())); }
                    else
                    {
                        foreach (Smena sm1 in emp.smens)
                        {
                            if (sm1 != null)
                                // {
                                sm1.delChas(Program.currentShop.MouthPrognozT.Find(f => f.DS.getData() == sm1.getData()));
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
                        foreach (Smena sm1 in emp.smens)
                        {
                            if (sm1 != null)
                                // {
                                sm1.addChas(Program.currentShop.MouthPrognozT.Find(f => f.DS.getData() == sm1.getData()));
                            // }else { MessageBox.Show(emp.smens.Count+"count  id=" +emp.getID()); }
                            if (emp.getNormRab() == Program.normchas) break;
                        }
                    }

                }
                emp.setStatus(2);
            }
            Program.itogChass();
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
