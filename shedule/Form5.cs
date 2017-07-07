
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace shedule
{
    public partial class Form5 : Form
    {
        private BackgroundWorker bw = new BackgroundWorker();
        static public int[] chartX;
        static public string[] chartXStr;
        static public int[] chartY2;
        static public int[] chartY1;
        static public int Nday = 0;

        public Form5()
        {
            InitializeComponent();
        }

        public  void FondOplaty()
        {
            button1.Visible = false;
            button2.Visible = false;
            chart1.Series.Clear();
            chart1.Series.Add(new Series("текущий фонд оплаты"));
            chart1.Series.Add(new Series("Оптимальный фонд оплаты"));
           


          
            chartXStr = new string[Program.currentShop.tsr.Count + 1];
            chartY1 = new int[Program.currentShop.tsr.Count + 1];
            chartY2 = new int[Program.currentShop.tsr.Count + 1];

            int i = 0;
            foreach(TSR tsr in Program.currentShop.tsr){
                chartXStr[i] = tsr.getOtobragenie();
                chartY1[i] = tsr.getCount() * tsr.getZarp();

                chartY2[i] = Program.currentShop.employes.FindAll(t => t.getTip() == tsr.getTip()).Count * tsr.getZarp();

                i++;
            }

            chart1.Series["текущий фонд оплаты"].Points.DataBindXY(chartXStr, chartY1);
            chart1.Series["Оптимальный фонд оплаты"].Points.DataBindXY(chartXStr, chartY2);

        }

        public void PotrVPerson()
        {
            button1.Visible = false;
            button2.Visible = false;
            chart1.Series.Clear();
            chart1.Series.Add(new Series("Текущее количество персонала"));
            chart1.Series.Add(new Series("Оптимальное количество"));
           



            chartXStr = new string[Program.currentShop.tsr.Count + 1];
            chartY1 = new int[Program.currentShop.tsr.Count + 1];
            chartY2 = new int[Program.currentShop.tsr.Count + 1];

            int i = 0;
            foreach (TSR tsr in Program.currentShop.tsr)
            {
                chartXStr[i] = tsr.getOtobragenie();
                chartY1[i] = tsr.getCount();

                chartY2[i] = Program.currentShop.employes.FindAll(t => t.getTip() == tsr.getTip()).Count ;

                i++;
            }

            chart1.Series["Текущее количество персонала"].Points.DataBindXY(chartXStr, chartY1);
            chart1.Series["Оптимальное количество"].Points.DataBindXY(chartXStr, chartY2);

        }

        public void Templates() {
            button1.Visible = true;
            button2.Visible = true;
            Nday = 0;
           
           

            Program.currentShop.MouthPrognozT[Nday].DS.CreateChartDaySaleCheck();
            Program.currentShop.MouthPrognozT[Nday].DS.CreateChartDaySaleClick();

            chart1.Series.Clear();
             chart1.Series.Add(new Series("Прогнозное количество кликов"));
            chart1.Series.Add(new Series("Прогнозное количество чеков"));
           

            chart1.Series["Прогнозное количество кликов"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Прогнозное количество кликов"].Color = System.Drawing.Color.Green;
            chart1.Series["Прогнозное количество чеков"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Прогнозное количество чеков"].Color = System.Drawing.Color.Red;
           

            chart1.Series["Прогнозное количество чеков"].Points.DataBindXY(Program.currentShop.MouthPrognozT[Nday].DS.ChartClick.X, Program.currentShop.MouthPrognozT[Nday].DS.ChartClick.Y);
            chart1.Series["Прогнозное количество кликов"].Points.DataBindXY(Program.currentShop.MouthPrognozT[Nday].DS.ChartCheck.X, Program.currentShop.MouthPrognozT[Nday].DS.ChartCheck.Y);
           

            var l = chart1.Legends;
            chart1.Series[2].IsVisibleInLegend = false;
            chart1.Series[3].IsVisibleInLegend = false;
        }



        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {

            }
            else if (e.Error != null)
            {

            }
            else
            {
                switch (Program.tipDiagram)
                {
                    case 1: progressBar1.Value = 12; Templates(); progressBar1.Value = 16; break;
                    case 3:
                        progressBar1.Value = 14; FondOplaty(); progressBar1.Value = 16; break;
                    case 2:
                        progressBar1.Value = 14; PotrVPerson(); progressBar1.Value = 16; break;

                }
                progressBar1.Value = progressBar1.Maximum;

                progressBar1.Visible = false;
                label3.Visible = false;
            }
        }



        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 2: label3.Text = "Создание прогноза продаж"; break;
                case 4: label3.Text = "Подсчет оптимальной загруженности"; break;
                case 6: label3.Text = "Оптимальная расстановка смен"; break;
                case 8: label3.Text = "Запись в Exel"; break;

            }
            progressBar1.Value = e.ProgressPercentage;
        }


        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bg = sender as BackgroundWorker;
            switch (Program.tipDiagram)
            {
                case 1: bg.ReportProgress(4); Program.createPrognoz3(); bg.ReportProgress(10); break;
                case 3:
                    bg.ReportProgress(4);
                    Program.createPrognoz();
                    bg.ReportProgress(8);
                    Program.OptimCountSotr(); bg.ReportProgress(12); break;
                case 2:
                    bg.ReportProgress(4);
                    Program.createPrognoz();
                    bg.ReportProgress(8);
                    Program.OptimCountSotr(); break;
                    bg.ReportProgress(12);
            }
        }



    private void Form5_Load(object sender, EventArgs e)
        {
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();

        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            Nday++;
            if (Nday < Program.currentShop.MouthPrognozT.Count)
            {
                /* getChart();
                 // 
                 chart1.Series["s1"].Points.DataBindXY(chartX, chartY1);
                 chart1.Series["s2"].Points.DataBindXY(chartX, chartY2);
                 */
                label1.Text = Program.currentShop.MouthPrognozT[Nday].getData().ToString();
                Program.currentShop.MouthPrognozT[Nday].DS.CreateChartDaySaleCheck();
                Program.currentShop.MouthPrognozT[Nday].DS.CreateChartDaySaleClick();

                chart1.Series["Прогнозное количество чеков"].Points.DataBindXY(Program.currentShop.MouthPrognozT[Nday].DS.ChartClick.X, Program.currentShop.MouthPrognozT[Nday].DS.ChartClick.Y);
                chart1.Series["Прогнозное количество кликов"].Points.DataBindXY(Program.currentShop.MouthPrognozT[Nday].DS.ChartCheck.X, Program.currentShop.MouthPrognozT[Nday].DS.ChartCheck.Y);
            }
            else { Nday--; MessageBox.Show("Больше данных нет"); }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Nday--;
            if (Nday > -1)
            {
              

                label1.Text = Program.currentShop.MouthPrognozT[Nday].getData().ToString();
                Program.currentShop.MouthPrognozT[Nday].DS.CreateChartDaySaleCheck();
                Program.currentShop.MouthPrognozT[Nday].DS.CreateChartDaySaleClick();

                chart1.Series["Прогнозное количество чеков"].Points.DataBindXY(Program.currentShop.MouthPrognozT[Nday].DS.ChartClick.X, Program.currentShop.MouthPrognozT[Nday].DS.ChartClick.Y);
                chart1.Series["Прогнозное количество кликов"].Points.DataBindXY(Program.currentShop.MouthPrognozT[Nday].DS.ChartCheck.X, Program.currentShop.MouthPrognozT[Nday].DS.ChartCheck.Y);
            }
            else { Nday++; MessageBox.Show("Больше данных нет"); }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
