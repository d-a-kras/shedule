namespace shedule
{
    partial class fSettings
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewDB = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.bSaveSettings = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonDBEdit = new System.Windows.Forms.Button();
            this.buttonDBAdd = new System.Windows.Forms.Button();
            this.buttonDBDelete = new System.Windows.Forms.Button();
            this.buttonActivateConnection = new System.Windows.Forms.Button();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Server = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameDB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sheme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonDefaultForShop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelDBdefault = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelDBdefault);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dataGridViewDB);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.groupBox1.Location = new System.Drawing.Point(5, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(655, 232);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки подключения к базе данных";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dataGridViewDB
            // 
            this.dataGridViewDB.AllowUserToAddRows = false;
            this.dataGridViewDB.AllowUserToDeleteRows = false;
            this.dataGridViewDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.IsActive,
            this.Server,
            this.NameDB,
            this.Login,
            this.Password,
            this.Sheme});
            this.dataGridViewDB.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewDB.Name = "dataGridViewDB";
            this.dataGridViewDB.ReadOnly = true;
            this.dataGridViewDB.Size = new System.Drawing.Size(642, 153);
            this.dataGridViewDB.TabIndex = 9;
            this.dataGridViewDB.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(531, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 36);
            this.button2.TabIndex = 8;
            this.button2.Text = "Изменить путь к папке сохранения";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 7;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // bSaveSettings
            // 
            this.bSaveSettings.Location = new System.Drawing.Point(666, 250);
            this.bSaveSettings.Name = "bSaveSettings";
            this.bSaveSettings.Size = new System.Drawing.Size(75, 23);
            this.bSaveSettings.TabIndex = 1;
            this.bSaveSettings.Text = "Сохранить";
            this.bSaveSettings.UseVisualStyleBackColor = true;
            this.bSaveSettings.Click += new System.EventHandler(this.bSaveSettings_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 250);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "праздники";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(93, 250);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonDBEdit
            // 
            this.buttonDBEdit.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonDBEdit.Location = new System.Drawing.Point(666, 127);
            this.buttonDBEdit.Name = "buttonDBEdit";
            this.buttonDBEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonDBEdit.TabIndex = 4;
            this.buttonDBEdit.Text = "Изменить";
            this.buttonDBEdit.UseVisualStyleBackColor = true;
            this.buttonDBEdit.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonDBAdd
            // 
            this.buttonDBAdd.Location = new System.Drawing.Point(666, 156);
            this.buttonDBAdd.Name = "buttonDBAdd";
            this.buttonDBAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonDBAdd.TabIndex = 5;
            this.buttonDBAdd.Text = "Добавить";
            this.buttonDBAdd.UseVisualStyleBackColor = true;
            this.buttonDBAdd.Click += new System.EventHandler(this.buttonDBAdd_Click);
            // 
            // buttonDBDelete
            // 
            this.buttonDBDelete.Location = new System.Drawing.Point(666, 185);
            this.buttonDBDelete.Name = "buttonDBDelete";
            this.buttonDBDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDBDelete.TabIndex = 6;
            this.buttonDBDelete.Text = "Удалить";
            this.buttonDBDelete.UseVisualStyleBackColor = true;
            this.buttonDBDelete.Click += new System.EventHandler(this.buttonDBDelete_Click);
            // 
            // buttonActivateConnection
            // 
            this.buttonActivateConnection.Location = new System.Drawing.Point(666, 12);
            this.buttonActivateConnection.Name = "buttonActivateConnection";
            this.buttonActivateConnection.Size = new System.Drawing.Size(75, 42);
            this.buttonActivateConnection.TabIndex = 7;
            this.buttonActivateConnection.Text = "Сделать основным";
            this.buttonActivateConnection.UseVisualStyleBackColor = true;
            this.buttonActivateConnection.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(shedule.Form1);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // IsActive
            // 
            this.IsActive.HeaderText = "Основное соединение";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Server
            // 
            this.Server.HeaderText = "Сервер";
            this.Server.Name = "Server";
            this.Server.ReadOnly = true;
            // 
            // NameDB
            // 
            this.NameDB.HeaderText = "Имя БД";
            this.NameDB.Name = "NameDB";
            this.NameDB.ReadOnly = true;
            // 
            // Login
            // 
            this.Login.HeaderText = "Пользователь";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            // 
            // Password
            // 
            this.Password.HeaderText = "Пароль";
            this.Password.Name = "Password";
            this.Password.ReadOnly = true;
            // 
            // Sheme
            // 
            this.Sheme.HeaderText = "Схема";
            this.Sheme.Name = "Sheme";
            this.Sheme.ReadOnly = true;
            // 
            // buttonDefaultForShop
            // 
            this.buttonDefaultForShop.Location = new System.Drawing.Point(666, 60);
            this.buttonDefaultForShop.Name = "buttonDefaultForShop";
            this.buttonDefaultForShop.Size = new System.Drawing.Size(75, 61);
            this.buttonDefaultForShop.TabIndex = 8;
            this.buttonDefaultForShop.Text = "Сделать по умолчанию для магазина";
            this.buttonDefaultForShop.UseVisualStyleBackColor = true;
            this.buttonDefaultForShop.Click += new System.EventHandler(this.button4_Click_2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Текущее соединение для данного магазина:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Путь до папки сохранения по умолчанию:";
            // 
            // labelDBdefault
            // 
            this.labelDBdefault.AutoSize = true;
            this.labelDBdefault.Location = new System.Drawing.Point(248, 178);
            this.labelDBdefault.Name = "labelDBdefault";
            this.labelDBdefault.Size = new System.Drawing.Size(0, 13);
            this.labelDBdefault.TabIndex = 13;
            // 
            // fSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 275);
            this.Controls.Add(this.buttonDefaultForShop);
            this.Controls.Add(this.buttonActivateConnection);
            this.Controls.Add(this.buttonDBDelete);
            this.Controls.Add(this.buttonDBAdd);
            this.Controls.Add(this.buttonDBEdit);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bSaveSettings);
            this.Controls.Add(this.groupBox1);
            this.Name = "fSettings";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.fSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bSaveSettings;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonDBEdit;
        private System.Windows.Forms.Button buttonDBAdd;
        private System.Windows.Forms.Button buttonDBDelete;
        private System.Windows.Forms.DataGridView dataGridViewDB;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.Button buttonActivateConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn Server;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sheme;
        private System.Windows.Forms.Button buttonDefaultForShop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelDBdefault;
    }
}