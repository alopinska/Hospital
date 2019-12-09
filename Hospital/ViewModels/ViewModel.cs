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

    /// <summary></summary>
    /// <seealso cref="Hospital_View.BaseViewModel" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class ViewModel : BaseViewModel, INotifyPropertyChanged
    {
        /// <summary>Gets or sets the selected employee.</summary>
        /// <value>The selected employee.</value>
        public Employee SelectedEmployee { get; set; }

        /// <summary>Gets or sets the hospital.</summary>
        /// <value>The hospital.</value>
        public Hospital Hospital { get; set; }

        /// <summary>Gets or sets the selected duty.</summary>
        /// <value>The selected duty.</value>
        public Duty SelectedDuty { get; set; }


        /// <summary>Gets or sets a value indicating whether this instance is logged user admin.</summary>
        /// <value>
        ///   <c>true</c> if this instance is logged user admin; otherwise, <c>false</c>.</value>
        public bool IsLoggedUserAdmin { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether this instance is edit mode off.</summary>
        /// <value>
        ///   <c>true</c> if this instance is edit mode off; otherwise, <c>false</c>.</value>
        public bool IsEditModeOff { get; set; }


        /// <summary>The employees</summary>
        private ObservableCollection<Employee> _employees;

        /// <summary>Gets or sets the employees.</summary>
        /// <value>The employees.</value>
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return _employees; ;
            }
            set
            {
                _employees = value;
                OnPropertyChanged("Employees");
            }
        }


        /// <summary>The duties</summary>
        private ObservableCollection<Duty> _duties = new ObservableCollection<Duty>();


        /// <summary>Gets or sets the duties.</summary>
        /// <value>The duties.</value>
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

        /// <summary>Initializes a new instance of the <see cref="ViewModel"/> class.</summary>
        public ViewModel()
        {
            this.SelectedDuty = new Duty();
            this.SelectedEmployee = new Employee();
            this.Hospital = new Hospital();
            LoadDataForListView();            
            SetListOfDutiesForSelectedEmployee();
        }



        /// <summary>Adds the duty.</summary>
        /// <param name="date">The date.</param>
        public void AddDuty(DateTime date)
        {
            if (SelectedEmployee != null)
            {
                if (SelectedEmployee is Physician)
                {
                    ((Physician)SelectedEmployee).AddDuty(date);
                }
                else if (SelectedEmployee is Nurse)
                {
                    ((Nurse)SelectedEmployee).AddDuty(date);
                }
                Duties.Add(new Duty(date));
            }
        }

        /// <summary>Removes the duty.</summary>
        public void RemoveDuty()
        {
            if(this.SelectedDuty != null)
            {
                if (SelectedEmployee is Physician)
                {
                    ((Physician)SelectedEmployee).RemoveDuty(SelectedDuty.Date);
                }
                else if (SelectedEmployee is Nurse)
                {
                    ((Nurse)SelectedEmployee).RemoveDuty(SelectedDuty.Date);
                }
                Duties.Remove(SelectedDuty);
            }
        }

        /// <summary>
        /// Sets the list of duties for selected employee.
        /// </summary>
        /// <para>Objects are ordered by <c>Duty.Date</c>.</para>
        public void SetListOfDutiesForSelectedEmployee()
        {            
            if (SelectedEmployee != null)
            {
                Duties.Clear();
                if (SelectedEmployee is Nurse) this.Duties = new ObservableCollection<Duty>(((Nurse)SelectedEmployee).Duties.OrderBy(x => x.Date));
                else if (SelectedEmployee is Physician) this.Duties = new ObservableCollection<Duty>(((Physician)SelectedEmployee).Duties.OrderBy(x => x.Date));
                
            }
        }

        /// <summary>Serializes all data.</summary>
        public void SerializeAllData()
        {
            this.Hospital.Staff = this.Employees.ToList();
            this.Hospital.SerializeData();
        }

        /// <summary>Loads the data for ListView.</summary>
        private void LoadDataForListView()
        {
            Employees = new ObservableCollection<Employee>(Hospital.Staff.OrderBy(x => x.Surname));           
        }

        #region Data input validation
        public bool VerifyPasswordAndLogin(string login, string password)
        {            
            if (this.Hospital.Staff.Where(x => x.Login == login)
              .Where(x => x.Password == password).Any())
            {
                var _empl = this.Hospital.Staff.Where(x => x.Login == login)
              .Where(x => x.Password == password).FirstOrDefault();

                this.IsLoggedUserAdmin = _empl.IsAdmin ? true : false;
                this.SelectedEmployee = _empl;
                return true;
            }
            else
            {
                return false;
            }
        }              

        public bool ValidateDutyTerm(DateTime date)
        {
            if (!VerifyDoubledDutyDate(date)) return ReturnErrorMsg("TheSameDay");
            if (!VerifyMaxNumberOfDutiesCondition(date)) return ReturnErrorMsg("TooManyDuties");
            if (!VerifyDayByDayCondition(date)) return ReturnErrorMsg("DayByDay");
            if (SelectedEmployee is Physician)
            {
                if (!VerifySingleSpecializationOnDutyPerDay(date)) return ReturnErrorMsg("Specialization");
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
        #endregion
    }
}


