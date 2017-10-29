using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using shedule.Code;

namespace shedule
{
    public partial class Form4 : Form
    {
        Label[] ld;
        List<DataForCalendary> ldfc = new List<DataForCalendary>();
        private List<Label> _checkedLabels;
        private int calendarYear;
        private int EditCell;

        private static Shop _handledShop;

        #region OnLoadForm -- действия которые происходят при загрузке формы/нажатии на кнопку следующего года

        private void CalendarConstructor(int year)
        {
            if (Helper.CheckNextYearCalendarIsExist() && DateTime.Now.Year == year)
            {
                buttonCalendarNextYear.Visible = true;
                buttonCalendarNextYear.Text = (year + 1).ToString();
            }

            if (DateTime.Now.Year == year)
            {
               // _handledShop = Program.currentShop;
                //  _handledShop.DFCs = new List<DataForCalendary>();
                _handledShop = CopyHelper.CreateDeepCopy(Program.currentShop);
                _handledShop.DFCs = Program.currentShop.DFCs.FindAll(t => t.getData().Year == DateTime.Now.Year);

            }
            else
            {
                _handledShop = CopyHelper.CreateDeepCopy(Program.currentShop);
                CalendarHelper.GetListDateForShop(_handledShop, year);
            }

            _checkedLabels = new List<Label>();
            comboBox1.SelectedIndex = 0;
            calendarYear = year;
        }

        private void CalendarLoad()
        {
            textBoxEnd.Enabled = false;
            textBoxStart.Enabled = false;

            dataGridViewCalendar.DataSource = CreateTable();
            if (calendarYear==DateTime.Now.Year) {
                for (int i = 0; i < DateTime.Now.Month; i++)
                {
                    dataGridViewCalendar.Rows[i].Cells[4].ReadOnly = true;
                } }
            dataGridViewCalendar.Columns[0].ReadOnly = true;
            dataGridViewCalendar.Columns[1].ReadOnly = true;
            dataGridViewCalendar.Columns[2].ReadOnly = true;
            dataGridViewCalendar.Columns[3].ReadOnly = true;
            CreateCalendar();
        }

        #endregion

        public Form4(int year)
        {
            InitializeComponent();
            CalendarConstructor(year);
        }


        private void CreateCalendar()
        {
            
            ld = new Label[_handledShop.DFCs.Count];
            int m, i, j;
            int k = 0;

            foreach (DataForCalendary d in _handledShop.DFCs)
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
                row["норма часов"] = _handledShop.NormaChasov[i - 1].getNormChas();
                dt.Rows.Add(row);
            }
            return dt;
        }

        private static void readNarma()
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            CalendarLoad();
        }

        private void buttonAddCalendary_Click(object sender, EventArgs e)
        {
            if (_handledShop.RaznChas != 0)
            {
                if (_handledShop.RaznChas > 0) { MessageBox.Show("Несоответствие в норме часов. Добавьте " + _handledShop.RaznChas + " в месяц, где не более "+(180- _handledShop.RaznChas)); }
                else
                {
                    MessageBox.Show("Несоответствие в норме часов. Уменьшите на " + Math.Abs(_handledShop.RaznChas) + " в каком-нибудь месяце. ");

                }
            }
            else
            {
                Program.WriteNormChas();
                string readPath = Environment.CurrentDirectory + @"\Shops\" + _handledShop.getIdShop() + $@"\Calendar{calendarYear}";

                foreach (var l in ldfc)
                {
                    var programDate = _handledShop.DFCs.SingleOrDefault(x => x.getData() == l.getData());
                    if (programDate == null) throw new ArgumentNullException(nameof(programDate), "Несуществующая дата");
                    programDate.setTimeBaE(l.getTimeStart(), l.getTimeEnd());
                }

                using (StreamWriter sw = new StreamWriter(readPath, false, Encoding.Default))
                {

                    foreach (DataForCalendary d in _handledShop.DFCs)
                        sw.WriteLine(d.getData() + "#" + d.getTip() + "#" + d.getTimeStart() + "#" + d.getTimeEnd());
                }
                Program.HandledShops.Add(_handledShop.getIdShop());
                MessageBox.Show("Данные сохранены");
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
                    textBoxStart.Text = _handledShop.DFCs.Find(t => (t.getTip() == 1)).getTimeStart().ToString();
                    textBoxEnd.Text = _handledShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString();
                    ldfc.Clear();
                    ldfc =
                        _handledShop.DFCs.FindAll(
                            t =>
                                (t.getTip() == 1) || (t.getTip() == 2) || (t.getTip() == 3) || (t.getTip() == 4) ||
                                (t.getTip() == 5));
                    SetCheckedDaysByDates(ldfc);
                    break;
                case 2:
                    textBoxStart.Text = _handledShop.DFCs.Find(t => (t.getTip() == 7)).getTimeStart().ToString();
                    textBoxEnd.Text = _handledShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString();
                    ldfc.Clear();
                    ldfc = _handledShop.DFCs.FindAll(t => (t.getTip() == 6) || (t.getTip() == 7));
                    SetCheckedDaysByDates(ldfc);
                    break;
                case 3:
                    textBoxStart.Text = _handledShop.DFCs.Find(t => (t.getTip() == 8)).getTimeStart().ToString();
                    textBoxEnd.Text = _handledShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString();
                    ldfc.Clear();
                    ldfc = _handledShop.DFCs.FindAll(t => (t.getTip() == 8));
                    SetCheckedDaysByDates(ldfc);
                    break;
                case 4:
                    textBoxStart.Text = _handledShop.DFCs.Find(t => (t.getTip() == 9)).getTimeStart().ToString();
                    textBoxEnd.Text = _handledShop.DFCs.Find(t => (t.getTip() == 1)).getTimeEnd().ToString();
                    ldfc.Clear();
                    ldfc = _handledShop.DFCs.FindAll(t => (t.getTip() == 9));
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
                int dayLength = int.Parse(textBoxEnd.Text) - int.Parse(textBoxStart.Text);
                if (dayLength < 9)
                {
                    MessageBox.Show("Длина дня не должна быть меньше 8 часов!");
                    return;
                }
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
            Dictionary<DateTime, Label> datesOfLabels = new Dictionary<DateTime, Label>(_handledShop.DFCs.Count);

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

        private void textBoxStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBoxEnd_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxStart_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewCalendar_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int newZn;
            if (int.TryParse(dataGridViewCalendar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out newZn))
            {
                if (newZn > 180) { MessageBox.Show("Введите число не более 180"); dataGridViewCalendar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = EditCell; }
                else
                {
                    _handledShop.RaznChas += EditCell - newZn;
                    _handledShop.NormaChasov[e.RowIndex].setCountChas(newZn);
                }
            }
            else
            {
                MessageBox.Show("Нужно вводить число");
                dataGridViewCalendar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = EditCell;
            }
        }

        private void dataGridViewCalendar_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            EditCell = int.Parse(dataGridViewCalendar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }

        private void buttonCalendarNextYear_Click(object sender, EventArgs e)
        {

            int newFormYear = calendarYear;
            if (DateTime.Now.Year == calendarYear)
            {
                newFormYear += 1;
            }
            else
            {
                newFormYear = DateTime.Now.Year;
            }
            Form4 form = new Form4(newFormYear);
            form.ShowDialog();
        }

     

        private void dataGridViewCalendar_CellErrorTextChanged(object sender, DataGridViewCellEventArgs e)
        {
            
               
            
        }

        private void dataGridViewCalendar_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Нужно вводить число");
           
        }

        private void textBoxEnd_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtbCheckedDays_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
