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
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        /// <summary>Gets or sets the surname.</summary>
        /// <value>The surname.</value>
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        /// <summary>Gets or sets the pesel.</summary>
        /// <value>The pesel.</value>
        public long PESEL
        {
            get { return pesel; }
            set
            {
                pesel = value;
                OnPropertyChanged("PESEL");
            }
        }
        /// <summary>Gets or sets the login.</summary>
        /// <value>The login.</value>
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        /// <summary>Gets or sets the job title.</summary>
        /// <value>The job title.</value>
        public string JobTitle
        {
            get { return jobTitle; }
            set
            {
                jobTitle = value;
                OnPropertyChanged("JobTitle");
            }
        }
        /// <summary>Gets or sets a value indicating whether this instance is admin.</summary>
        /// <value>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.</value>
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
