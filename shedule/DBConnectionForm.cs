﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using shedule.Models;

namespace shedule
{
    public partial class DBConnectionForm : Form
    {
        public Connection connection;
        public DBConnectionForm(Connection con)
        {
            
            InitializeComponent();
            this.connection = con;
            this.tbServerAddress.Text = con.server;
            this.tbServerLogin.Text = con.login;
            this.tbServerPassword.Text = con.password;
            this.tbSheme.Text = con.sheme;
            this.tbNameDB.Text = con.nameDB;
        }

        public string GetLogin() {
            return this.tbServerLogin.Text;
        }
        public string GetPassword()
        {
            return this.tbServerPassword.Text;
        }
        public string GetAddress()
        {
            return this.tbServerAddress.Text;
        }
        public string GetSheme()
        {
            return this.tbSheme.Text;
        }
        public string GetNameDB()
        {
            return this.tbNameDB.Text;
        }

        public void SetLogin(String value)
        {
            this.tbServerLogin.Text= value;
        }
        public void SetPassword(String value)
        {
            this.tbServerPassword.Text = value;
        }
        public void SetAddress(String value)
        {
            this.tbServerAddress.Text = value;
        }

        public void SetSheme(String value)
        {
            this.tbSheme.Text = value;
        }

        private void DBConnectionForm_Load(object sender, EventArgs e)
        {
           
        }

        private void bSaveSettings_Click(object sender, EventArgs e)
        {
            
        }

        private void DBConnectionForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
