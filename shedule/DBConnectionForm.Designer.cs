namespace shedule
{
    partial class DBConnectionForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbSheme = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbServerPassword = new System.Windows.Forms.TextBox();
            this.tbServerLogin = new System.Windows.Forms.TextBox();
            this.tbServerAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bSaveSettings = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tbNameDB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbNameDB);
            this.groupBox1.Controls.Add(this.tbSheme);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbServerPassword);
            this.groupBox1.Controls.Add(this.tbServerLogin);
            this.groupBox1.Controls.Add(this.tbServerAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(5, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 172);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки подключения к базе данных";
            // 
            // tbSheme
            // 
            this.tbSheme.Location = new System.Drawing.Point(72, 144);
            this.tbSheme.Name = "tbSheme";
            this.tbSheme.Size = new System.Drawing.Size(199, 20);
            this.tbSheme.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Схема";
            // 
            // tbServerPassword
            // 
            this.tbServerPassword.Location = new System.Drawing.Point(72, 91);
            this.tbServerPassword.Name = "tbServerPassword";
            this.tbServerPassword.PasswordChar = '*';
            this.tbServerPassword.Size = new System.Drawing.Size(199, 20);
            this.tbServerPassword.TabIndex = 6;
            // 
            // tbServerLogin
            // 
            this.tbServerLogin.Location = new System.Drawing.Point(72, 65);
            this.tbServerLogin.Name = "tbServerLogin";
            this.tbServerLogin.Size = new System.Drawing.Size(199, 20);
            this.tbServerLogin.TabIndex = 5;
            // 
            // tbServerAddress
            // 
            this.tbServerAddress.Location = new System.Drawing.Point(101, 39);
            this.tbServerAddress.Name = "tbServerAddress";
            this.tbServerAddress.Size = new System.Drawing.Size(170, 20);
            this.tbServerAddress.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Адрес сервера:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Пароль:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Логин:";
            // 
            // bSaveSettings
            // 
            this.bSaveSettings.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bSaveSettings.Location = new System.Drawing.Point(201, 190);
            this.bSaveSettings.Name = "bSaveSettings";
            this.bSaveSettings.Size = new System.Drawing.Size(75, 23);
            this.bSaveSettings.TabIndex = 1;
            this.bSaveSettings.Text = "Сохранить";
            this.bSaveSettings.UseVisualStyleBackColor = true;
            this.bSaveSettings.Click += new System.EventHandler(this.bSaveSettings_Click);
            // 
            // tbNameDB
            // 
            this.tbNameDB.Location = new System.Drawing.Point(72, 118);
            this.tbNameDB.Name = "tbNameDB";
            this.tbNameDB.Size = new System.Drawing.Size(199, 20);
            this.tbNameDB.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Имя ДБ";
            // 
            // DBConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 225);
            this.Controls.Add(this.bSaveSettings);
            this.Controls.Add(this.groupBox1);
            this.Name = "DBConnectionForm";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.DBConnectionForm_Load_1);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bSaveSettings;
        private System.Windows.Forms.TextBox tbServerPassword;
        private System.Windows.Forms.TextBox tbServerLogin;
        private System.Windows.Forms.TextBox tbServerAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox tbSheme;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbNameDB;
    }
}