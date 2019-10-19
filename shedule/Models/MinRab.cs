using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Models
{
    public class MinRab  : INotifyPropertyChanged
    {
        int MinCount;
        int Time;
        bool Otobragenie;

        public int Id { get; set; }

        public int minCount
        {
            get { return MinCount; }
            set
            {
                MinCount = value;
                OnPropertyChanged("MinCount");
            }
        }
        public int time
        {
            get { return Time; }
            set
            {
                Time = value;
                OnPropertyChanged("Time");
            }
        }
        public bool otobragenie
        {
            get { return Otobragenie; }
            set
            {
                Otobragenie = value;
                OnPropertyChanged("Otobragenie");
            }
        }

        public MinRab(int mc, int t, bool o)
        {
            this.MinCount = mc;
            this.Time = t;
            this.Otobragenie = o;
        }

        public int getMinCount()
        {
            return this.MinCount;
        }

        public int getTimeMinRab()
        {
            return this.Time;
        }
        public bool getOtobragenie()
        {
            return this.Otobragenie;
        }

        public void setOtobragenie(bool b)
        {
            this.Otobragenie = b;
        }

        public void setMinCount(int mc)
        {
            this.MinCount = mc;
        }

        public void setTime(int mt)
        {
            this.Time = mt;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
