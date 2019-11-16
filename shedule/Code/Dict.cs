using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Code
{
    public class Dict<T1, T2>
    {
        public T1 type { get; set; }
        public T2 value { get; set; }

        public T2 getValue()=> this.value;

    }
}
