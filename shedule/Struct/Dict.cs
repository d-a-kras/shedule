using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using schedule.Models;

namespace shedule.Code
{
    public class Dict<T1, T2>
    {
        public T1 type { get; set; }
        public T2 value { get; set; }

        public T2 getValue()=> this.value;

       public static List<Dict<T1,T2>> Init(T1 t, List<T2> val) {
            List<Dict<T1, T2>> result = new List<Dict<T1, T2>>(); 
            foreach (var v in val) {
                result.Add(new Dict<T1, T2> { type = t, value = v });
            }
            return result;
        }

        public static List<T2> getList( List<Dict<T1,T2>> dicts)
        {
            List<T2> result = new List< T2>();
            foreach (var dict in dicts)
            {
                result.Add(dict.getValue());
            }
            return result;
        }

        internal List<Dict<bool, Forecast>> Init(bool v, List<Forecast> forecasts)
        {
            throw new NotImplementedException();
        }
    }
}
