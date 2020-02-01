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
        public const string Version = "1.4.9";
        public const string ReleaseDate = "18.01.2019";
        public const bool IsThrowExceptionOnNullResult = true; //будет ли программа порождать исключение если из базы вернулось 0 результатов

        #region цвета
        public static Color buttonColor = Color.FromArgb(13, 88, 166);//Color.FromArgb(50, 110, 255);
        public static Color selectedButtonColor = Color.FromArgb(51, 0, 153);
        public static Color formColor = Color.SteelBlue;
        public static Color backColor = Color.Azure;
        public static Color comboBoxColor = Color.FromArgb(150, 203, 255);
        #endregion

    }
}
