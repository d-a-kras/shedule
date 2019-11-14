using shedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shedule.Code
{
    class ForForecast
    {
        static List<hourSale> Raznica = new List<hourSale>();

        public static List<PrognDaySale> kernelForecast(List<daySale> daysSale, int shopId) {

            List<PrognDaySale> PDSs = new List<PrognDaySale>();
            DateTime tdt = DateTime.Today;
            bool pr = false;

            foreach (daySale ds in daysSale)
            {

                ds.setTip(ds.getTip());
                if (ds.getTip() == 8 || ds.getTip() == 9) { pr = true; }

            }


            Helper.readDays8and9(DateTime.Now.Year);


            for (int i = 1; i < 10; i++)
            {
                PDSs.Add(new PrognDaySale(shopId, tdt, i));
                foreach (daySale ds in daysSale)
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


                        Sclick = (int)Math.Ceiling((double)((Sclick / h.Count) * ((100 + (float)Program.otkr_konkurenta) / 100) * ((100 - (float)Program.zakr_konkurenta) / 100) * ((100 + (float)Program.rost_reklam) / 100) * ((100 + (float)Program.snig_reklam) / 100)));
                        Scheck = (int)Math.Ceiling((double)((Scheck / h.Count) * ((100 + (float)Program.otkr_konkurenta) / 100) * ((100 - (float)Program.zakr_konkurenta) / 100) * ((100 + (float)Program.rost_reklam) / 100) * ((100 + (float)Program.snig_reklam) / 100)));
                        pds.hoursSale.Add(new hourSale(shopId, h[0].getData(), h[0].getNHour(), Scheck, Sclick));
                        //MessageBox.Show(Scheck+" ");
                    }
                }


            }
            return PDSs;
        }
        public static bool createPrognoz(bool current, bool isMp, bool first)
        {
            Program.CheckDeistvFactors();
            Program.currentShop.MouthPrognoz.Clear();
            Program.currentShop.MouthPrognozT.Clear();
            DateTime ydt = DateTime.Now.AddDays(-1);
            DateTime tdt = DateTime.Today;

            DateTime d2 = DateTime.Now.AddDays(-30);

            if ((first) || (Program.currentShop.daysSale.Count == 0))
            {
                //   if (connect)
                //  {
                if (!Program.isOffline)
                {
                    Program.currentShop.daysSale.Clear();
                    if (isMp)
                    {
                      Program.currentShop.daysSale=createListDaySale(d2, ydt, Program.currentShop.getIdShopFM());
                    }
                    else
                    {
                        Program.currentShop.daysSale = createListDaySale(d2, ydt, Program.currentShop.getIdShop());
                    }
                }

                if ((isMp) && (Program.isOffline))
                {
                    Program.currentShop.daysSale = createListDaySale(d2, ydt, Program.currentShop.getIdShopFM());
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


           

            List<PrognDaySale> PDSs = kernelForecast(Program.currentShop.daysSale,Program.currentShop.getIdShop());
            /////

            DateTime fd;

            int dim;
            daySale d;
            if (current)
            {

                fd = new DateTime(tdt.Year, tdt.Month, tdt.AddDays(1).Day);
                dim = DateTime.DaysInMonth(fd.Year, fd.Month);

            }
            else
            {
                fd = new DateTime(tdt.AddMonths(1).Year, tdt.AddMonths(1).Month, 1);
                dim = DateTime.DaysInMonth(fd.Year, fd.Month);
            }

            for (int i = fd.Day; i <= dim; i++)
            {
                try
                {
                    d = new daySale(Program.currentShop.getIdShop(), new DateTime(fd.Year, fd.Month, i));
                    d.whatTip();
                    d.hoursSale = PDSs.Find(t => t.getTip() == d.getTip()).hoursSale;
                    Program.currentShop.MouthPrognoz.Add(d);
                }
                catch
                {
                    //нужно чтоб вылазило сообщение о том что даты в календаре нет
                    MessageBox.Show($"Даты {i}.{fd.Month}.{fd.Year} нет в календаре!");
                    return false;
                }

            }



            foreach (daySale ds in Program.currentShop.MouthPrognoz)
            {
                createPrognozTemplate(ds);
            }

            return true;
        }

        public static void createPrognoz3()
        {
            Program.currentShop.MouthPrognoz.Clear();
            Program.currentShop.MouthPrognozT.Clear();
            DateTime ydt = DateTime.Now.AddDays(-1);
            DateTime tdt = DateTime.Today;
            List<PrognDaySale> PDSs = new List<PrognDaySale>();
            DateTime d2 = DateTime.Now.AddDays(-30);

            if (!Program.isOffline)
            {
                Program.currentShop.daysSale.Clear();
                Program.currentShop.daysSale = createListDaySale(d2, ydt, Program.currentShop.getIdShop());
            }

            foreach (daySale ds in Program.currentShop.daysSale)
            {
                ds.setTip(Program.currentShop.DFCs.Find(x => x.getData().ToShortDateString() == ds.getData().ToShortDateString()).getTip());


            }

            PDSs = kernelForecast(Program.currentShop.daysSale, Program.currentShop.getIdShop());

            

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
                        daySale d = new daySale(Program.currentShop.getIdShop(), new DateTime(fd[j].Year, fd[j].Month, i));
                        d.whatTip();
                        d.hoursSale = PDSs.Find(t => t.getTip() == d.getTip()).hoursSale;
                        Program.currentShop.MouthPrognoz.Add(d);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.ToString());
                        MessageBox.Show($"Даты {i}.{fd[j].Month}.{fd[j].Year} нет в календаре!");
                    }
                }
            }



            foreach (daySale ds in Program.currentShop.MouthPrognoz)
            {
                createPrognozTemplate(ds);
            }


        }

        static public void createPrognozTemplate(daySale ds)
        {
            //createDaySale(id, data);
            //DateTime data2= data.AddDays(10.0d);
            int id = Program.currentShop.getIdShop();
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

            Program.currentShop.MouthPrognozT.Add(twd);
            //MessageBox.Show("Шаблон магазина" + id + "за дату" + ds.getData() + "создан");


        }

        static public bool checkGraph()
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
            if (Program.currentShop.factors.Find(t => t.getName() == "KoefKassira") != null)
            {
                K = Program.currentShop.factors.Find(t => t.getName() == "KoefKassira").getTZnach();
            }
            else { K = Program.KoefKassira; }
            int EndSmena = sm.getStartSmena() + sm.getLenght() + 1;

            for (int i = sm.getStartSmena(); i < EndSmena; i++)
            {

                hourSale temp = Raznica.Find(x => x.getNHour() == i.ToString());
                // MessageBox.Show("temp1="+temp.getNHour()+" "+temp.getMinut());
                if (!(temp == null))
                {
                    int t = temp.getMinut() - K * 3;
                    Raznica.Add(new hourSale(Program.currentShop.getIdShop(), sm.getData(), i.ToString(), t));
                    // MessageBox.Show("Count Razniza=" + Raznica.Count);
                    Raznica.Remove(temp);
                    // MessageBox.Show("temp2=" + temp.getNHour()+" " + t+" Count Razniza=" + Raznica.Count);
                }
                else { Raznica.Add(new hourSale(Program.currentShop.getIdShop(), sm.getData(), i.ToString(), 0)); }


                // MessageBox.Show(t.ToString());
                // Raznica.Remove(Raznica.Find(x => x.getNHour() == i.ToString())) ;
                // Raznica.Add(new hourSale(temp.getIdShop(), temp.getData(), temp.getNHour(), t));

            }
            return sm;
        }




        static public void createTemplate(daySale ds)
        {
            //createDaySale(id, data);
            //DateTime data2= data.AddDays(10.0d);
            int id = Program.currentShop.getIdShop();
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

            Program.currentShop.templates.Add(twd);
            //MessageBox.Show("Шаблон магазина" + id + "за дату" + ds.getData() + "создан");


        }

        public static List<daySale> createListDaySale(DateTime n, DateTime k, int id, bool init=false)
        {
            Connection activeconnect = Connection.getActiveConnection(id);
            var connectionString = Connection.getConnectionString(activeconnect);
            string s1 = n.Year + "/" + Helper.NumberToString(n.Month) + "/" + Helper.NumberToString(n.Day);
            string s2 = k.Year + "/" + Helper.NumberToString(k.Month) + "/" + Helper.NumberToString(k.Day);
            string sql;
            /* if (curShop.getIdShop() == 0) {
                 id = Program.currentShop.getIdShopFM();
             }*/

            sql = ForDB.getSQL_statisticbyshopsdayhour(id, s1, s2, Connection.getSheme(activeconnect));  // "select * from "+ Connection.getSheme(activeconnect) + "get_StatisticByShopsDayHour('" + id + "', '" + s1 + "', '" + s2 + " 23:59:00')"; 

            var daysSale = new List<daySale>();
            List<hourSale> hss = new List<hourSale>();
            daySale ds;
            if (!init) {
                int countAttemption = 0;
                while (hss.Count == 0 && countAttemption < 2)
                {
                    countAttemption++;
                    hss = ForDB.getHourFromDB(connectionString, sql, id);

                    if (hss.Count > 1) countAttemption = 2;
                }

                if (hss.Count < 2 && Constants.IsThrowExceptionOnNullResult)
                {
                    countAttemption = 0;
                    MessageBox.Show("Ошибка соединения с базой данных ");
                    // throw new Exception("Соединение с базой нестабильно, данные не были получены.");
                }

                countAttemption = 0;
            }
            else {
                hss = ForDB.getHourFromDB(connectionString, sql, id);
            }

            if (hss.Count > 200)
            {
                //посчитать количество дней 
                TimeSpan ts = k - n;

                DateTime d = n;

                for (int i = 0; i <= ts.Days + 1; i++)
                {

                    ds = new daySale(id, d);
                    daysSale.Add(ds);
                    d = d.AddDays(1.0d);
                }
                // MessageBox.Show("Количество дней по ts " + ts.Days.ToString());
                //  MessageBox.Show("Количество часов "+hss.Count.ToString());
                foreach (hourSale hs in hss)
                {
                    daysSale.Find(x => x.getData().ToShortDateString() == hs.getData().ToShortDateString()).Add(hs);
                }

                //using (StreamWriter sm = new StreamWriter(@"D:\Users\tailer_d\Desktop\test\test.txt"))
                //{
                //    foreach (var s in results)
                //    {
                //        sm.WriteLine(s);
                //    }
                //}


            }
            else if(!init)
            {
                if (hss.Count > 0)
                {
                    string max = hss.Max(t => t.getData()).ToShortDateString();

                    MessageBox.Show("Данных недостаточно. Последняя запись в базу данных " + max);
                }
                else
                {
                    MessageBox.Show("Из базы данных не вернулись значения за прошлый месяц");
                }
                Form6 f6 = new Form6();
                f6.ShowDialog();
                var newid = f6.newId;
                if (++Program.errorNum <= Program.maxErrorNum)
                {
                    daysSale = createListDaySale(n, k, newid);
                }
                else
                {
                    //throw new Exception("Соединение с базой нестабильно, выгрузка невозможна.");
                }

            }
            return daysSale;
        }

        public static List<Forecast> convertToForecast(List<daySale> ds, int shopId, int month,int year)
        {
            List<PrognDaySale> prognDaySales = kernelForecast(ds,shopId);
            List<Forecast> forecasts = new List<Forecast>();
            //pds.hoursSale.Add(new hourSale(shopId, h[0].getData(), h[0].getNHour(), Scheck, Sclick));

            foreach (var progn in prognDaySales) {
                foreach (var hour in progn.hss) {
                    forecasts.Add(new Forecast(progn.getIdShop(), hour.getIntHour(),
                        progn.getTip(), month, year,
                        hour.getCountClick(), hour.getCountCheck())); 
                }
            }

            return forecasts;

        }

        static public void initForecasts()
        {
            List<mShop> shops = DBShop.getShops().Select(t=> t.convertMShop()).ToList();
            List<daySale> listDaySale = new List<daySale>();
            List<Forecast> forecasts = new List<Forecast>();

            foreach (var shop in shops) {
                DateTime dt = new DateTime(2019,9,1);
                for (int i=0;i<2;i++) {
                listDaySale = createListDaySale(dt.AddMonths(i), dt.AddMonths(i+1), shop.getIdShop(), true);
                forecasts = convertToForecast(listDaySale,shop.getIdShop(), dt.AddMonths(i).Month, dt.AddMonths(i).Year);
                Forecast.CreateOrUpdate(forecasts);
                }
            }
        }

    }
}
