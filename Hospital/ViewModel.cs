using Hospital_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_View
{
    
    public class ViewModel : BaseViewModel, INotifyPropertyChanged
    {       
        private Duty duty;
        private Employee employee;
        private Hospital hospital;
        public bool IsLoggedUserAdmin { get; set; } = true;
        public bool IsEditModeOff { get; set; }

        private ObservableCollection<Employee> _employees;         
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                _employees = value;
                OnPropertyChanged("Employee");
            }
        }

        //private ObservableCollection<string> fullNames;
        //public ObservableCollection<Employee> FullNames
        //{
        //    get { return fullNames; }
        //    set
        //    {
        //        fullNames = value;
        //        OnPropertyChanged("FullNames");
        //    }
        //}
        

        private void LoadDataToDisplay()
        {
            Employees = new ObservableCollection<Employee>(_Hospital.Staff);
            
        }

        #region Properties
        public Employee _Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("_Employee");
            }
        }       
        
        public Hospital _Hospital
        {
            get
            {
                return hospital;
            }
            set
            {
                hospital = value;
                OnPropertyChanged("_Hospital");
            }
        }

        public Duty _Duty
        {
            get
            {
                return duty;
            }
            set
            {
                duty = value;
                OnPropertyChanged("_Duty");
            }
        }
        #endregion

        public ViewModel()
        {            
            //duty = new Duty();
            employee = new Employee();
            hospital = new Hospital();
            LoadDataToDisplay();
            
        }

        public bool VerifyPasswordAndLogin(string login, string password)
        {

            return true;
            //if(this._Hospital.Staff.Where(x => x.Login == login)
            //  .Where(x => x.Password == password).Any())
            //{
            //    var _empl = this._Hospital.Staff.Where(x => x.Login == login)
            //  .Where(x => x.Password == password).FirstOrDefault();

            //    this.IsLoggedUserAdmin = _empl.IsAdmin ? true : false;
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}                
        }

        public void SerializeAllData()
        {
            this._Hospital.Staff = this.Employees.ToList();
            this._Hospital.SerializeData();
        }

        

        //public Employee AddNewEmployee(string _name, string _surname, string _jobTitle, long _pesel, string _specialization, int _licNumber,
        //    string _login, string _password, bool _isAdmin)
        //{
        //    switch (_jobTitle)
        //    {
        //        case "lekarz":
        //            Employee physician = new Physician(_name, _surname, _pesel, _login,
        //                _password, _jobTitle, _isAdmin, _specialization, _licNumber);
        //            _Hospital.Staff.Add(physician);
        //            return physician;

        //        case "pielęgniarka":
        //            Employee nurse = new Nurse(_name, _surname, _pesel, _login, _password, _jobTitle, _isAdmin);
        //            _Hospital.Staff.Add(nurse);
        //            return nurse;

        //        default:
        //            Employee employee = new Employee(_name, _surname, _pesel, _login, _password, _jobTitle, _isAdmin);
        //            _Hospital.Staff.Add(employee);
        //            return employee;
        //    }
        //}

       
        

    }



}


