using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule.Models
{
    [Serializable]
    public class hourSale
    {

        private int idShop;
        private DateTime Data;
        private string weekday;
        private string NHour;
        private int countCheck;
        private int countClick;
        private double countTov;
        private int Minute;

        public double getCountTov()
        {
            return this.countTov;
        }

        public void setClick(int c)
        {
            this.countClick = c;
        }

        public void setCheck(int ch)
        {
            this.countCheck = ch;
        }

        public hourSale(int idS, DateTime D, string NH, int countCh, int countCl)
        {
            this.idShop = idS;
            this.Data = D;

            this.NHour = NH;
            this.countCheck = countCh;
            this.countClick = countCl;

        }

        public hourSale(int idS, DateTime D, string NH, string dn, int countCh, int countCl)
        {
            this.idShop = idS;
            this.Data = D;
            this.weekday = dn;
            this.NHour = NH;
            this.countCheck = countCh;
            this.countClick = countCl;

        }

        public hourSale(int ids, DateTime D, string NH, int m)
        {
            this.idShop = ids;
            this.Data = D;
            this.NHour = NH;
            this.Minute = m;
        }

        public hourSale(int idS, DateTime D, string NH, string w, int countCh, int countCl, double ct)
        {
            this.idShop = idS;
            this.Data = D;
            this.weekday = w;
            this.NHour = NH;
            this.countCheck = countCh;
            this.countClick = countCl;
            this.countTov = ct;
        }

        public void setTime(int n)
        {
            this.Minute = n;

        }

        public int getIdShop()
        {
            return this.idShop;
        }

        public DateTime getData() { return this.Data; }

        public string getWeekday() { return this.weekday; }

        public string getNHour() { return this.NHour; }

        public int getIntHour() {
            int hour = 0;
            int.TryParse(this.NHour, out hour);

            return hour;
        }

        public int getCountClick() { return this.countClick; }

        public int getCountCheck() { return this.countCheck; }

        public int getMinut()
        {
            if (this.Minute == 0)
            {
                return (this.getCountCheck() * Program.TimeRech + this.getCountClick() * Program.TimeClick) * Program.KoefKassira / 100;
            }
            else return this.Minute;
        }

        public int getTime()
        {
            return (this.getCountCheck() * Program.TimeClick + this.getCountClick() * Program.TimeClick);
        }

    }
}
