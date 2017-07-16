using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace shedule
{
    public partial class Form4 : Form
    {
        Label[] ld;
        List<DataForCalendary> ldfc = new List<DataForCalendary>();
        private List<Label> _checkedLabels;
        private int calendarYear;

        public Form4(int year)
        {
            InitializeComponent();
            _checkedLabels = new List<Label>();
            comboBox1.SelectedIndex = 0;
            calendarYear = year;
        }


        private void CreateCalendar()
        {
            ld = new Label[Program.currentShop.DFCs.Count];
            int m, i, j;
            int k = 0;

            foreach (DataForCalendary d in Program.currentShop.DFCs)
            {
                i = d.getNWeekday() - 1;

                ld[k] = new Label();
                ld[k].Click += ld_Click;

                ld[k].Text = d.getNiM().ToString();
                switch (d.getTip())
                {
                    case 1:
                        ld[k].BackColor = Color.White;
                        break;
                    case 2:
                        ld[k].BackColor = Color.White;
                        break;
                    case 3:
                        ld[k].BackColor = Color.White;
                        break;
                    case 4:
                        ld[k].BackColor = Color.White;
                        break;
                    case 5:
                        ld[k].BackColor = Color.White;
                        break;
                    case 6:
                        ld[k].BackColor = Color.Orange;
                        break;
                    case 7:
                        ld[k].BackColor = Color.Orange;
                        break;
                    case 8:
                        ld[k].BackColor = Color.Red;
                        break;
                    case 9:
                        ld[k].BackColor = Color.Yellow;
                        break;
                }

                j = (d.getNiM() + DataForCalendary.OON(d.getData()) - 1) / 7;

                m = d.getNM();
                (Controls["tableLayoutPanel" + m] as TableLayoutPanel)?.Controls.Add(ld[k], i, j);
                ld[k].Tag = d.getData().Day + " " + d.getData().Month + " " + d.getData().Year + ";" + d.getTimeStart() +
                            ";" + d.getTimeEnd();
                k++;
            }
        }

        private void ld_Click(object sender, EventArgs e)
        {
            Label currentLabel = sender as Label;
            var t = GetDataFromLabel(currentLabel);

            labelData.Text = $"{t.Item2[0]} {Program.getMonthInString(int.Parse(t.Item2[1]))} {t.Item2[2]}";

            textBoxStart.Text = t.Item1[1];
            textBoxEnd.Text = t.Item1[2];

            if (comboBox1.SelectedIndex == 5 && ModifierKeys == Keys.Control)
            {
                if (currentLabel != null)
                {
                    if (!_checkedLabels.Any(x => x.Text == currentLabel.Text))
                    {
                        ldfc.Add(
                            new DataForCalendary(new DateTime(int.Parse(t.Item2[2]), int.Parse(t.Item2[1]),
                                int.Parse(t.Item2[0]))));
                        SetLabelView(currentLabel);
                    }
                    else
                    {
                        ldfc.Remove(
                            new DataForCalendary(new DateTime(int.Parse(t.Item2[2]), int.Parse(t.Item2[1]),
                                int.Parse(t.Item2[0]))));
                        ResetLabelView(currentLabel);
                        _checkedLabels.Remove(currentLabel);
                    }
                }
                SetRtbCheckDaysText();
            }
            if (comboBox1.SelectedIndex == 5 && ModifierKeys != Keys.Control)
            {
                ldfc.Clear();
                ldfc.Add(
                    new DataForCalendary(new DateTime(int.Parse(t.Item2[2]), int.Parse(t.Item2[1]),
                        int.Parse(t.Item2[0]))));
                ResetCheckedLabels();
                _checkedLabels.Clear();
                SetLabelView(currentLabel);
                SetRtbCheckDaysText();
            }

           
        }

        private static DataTable CreateTable()
        {
            //создаём таблицу
            string[] months = Program.getMonths();
            DataTable dt = new DataTable("norm");
            //создаём три колонки
            DataColumn Mounth = new DataColumn("месяц", typeof(string));

            DataColumn colCountDayInMonth = new DataColumn("количество дней в месяце", typeof(Int16));
            DataColumn colCountDayRab = new DataColumn("количество рабочих дней", typeof(Int16));
            DataColumn colCountDayVuh = new DataColumn("количество выходных дней", typeof(Int16));
            DataColumn normCh = new DataColumn("норма часов", typeof(Int16));

            //добавляем колонки в таблицу
            dt.Columns.Add(Mounth);
            dt.Columns.Add(colCountDayInMonth);
            dt.Columns.Add(colCountDayRab);
            dt.Columns.Add(colCountDayVuh);
            dt.Columns.Add(normCh);
            DataRow row = null;
            //создаём новую строку

            //заполняем строку значениями

            for (int i = 1; i <= 12; i++)
            {
                row = dt.NewRow();
                row["месяц"] = months[i - 1];
                row["количество дней в месяце"] = DateTime.DaysInMonth(DateTime.Today.Year, i);
                row["количество рабочих дней"] = Program.RD[i - 1];
                row["количество выходных дней"] = DateTime.DaysInMonth(DateTime.Today.Year, i) - Program.RD[i - 1];
                row["норма часов"] = Program.RD[i - 1] * 8 - Program.PHD[i - 1];
                dt.Rows.Add(row);
            }
            return dt;
        }


        private void Form4_Load(object sender, EventArgs e)

        {
            textBoxEnd.Enabled = false;
            textBoxStart.Enabled = false;

            dataGridViewCalendar.DataSource = CreateTable();
            CreateCalendar();
        }

        private void buttonAddCalendary_Click(object sender, EventArgs e)
        {
            string readPath = Environment.CurrentDirectory + @"\Shops\" + Program.currentShop.getIdShop() + $@"\Calendar{calendarYear}";

            foreach (var l in ldfc)
            {
                var programDate = Program.currentShop.DFCs.SingleOrDefault(x => x.getData() == l.getData());
                if (programDate == null) throw new ArgumentNullException(nameof(programDate), "Несуществующая дата");
                programDate.setTimeBaE(l.getTimeStart(), l.getTimeEnd());
            }
            
            using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
            {

                foreach (DataForCalendary d in Program.currentShop.DFCs)
                    sw.WriteLine(d.getData() + "#" + d.getTip() + "#" + d.getTimeStart() + "#" + d.getTimeEnd());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultipleSelectModeOff();
            // MessageBox.Show(comboBox1.SelectedItem.ToString());
            ResetCheckedLabels();
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    textBoxStart.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 1)).getTimeStart().ToString();
                    textBoxEnd.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString();
                    ldfc.Clear();
                    ldfc =
                        Program.currentShop.DFCs.FindAll(
                            t =>
                                (t.getTip() == 1) || (t.getTip() == 2) || (t.getTip() == 3) || (t.getTip() == 4) ||
                                (t.getTip() == 5));
                    SetCheckedDaysByDates(ldfc);
                    break;
                case 2:
                    textBoxStart.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 7)).getTimeStart().ToString();
                    textBoxEnd.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString();
                    ldfc.Clear();
                    ldfc = Program.currentShop.DFCs.FindAll(t => (t.getTip() == 6) || (t.getTip() == 7));
                    SetCheckedDaysByDates(ldfc);
                    break;
                case 3:
                    textBoxStart.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 8)).getTimeStart().ToString();
                    textBoxEnd.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString();
                    ldfc.Clear();
                    ldfc = Program.currentShop.DFCs.FindAll(t => (t.getTip() == 8));
                    SetCheckedDaysByDates(ldfc);
                    break;
                case 4:
                    textBoxStart.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 9)).getTimeStart().ToString();
                    textBoxEnd.Text = Program.currentShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString();
                    ldfc.Clear();
                    ldfc = Program.currentShop.DFCs.FindAll(t => (t.getTip() == 9));
                    SetCheckedDaysByDates(ldfc);
                    break;
                case 5:
                    {
                        MultipleSelectModeOn();
                        textBoxStart.Text = "";
                        textBoxEnd.Text = "";
                        break;
                    }
                case 0:
                {    
                    break;
                }
                default: break;
            }
            SetCheckedLabels();
        }


        private void RedactirStartEndDay()
        {
            if (ldfc.Count != 0)
            {
                foreach (DataForCalendary dfc in ldfc)
                {
                    dfc.setTimeBaE(int.Parse(textBoxStart.Text), int.Parse(textBoxEnd.Text));

                }

                foreach (var l in _checkedLabels)
                {
                    var t = GetDataFromLabel(l);
                    l.Tag =
                        $"{t.Item2[0]} {t.Item2[1]} {t.Item2[2]};{int.Parse(textBoxStart.Text)};{int.Parse(textBoxEnd.Text)};";
                }
            }
            else
            {
                MessageBox.Show("Даты не выбраны");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Редактировать")
            {
                button1.Text = "Применить";
                textBoxEnd.Enabled = true;
                textBoxStart.Enabled = true;
            }
            else
            {
                button1.Text = "Редактировать";
                textBoxEnd.Enabled = false;
                textBoxStart.Enabled = false;
                RedactirStartEndDay();
            }
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            //e.KeyCode
        }

        /// <summary>
        /// Клик на сброс выделения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            UnselectCheckedDays();
        }

        /// <summary>
        /// Клик на выключения режима выделения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bMSelectOff_Click(object sender, EventArgs e)
        {
            MultipleSelectModeOff();
            comboBox1.SelectedIndex = 0;
            

        }

        /// <summary>
        /// Сбрасывает выделение ячеек календаря
        /// </summary>
        private void UnselectCheckedDays()
        {
            ldfc.Clear();
            rtbCheckedDays.Text = "";
            ResetCheckedLabels();
        }

        /// <summary>
        /// Выключает режим выделения дней
        /// </summary>
        private void MultipleSelectModeOff()
        {
            comboBox1.Enabled = true;
            UnselectCheckedDays();
            bResetSelection.Visible = false;
            bMSelectOff.Visible = false;
            gbCheckedDays.Visible = false;
        }

        /// <summary>
        /// Включает режим выделения дней
        /// </summary>
        private void MultipleSelectModeOn()
        {
            comboBox1.Enabled = false;
            bResetSelection.Visible = true;
            bMSelectOff.Visible = true;
            gbCheckedDays.Visible = true;
        }

        /// <summary>
        /// Сбрасывает настройки для выделенных лейблов
        /// </summary>
        private void ResetCheckedLabels()
        {
            foreach (var l in _checkedLabels)
            {
                ResetLabelView(l);
            }
            _checkedLabels.Clear();
        }

        /// <summary>
        /// Устанавливает настройки для выделенных лейблов
        /// </summary>
        private void SetCheckedLabels()
        {
            foreach (var l in _checkedLabels)
            {
                SetLabelView(l, false);
            }
        }

        /// <summary>
        /// Сбрасывает настройки цвета и границы лейбла
        /// </summary>
        /// <param name="l"></param>
        private void ResetLabelView(Label l)
        {
            if (l != null)
            {
                l.BorderStyle = BorderStyle.None;
                l.Font = new Font(FontFamily.GenericSansSerif, 8);
            }
        }

        /// <summary>
        /// Устанавливает настройки цвета и границы лейбла
        /// </summary>
        /// <param name="l"></param>
        private void SetLabelView(Label l, bool addToCheckedLabels = true)
        {
            if (l != null)
            {
                l.BorderStyle = BorderStyle.FixedSingle;
                l.Font = new Font(FontFamily.GenericSansSerif, 5);
                if (addToCheckedLabels)
                {
                    _checkedLabels.Add(l);
                }

            }

        }

        /// <summary>
        /// Обновляет поле rtbCheckDays
        /// </summary>
        private void SetRtbCheckDaysText()
        {
            rtbCheckedDays.Text = "";
            foreach (var chLbl in _checkedLabels)
            {
                var t = GetDataFromLabel(chLbl);
                rtbCheckedDays.Text +=
                    $"{new DateTime(int.Parse(t.Item2[2]), int.Parse(t.Item2[1]), int.Parse(t.Item2[0])):dd MMMM yyyy};";
            }
        }

        /// <summary>
        /// Возвращает объект Tuple, где Item1 это массив {01 01 2001}, Item 2 {9 23}
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        private Tuple<string[], string[]> GetDataFromLabel(Label label)
        {
            var s1 = label?.Tag.ToString() ?? "0;0;0";

            var s = s1.Split(';');
            var d = s[0].Split(' ');
            return new Tuple<string[], string[]>(s, d);
        }

        /// <summary>
        /// По переданным данным заполняет лист ячеек для выделения
        /// </summary>
        /// <param name="dates"></param>
        private void SetCheckedDaysByDates(List<DataForCalendary> dates)
        {
            Dictionary<DateTime, Label> datesOfLabels = new Dictionary<DateTime, Label>(Program.currentShop.DFCs.Count);

            foreach (var d in ld)
            {
                var t = GetDataFromLabel(d);
                DateTime dt = new DateTime(int.Parse(t.Item2[2]), int.Parse(t.Item2[1]), int.Parse(t.Item2[0]));
                datesOfLabels.Add(dt, d);
            }

            foreach (var day in dates)
            {
                DateTime dtCalendar = day.getData();
                var label = datesOfLabels[dtCalendar];
                _checkedLabels.Add(label);
            }
        }
    }
}
