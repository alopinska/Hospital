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

    public class ViewModel : INotifyPropertyChanged
    {

        //private Nurse nurse;
        //private Physician physician;
        //private Duty duty;

        private Employee employee;
        private Hospital hospital;

        //private ObservableCollection<Employee> _employees;
        //public ObservableCollection<Employee> Employees
        //{
        //    get
        //    {
        //        return _employees;
        //    }
        //    set
        //    {
        //        _employees = value;
        //        OnPropertyChanged("Employee");
        //    }
        //}

        private void LoadDataToCollection()
        {

            //foreach (var item in _Hospital.Staff)
            //{
            //    Employees.Add((Employee)item);
            //}
        }




        #region Model properties
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

        //public Nurse _Nurse
        //{
        //    get
        //    {
        //        return nurse;
        //    }
        //    set
        //    {
        //        nurse = value;
        //        OnPropertyChanged("_Nurse");
        //    }
        //}

        //public Physician _Physician
        //{
        //    get
        //    {
        //        return physician;
        //    }
        //    set
        //    {
        //        physician = value;
        //        OnPropertyChanged("_Physician");
        //    }
        //}
        
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

        //public Duty _Duty
        //{
        //    get
        //    {
        //        return duty;
        //    }
        //    set
        //    {
        //        duty = value;
        //        OnPropertyChanged("_Duty");
        //    }
        //}
        #endregion

        public ViewModel()
        {
            //nurse = new Nurse();
            //physician = new Physician();
            //duty = new Duty();

            employee = new Employee();
            hospital = new Hospital();
            //LoadDataToCollection();
        }





        public bool VerifyPasswordAndLogin(string login, string password)
        {

            return true;
            //return this._Hospital.Staff.Where(x => x.Login == login)
            //    .Where(x => x.Password == password).Any() ? true : false;
        }

        public void SerializeAllData()
        {
            DataDispatcher.SerializeData(this._Hospital.Staff);
        }

        //public void DeserializeData()
        //{
        //    this._Hospital.DeserializeData();
        //}

        public Employee AddNewEmployee(string _name, string _surname, string _jobTitle, long _pesel, string _specialization, long _licNumber,
            string _login, string _password, bool _isAdmin)
        {
            switch (_jobTitle)
            {
                case "lekarz":
                    Employee physician = new Physician(_name, _surname, _pesel, _login,
                        _password, _jobTitle, _isAdmin, _specialization, _licNumber);
                    _Hospital.Staff.Add(physician);
                    return physician;

                case "pielęgniarka":
                    Employee nurse = new Nurse(_name, _surname, _pesel, _login, _password, _jobTitle, _isAdmin);
                    _Hospital.Staff.Add(nurse);
                    return nurse;

                default:
                    Employee employee = new Employee(_name, _surname, _pesel, _login, _password, _jobTitle, _isAdmin);
                    _Hospital.Staff.Add(employee);
                    return employee;
            }
        }

       
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string _property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_property));
        }

    }



}


