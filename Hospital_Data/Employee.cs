using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Data
{
    /// <summary>
    /// <para>Class marked for serialization</para>
    /// <para>Generic Employee class, inherited by the Nurse class and Physician class</para>
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    public class Employee : INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private long pesel;
        private string login;
        private string password;
        private string jobTitle;
        private bool isAdmin;         

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

        /// <summary>Initializes a new instance of the <see cref="Employee"/> class.</summary>
        public Employee()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="Employee"/> class.</summary>
        /// <param name="_employee">The employee.</param>
        public Employee(Employee _employee)
        {
            this.name = _employee.Name;
            this.surname = _employee.Surname;
            this.pesel = _employee.PESEL;
            this.login = _employee.Login;
            this.password = _employee.Password;
            this.jobTitle = _employee.JobTitle;
            this.isAdmin = _employee.IsAdmin;
        }

        /// <summary>Occurs when a property value changes.</summary>
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Called when [property changed].</summary>
        /// <param name="_property">The property.</param>
        protected void OnPropertyChanged([CallerMemberName] string _property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_property));
        }
    }
}
