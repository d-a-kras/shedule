using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Models
{


    public class Connection : INotifyPropertyChanged
    {
        private string Server;
        private string Login;
        private string Password;


        public int Id { get; set; }

        public string server
        {
            get { return Server; }
            set
            {
                Server = value;
                OnPropertyChanged("Server");
            }
        }
        public string login
        {
            get { return Login; }
            set
            {
                Login = value;
                OnPropertyChanged("Login");
            }
        }
        public string password
        {
            get { return Password; }
            set
            {
                Password = value;
                OnPropertyChanged("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
