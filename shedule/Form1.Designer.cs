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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.кодМагазинаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.датаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.времяDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.деньНеделиDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.количествоЧековDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.количествоСканированийDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.количествоТовараDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.getStatisticByShopsDayHourBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new shedule.DataSet1();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.get_StatisticByShopsDayHourTableAdapter = new shedule.DataSet1TableAdapters.get_StatisticByShopsDayHourTableAdapter();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getStatisticByShopsDayHourBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.кодМагазинаDataGridViewTextBoxColumn,
            this.датаDataGridViewTextBoxColumn,
            this.времяDataGridViewTextBoxColumn,
            this.деньНеделиDataGridViewTextBoxColumn,
            this.количествоЧековDataGridViewTextBoxColumn,
            this.количествоСканированийDataGridViewTextBoxColumn,
            this.количествоТовараDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.getStatisticByShopsDayHourBindingSource;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(34, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // кодМагазинаDataGridViewTextBoxColumn
            // 
            this.кодМагазинаDataGridViewTextBoxColumn.DataPropertyName = "КодМагазина";
            this.кодМагазинаDataGridViewTextBoxColumn.HeaderText = "КодМагазина";
            this.кодМагазинаDataGridViewTextBoxColumn.Name = "кодМагазинаDataGridViewTextBoxColumn";
            // 
            // датаDataGridViewTextBoxColumn
            // 
            this.датаDataGridViewTextBoxColumn.DataPropertyName = "Дата";
            this.датаDataGridViewTextBoxColumn.HeaderText = "Дата";
            this.датаDataGridViewTextBoxColumn.Name = "датаDataGridViewTextBoxColumn";
            // 
            // времяDataGridViewTextBoxColumn
            // 
            this.времяDataGridViewTextBoxColumn.DataPropertyName = "Время";
            this.времяDataGridViewTextBoxColumn.HeaderText = "Время";
            this.времяDataGridViewTextBoxColumn.Name = "времяDataGridViewTextBoxColumn";
            // 
            // деньНеделиDataGridViewTextBoxColumn
            // 
            this.деньНеделиDataGridViewTextBoxColumn.DataPropertyName = "ДеньНедели";
            this.деньНеделиDataGridViewTextBoxColumn.HeaderText = "ДеньНедели";
            this.деньНеделиDataGridViewTextBoxColumn.Name = "деньНеделиDataGridViewTextBoxColumn";
            // 
            // количествоЧековDataGridViewTextBoxColumn
            // 
            this.количествоЧековDataGridViewTextBoxColumn.DataPropertyName = "КоличествоЧеков";
            this.количествоЧековDataGridViewTextBoxColumn.HeaderText = "КоличествоЧеков";
            this.количествоЧековDataGridViewTextBoxColumn.Name = "количествоЧековDataGridViewTextBoxColumn";
            // 
            // количествоСканированийDataGridViewTextBoxColumn
            // 
            this.количествоСканированийDataGridViewTextBoxColumn.DataPropertyName = "КоличествоСканирований";
            this.количествоСканированийDataGridViewTextBoxColumn.HeaderText = "КоличествоСканирований";
            this.количествоСканированийDataGridViewTextBoxColumn.Name = "количествоСканированийDataGridViewTextBoxColumn";
            // 
            // количествоТовараDataGridViewTextBoxColumn
            // 
            this.количествоТовараDataGridViewTextBoxColumn.DataPropertyName = "КоличествоТовара";
            this.количествоТовараDataGridViewTextBoxColumn.HeaderText = "КоличествоТовара";
            this.количествоТовараDataGridViewTextBoxColumn.Name = "количествоТовараDataGridViewTextBoxColumn";
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
            this.button1.Location = new System.Drawing.Point(100, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(435, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(344, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(254, 150);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(298, 256);
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
            this.button4.Location = new System.Drawing.Point(279, 191);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 356);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getStatisticByShopsDayHourBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn кодМагазинаDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn датаDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn времяDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn деньНеделиDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn количествоЧековDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn количествоСканированийDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn количествоТовараDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource getStatisticByShopsDayHourBindingSource;
        private DataSet1 dataSet1;
        private DataSet1TableAdapters.get_StatisticByShopsDayHourTableAdapter get_StatisticByShopsDayHourTableAdapter;
        private System.Windows.Forms.Button button4;
    }
}

