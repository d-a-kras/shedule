using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace shedule
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

           // webBrowser1.Navigate(Path.Combine("file:///", Environment.CurrentDirectory, @"\help.pdf");
            webBrowser1.Navigate("file:///"+Environment.CurrentDirectory+@"\help.pdf");

        }


    }
}
