using schedule.Code;
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
                Month = value;
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
