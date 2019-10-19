using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Models
{
    public class MinRab
    {
        int MinCount;
        int Time;
        bool Otobragenie;
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


    }

}
