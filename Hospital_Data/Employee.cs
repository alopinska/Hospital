using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Data
{
    [Serializable]
    public class Employee : INotifyPropertyChanged
    {
        private string name { get; set; }
        private string surname { get; set; }
        private long pesel { get; set; }
        private string login { get; set; }
        private string password { get; set; }
        private string jobTitle { get; set; }
        private bool isAdmin { get; set; }

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }        
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public long PESEL
        {
            get { return pesel; }
            set
            {
                pesel = value;
                OnPropertyChanged("PESEL");
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public string JobTitle
        {
            get { return jobTitle; }
            set
            {
                jobTitle = value;
                OnPropertyChanged("JobTitle");
            }
        }
        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                isAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }
#endregion

        public Employee()
        {

        }

        public Employee(string _name, string _surname, long _pesel, string _login, string _password, string _jobtitle, bool _isAdmin)
        {
            this.Name = _name;
            this.Surname = _surname;
            this.PESEL = _pesel;
            this.Login = _login;
            this.Password = _password;
            this.JobTitle = _jobtitle;
            this.IsAdmin = _isAdmin;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string _property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_property));
        }
    }
}
