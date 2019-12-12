using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule.Code
{
    public static class Constants
    {
        public const string ListName = "График";
        public const string Version = "1.4.1";
        public const string ReleaseDate = "10.12.2019";
        public const bool IsThrowExceptionOnNullResult = true; //будет ли программа порождать исключение если из базы вернулось 0 результатов

        public static Color buttonColor = Color.FromArgb(255, 160, 122);
    }
}
