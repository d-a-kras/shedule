using schedule.Models;
using shedule.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace schedule.Code
{
    class ForForecast
    {
        static List<hourSale> Raznica = new List<hourSale>();

        private static Dictionary<int,double> getKoeff(int type) {
            Dictionary<int,double> koeff = new Dictionary<int, double>();

            switch (type) {
                case 1: koeff.Add(1,1);break;
                case 2: koeff.Add(1,0.6); koeff.Add(2,0.4); break;
                case 3: koeff.Add(1,0.45); koeff.Add(2,0.3); koeff.Add(3,0.25); break;
                case 4: koeff.Add(1,0.35); koeff.Add(2,0.25); koeff.Add(3,0.2); koeff.Add(4,0.2); break;
                case 5: koeff.Add(1, 0.6); koeff.Add(3, 0.4); break;
                case 6: koeff.Add(2, 0.6); koeff.Add(3, 0.4); break;
                case 7: koeff.Add(2, 0.35); koeff.Add(3, 0.25); koeff.Add(4, 0.2); break;

            }
            return koeff;
        }

        private static int getType(List<Dict<int,Forecast>> forecasts)
        {
            int result = 1;
            if (forecasts.Exists(t => t.type == 4)) {
                if (!forecasts.Exists(t => t.type == 2))
                {
                    result = 7;
                }
                else
                {
                    result = 4;
                }
            }else if (forecasts.Exists(t => t.type == 3) && !forecasts.Exists(t => t.type == 2))
            {
                result = 5;
            }else if (forecasts.Exists(t => t.type == 3) )
            {
                if (forecasts.Exists(t => t.type == 1))
                {
                    result = 3;
                }
                else {
                    result = 6;
                }
                
            }else if (forecasts.Exists(t => t.type == 2))
            {
                result = 2;
            }

            return result;
        }

        public static bool createPrognoz(bool current, bool isMp, bool first)
        {
            Program.CheckDeistvFactors();
            Program.currentShop.MouthPrognoz.Clear();
            Program.currentShop.MouthPrognozT.Clear();

            List<Dict<int,Forecast>> forecasts = Forecast.getForecastForShop(Program.currentShop.getIdShop());

            forecasts.AddRange(HolyDayCalendarPrognoz(Program.currentShop.getIdShop(), current));

            if (!CheckForecast(forecasts)) {
                daysaleFromDB(first, isMp);
            }


            List<PrognDaySale> PDSs = kernelForecast(Program.currentShop.getIdShop(),Program.currentShop.daysSale, forecasts);

            Program.currentShop.MouthPrognoz = createCalendarPrognoz(current, PDSs);

            if (Program.currentShop.MouthPrognoz.Count == 0)
            {
                return false;
            }

            foreach (daySale ds in Program.currentShop.MouthPrognoz)
            {
                //создание прогнозных смен
                createPrognozTemplate(ds);
            }

            return true;
        }

        private static bool CheckForecast(List<Dict<int, Forecast>> forecasts) {
            DateTime dt = DateTime.Now.AddMonths(-1);
            return forecasts.Count(t=>t.getValue().year==dt.Year && t.getValue().month ==dt.Month)>300;
        }

        private static List<Dict<int,Forecast>> HolyDayCalendarPrognoz(int shopId,bool current)
        {
            DateTime fd;
            DateTime tdt = DateTime.Today;
            int dim;
            daySale d;
            bool result8 = false;
            bool result9 = false;
            List<Dict<int, Forecast>> result = new List<Dict<int, Forecast>>();
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
                    if (d.getTip()==8 ) {
                        result8 = true;
                    }
                    if (d.getTip() == 9) {
                        result9 = true;
                    }

                }
                catch(Exception ex)
                {
                    Logger.Error(ex.ToString());
               }

            }

            if (result8) {
                result.AddRange(Forecast.GetForecastHolyDay(shopId, fd.Month, 8));
            }
            if (result9)
            {
                result.AddRange(Forecast.GetForecastHolyDay(shopId, fd.Month, 9));
            }

            return result;
        }

        private static List<daySale> createCalendarPrognoz(bool current, List<PrognDaySale> PDSs) {
            DateTime fd;
            DateTime tdt = DateTime.Today;
            int dim;
            daySale d;
            List<daySale> MonthPrognoz = new List<daySale>();
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
                    //нужный прогноз в нужный час
                    d.hoursSale = PDSs.Find(t => t.getTip() == d.getTip()).hoursSale;
                    MonthPrognoz.Add(d);
                }
                catch
                {
                    //нужно чтоб вылазило сообщение о том что даты в календаре нет
                    MessageBox.Show($"Даты {i}.{fd.Month}.{fd.Year} нет в календаре!");
                    //return false;
                }

            }

            return MonthPrognoz;
        }
        public static List<PrognDaySale> kernelForecast(int shopId,List<daySale> daysSale, List<Dict<int,Forecast>> forecasts=null) {
            if (forecasts==null) {
                forecasts = new List<Dict<int, Forecast>>();
            }
            List<PrognDaySale> PDSs = new List<PrognDaySale>();
            List<Forecast> forecastfordb = new List<Forecast>();
            List<Forecast> temp = new List<Forecast>();
            Forecast forecast = new Forecast();
            List<PrognDaySale> prognDaySales = new List<PrognDaySale>();
            DateTime tdt = DateTime.Today;
            bool pr = false;

            foreach (daySale ds in daysSale)
            {

                ds.setTip(ds.getTip());
                //if (ds.getTip() == 8 || ds.getTip() == 9) { pr = true; }

            }


           // Helper.readDays8and9(DateTime.Now.Year);


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

            foreach (var pd in PDSs)
            { 
                foreach (hourSale hourSale in pd.hoursSale) {
                    forecast = new Forecast(shopId, hourSale.getIntHour(), pd.getTip(), tdt.AddMonths(-1).Month, tdt.AddMonths(-1).Year, hourSale.getCountClick(), hourSale.getCountCheck());
                    
                    forecastfordb.Add(forecast);
                    forecasts.Add(new Dict<int, Forecast> { type = 1, value = forecast });
                }
            }

            if (forecastfordb.Count>0) {
                Forecast.CreateOrUpdate(forecastfordb);
            }

            for (int i = 1; i < 10; i++)
            {
                for (int j = 7; j < 24; j++)
                {
                    var tempforecasts = forecasts.FindAll(t => t.getValue().dayType == i && t.getValue().hour == j);

                    if (tempforecasts.Count > 0) {

                        temp.Add(ActualPrognoz(tempforecasts));
                        if (PDSs.ElementAtOrDefault(i).hoursSale.Find(t => t.getIntHour() == j) == null)
                        {
                            PDSs.ElementAtOrDefault(i).hoursSale.Add(new hourSale(shopId, new DateTime(), j.ToString(), temp.Find(t => t.hour == j && t.dayType == i).countCheques, temp.Find(t => t.hour == j && t.dayType == i).countClick));
                        }
                        else {
                            PDSs.ElementAtOrDefault(i).hoursSale.Find( x=> x.getIntHour()== j).setCheck(temp.Find(t => t.hour == j && t.dayType == i).countCheques);
                            PDSs.ElementAtOrDefault(i).hoursSale.Find(x => x.getIntHour() == j).setClick(temp.Find(t => t.hour == j && t.dayType == i).countClick);
                        }
                    } 
                    
                }
            }

          /*  foreach (var pd in PDSs)
            {
                foreach (hourSale hourSale in pd.hoursSale)
                {
                    
                }
            }*/

            return PDSs;
        }

        private static Dict<int,int> Calc(List<Dict<int, Forecast>> forecasts) {
            List<Forecast> templistForecast = new List<Forecast>();
            int type = getType(forecasts);
            Dictionary<int, double> koeff = getKoeff(type);
            double click = 0;
            double check = 0;
            double tempclick = 0;
            double tempcheck = 0;

            for (int i = 1; i < 5; i++)
            {
                templistForecast = forecasts.FindAll(t => t.type == i).Select(t => t.value).ToList();
                foreach (var temp in templistForecast)
                {
                    tempcheck += temp.countCheques * koeff.First(t => t.Key == i).Value;
                    tempclick += temp.countClick * koeff.First(t => t.Key == i).Value;
                }
                if (templistForecast.Count>0) {
                    click += tempclick / templistForecast.Count; tempclick = 0;
                    check += tempcheck / templistForecast.Count; tempcheck = 0; 
                }
            }
            return new Dict<int, int> { type = (int)check, value = (int)click };
        }

        private static Forecast ActualPrognoz(List<Dict<int, Forecast>> forecasts)
        {

            Forecast result = new Forecast(forecasts[0].value.shopId, forecasts[0].value.hour, forecasts[0].value.dayType, forecasts[0].value.month, forecasts[0].value.year, forecasts[0].value.countClick, forecasts[0].value.countCheques);

            if (forecasts.Count == 1) {
                result = forecasts[0].getValue();
            }
            else
            {
              Dict<int,int> dict = Calc(forecasts);
              result.countCheques = dict.type;
              result.countClick = dict.value;

            }
           return result;
        }

         private static void daysaleFromDB(bool first, bool isMp) {
            DateTime ydt = DateTime.Now.AddDays(-1);
            DateTime d2 = DateTime.Now.AddDays(-30);
            List<Dict<int,Forecast>> forecasts = new List<Dict<int, Forecast>>();

            if ((first) || (Program.currentShop.daysSale.Count == 0))
            {
                //   if (connect)
                //  {
                if (!Program.isOffline)
                {
                    Program.currentShop.daysSale.Clear();
                    if (isMp)
                    {
                        Program.currentShop.daysSale = createListDaySale(d2, ydt, Program.currentShop.getIdShopFM());
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

          


        }


        public static void createPrognoz3()
        {
            Program.currentShop.MouthPrognoz.Clear();
            Program.currentShop.MouthPrognozT.Clear();
            DateTime ydt = DateTime.Now.AddDays(-1);
            DateTime tdt = DateTime.Today;
            
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

            List<Dict<int, Forecast>> forecasts = Forecast.getForecastForShop(Program.currentShop.getIdShop());

            forecasts.AddRange(HolyDayCalendarPrognoz(Program.currentShop.getIdShop(), false));


            List<PrognDaySale> PDSs = kernelForecast(Program.currentShop.getIdShop(),Program.currentShop.daysSale, forecasts);



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
            List<PrognDaySale> prognDaySales = kernelForecast(shopId,ds);
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
           // Shop shop = new Shop(301, "");
                Program.currentShop = new Shop(shop.getIdShop(), shop.getAddress());
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
