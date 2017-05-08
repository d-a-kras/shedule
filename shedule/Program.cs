using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shedule
{
    static class Program
    {
        enum Position {
            cashier,
            seller,
            loader
        }
        enum Timetable {

        }

        public class Watch {
            int coming;
            int departure;
            DateTime date;
            int IdEmployee;
                
        }

        public class employee
        {
            int IdEmployee;
            Position position;
            String name;
            int CountHours;
            Timetable timetable;
            List<Watch> Wathes;

            public employee(int x1, int y1, String z1)
            {
                
            }
        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }



   
}
