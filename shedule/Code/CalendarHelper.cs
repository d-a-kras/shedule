using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Code
{
    public static class CalendarHelper
    {
        /// <summary>
        /// Копия метода GetListDate, но работает с переданным магазином, а не с тем что в Program
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static Shop GetListDateForShop(Shop shop, int year)
        {
            int[] RD = new int[12];
            int[] PHD = new int[12];

            if (shop != null)
            {
                try
                {
                    DateTime.Parse($"01-01-{year}");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString());
                    throw new Exception($"Значение {year} недопустимо в качестве года!");
                }

                shop.DFCs.Clear();
                string readPath = Environment.CurrentDirectory + @"\Shops\" + shop.getIdShop() + $@"\Calendar{year}";
                // MessageBox.Show(readPath);
                try
                {
                    using (StreamReader sr = new StreamReader(readPath, Encoding.Default))
                    {
                        string line;
                        string[] s = new string[4];

                        while ((line = sr.ReadLine()) != null)
                        {
                            s = line.Split('#');
                            DataForCalendary d = new DataForCalendary(DateTime.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
                            // MessageBox.Show(DateTime.Parse(s[0]).Month.ToString());
                            switch (int.Parse(s[1]))
                            {
                                case 1: RD[DateTime.Parse(s[0]).Month - 1]++; break;
                                case 2: RD[DateTime.Parse(s[0]).Month - 1]++; break;
                                case 3: RD[DateTime.Parse(s[0]).Month - 1]++; break;
                                case 4: RD[DateTime.Parse(s[0]).Month - 1]++; break;
                                case 5: RD[DateTime.Parse(s[0]).Month - 1]++; break;

                                case 9: PHD[DateTime.Parse(s[0]).Month - 1]++; break;

                            }

                            shop.DFCs.Add(d);
                        }
                        //   MessageBox.Show("DFCs.Add 1");
                    }



                }
                catch
                {
                    //  MessageBox.Show(ex.Message);

                    Program.ReadCalendarFromXML(year);
                    for (int i = 1; i <= 12; i++)
                    {
                        RD[i - 1] = 0;
                        PHD[i - 1] = 0;
                        int countDays = DateTime.DaysInMonth(year, i);
                        for (int k = 1; k <= countDays; k++)
                        {
                            DataForCalendary dfc = new DataForCalendary(new DateTime(year, i, k));
                            int t = dfc.getTip();
                            if ((t == 1) || (t == 2) || (t == 3) || (t == 4) || (t == 5)) { RD[i - 1]++; }
                            if (t == 9) { PHD[i - 1]++; }

                            if (shop.DFCs.Find(x => x.getData() == dfc.getData()) != null)
                            {

                            }
                            else shop.DFCs.Add(dfc);

                        }
                    }
                    //  MessageBox.Show("DFCs.Add ex");
                    int s1 = 8;
                    int s2 = 9;
                    int f1 = 23;
                    int f2 = 23;
                    if ((Program.currentShop.DFCs.Find(t=>t.getTip()==1)!=null)&&(Program.currentShop.DFCs.Find(t => t.getTip() == 7)!=null)) {
                        s1 = Program.currentShop.DFCs.Find(t => t.getTip() == 1).getTimeStart();
                        s2 = Program.currentShop.DFCs.Find(t => t.getTip() == 7).getTimeStart();
                        f1 = Program.currentShop.DFCs.Find(t => t.getTip() == 1).getTimeEnd();
                        f2 = Program.currentShop.DFCs.Find(t => t.getTip() == 7).getTimeEnd();

                    }
                    foreach (DataForCalendary dfc in shop.DFCs)
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
                        foreach (DataForCalendary d in shop.DFCs)
                            sw.WriteLine(d.getData() + "#" + d.getTip() + "#" + d.getTimeStart() + "#" + d.getTimeEnd());
                    }

                }
            }
            return shop;
        }
    }
}
