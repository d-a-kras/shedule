using schedule;
using schedule.Code;
using schedule.Models;
using shedule.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shedule.Code
{
    class Init
    {
        static public void initForecasts()
        {
            List<mShop> shops = DBShop.getShops().Select(t => t.convertMShop()).ToList();
            List<daySale> listDaySale = new List<daySale>();
            List<Forecast> forecasts = new List<Forecast>();
            shops = shops.FindAll(t => t.getIdShop() >= 396);
            foreach (var shop in shops)
            {
                // Shop shop = new Shop(301, "");
                Program.currentShop = new Shop(shop.getIdShop(), shop.getAddress());
                Logger.Log.Info("Init=" + shop.getIdShop());
                DateTime dt = new DateTime(2019, 9, 1);
                for (int i = 0; i < 2; i++)
                {
                    listDaySale = ForForecast.createListDaySale(dt.AddMonths(i), dt.AddMonths(i + 1), shop.getIdShop(), true);
                    forecasts = ForForecast.convertToForecast(listDaySale, shop.getIdShop(), dt.AddMonths(i).Month, dt.AddMonths(i).Year);
                    Forecast.CreateOrUpdate(forecasts);
                }
            }
        }

        static public void initCalendar()
        {
            int year = DateTime.Now.Year+1;

            List<mShop> shops = DBShop.getShops().Select(t => t.convertMShop()).ToList();
            foreach (var shop in shops) {
                Program.currentShop = new Shop(shop.getIdShop(), shop.getAddress()); 
                Program.ReadCalendarFromXML(year);
                string readPath = Environment.CurrentDirectory + @"\Shops\" + shop.getIdShop() + $@"\Calendar{year}";
                for (int i = 1; i <= 12; i++)
                {
                    Program.RD[i - 1] = 0;
                    Program.PHD[i - 1] = 0;
                    int countDays = DateTime.DaysInMonth(year, i);
                    for (int k = 1; k <= countDays; k++)
                    {
                        DataForCalendary dfc = new DataForCalendary(new DateTime(year, i, k));
                        int t = dfc.getTip();
                        if ((t == 1) || (t == 2) || (t == 3) || (t == 4) || (t == 5)) { Program.RD[i - 1]++; }
                        if (t == 9) { Program.PHD[i - 1]++; }

                        if (Program.currentShop.DFCs.Find(x => x.getData() == dfc.getData()) != null)
                        {

                        }
                        else Program.currentShop.DFCs.Add(dfc);

                    }
                }
                //  MessageBox.Show("DFCs.Add ex");
                int s1 = 8;
                int s2 = 9;
                int f1 = 23;
                int f2 = 23;
              /*  if ((Program.currentShop.DFCs.Find(t => t.getTip() == 1) != null) && (Program.currentShop.DFCs.Find(t => t.getTip() == 7) != null))
                {
                    s1 = Program.currentShop.DFCs.Find(t => t.getTip() == 1).getTimeStart();
                    s2 = Program.currentShop.DFCs.Find(t => t.getTip() == 7).getTimeStart();
                    f1 = Program.currentShop.DFCs.Find(t => t.getTip() == 1).getTimeEnd();
                    f2 = Program.currentShop.DFCs.Find(t => t.getTip() == 7).getTimeEnd();

                }*/
                foreach (DataForCalendary dfc in Program.currentShop.DFCs)
                {
                    switch (dfc.getTip())
                    {
                        case 1: dfc.setTimeBaE(s1, f1); break;
                        case 2: dfc.setTimeBaE(s1, f1); break;
                        case 3: dfc.setTimeBaE(s1, f1); break;
                        case 4: dfc.setTimeBaE(s1, f1); break;
                        case 5: dfc.setTimeBaE(s1, f1); break;
                        case 6: dfc.setTimeBaE(s2, f2); break;
                        case 7: dfc.setTimeBaE(s2, f2); break;
                        case 8: dfc.setTimeBaE(s2, f2); break;
                        case 9: dfc.setTimeBaE(s2, f2); break;
                        case 10: dfc.setTimeBaE(s2, f2); break;
                        default: dfc.setTimeBaE(8, 23); break;
                    }
                }

                string directoryPath = readPath.Split(new string[] { "\\" }, StringSplitOptions.None)
                    .Reverse()
                    .Skip(1)
                    .Reverse()
                    .Aggregate((prev, current) => prev + "\\" + current);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {
                    foreach (DataForCalendary d in Program.currentShop.DFCs)
                        sw.WriteLine(d.getData() + "#" + d.getTip() + "#" + d.getTimeStart() + "#" + d.getTimeEnd());
                }
            }
        }

        static public void initNewStyle(List<Control> elements)
        {
            foreach (var element in elements) {
                if (element is Form)
                {
                    ((Form)element).BackColor = Constants.formColor;//Color.FromArgb(29, 125, 143);
                }
                else if (element is Button)
                {
                    ((Button)element).BackColor = Constants.buttonColor;
                    ((Button)element).FlatStyle = FlatStyle.Flat;
                   // if (element.Name!="buttonReadMGraf") {
                        ((Button)element).ForeColor = Constants.backColor; 
                   // }
                    ((Button)element).Font = new Font("Yu Gothic Ul Semibold", 7);
                    if ((element.Name == "button14") || (element.Name == "button6") || (element.Name == "buttonExport1"))
                    {
                        element.BackColor = Color.Gray;
                        element.Enabled = false;
                        ((Button)element).Font = new Font("Yu Gothic Ul Semibold", 9);
                    }
                }else if (element is Label && element.Name!= "label9")
                {
                    if (element.Name != "label4" && element.Name!= "label5" && element.Name != "label3")
                    {
                        ((Label)element).ForeColor = Constants.backColor;
                    }
                    
                    if (element.Name!="label17" && element.Name != "lbCurrentVersion") {
                        ((Label)element).Font = new Font("Yu Gothic Ul Semibold", 9, FontStyle.Bold); 
                    }
                   
                }
                else if (element is ComboBox)
                {
                    
                    ((ComboBox)element).BackColor = Constants.comboBoxColor;//Color.FromArgb(92, 181, 197);
                    
                }
                else if (element is ListBox)
                {
                    ((ListBox)element).BackColor = Constants.backColor;// Color.FromArgb(186, 223, 209);
                }
                else if (element is TabPage)
                {
                    ((TabPage)element).BackColor = Constants.backColor; //Color.FromArgb(186, 223, 209);
                }
            }
           
        }

        public static IEnumerable<Control> GetSelfAndChildrenRecursive(Control parent)
        {
            List<Control> controls = new List<Control>();

            foreach (Control child in parent.Controls)
            {
                controls.AddRange(GetSelfAndChildrenRecursive(child));
            }

            controls.Add(parent);

            return controls;
        }


        public static void new_style(Form f)
        {
            var controls = GetSelfAndChildrenRecursive(f);
            Init.initNewStyle(controls.ToList());
        }
    }
     
}
