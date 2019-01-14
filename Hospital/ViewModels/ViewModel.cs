using Hospital_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital_View
{

    public class ViewModel : BaseViewModel, INotifyPropertyChanged
    {

        public Employee SelectedEmployee { get; set; }
        public Hospital Hospital { get; set; }
        public Duty SelectedDuty { get; set; }


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
                OnPropertyChanged("Employees");
            }
        }

        private ObservableCollection<Duty> _duties = new ObservableCollection<Duty>();
        public ObservableCollection<Duty> Duties
        {
            get
            {
                return _duties;
            }
            set
            {
                _duties = value;
                OnPropertyChanged("Duties");
            }
        }

        public ViewModel()
        {
            this.SelectedDuty = new Duty();
            this.SelectedEmployee = new Employee();
            this.Hospital = new Hospital();
            LoadDataForListView();            
            SetListOfDutiesForSelectedEmployee();
        }

        private void LoadDataForListView()
        {
            Employees = new ObservableCollection<Employee>(Hospital.Staff);
        }       

        public void AddDuty(DateTime date)
        {
            if (SelectedEmployee != null)
            {
                if (SelectedEmployee is Physician)
                {
                    ((Physician)SelectedEmployee).AddDuty(date);
                }
                else if (SelectedEmployee is Physician)
                {
                    ((Physician)SelectedEmployee).AddDuty(date);
                }
                Duties.Add(new Duty(date));
            }

        }


        public void SetListOfDutiesForSelectedEmployee()
        {            
            if (SelectedEmployee != null)
            {
                Duties.Clear();
                if (SelectedEmployee is Nurse) this.Duties = new ObservableCollection<Duty>(((Nurse)SelectedEmployee).Duties);
                else if (SelectedEmployee is Physician) this.Duties = new ObservableCollection<Duty>(((Physician)SelectedEmployee).Duties);
            }
        }    


        public bool VerifyPasswordAndLogin(string login, string password)
        {
            //return true;
            if (this.Hospital.Staff.Where(x => x.Login == login)
              .Where(x => x.Password == password).Any())
            {
                var _empl = this.Hospital.Staff.Where(x => x.Login == login)
              .Where(x => x.Password == password).FirstOrDefault();

                //this.IsLoggedUserAdmin = _empl.IsAdmin ? true : false;
                this.SelectedEmployee = _empl;
                return true;
            }
            else
            {
                return true;
            }
        }

        public void SerializeAllData()
        {
            this.Hospital.Staff = this.Employees.ToList();
            this.Hospital.SerializeData();
        }        

        public bool ValidateDutyTerm(DateTime date)
        {
            if (!VerifyDoubledDutyDate(date)) ReturnErrorMsg("TheSameDay");
            if (!VerifyMaxNumberOfDutiesCondition(date)) ReturnErrorMsg("TooManyDuties");
            if (!VerifyDayByDayCondition(date)) ReturnErrorMsg("DayByDay");
            if (SelectedEmployee is Physician)
            {
                if (!VerifySingleSpecializationOnDutyPerDay(date)) ReturnErrorMsg("Specialization");
            }
            return true;
        }

        private bool VerifyMaxNumberOfDutiesCondition(DateTime date)
        {
            if (SelectedEmployee is Physician)
            {
                return ((Physician)SelectedEmployee).Duties.Where(x => x.Date.Month == date.Month).Count() >= 10 ? false : true;
            }
            else
            {
                return ((Nurse)SelectedEmployee).Duties.Where(x => x.Date.Month == date.Month).Count() >= 10 ? false : true;
            }
        }

        private bool VerifyDoubledDutyDate(DateTime date)
        {
            if (SelectedEmployee is Physician)
            {
                return ((Physician)SelectedEmployee).Duties.Where(x => x.Date == date).Any() ? false : true;
            }
            else
            {
                return ((Nurse)SelectedEmployee).Duties.Where(x => x.Date == date).Any() ? false : true;
            }
        }

        private bool VerifyDayByDayCondition(DateTime date)
        {
            if (SelectedEmployee is Physician)
            {
                return ((Physician)SelectedEmployee).Duties.Where(x => x.Date.AddDays(1) == date || x.Date.AddDays(-1) == date).Any() ? false : true;
            }
            else
            {
                return ((Nurse)SelectedEmployee).Duties.Where(x => x.Date.AddDays(1) == date || x.Date.AddDays(-1) == date).Any() ? false : true;
            }
        }

        private bool VerifySingleSpecializationOnDutyPerDay(DateTime date)
        {
            var physiciansWithSelectedSpecialization = Employees.Where(x => x is Physician).Where(x => (x as Physician).Specialization == ((Physician)SelectedEmployee).Specialization).ToList();
            foreach (var physician in physiciansWithSelectedSpecialization)
            {
                if (((Physician)physician).Duties.Where(x => x.Date == date).Any()) return false;
            }
            return true;
        }

        private bool ReturnErrorMsg(string fieldWithError)
        {
            string msgContent = "";
            switch (fieldWithError)
            {
                case "TooManyDuties":
                    msgContent = "Zbyt wiele dyżurów w tym miesiącu (maksymalnie 10 dla pracownika)";
                    break;
                case "DayByDay":
                    msgContent = "Ten pracownik ma już ustalony dyżur w dniu poprzedzającym lub następującym po wybranej dacie";
                    break;
                case "Specialization":
                    msgContent = "Inny lekarz tej specjalizacji pełni dyżur we wskazanym dniu";
                    break;
                case "TheSameDay":
                    msgContent = "Ten pracownik pełni dyżur wkazanego dnia";
                    break;
            }
            MessageBox.Show($"{msgContent}", "Operacja wstrzymana", MessageBoxButton.OK, MessageBoxImage.Stop);
            return false;
        }

    }
}


