using schedule.Code;
using shedule.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schedule.Models
{
    public class Forecast : INotifyPropertyChanged
    {
        int ShopId;
        int Hour;
        int DayType;
        int Month;
        int Year;
        int CountClick;
        int CountCheques;

        public int Id { get; set; }

        public int hour
        {
            get { return Hour; }
            set
            {
                Hour = value;
                OnPropertyChanged("Hour");
            }
        }
        public int dayType
        {
            get { return DayType; }
            set
            {
                DayType = value;
                OnPropertyChanged("DayType");
            }
        }

        public int month
        {
            get { return Month; }
            set
            {
                Month = value;
                OnPropertyChanged("Month");
            }
        }
        public int countClick
        {
            get { return CountClick; }
            set
            {
                CountClick = value;
                OnPropertyChanged("CountClick");
            }
        }

        public int year
        {
            get { return Year; }
            set
            {
                Year = value;
                OnPropertyChanged("Year");
            }
        }
        public int countCheques
        {
            get { return CountCheques; }
            set
            {
                CountCheques = value;
                OnPropertyChanged("CountCheques");
            }
        }

        public int shopId
        {
            get { return ShopId; }
            set
            {
                ShopId = value;
                OnPropertyChanged("ShopId");
            }
        }

        public Forecast()
        {
        }

        public Forecast(int ShopId, int Hour, int DayType, int Month, int Year, int CountClick,int CountCheques)
        {
            this.ShopId = ShopId;
            this.Hour = Hour;
            this.DayType = DayType;
            this.Month = Month;
            this.Year = Year;
            this.CountClick = CountClick;
            this.CountCheques = CountCheques;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

      
        public static List<Forecast> Read(int shopId, int month, int year)
        {
            List<Forecast> forecasts = new List<Forecast>();
            try
            {
                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.Forecasts.Load();
                BindingList<Forecast> DataContext = db.Forecasts.Local.ToBindingList();
                forecasts = db.Forecasts.Where(t => t.shopId == shopId && t.year == year && t.month==month).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString()); 
            }
            return forecasts;
        }

        public static List<Dict<int,Forecast>> GetForecastHolyDay(int shopId, int month, int type)
        {
            List<Dict<int,Forecast>> dict_forecasts = new List<Dict<int,Forecast>>();
            List<Forecast> forecasts = new List< Forecast>();
            List<Forecast> tempforecasts= new List<Forecast>();
            try
            {
                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.Forecasts.Load();
                BindingList<Forecast> DataContext = db.Forecasts.Local.ToBindingList();
                forecasts = db.Forecasts.Where(t => t.shopId == shopId && t.dayType == type && t.month == month).ToList();
                tempforecasts = forecasts.Where(t => t.month == month).ToList();
                foreach(var forecast in tempforecasts)
                {
                    dict_forecasts.Add(new Dict<int,Forecast>{ type = 1, value = forecast });
                }
                tempforecasts = forecasts.Where(t => t.month != month).ToList();
                foreach (var forecast in tempforecasts)
                {
                    dict_forecasts.Add(new Dict<int, Forecast> { type = 2, value = forecast });
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
            return dict_forecasts;
        }

        public static List<Dict<int,Forecast>> getForecastForShop(int shopId)
        {
            List<Dict<int, Forecast>> forecasts = new List<Dict<int, Forecast>>();
           
            List<Forecast> forecasts1 = new List<Forecast>();
            List<Forecast> temp = new List<Forecast>();
            try
            {
                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.Forecasts.Load();
                BindingList<Forecast> DataContext = db.Forecasts.Local.ToBindingList();

                forecasts1 = db.Forecasts.Where(t => t.shopId == shopId).ToList();
                List< DateTime> dates = new List< DateTime>();
                DateTime now = DateTime.Now;
                dates.Add( new DateTime(now.Year, now.AddMonths(-1).Month, 1));
                dates.Add( new DateTime(now.Year, now.AddMonths(-2).Month, 1));
                dates.Add( new DateTime(now.Year, now.AddMonths(-3).Month, 1));
                dates.Add( new DateTime(now.AddYears(-1).Year, now.AddMonths(1).Month, 1));


                temp= forecasts1.FindAll(t => 
                 t.year == dates[0].Year && t.month == dates[0].Month) ;
                foreach (Forecast t in temp) {
                    forecasts.Add(new Dict<int, Forecast> { type = 1, value = t });
                }
                temp = forecasts1.FindAll(t =>
                  t.year == dates[1].Year && t.month == dates[1].Month);
                foreach (Forecast t in temp)
                {
                    forecasts.Add(new Dict<int, Forecast> { type = 3, value = t });
                }
                temp = forecasts1.FindAll(t =>
                 t.year == dates[2].Year && t.month == dates[2].Month);
                foreach (Forecast t in temp)
                {
                    forecasts.Add(new Dict<int, Forecast> { type = 4, value = t });
                }
                temp = forecasts1.FindAll(t =>
                 t.year == dates[3].Year && t.month == dates[3].Month);
                foreach (Forecast t in temp)
                {
                    forecasts.Add(new Dict<int, Forecast> { type = 2, value = t });
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
            return forecasts;

        }



        public static void CreateOrUpdate(List<Forecast> forecasts)
        {
            try
            {

                Models.ApplicationContext db;
                db = new Models.ApplicationContext();
                db.Forecasts.Load();
                BindingList<Forecast> DataContext = db.Forecasts.Local.ToBindingList();
                foreach (var forecast in forecasts) {
                    Forecast forecast1 = db.Forecasts.FirstOrDefault(t => t.shopId == forecast.shopId && t.year == forecast.year && t.month == forecast.month && t.dayType == forecast.dayType);
                    if (forecast1 == null)
                    {
                        db.Forecasts.Add(forecast);
                    }
                    else
                    {
                        forecast1.countCheques = forecast.countCheques;
                        forecast1.countClick = forecast.countClick;
                        
                    }
                }
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                String str = ex.ToString();
                Logger.Error(ex.ToString());
            }

        }


    }

}
