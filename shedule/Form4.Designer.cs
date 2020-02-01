namespace schedule
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelData = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStart = new System.Windows.Forms.TextBox();
            this.textBoxEnd = new System.Windows.Forms.TextBox();
            this.dataGridViewCalendar = new System.Windows.Forms.DataGridView();
            this.labelM1 = new System.Windows.Forms.Label();
            this.labelM2 = new System.Windows.Forms.Label();
            this.labelM3 = new System.Windows.Forms.Label();
            this.labelM4 = new System.Windows.Forms.Label();
            this.labelM5 = new System.Windows.Forms.Label();
            this.labelM6 = new System.Windows.Forms.Label();
            this.labelM7 = new System.Windows.Forms.Label();
            this.labelM8 = new System.Windows.Forms.Label();
            this.labelM9 = new System.Windows.Forms.Label();
            this.labelM10 = new System.Windows.Forms.Label();
            this.labelM11 = new System.Windows.Forms.Label();
            this.labelM12 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddCalendary = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.bResetSelection = new System.Windows.Forms.Button();
            this.gbCheckedDays = new System.Windows.Forms.GroupBox();
            this.rtbCheckedDays = new System.Windows.Forms.RichTextBox();
            this.bMSelectOff = new System.Windows.Forms.Button();
            this.buttonCalendarNextYear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalendar)).BeginInit();
            this.gbCheckedDays.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "-",
            "все будние ",
            "все выходные",
            "все праздничные дни",
            "все предпраздничные дни",
            "выбрать несколько дат"});
            this.comboBox1.Location = new System.Drawing.Point(12, 425);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(139, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelData
            // 
            this.labelData.AutoSize = true;
            this.labelData.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.labelData.Location = new System.Drawing.Point(798, 9);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(166, 26);
            this.labelData.TabIndex = 5;
            this.labelData.Text = "Выберите дату";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(799, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Начало работы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(787, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Окончание работы";
            // 
            // textBoxStart
            // 
            this.textBoxStart.Location = new System.Drawing.Point(823, 69);
            this.textBoxStart.Name = "textBoxStart";
            this.textBoxStart.Size = new System.Drawing.Size(100, 20);
            this.textBoxStart.TabIndex = 8;
            this.textBoxStart.TextChanged += new System.EventHandler(this.textBoxStart_TextChanged);
            this.textBoxStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxStart_KeyPress);
            // 
            // textBoxEnd
            // 
            this.textBoxEnd.Location = new System.Drawing.Point(823, 139);
            this.textBoxEnd.Name = "textBoxEnd";
            this.textBoxEnd.Size = new System.Drawing.Size(100, 20);
            this.textBoxEnd.TabIndex = 9;
            this.textBoxEnd.TextChanged += new System.EventHandler(this.textBoxEnd_TextChanged);
            this.textBoxEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form4_KeyDown);
            this.textBoxEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxEnd_KeyPress);
            // 
            // dataGridViewCalendar
            // 
            this.dataGridViewCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCalendar.Location = new System.Drawing.Point(211, 423);
            this.dataGridViewCalendar.Name = "dataGridViewCalendar";
            this.dataGridViewCalendar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewCalendar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewCalendar.Size = new System.Drawing.Size(553, 223);
            this.dataGridViewCalendar.TabIndex = 10;
            this.dataGridViewCalendar.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewCalendar_CellBeginEdit);
            this.dataGridViewCalendar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCalendar_CellContentClick);
            this.dataGridViewCalendar.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCalendar_CellEndEdit);
            this.dataGridViewCalendar.CellErrorTextChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCalendar_CellErrorTextChanged);
            this.dataGridViewCalendar.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewCalendar_DataError);
            // 
            // labelM1
            // 
            this.labelM1.AutoSize = true;
            this.labelM1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM1.Location = new System.Drawing.Point(69, 0);
            this.labelM1.Name = "labelM1";
            this.labelM1.Size = new System.Drawing.Size(72, 20);
            this.labelM1.TabIndex = 11;
            this.labelM1.Text = "Январь";
            // 
            // labelM2
            // 
            this.labelM2.AutoSize = true;
            this.labelM2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM2.Location = new System.Drawing.Point(251, 0);
            this.labelM2.Name = "labelM2";
            this.labelM2.Size = new System.Drawing.Size(85, 20);
            this.labelM2.TabIndex = 12;
            this.labelM2.Text = "Февраль";
            // 
            // labelM3
            // 
            this.labelM3.AutoSize = true;
            this.labelM3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM3.Location = new System.Drawing.Point(453, 0);
            this.labelM3.Name = "labelM3";
            this.labelM3.Size = new System.Drawing.Size(53, 20);
            this.labelM3.TabIndex = 13;
            this.labelM3.Text = "Март";
            // 
            // labelM4
            // 
            this.labelM4.AutoSize = true;
            this.labelM4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM4.Location = new System.Drawing.Point(641, 0);
            this.labelM4.Name = "labelM4";
            this.labelM4.Size = new System.Drawing.Size(71, 20);
            this.labelM4.TabIndex = 14;
            this.labelM4.Text = "Апрель";
            // 
            // labelM5
            // 
            this.labelM5.AutoSize = true;
            this.labelM5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM5.Location = new System.Drawing.Point(77, 137);
            this.labelM5.Name = "labelM5";
            this.labelM5.Size = new System.Drawing.Size(43, 20);
            this.labelM5.TabIndex = 15;
            this.labelM5.Text = "Май";
            // 
            // labelM6
            // 
            this.labelM6.AutoSize = true;
            this.labelM6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM6.Location = new System.Drawing.Point(266, 137);
            this.labelM6.Name = "labelM6";
            this.labelM6.Size = new System.Drawing.Size(53, 20);
            this.labelM6.TabIndex = 16;
            this.labelM6.Text = "Июнь";
            // 
            // labelM7
            // 
            this.labelM7.AutoSize = true;
            this.labelM7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM7.Location = new System.Drawing.Point(444, 137);
            this.labelM7.Name = "labelM7";
            this.labelM7.Size = new System.Drawing.Size(53, 20);
            this.labelM7.TabIndex = 17;
            this.labelM7.Text = "Июль";
            // 
            // labelM8
            // 
            this.labelM8.AutoSize = true;
            this.labelM8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM8.Location = new System.Drawing.Point(641, 137);
            this.labelM8.Name = "labelM8";
            this.labelM8.Size = new System.Drawing.Size(66, 20);
            this.labelM8.TabIndex = 18;
            this.labelM8.Text = "Август";
            // 
            // labelM9
            // 
            this.labelM9.AutoSize = true;
            this.labelM9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM9.Location = new System.Drawing.Point(60, 277);
            this.labelM9.Name = "labelM9";
            this.labelM9.Size = new System.Drawing.Size(91, 20);
            this.labelM9.TabIndex = 19;
            this.labelM9.Text = "Сентябрь";
            // 
            // labelM10
            // 
            this.labelM10.AutoSize = true;
            this.labelM10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM10.Location = new System.Drawing.Point(251, 277);
            this.labelM10.Name = "labelM10";
            this.labelM10.Size = new System.Drawing.Size(81, 20);
            this.labelM10.TabIndex = 20;
            this.labelM10.Text = "Октябрь";
            // 
            // labelM11
            // 
            this.labelM11.AutoSize = true;
            this.labelM11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM11.Location = new System.Drawing.Point(444, 277);
            this.labelM11.Name = "labelM11";
            this.labelM11.Size = new System.Drawing.Size(72, 20);
            this.labelM11.TabIndex = 21;
            this.labelM11.Text = "Ноябрь";
            // 
            // labelM12
            // 
            this.labelM12.AutoSize = true;
            this.labelM12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelM12.Location = new System.Drawing.Point(631, 277);
            this.labelM12.Name = "labelM12";
            this.labelM12.Size = new System.Drawing.Size(81, 20);
            this.labelM12.TabIndex = 22;
            this.labelM12.Text = "Декабрь";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(182, 120);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(200, 23);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(183, 120);
            this.tableLayoutPanel2.TabIndex = 24;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 7;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel3.Location = new System.Drawing.Point(389, 23);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(185, 120);
            this.tableLayoutPanel3.TabIndex = 25;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel4.Location = new System.Drawing.Point(581, 23);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 6;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(185, 120);
            this.tableLayoutPanel4.TabIndex = 26;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 7;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.Location = new System.Drawing.Point(12, 160);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(182, 120);
            this.tableLayoutPanel5.TabIndex = 27;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 7;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel6.Location = new System.Drawing.Point(200, 160);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 6;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(183, 120);
            this.tableLayoutPanel6.TabIndex = 28;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 7;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel7.Location = new System.Drawing.Point(389, 160);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 6;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(185, 120);
            this.tableLayoutPanel7.TabIndex = 29;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 7;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel8.Location = new System.Drawing.Point(581, 160);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 6;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(183, 120);
            this.tableLayoutPanel8.TabIndex = 30;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 7;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel9.Location = new System.Drawing.Point(12, 300);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 6;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(182, 120);
            this.tableLayoutPanel9.TabIndex = 31;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 7;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel10.Location = new System.Drawing.Point(200, 300);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 6;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(183, 120);
            this.tableLayoutPanel10.TabIndex = 32;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 7;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel11.Location = new System.Drawing.Point(389, 300);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 6;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(185, 120);
            this.tableLayoutPanel11.TabIndex = 33;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 7;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel12.Location = new System.Drawing.Point(581, 300);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 6;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(183, 120);
            this.tableLayoutPanel12.TabIndex = 34;
            // 
            // buttonAddCalendary
            // 
            this.buttonAddCalendary.Location = new System.Drawing.Point(890, 616);
            this.buttonAddCalendary.Name = "buttonAddCalendary";
            this.buttonAddCalendary.Size = new System.Drawing.Size(75, 23);
            this.buttonAddCalendary.TabIndex = 35;
            this.buttonAddCalendary.Text = "Сохранить";
            this.buttonAddCalendary.UseVisualStyleBackColor = true;
            this.buttonAddCalendary.Click += new System.EventHandler(this.buttonAddCalendary_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(823, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "Редактировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bResetSelection
            // 
            this.bResetSelection.Location = new System.Drawing.Point(12, 452);
            this.bResetSelection.Name = "bResetSelection";
            this.bResetSelection.Size = new System.Drawing.Size(139, 23);
            this.bResetSelection.TabIndex = 37;
            this.bResetSelection.Text = "Сброс выделения";
            this.bResetSelection.UseVisualStyleBackColor = true;
            this.bResetSelection.Visible = false;
            this.bResetSelection.Click += new System.EventHandler(this.button2_Click);
            // 
            // gbCheckedDays
            // 
            this.gbCheckedDays.Controls.Add(this.rtbCheckedDays);
            this.gbCheckedDays.Location = new System.Drawing.Point(791, 250);
            this.gbCheckedDays.Name = "gbCheckedDays";
            this.gbCheckedDays.Size = new System.Drawing.Size(168, 318);
            this.gbCheckedDays.TabIndex = 38;
            this.gbCheckedDays.TabStop = false;
            this.gbCheckedDays.Text = "Выбранные дни";
            this.gbCheckedDays.Visible = false;
            // 
            // rtbCheckedDays
            // 
            this.rtbCheckedDays.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCheckedDays.Location = new System.Drawing.Point(7, 20);
            this.rtbCheckedDays.Name = "rtbCheckedDays";
            this.rtbCheckedDays.ReadOnly = true;
            this.rtbCheckedDays.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbCheckedDays.Size = new System.Drawing.Size(155, 292);
            this.rtbCheckedDays.TabIndex = 0;
            this.rtbCheckedDays.Text = "";
            this.rtbCheckedDays.TextChanged += new System.EventHandler(this.rtbCheckedDays_TextChanged);
            // 
            // bMSelectOff
            // 
            this.bMSelectOff.Location = new System.Drawing.Point(13, 481);
            this.bMSelectOff.Name = "bMSelectOff";
            this.bMSelectOff.Size = new System.Drawing.Size(138, 38);
            this.bMSelectOff.TabIndex = 39;
            this.bMSelectOff.Text = "Выключить режим выделения";
            this.bMSelectOff.UseVisualStyleBackColor = true;
            this.bMSelectOff.Visible = false;
            this.bMSelectOff.Click += new System.EventHandler(this.bMSelectOff_Click);
            // 
            // buttonCalendarNextYear
            // 
            this.buttonCalendarNextYear.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.buttonCalendarNextYear.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.buttonCalendarNextYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCalendarNextYear.Location = new System.Drawing.Point(791, 610);
            this.buttonCalendarNextYear.Name = "buttonCalendarNextYear";
            this.buttonCalendarNextYear.Size = new System.Drawing.Size(64, 36);
            this.buttonCalendarNextYear.TabIndex = 40;
            this.buttonCalendarNextYear.Text = "2018";
            this.buttonCalendarNextYear.UseVisualStyleBackColor = false;
            this.buttonCalendarNextYear.Visible = false;
            this.buttonCalendarNextYear.Click += new System.EventHandler(this.buttonCalendarNextYear_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 651);
            this.Controls.Add(this.buttonCalendarNextYear);
            this.Controls.Add(this.bMSelectOff);
            this.Controls.Add(this.gbCheckedDays);
            this.Controls.Add(this.bResetSelection);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonAddCalendary);
            this.Controls.Add(this.tableLayoutPanel12);
            this.Controls.Add(this.tableLayoutPanel11);
            this.Controls.Add(this.tableLayoutPanel10);
            this.Controls.Add(this.tableLayoutPanel9);
            this.Controls.Add(this.tableLayoutPanel8);
            this.Controls.Add(this.tableLayoutPanel7);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelM12);
            this.Controls.Add(this.labelM11);
            this.Controls.Add(this.labelM10);
            this.Controls.Add(this.labelM9);
            this.Controls.Add(this.labelM8);
            this.Controls.Add(this.labelM7);
            this.Controls.Add(this.labelM6);
            this.Controls.Add(this.labelM5);
            this.Controls.Add(this.labelM4);
            this.Controls.Add(this.labelM3);
            this.Controls.Add(this.labelM2);
            this.Controls.Add(this.labelM1);
            this.Controls.Add(this.dataGridViewCalendar);
            this.Controls.Add(this.textBoxEnd);
            this.Controls.Add(this.textBoxStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelData);
            this.Controls.Add(this.comboBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form4";
            this.Text = "Производственный календарь";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form4_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalendar)).EndInit();
            this.gbCheckedDays.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxStart;
        private System.Windows.Forms.TextBox textBoxEnd;
        private System.Windows.Forms.DataGridView dataGridViewCalendar;
        private System.Windows.Forms.Label labelM1;
        private System.Windows.Forms.Label labelM2;
        private System.Windows.Forms.Label labelM3;
        private System.Windows.Forms.Label labelM4;
        private System.Windows.Forms.Label labelM5;
        private System.Windows.Forms.Label labelM6;
        private System.Windows.Forms.Label labelM7;
        private System.Windows.Forms.Label labelM8;
        private System.Windows.Forms.Label labelM9;
        private System.Windows.Forms.Label labelM10;
        private System.Windows.Forms.Label labelM11;
        private System.Windows.Forms.Label labelM12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.Button buttonAddCalendary;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bResetSelection;
        private System.Windows.Forms.GroupBox gbCheckedDays;
        private System.Windows.Forms.Button bMSelectOff;
        private System.Windows.Forms.RichTextBox rtbCheckedDays;
        private System.Windows.Forms.Button buttonCalendarNextYear;
    }
}