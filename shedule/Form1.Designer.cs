namespace shedule
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.getStatisticByShopsDayHourBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new shedule.DataSet1();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.get_StatisticByShopsDayHourTableAdapter = new shedule.DataSet1TableAdapters.get_StatisticByShopsDayHourTableAdapter();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.labelStatus1 = new System.Windows.Forms.Label();
            this.labelStatus2 = new System.Windows.Forms.Label();
            this.button_refresh_list_shops = new System.Windows.Forms.Button();
            this.get_StatisticByShopsDayHourTableAdapter1 = new shedule.DataSet1TableAdapters.get_StatisticByShopsDayHourTableAdapter();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonTest = new System.Windows.Forms.Button();
            this.panelMultShops = new System.Windows.Forms.Panel();
            this.buttonSingleShop = new System.Windows.Forms.Button();
            this.buttonMdel = new System.Windows.Forms.Button();
            this.buttonMadd = new System.Windows.Forms.Button();
            this.listBoxMShops = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxMPartShops = new System.Windows.Forms.ListBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.panelSingleShop = new System.Windows.Forms.Panel();
            this.buttonMultShops = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonRaspisanie = new System.Windows.Forms.Button();
            this.buttonCalendar = new System.Windows.Forms.Button();
            this.buttonKassov = new System.Windows.Forms.Button();
            this.panelCalendar = new System.Windows.Forms.Panel();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.panelTRasp = new System.Windows.Forms.Panel();
            this.buttonPTSR = new System.Windows.Forms.Button();
            this.dataGridViewForTSR = new System.Windows.Forms.DataGridView();
            this.panelKassOper = new System.Windows.Forms.Panel();
            this.buttonImportKasOper = new System.Windows.Forms.Button();
            this.radioButtonIzBD = new System.Windows.Forms.RadioButton();
            this.radioButtonIzFile = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelGruz = new System.Windows.Forms.Panel();
            this.panelUpravlenie = new System.Windows.Forms.Panel();
            this.buttonParamOptimiz = new System.Windows.Forms.Button();
            this.buttonVariantsSmen = new System.Windows.Forms.Button();
            this.buttonFactors = new System.Windows.Forms.Button();
            this.panelParamOptim = new System.Windows.Forms.Panel();
            this.buttonApplyParamsOptim = new System.Windows.Forms.Button();
            this.radioButtonObRabTime = new System.Windows.Forms.RadioButton();
            this.radioButtonMinTime = new System.Windows.Forms.RadioButton();
            this.radioButtonMinFondOpl = new System.Windows.Forms.RadioButton();
            this.panelDopusVarSmen = new System.Windows.Forms.Panel();
            this.panelSmen5 = new System.Windows.Forms.Panel();
            this.textBox51 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox52 = new System.Windows.Forms.TextBox();
            this.buttonRedact5 = new System.Windows.Forms.Button();
            this.buttonDelSmen5 = new System.Windows.Forms.Button();
            this.panelSmen3 = new System.Windows.Forms.Panel();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.buttonRedact3 = new System.Windows.Forms.Button();
            this.buttonDelSmen3 = new System.Windows.Forms.Button();
            this.panelSmen4 = new System.Windows.Forms.Panel();
            this.textBox41 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.buttonRedact4 = new System.Windows.Forms.Button();
            this.buttonDelSmen4 = new System.Windows.Forms.Button();
            this.panelSmen2 = new System.Windows.Forms.Panel();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.buttonRedact2 = new System.Windows.Forms.Button();
            this.buttonDelSmen2 = new System.Windows.Forms.Button();
            this.panelSmen1 = new System.Windows.Forms.Panel();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.buttonRedactir1 = new System.Windows.Forms.Button();
            this.buttonDelSmen1 = new System.Windows.Forms.Button();
            this.buttonAddVarSmen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAplyVarSmen = new System.Windows.Forms.Button();
            this.panelFactors = new System.Windows.Forms.Panel();
            this.buttonAplyFactors = new System.Windows.Forms.Button();
            this.buttonClearFactors = new System.Windows.Forms.Button();
            this.dataGridViewFactors = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonExport1 = new System.Windows.Forms.Button();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxSpeed = new System.Windows.Forms.TextBox();
            this.labelTimeSotr = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelData = new System.Windows.Forms.Label();
            this.labelTimeClick = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.textBoxTimeClick = new System.Windows.Forms.TextBox();
            this.labelTimeTell = new System.Windows.Forms.Label();
            this.buttonPrevios = new System.Windows.Forms.Button();
            this.textBoxTimeTell = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonReadTekShedule = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.getStatisticByShopsDayHourBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.panelMultShops.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.panelSingleShop.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelCalendar.SuspendLayout();
            this.panelTRasp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForTSR)).BeginInit();
            this.panelKassOper.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelUpravlenie.SuspendLayout();
            this.panelParamOptim.SuspendLayout();
            this.panelDopusVarSmen.SuspendLayout();
            this.panelSmen5.SuspendLayout();
            this.panelSmen3.SuspendLayout();
            this.panelSmen4.SuspendLayout();
            this.panelSmen2.SuspendLayout();
            this.panelSmen1.SuspendLayout();
            this.panelFactors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // getStatisticByShopsDayHourBindingSource
            // 
            this.getStatisticByShopsDayHourBindingSource.DataMember = "get_StatisticByShopsDayHour";
            this.getStatisticByShopsDayHourBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(295, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(398, 83);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(185, 43);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // get_StatisticByShopsDayHourTableAdapter
            // 
            this.get_StatisticByShopsDayHourTableAdapter.ClearBeforeFill = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(408, 29);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(4, 17);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(176, 329);
            this.listBox1.TabIndex = 9;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // labelStatus1
            // 
            this.labelStatus1.AutoSize = true;
            this.labelStatus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelStatus1.Location = new System.Drawing.Point(29, 10);
            this.labelStatus1.Name = "labelStatus1";
            this.labelStatus1.Size = new System.Drawing.Size(61, 17);
            this.labelStatus1.TabIndex = 8;
            this.labelStatus1.Text = "Статус: ";
            // 
            // labelStatus2
            // 
            this.labelStatus2.AutoSize = true;
            this.labelStatus2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelStatus2.Location = new System.Drawing.Point(76, 27);
            this.labelStatus2.Name = "labelStatus2";
            this.labelStatus2.Size = new System.Drawing.Size(0, 17);
            this.labelStatus2.TabIndex = 9;
            // 
            // button_refresh_list_shops
            // 
            this.button_refresh_list_shops.Location = new System.Drawing.Point(4, 358);
            this.button_refresh_list_shops.Name = "button_refresh_list_shops";
            this.button_refresh_list_shops.Size = new System.Drawing.Size(75, 49);
            this.button_refresh_list_shops.TabIndex = 10;
            this.button_refresh_list_shops.Text = "обновить список магазинов";
            this.button_refresh_list_shops.UseVisualStyleBackColor = true;
            this.button_refresh_list_shops.Click += new System.EventHandler(this.button_refresh_list_shops_Click);
            // 
            // get_StatisticByShopsDayHourTableAdapter1
            // 
            this.get_StatisticByShopsDayHourTableAdapter1.ClearBeforeFill = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(681, 13);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 11;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // panelMultShops
            // 
            this.panelMultShops.Controls.Add(this.buttonSingleShop);
            this.panelMultShops.Controls.Add(this.buttonMdel);
            this.panelMultShops.Controls.Add(this.buttonMadd);
            this.panelMultShops.Controls.Add(this.listBoxMShops);
            this.panelMultShops.Controls.Add(this.groupBox1);
            this.panelMultShops.Location = new System.Drawing.Point(3, 42);
            this.panelMultShops.Name = "panelMultShops";
            this.panelMultShops.Size = new System.Drawing.Size(764, 435);
            this.panelMultShops.TabIndex = 12;
            // 
            // buttonSingleShop
            // 
            this.buttonSingleShop.Location = new System.Drawing.Point(29, 384);
            this.buttonSingleShop.Name = "buttonSingleShop";
            this.buttonSingleShop.Size = new System.Drawing.Size(75, 48);
            this.buttonSingleShop.TabIndex = 6;
            this.buttonSingleShop.Text = "Режим одного магазина";
            this.buttonSingleShop.UseVisualStyleBackColor = true;
            this.buttonSingleShop.Click += new System.EventHandler(this.buttonSingleShop_Click);
            // 
            // buttonMdel
            // 
            this.buttonMdel.Location = new System.Drawing.Point(141, 197);
            this.buttonMdel.Name = "buttonMdel";
            this.buttonMdel.Size = new System.Drawing.Size(45, 23);
            this.buttonMdel.TabIndex = 3;
            this.buttonMdel.Text = "<";
            this.buttonMdel.UseVisualStyleBackColor = true;
            this.buttonMdel.Click += new System.EventHandler(this.buttonMdel_Click);
            // 
            // buttonMadd
            // 
            this.buttonMadd.Location = new System.Drawing.Point(140, 162);
            this.buttonMadd.Name = "buttonMadd";
            this.buttonMadd.Size = new System.Drawing.Size(46, 23);
            this.buttonMadd.TabIndex = 2;
            this.buttonMadd.Text = ">";
            this.buttonMadd.UseVisualStyleBackColor = true;
            this.buttonMadd.Click += new System.EventHandler(this.buttonMadd_Click);
            // 
            // listBoxMShops
            // 
            this.listBoxMShops.FormattingEnabled = true;
            this.listBoxMShops.Location = new System.Drawing.Point(14, 36);
            this.listBoxMShops.Name = "listBoxMShops";
            this.listBoxMShops.Size = new System.Drawing.Size(120, 342);
            this.listBoxMShops.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxMPartShops);
            this.groupBox1.Controls.Add(this.tabControl2);
            this.groupBox1.Location = new System.Drawing.Point(192, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 428);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // listBoxMPartShops
            // 
            this.listBoxMPartShops.FormattingEnabled = true;
            this.listBoxMPartShops.Location = new System.Drawing.Point(10, 31);
            this.listBoxMPartShops.Name = "listBoxMPartShops";
            this.listBoxMPartShops.Size = new System.Drawing.Size(120, 342);
            this.listBoxMPartShops.TabIndex = 1;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Location = new System.Drawing.Point(141, 28);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(425, 345);
            this.tabControl2.TabIndex = 4;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(417, 319);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "управление";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(417, 319);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "результат";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // panelSingleShop
            // 
            this.panelSingleShop.Controls.Add(this.buttonMultShops);
            this.panelSingleShop.Controls.Add(this.button_refresh_list_shops);
            this.panelSingleShop.Controls.Add(this.listBox1);
            this.panelSingleShop.Controls.Add(this.tabControl1);
            this.panelSingleShop.Location = new System.Drawing.Point(13, 48);
            this.panelSingleShop.Name = "panelSingleShop";
            this.panelSingleShop.Size = new System.Drawing.Size(751, 429);
            this.panelSingleShop.TabIndex = 13;
            // 
            // buttonMultShops
            // 
            this.buttonMultShops.Location = new System.Drawing.Point(85, 358);
            this.buttonMultShops.Name = "buttonMultShops";
            this.buttonMultShops.Size = new System.Drawing.Size(75, 49);
            this.buttonMultShops.TabIndex = 11;
            this.buttonMultShops.Text = "Режим нескольких магазинов";
            this.buttonMultShops.UseVisualStyleBackColor = true;
            this.buttonMultShops.Click += new System.EventHandler(this.buttonMultShops_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.AccessibleName = "s";
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(186, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(518, 368);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonRaspisanie);
            this.tabPage1.Controls.Add(this.buttonCalendar);
            this.tabPage1.Controls.Add(this.buttonKassov);
            this.tabPage1.Controls.Add(this.panelCalendar);
            this.tabPage1.Controls.Add(this.panelTRasp);
            this.tabPage1.Controls.Add(this.panelKassOper);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(510, 342);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Вводные";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonRaspisanie
            // 
            this.buttonRaspisanie.BackColor = System.Drawing.Color.White;
            this.buttonRaspisanie.Location = new System.Drawing.Point(325, 15);
            this.buttonRaspisanie.Name = "buttonRaspisanie";
            this.buttonRaspisanie.Size = new System.Drawing.Size(99, 34);
            this.buttonRaspisanie.TabIndex = 2;
            this.buttonRaspisanie.Text = "Текущее штатное расписание";
            this.buttonRaspisanie.UseVisualStyleBackColor = false;
            this.buttonRaspisanie.Click += new System.EventHandler(this.buttonRaspisanie_Click);
            // 
            // buttonCalendar
            // 
            this.buttonCalendar.BackColor = System.Drawing.Color.White;
            this.buttonCalendar.Location = new System.Drawing.Point(187, 15);
            this.buttonCalendar.Name = "buttonCalendar";
            this.buttonCalendar.Size = new System.Drawing.Size(114, 34);
            this.buttonCalendar.TabIndex = 1;
            this.buttonCalendar.Text = "Производственный календарь";
            this.buttonCalendar.UseVisualStyleBackColor = false;
            this.buttonCalendar.Click += new System.EventHandler(this.buttonCalendar_Click);
            // 
            // buttonKassov
            // 
            this.buttonKassov.BackColor = System.Drawing.Color.MistyRose;
            this.buttonKassov.Location = new System.Drawing.Point(74, 15);
            this.buttonKassov.Name = "buttonKassov";
            this.buttonKassov.Size = new System.Drawing.Size(89, 34);
            this.buttonKassov.TabIndex = 0;
            this.buttonKassov.Text = "Кассовые операции";
            this.buttonKassov.UseVisualStyleBackColor = false;
            this.buttonKassov.Click += new System.EventHandler(this.buttonKassov_Click);
            // 
            // panelCalendar
            // 
            this.panelCalendar.BackColor = System.Drawing.Color.White;
            this.panelCalendar.Controls.Add(this.monthCalendar1);
            this.panelCalendar.Location = new System.Drawing.Point(20, 55);
            this.panelCalendar.Name = "panelCalendar";
            this.panelCalendar.Size = new System.Drawing.Size(437, 266);
            this.panelCalendar.TabIndex = 3;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(141, 49);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            // 
            // panelTRasp
            // 
            this.panelTRasp.Controls.Add(this.buttonPTSR);
            this.panelTRasp.Controls.Add(this.dataGridViewForTSR);
            this.panelTRasp.Location = new System.Drawing.Point(23, 55);
            this.panelTRasp.Name = "panelTRasp";
            this.panelTRasp.Size = new System.Drawing.Size(437, 266);
            this.panelTRasp.TabIndex = 4;
            // 
            // buttonPTSR
            // 
            this.buttonPTSR.Location = new System.Drawing.Point(339, 216);
            this.buttonPTSR.Name = "buttonPTSR";
            this.buttonPTSR.Size = new System.Drawing.Size(75, 23);
            this.buttonPTSR.TabIndex = 1;
            this.buttonPTSR.Text = "Применить";
            this.buttonPTSR.UseVisualStyleBackColor = true;
            this.buttonPTSR.Click += new System.EventHandler(this.buttonPTSR_Click);
            // 
            // dataGridViewForTSR
            // 
            this.dataGridViewForTSR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewForTSR.Location = new System.Drawing.Point(26, 19);
            this.dataGridViewForTSR.Name = "dataGridViewForTSR";
            this.dataGridViewForTSR.Size = new System.Drawing.Size(386, 167);
            this.dataGridViewForTSR.TabIndex = 0;
            // 
            // panelKassOper
            // 
            this.panelKassOper.Controls.Add(this.buttonImportKasOper);
            this.panelKassOper.Controls.Add(this.radioButtonIzBD);
            this.panelKassOper.Controls.Add(this.radioButtonIzFile);
            this.panelKassOper.Location = new System.Drawing.Point(20, 55);
            this.panelKassOper.Name = "panelKassOper";
            this.panelKassOper.Size = new System.Drawing.Size(437, 266);
            this.panelKassOper.TabIndex = 2;
            // 
            // buttonImportKasOper
            // 
            this.buttonImportKasOper.Location = new System.Drawing.Point(54, 120);
            this.buttonImportKasOper.Name = "buttonImportKasOper";
            this.buttonImportKasOper.Size = new System.Drawing.Size(75, 23);
            this.buttonImportKasOper.TabIndex = 2;
            this.buttonImportKasOper.Text = "загрузить";
            this.buttonImportKasOper.UseVisualStyleBackColor = true;
            this.buttonImportKasOper.Click += new System.EventHandler(this.buttonImportKasOper_Click);
            // 
            // radioButtonIzBD
            // 
            this.radioButtonIzBD.AutoSize = true;
            this.radioButtonIzBD.Location = new System.Drawing.Point(29, 49);
            this.radioButtonIzBD.Name = "radioButtonIzBD";
            this.radioButtonIzBD.Size = new System.Drawing.Size(193, 17);
            this.radioButtonIzBD.TabIndex = 0;
            this.radioButtonIzBD.TabStop = true;
            this.radioButtonIzBD.Text = "Загружать автоматически из БД";
            this.radioButtonIzBD.UseVisualStyleBackColor = true;
            this.radioButtonIzBD.CheckedChanged += new System.EventHandler(this.radioButtonIzBD_CheckedChanged);
            // 
            // radioButtonIzFile
            // 
            this.radioButtonIzFile.AutoSize = true;
            this.radioButtonIzFile.Location = new System.Drawing.Point(29, 97);
            this.radioButtonIzFile.Name = "radioButtonIzFile";
            this.radioButtonIzFile.Size = new System.Drawing.Size(351, 17);
            this.radioButtonIzFile.TabIndex = 1;
            this.radioButtonIzFile.TabStop = true;
            this.radioButtonIzFile.Text = "Загрузить вручную файл по отдельному магазину в формате xls";
            this.radioButtonIzFile.UseVisualStyleBackColor = true;
            this.radioButtonIzFile.CheckedChanged += new System.EventHandler(this.radioButtonIzFile_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelGruz);
            this.tabPage2.Controls.Add(this.panelUpravlenie);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(510, 342);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Управление";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelGruz
            // 
            this.panelGruz.Location = new System.Drawing.Point(137, 342);
            this.panelGruz.Name = "panelGruz";
            this.panelGruz.Size = new System.Drawing.Size(399, 239);
            this.panelGruz.TabIndex = 4;
            // 
            // panelUpravlenie
            // 
            this.panelUpravlenie.Controls.Add(this.buttonParamOptimiz);
            this.panelUpravlenie.Controls.Add(this.buttonVariantsSmen);
            this.panelUpravlenie.Controls.Add(this.buttonFactors);
            this.panelUpravlenie.Controls.Add(this.panelParamOptim);
            this.panelUpravlenie.Controls.Add(this.panelDopusVarSmen);
            this.panelUpravlenie.Controls.Add(this.panelFactors);
            this.panelUpravlenie.Location = new System.Drawing.Point(6, 6);
            this.panelUpravlenie.Name = "panelUpravlenie";
            this.panelUpravlenie.Size = new System.Drawing.Size(457, 330);
            this.panelUpravlenie.TabIndex = 0;
            // 
            // buttonParamOptimiz
            // 
            this.buttonParamOptimiz.BackColor = System.Drawing.Color.MistyRose;
            this.buttonParamOptimiz.Location = new System.Drawing.Point(39, 15);
            this.buttonParamOptimiz.Name = "buttonParamOptimiz";
            this.buttonParamOptimiz.Size = new System.Drawing.Size(102, 36);
            this.buttonParamOptimiz.TabIndex = 3;
            this.buttonParamOptimiz.Text = "Параметры оптимизации";
            this.buttonParamOptimiz.UseVisualStyleBackColor = false;
            this.buttonParamOptimiz.Click += new System.EventHandler(this.buttonParamOptimiz_Click);
            // 
            // buttonVariantsSmen
            // 
            this.buttonVariantsSmen.BackColor = System.Drawing.Color.White;
            this.buttonVariantsSmen.Location = new System.Drawing.Point(286, 18);
            this.buttonVariantsSmen.Name = "buttonVariantsSmen";
            this.buttonVariantsSmen.Size = new System.Drawing.Size(102, 36);
            this.buttonVariantsSmen.TabIndex = 2;
            this.buttonVariantsSmen.Text = "Допустимые варианты смен";
            this.buttonVariantsSmen.UseVisualStyleBackColor = false;
            this.buttonVariantsSmen.Click += new System.EventHandler(this.buttonVariantsSmen_Click);
            // 
            // buttonFactors
            // 
            this.buttonFactors.BackColor = System.Drawing.Color.White;
            this.buttonFactors.Location = new System.Drawing.Point(161, 18);
            this.buttonFactors.Name = "buttonFactors";
            this.buttonFactors.Size = new System.Drawing.Size(102, 36);
            this.buttonFactors.TabIndex = 1;
            this.buttonFactors.Text = "Факторы";
            this.buttonFactors.UseVisualStyleBackColor = false;
            this.buttonFactors.Click += new System.EventHandler(this.buttonFactors_Click);
            // 
            // panelParamOptim
            // 
            this.panelParamOptim.Controls.Add(this.buttonApplyParamsOptim);
            this.panelParamOptim.Controls.Add(this.radioButtonObRabTime);
            this.panelParamOptim.Controls.Add(this.radioButtonMinTime);
            this.panelParamOptim.Controls.Add(this.radioButtonMinFondOpl);
            this.panelParamOptim.Location = new System.Drawing.Point(6, 57);
            this.panelParamOptim.Name = "panelParamOptim";
            this.panelParamOptim.Size = new System.Drawing.Size(431, 248);
            this.panelParamOptim.TabIndex = 8;
            // 
            // buttonApplyParamsOptim
            // 
            this.buttonApplyParamsOptim.Location = new System.Drawing.Point(253, 111);
            this.buttonApplyParamsOptim.Name = "buttonApplyParamsOptim";
            this.buttonApplyParamsOptim.Size = new System.Drawing.Size(75, 23);
            this.buttonApplyParamsOptim.TabIndex = 3;
            this.buttonApplyParamsOptim.Text = "применить ";
            this.buttonApplyParamsOptim.UseVisualStyleBackColor = true;
            this.buttonApplyParamsOptim.Click += new System.EventHandler(this.buttonApplyParamsOptim_Click);
            // 
            // radioButtonObRabTime
            // 
            this.radioButtonObRabTime.AutoSize = true;
            this.radioButtonObRabTime.Location = new System.Drawing.Point(33, 65);
            this.radioButtonObRabTime.Name = "radioButtonObRabTime";
            this.radioButtonObRabTime.Size = new System.Drawing.Size(332, 17);
            this.radioButtonObRabTime.TabIndex = 2;
            this.radioButtonObRabTime.TabStop = true;
            this.radioButtonObRabTime.Text = "Общее рабочее время, отработанное персоналом магазина";
            this.radioButtonObRabTime.UseVisualStyleBackColor = true;
            this.radioButtonObRabTime.CheckedChanged += new System.EventHandler(this.radioButtonObRabTime_CheckedChanged);
            // 
            // radioButtonMinTime
            // 
            this.radioButtonMinTime.AutoSize = true;
            this.radioButtonMinTime.Location = new System.Drawing.Point(33, 42);
            this.radioButtonMinTime.Name = "radioButtonMinTime";
            this.radioButtonMinTime.Size = new System.Drawing.Size(310, 17);
            this.radioButtonMinTime.TabIndex = 1;
            this.radioButtonMinTime.TabStop = true;
            this.radioButtonMinTime.Text = "Минимизировать время ожидания покупателя на кассе";
            this.radioButtonMinTime.UseVisualStyleBackColor = true;
            this.radioButtonMinTime.CheckedChanged += new System.EventHandler(this.radioButtonMinTime_CheckedChanged);
            // 
            // radioButtonMinFondOpl
            // 
            this.radioButtonMinFondOpl.AutoSize = true;
            this.radioButtonMinFondOpl.Location = new System.Drawing.Point(33, 19);
            this.radioButtonMinFondOpl.Name = "radioButtonMinFondOpl";
            this.radioButtonMinFondOpl.Size = new System.Drawing.Size(213, 17);
            this.radioButtonMinFondOpl.TabIndex = 0;
            this.radioButtonMinFondOpl.TabStop = true;
            this.radioButtonMinFondOpl.Text = "Минимизировать фонд оплаты труда";
            this.radioButtonMinFondOpl.UseVisualStyleBackColor = true;
            this.radioButtonMinFondOpl.CheckedChanged += new System.EventHandler(this.radioButtonMinFondOpl_CheckedChanged);
            // 
            // panelDopusVarSmen
            // 
            this.panelDopusVarSmen.Controls.Add(this.panelSmen5);
            this.panelDopusVarSmen.Controls.Add(this.panelSmen3);
            this.panelDopusVarSmen.Controls.Add(this.panelSmen4);
            this.panelDopusVarSmen.Controls.Add(this.panelSmen2);
            this.panelDopusVarSmen.Controls.Add(this.panelSmen1);
            this.panelDopusVarSmen.Controls.Add(this.buttonAddVarSmen);
            this.panelDopusVarSmen.Controls.Add(this.label1);
            this.panelDopusVarSmen.Controls.Add(this.buttonAplyVarSmen);
            this.panelDopusVarSmen.Location = new System.Drawing.Point(3, 57);
            this.panelDopusVarSmen.Name = "panelDopusVarSmen";
            this.panelDopusVarSmen.Size = new System.Drawing.Size(434, 245);
            this.panelDopusVarSmen.TabIndex = 2;
            // 
            // panelSmen5
            // 
            this.panelSmen5.Controls.Add(this.textBox51);
            this.panelSmen5.Controls.Add(this.label10);
            this.panelSmen5.Controls.Add(this.textBox52);
            this.panelSmen5.Controls.Add(this.buttonRedact5);
            this.panelSmen5.Controls.Add(this.buttonDelSmen5);
            this.panelSmen5.Location = new System.Drawing.Point(12, 161);
            this.panelSmen5.Name = "panelSmen5";
            this.panelSmen5.Size = new System.Drawing.Size(255, 32);
            this.panelSmen5.TabIndex = 30;
            // 
            // textBox51
            // 
            this.textBox51.Location = new System.Drawing.Point(3, 3);
            this.textBox51.MaxLength = 1;
            this.textBox51.Name = "textBox51";
            this.textBox51.ReadOnly = true;
            this.textBox51.Size = new System.Drawing.Size(29, 20);
            this.textBox51.TabIndex = 2;
            this.textBox51.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox51_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label10.Location = new System.Drawing.Point(38, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 20);
            this.label10.TabIndex = 4;
            this.label10.Text = "/";
            // 
            // textBox52
            // 
            this.textBox52.Location = new System.Drawing.Point(58, 3);
            this.textBox52.MaxLength = 1;
            this.textBox52.Name = "textBox52";
            this.textBox52.ReadOnly = true;
            this.textBox52.Size = new System.Drawing.Size(29, 20);
            this.textBox52.TabIndex = 3;
            this.textBox52.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox52_KeyPress);
            // 
            // buttonRedact5
            // 
            this.buttonRedact5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonRedact5.Location = new System.Drawing.Point(93, 3);
            this.buttonRedact5.Name = "buttonRedact5";
            this.buttonRedact5.Size = new System.Drawing.Size(77, 23);
            this.buttonRedact5.TabIndex = 6;
            this.buttonRedact5.Text = "редактировать";
            this.buttonRedact5.UseVisualStyleBackColor = true;
            this.buttonRedact5.Click += new System.EventHandler(this.buttonRedact5_Click);
            // 
            // buttonDelSmen5
            // 
            this.buttonDelSmen5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonDelSmen5.Location = new System.Drawing.Point(176, 5);
            this.buttonDelSmen5.Name = "buttonDelSmen5";
            this.buttonDelSmen5.Size = new System.Drawing.Size(75, 23);
            this.buttonDelSmen5.TabIndex = 7;
            this.buttonDelSmen5.Text = "удалить";
            this.buttonDelSmen5.UseVisualStyleBackColor = true;
            this.buttonDelSmen5.Click += new System.EventHandler(this.buttonDelSmen5_Click);
            // 
            // panelSmen3
            // 
            this.panelSmen3.Controls.Add(this.textBox31);
            this.panelSmen3.Controls.Add(this.label11);
            this.panelSmen3.Controls.Add(this.textBox32);
            this.panelSmen3.Controls.Add(this.buttonRedact3);
            this.panelSmen3.Controls.Add(this.buttonDelSmen3);
            this.panelSmen3.Location = new System.Drawing.Point(9, 88);
            this.panelSmen3.Name = "panelSmen3";
            this.panelSmen3.Size = new System.Drawing.Size(255, 32);
            this.panelSmen3.TabIndex = 30;
            // 
            // textBox31
            // 
            this.textBox31.Location = new System.Drawing.Point(3, 3);
            this.textBox31.MaxLength = 1;
            this.textBox31.Name = "textBox31";
            this.textBox31.ReadOnly = true;
            this.textBox31.Size = new System.Drawing.Size(29, 20);
            this.textBox31.TabIndex = 2;
            this.textBox31.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox31_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label11.Location = new System.Drawing.Point(38, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 20);
            this.label11.TabIndex = 4;
            this.label11.Text = "/";
            // 
            // textBox32
            // 
            this.textBox32.Location = new System.Drawing.Point(58, 3);
            this.textBox32.MaxLength = 1;
            this.textBox32.Name = "textBox32";
            this.textBox32.ReadOnly = true;
            this.textBox32.Size = new System.Drawing.Size(29, 20);
            this.textBox32.TabIndex = 3;
            this.textBox32.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox32_KeyPress);
            // 
            // buttonRedact3
            // 
            this.buttonRedact3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonRedact3.Location = new System.Drawing.Point(93, 3);
            this.buttonRedact3.Name = "buttonRedact3";
            this.buttonRedact3.Size = new System.Drawing.Size(77, 23);
            this.buttonRedact3.TabIndex = 6;
            this.buttonRedact3.Text = "редактировать";
            this.buttonRedact3.UseVisualStyleBackColor = true;
            this.buttonRedact3.Click += new System.EventHandler(this.buttonRedact3_Click);
            // 
            // buttonDelSmen3
            // 
            this.buttonDelSmen3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonDelSmen3.Location = new System.Drawing.Point(176, 5);
            this.buttonDelSmen3.Name = "buttonDelSmen3";
            this.buttonDelSmen3.Size = new System.Drawing.Size(75, 23);
            this.buttonDelSmen3.TabIndex = 7;
            this.buttonDelSmen3.Text = "удалить";
            this.buttonDelSmen3.UseVisualStyleBackColor = true;
            this.buttonDelSmen3.Click += new System.EventHandler(this.buttonDelSmen3_Click);
            // 
            // panelSmen4
            // 
            this.panelSmen4.Controls.Add(this.textBox41);
            this.panelSmen4.Controls.Add(this.label12);
            this.panelSmen4.Controls.Add(this.textBox42);
            this.panelSmen4.Controls.Add(this.buttonRedact4);
            this.panelSmen4.Controls.Add(this.buttonDelSmen4);
            this.panelSmen4.Location = new System.Drawing.Point(9, 123);
            this.panelSmen4.Name = "panelSmen4";
            this.panelSmen4.Size = new System.Drawing.Size(255, 32);
            this.panelSmen4.TabIndex = 30;
            // 
            // textBox41
            // 
            this.textBox41.Location = new System.Drawing.Point(3, 3);
            this.textBox41.MaxLength = 1;
            this.textBox41.Name = "textBox41";
            this.textBox41.ReadOnly = true;
            this.textBox41.Size = new System.Drawing.Size(29, 20);
            this.textBox41.TabIndex = 2;
            this.textBox41.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox41_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label12.Location = new System.Drawing.Point(38, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 20);
            this.label12.TabIndex = 4;
            this.label12.Text = "/";
            // 
            // textBox42
            // 
            this.textBox42.Location = new System.Drawing.Point(58, 3);
            this.textBox42.MaxLength = 1;
            this.textBox42.Name = "textBox42";
            this.textBox42.ReadOnly = true;
            this.textBox42.Size = new System.Drawing.Size(29, 20);
            this.textBox42.TabIndex = 3;
            this.textBox42.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox42_KeyPress);
            // 
            // buttonRedact4
            // 
            this.buttonRedact4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonRedact4.Location = new System.Drawing.Point(93, 3);
            this.buttonRedact4.Name = "buttonRedact4";
            this.buttonRedact4.Size = new System.Drawing.Size(77, 23);
            this.buttonRedact4.TabIndex = 6;
            this.buttonRedact4.Text = "редактировать";
            this.buttonRedact4.UseVisualStyleBackColor = true;
            this.buttonRedact4.Click += new System.EventHandler(this.buttonRedact4_Click);
            // 
            // buttonDelSmen4
            // 
            this.buttonDelSmen4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonDelSmen4.Location = new System.Drawing.Point(176, 5);
            this.buttonDelSmen4.Name = "buttonDelSmen4";
            this.buttonDelSmen4.Size = new System.Drawing.Size(75, 23);
            this.buttonDelSmen4.TabIndex = 7;
            this.buttonDelSmen4.Text = "удалить";
            this.buttonDelSmen4.UseVisualStyleBackColor = true;
            this.buttonDelSmen4.Click += new System.EventHandler(this.buttonDelSmen4_Click);
            // 
            // panelSmen2
            // 
            this.panelSmen2.Controls.Add(this.textBox21);
            this.panelSmen2.Controls.Add(this.label9);
            this.panelSmen2.Controls.Add(this.textBox22);
            this.panelSmen2.Controls.Add(this.buttonRedact2);
            this.panelSmen2.Controls.Add(this.buttonDelSmen2);
            this.panelSmen2.Location = new System.Drawing.Point(9, 54);
            this.panelSmen2.Name = "panelSmen2";
            this.panelSmen2.Size = new System.Drawing.Size(255, 32);
            this.panelSmen2.TabIndex = 30;
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(3, 3);
            this.textBox21.MaxLength = 1;
            this.textBox21.Name = "textBox21";
            this.textBox21.ReadOnly = true;
            this.textBox21.Size = new System.Drawing.Size(29, 20);
            this.textBox21.TabIndex = 2;
            this.textBox21.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox21_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label9.Location = new System.Drawing.Point(38, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 20);
            this.label9.TabIndex = 4;
            this.label9.Text = "/";
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(58, 3);
            this.textBox22.MaxLength = 1;
            this.textBox22.Name = "textBox22";
            this.textBox22.ReadOnly = true;
            this.textBox22.Size = new System.Drawing.Size(29, 20);
            this.textBox22.TabIndex = 3;
            this.textBox22.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox22_KeyPress);
            // 
            // buttonRedact2
            // 
            this.buttonRedact2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonRedact2.Location = new System.Drawing.Point(93, 3);
            this.buttonRedact2.Name = "buttonRedact2";
            this.buttonRedact2.Size = new System.Drawing.Size(77, 23);
            this.buttonRedact2.TabIndex = 6;
            this.buttonRedact2.Text = "редактировать";
            this.buttonRedact2.UseVisualStyleBackColor = true;
            this.buttonRedact2.Click += new System.EventHandler(this.buttonRedact2_Click);
            // 
            // buttonDelSmen2
            // 
            this.buttonDelSmen2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonDelSmen2.Location = new System.Drawing.Point(176, 5);
            this.buttonDelSmen2.Name = "buttonDelSmen2";
            this.buttonDelSmen2.Size = new System.Drawing.Size(75, 23);
            this.buttonDelSmen2.TabIndex = 7;
            this.buttonDelSmen2.Text = "удалить";
            this.buttonDelSmen2.UseVisualStyleBackColor = true;
            this.buttonDelSmen2.Click += new System.EventHandler(this.buttonDelSmen2_Click);
            // 
            // panelSmen1
            // 
            this.panelSmen1.Controls.Add(this.textBox11);
            this.panelSmen1.Controls.Add(this.label2);
            this.panelSmen1.Controls.Add(this.textBox12);
            this.panelSmen1.Controls.Add(this.buttonRedactir1);
            this.panelSmen1.Controls.Add(this.buttonDelSmen1);
            this.panelSmen1.Location = new System.Drawing.Point(9, 16);
            this.panelSmen1.Name = "panelSmen1";
            this.panelSmen1.Size = new System.Drawing.Size(255, 32);
            this.panelSmen1.TabIndex = 29;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(3, 3);
            this.textBox11.MaxLength = 1;
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(29, 20);
            this.textBox11.TabIndex = 2;
            this.textBox11.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox11_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(38, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "/";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(58, 3);
            this.textBox12.MaxLength = 1;
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(29, 20);
            this.textBox12.TabIndex = 3;
            this.textBox12.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox12_KeyPress);
            // 
            // buttonRedactir1
            // 
            this.buttonRedactir1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonRedactir1.Location = new System.Drawing.Point(93, 3);
            this.buttonRedactir1.Name = "buttonRedactir1";
            this.buttonRedactir1.Size = new System.Drawing.Size(77, 23);
            this.buttonRedactir1.TabIndex = 6;
            this.buttonRedactir1.Text = "редактировать";
            this.buttonRedactir1.UseVisualStyleBackColor = true;
            this.buttonRedactir1.Click += new System.EventHandler(this.buttonRedactir1_Click);
            // 
            // buttonDelSmen1
            // 
            this.buttonDelSmen1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonDelSmen1.Location = new System.Drawing.Point(176, 5);
            this.buttonDelSmen1.Name = "buttonDelSmen1";
            this.buttonDelSmen1.Size = new System.Drawing.Size(75, 23);
            this.buttonDelSmen1.TabIndex = 7;
            this.buttonDelSmen1.Text = "удалить";
            this.buttonDelSmen1.UseVisualStyleBackColor = true;
            this.buttonDelSmen1.Click += new System.EventHandler(this.buttonDelSmen1_Click);
            // 
            // buttonAddVarSmen
            // 
            this.buttonAddVarSmen.Location = new System.Drawing.Point(9, 203);
            this.buttonAddVarSmen.Name = "buttonAddVarSmen";
            this.buttonAddVarSmen.Size = new System.Drawing.Size(75, 23);
            this.buttonAddVarSmen.TabIndex = 5;
            this.buttonAddVarSmen.Text = "+ добавить";
            this.buttonAddVarSmen.UseVisualStyleBackColor = true;
            this.buttonAddVarSmen.Click += new System.EventHandler(this.buttonAddVarSmen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Варианты смен:";
            // 
            // buttonAplyVarSmen
            // 
            this.buttonAplyVarSmen.Location = new System.Drawing.Point(283, 203);
            this.buttonAplyVarSmen.Name = "buttonAplyVarSmen";
            this.buttonAplyVarSmen.Size = new System.Drawing.Size(76, 23);
            this.buttonAplyVarSmen.TabIndex = 0;
            this.buttonAplyVarSmen.Text = "Применить";
            this.buttonAplyVarSmen.UseVisualStyleBackColor = true;
            this.buttonAplyVarSmen.Click += new System.EventHandler(this.buttonAplyVarSmen_Click);
            // 
            // panelFactors
            // 
            this.panelFactors.Controls.Add(this.buttonAplyFactors);
            this.panelFactors.Controls.Add(this.buttonClearFactors);
            this.panelFactors.Controls.Add(this.dataGridViewFactors);
            this.panelFactors.Location = new System.Drawing.Point(6, 60);
            this.panelFactors.Name = "panelFactors";
            this.panelFactors.Size = new System.Drawing.Size(431, 245);
            this.panelFactors.TabIndex = 4;
            // 
            // buttonAplyFactors
            // 
            this.buttonAplyFactors.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonAplyFactors.Location = new System.Drawing.Point(346, 216);
            this.buttonAplyFactors.Name = "buttonAplyFactors";
            this.buttonAplyFactors.Size = new System.Drawing.Size(62, 23);
            this.buttonAplyFactors.TabIndex = 1;
            this.buttonAplyFactors.Text = "Применить";
            this.buttonAplyFactors.UseVisualStyleBackColor = true;
            this.buttonAplyFactors.Click += new System.EventHandler(this.buttonAplyFactors_Click);
            // 
            // buttonClearFactors
            // 
            this.buttonClearFactors.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.buttonClearFactors.Location = new System.Drawing.Point(266, 216);
            this.buttonClearFactors.Name = "buttonClearFactors";
            this.buttonClearFactors.Size = new System.Drawing.Size(61, 23);
            this.buttonClearFactors.TabIndex = 0;
            this.buttonClearFactors.Text = "Очистить";
            this.buttonClearFactors.UseVisualStyleBackColor = true;
            // 
            // dataGridViewFactors
            // 
            this.dataGridViewFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactors.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFactors.Name = "dataGridViewFactors";
            this.dataGridViewFactors.Size = new System.Drawing.Size(431, 210);
            this.dataGridViewFactors.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonExport1);
            this.tabPage3.Controls.Add(this.comboBox3);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(510, 342);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Таблицы";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonExport1
            // 
            this.buttonExport1.Location = new System.Drawing.Point(405, 299);
            this.buttonExport1.Name = "buttonExport1";
            this.buttonExport1.Size = new System.Drawing.Size(75, 23);
            this.buttonExport1.TabIndex = 6;
            this.buttonExport1.Text = "export в .xls";
            this.buttonExport1.UseVisualStyleBackColor = true;
            this.buttonExport1.Click += new System.EventHandler(this.buttonExport1_Click);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "график на месяц",
            "Прогноз на 3 месяца по кассовым операциям",
            "Потребность в персонале",
            "Экономический эффект"});
            this.comboBox3.Location = new System.Drawing.Point(287, 25);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(172, 21);
            this.comboBox3.TabIndex = 4;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label4.Location = new System.Drawing.Point(28, 12);
            this.label4.MaximumSize = new System.Drawing.Size(200, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 50);
            this.label4.TabIndex = 1;
            this.label4.Text = "Что образить (с учетом факторов и параметров оптимизации)";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.button6);
            this.tabPage5.Controls.Add(this.panel2);
            this.tabPage5.Controls.Add(this.button5);
            this.tabPage5.Controls.Add(this.buttonReadTekShedule);
            this.tabPage5.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(510, 342);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "гистограммы";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(9, 298);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(100, 38);
            this.button6.TabIndex = 11;
            this.button6.Text = "рассчитать и отобразить ";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxSpeed);
            this.panel2.Controls.Add(this.labelTimeSotr);
            this.panel2.Controls.Add(this.chart1);
            this.panel2.Controls.Add(this.labelData);
            this.panel2.Controls.Add(this.labelTimeClick);
            this.panel2.Controls.Add(this.buttonNext);
            this.panel2.Controls.Add(this.textBoxTimeClick);
            this.panel2.Controls.Add(this.labelTimeTell);
            this.panel2.Controls.Add(this.buttonPrevios);
            this.panel2.Controls.Add(this.textBoxTimeTell);
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(466, 295);
            this.panel2.TabIndex = 10;
            // 
            // textBoxSpeed
            // 
            this.textBoxSpeed.Location = new System.Drawing.Point(132, 274);
            this.textBoxSpeed.Name = "textBoxSpeed";
            this.textBoxSpeed.Size = new System.Drawing.Size(42, 20);
            this.textBoxSpeed.TabIndex = 9;
            // 
            // labelTimeSotr
            // 
            this.labelTimeSotr.AutoSize = true;
            this.labelTimeSotr.Location = new System.Drawing.Point(9, 274);
            this.labelTimeSotr.Name = "labelTimeSotr";
            this.labelTimeSotr.Size = new System.Drawing.Size(116, 13);
            this.labelTimeSotr.TabIndex = 8;
            this.labelTimeSotr.Text = "Скорость cотрудника";
            // 
            // chart1
            // 
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart1.Legends.Add(legend5);
            this.chart1.Location = new System.Drawing.Point(3, 19);
            this.chart1.Name = "chart1";
            series9.ChartArea = "ChartArea1";
            series9.Legend = "Legend1";
            series9.Name = "s1";
            series10.ChartArea = "ChartArea1";
            series10.Legend = "Legend1";
            series10.Name = "s2";
            this.chart1.Series.Add(series9);
            this.chart1.Series.Add(series10);
            this.chart1.Size = new System.Drawing.Size(457, 198);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "гистограмма ";
            // 
            // labelData
            // 
            this.labelData.AutoSize = true;
            this.labelData.Location = new System.Drawing.Point(161, 3);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(66, 13);
            this.labelData.TabIndex = 3;
            this.labelData.Text = "Месяц дата";
            this.labelData.Click += new System.EventHandler(this.label13_Click);
            // 
            // labelTimeClick
            // 
            this.labelTimeClick.AutoSize = true;
            this.labelTimeClick.Location = new System.Drawing.Point(3, 221);
            this.labelTimeClick.Name = "labelTimeClick";
            this.labelTimeClick.Size = new System.Drawing.Size(103, 13);
            this.labelTimeClick.TabIndex = 2;
            this.labelTimeClick.Text = "Время клика (сек )";
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(337, 238);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(76, 35);
            this.buttonNext.TabIndex = 7;
            this.buttonNext.Text = "Следующий день";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // textBoxTimeClick
            // 
            this.textBoxTimeClick.Location = new System.Drawing.Point(132, 221);
            this.textBoxTimeClick.Name = "textBoxTimeClick";
            this.textBoxTimeClick.Size = new System.Drawing.Size(42, 20);
            this.textBoxTimeClick.TabIndex = 1;
            // 
            // labelTimeTell
            // 
            this.labelTimeTell.AutoSize = true;
            this.labelTimeTell.Location = new System.Drawing.Point(3, 249);
            this.labelTimeTell.Name = "labelTimeTell";
            this.labelTimeTell.Size = new System.Drawing.Size(123, 13);
            this.labelTimeTell.TabIndex = 4;
            this.labelTimeTell.Text = "Время разговора (сек)";
            // 
            // buttonPrevios
            // 
            this.buttonPrevios.Location = new System.Drawing.Point(219, 238);
            this.buttonPrevios.Name = "buttonPrevios";
            this.buttonPrevios.Size = new System.Drawing.Size(86, 35);
            this.buttonPrevios.TabIndex = 6;
            this.buttonPrevios.Text = "Предыдущий день";
            this.buttonPrevios.UseVisualStyleBackColor = true;
            this.buttonPrevios.Click += new System.EventHandler(this.buttonPrevios_Click);
            // 
            // textBoxTimeTell
            // 
            this.textBoxTimeTell.Location = new System.Drawing.Point(132, 247);
            this.textBoxTimeTell.Name = "textBoxTimeTell";
            this.textBoxTimeTell.Size = new System.Drawing.Size(42, 20);
            this.textBoxTimeTell.TabIndex = 5;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(375, 298);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 38);
            this.button5.TabIndex = 9;
            this.button5.Text = "Считать операции";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonReadTekShedule
            // 
            this.buttonReadTekShedule.Location = new System.Drawing.Point(283, 298);
            this.buttonReadTekShedule.Name = "buttonReadTekShedule";
            this.buttonReadTekShedule.Size = new System.Drawing.Size(75, 38);
            this.buttonReadTekShedule.TabIndex = 8;
            this.buttonReadTekShedule.Text = "Считать расписание";
            this.buttonReadTekShedule.UseVisualStyleBackColor = true;
            this.buttonReadTekShedule.Click += new System.EventHandler(this.buttonReadTekShedule_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(769, 490);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.labelStatus2);
            this.Controls.Add(this.labelStatus1);
            this.Controls.Add(this.panelSingleShop);
            this.Controls.Add(this.panelMultShops);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.getStatisticByShopsDayHourBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.panelMultShops.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.panelSingleShop.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelCalendar.ResumeLayout(false);
            this.panelTRasp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForTSR)).EndInit();
            this.panelKassOper.ResumeLayout(false);
            this.panelKassOper.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panelUpravlenie.ResumeLayout(false);
            this.panelParamOptim.ResumeLayout(false);
            this.panelParamOptim.PerformLayout();
            this.panelDopusVarSmen.ResumeLayout(false);
            this.panelDopusVarSmen.PerformLayout();
            this.panelSmen5.ResumeLayout(false);
            this.panelSmen5.PerformLayout();
            this.panelSmen3.ResumeLayout(false);
            this.panelSmen3.PerformLayout();
            this.panelSmen4.ResumeLayout(false);
            this.panelSmen4.PerformLayout();
            this.panelSmen2.ResumeLayout(false);
            this.panelSmen2.PerformLayout();
            this.panelSmen1.ResumeLayout(false);
            this.panelSmen1.PerformLayout();
            this.panelFactors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.BindingSource getStatisticByShopsDayHourBindingSource;
        private DataSet1 dataSet1;
        private DataSet1TableAdapters.get_StatisticByShopsDayHourTableAdapter get_StatisticByShopsDayHourTableAdapter;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button_refresh_list_shops;
        private DataSet1TableAdapters.get_StatisticByShopsDayHourTableAdapter get_StatisticByShopsDayHourTableAdapter1;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonTest;
        public System.Windows.Forms.Label labelStatus1;
        public System.Windows.Forms.Label labelStatus2;
        private System.Windows.Forms.Panel panelMultShops;
        private System.Windows.Forms.ListBox listBoxMPartShops;
        private System.Windows.Forms.ListBox listBoxMShops;
        private System.Windows.Forms.Button buttonMdel;
        private System.Windows.Forms.Button buttonMadd;
        private System.Windows.Forms.Panel panelSingleShop;
        private System.Windows.Forms.Button buttonSingleShop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button buttonMultShops;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonRaspisanie;
        private System.Windows.Forms.Button buttonCalendar;
        private System.Windows.Forms.Button buttonKassov;
        private System.Windows.Forms.Panel panelCalendar;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Panel panelTRasp;
        private System.Windows.Forms.Button buttonPTSR;
        private System.Windows.Forms.DataGridView dataGridViewForTSR;
        private System.Windows.Forms.Panel panelKassOper;
        private System.Windows.Forms.Button buttonImportKasOper;
        private System.Windows.Forms.RadioButton radioButtonIzBD;
        private System.Windows.Forms.RadioButton radioButtonIzFile;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panelGruz;
        private System.Windows.Forms.Panel panelUpravlenie;
        private System.Windows.Forms.Button buttonParamOptimiz;
        private System.Windows.Forms.Button buttonVariantsSmen;
        private System.Windows.Forms.Button buttonFactors;
        private System.Windows.Forms.Panel panelParamOptim;
        private System.Windows.Forms.Button buttonApplyParamsOptim;
        private System.Windows.Forms.RadioButton radioButtonObRabTime;
        private System.Windows.Forms.RadioButton radioButtonMinTime;
        private System.Windows.Forms.RadioButton radioButtonMinFondOpl;
        private System.Windows.Forms.Panel panelDopusVarSmen;
        private System.Windows.Forms.Panel panelSmen5;
        private System.Windows.Forms.TextBox textBox51;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox52;
        private System.Windows.Forms.Button buttonRedact5;
        private System.Windows.Forms.Button buttonDelSmen5;
        private System.Windows.Forms.Panel panelSmen3;
        private System.Windows.Forms.TextBox textBox31;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox32;
        private System.Windows.Forms.Button buttonRedact3;
        private System.Windows.Forms.Button buttonDelSmen3;
        private System.Windows.Forms.Panel panelSmen4;
        private System.Windows.Forms.TextBox textBox41;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox42;
        private System.Windows.Forms.Button buttonRedact4;
        private System.Windows.Forms.Button buttonDelSmen4;
        private System.Windows.Forms.Panel panelSmen2;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.Button buttonRedact2;
        private System.Windows.Forms.Button buttonDelSmen2;
        private System.Windows.Forms.Panel panelSmen1;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Button buttonRedactir1;
        private System.Windows.Forms.Button buttonDelSmen1;
        private System.Windows.Forms.Button buttonAddVarSmen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAplyVarSmen;
        private System.Windows.Forms.Panel panelFactors;
        private System.Windows.Forms.Button buttonAplyFactors;
        private System.Windows.Forms.Button buttonClearFactors;
        private System.Windows.Forms.DataGridView dataGridViewFactors;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button buttonExport1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxSpeed;
        private System.Windows.Forms.Label labelTimeSotr;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.Label labelTimeClick;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.TextBox textBoxTimeClick;
        private System.Windows.Forms.Label labelTimeTell;
        private System.Windows.Forms.Button buttonPrevios;
        private System.Windows.Forms.TextBox textBoxTimeTell;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonReadTekShedule;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

