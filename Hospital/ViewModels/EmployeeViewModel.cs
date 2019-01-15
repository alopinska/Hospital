using Hospital_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital_View
{
    public class EmployeeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ViewModel viewModel;
        private string targetObjectType;
        public string TargetObjectType
        {
            get
            {
                return string.IsNullOrEmpty(targetObjectType) ? " " : targetObjectType;
            }
            set
            {
                targetObjectType = value;
                OnPropertyChanged("TargetObjectType");
            }
        }

        public bool IsCreatingNewEmployee { get; set; }
        public bool HasTextChanged { get; set; }
        public Employee EmployeeBackup { get; set; }
        public Employee Employee { get; set; }
        public Physician Physician { get; set; }
        public Nurse Nurse { get; set; }

        public EmployeeViewModel(ViewModel vm, Employee employee = null)
        {
            this.viewModel = vm;
            this.Employee = new Employee();
            this.IsCreatingNewEmployee = vm.IsEditModeOff;
            this.TargetObjectType = employee?.JobTitle;
            if (employee != null)
            {
                if (employee is Physician)
                {
                    this.Employee = this.Physician = new Physician((Physician)employee);
                    this.EmployeeBackup = DataDispatcher.CloneBySerialization<Physician>(employee as Physician);
                }
                else if (employee is Nurse)
                {
                    this.Employee = this.Nurse = new Nurse((Nurse)employee);
                    this.EmployeeBackup = DataDispatcher.CloneBySerialization<Nurse>(employee as Nurse);
                }
                else
                {
                    this.Employee = new Employee(employee);
                    this.EmployeeBackup = DataDispatcher.CloneBySerialization<Employee>(employee);
                }
            }
        }

        public void ReorganizeDataAccordingToEmployeeType(string type, Employee _emp)
        {
            switch (type)
            {
                case "lekarz":
                    this.Employee = this.Physician = new Physician(_emp);
                    break;
                case "pielęgniarka":
                    this.Employee = this.Nurse = new Nurse(_emp);
                    break;
                default:
                    this.Employee = new Employee(_emp);
                    break;
            }
        }       

        public bool AddNewEmployee()
        {
            switch (this.TargetObjectType)
            {
                case "lekarz":
                    if (ValidateInput(this.viewModel.Employees, this.Physician))
                    {
                        this.viewModel.Employees.Add(this.Physician);
                        return true;
                    }
                    else return false;
                case "pielęgniarka":
                    if (ValidateInput(this.viewModel.Employees, this.Nurse))
                    {
                        this.viewModel.Employees.Add(this.Nurse);
                        return true;
                    }
                    else return false;
                default:
                    if (ValidateInput(this.viewModel.Employees, this.Employee))
                    {
                        this.viewModel.Employees.Add(this.Employee);
                        return true;
                    }
                    else return false;
            }
        }

        #region Data input validation
        public bool ValidateInput(ObservableCollection<Employee> Employees, Employee employee)
        {
            if (!VerifyEmptyFields(employee)) return false;
            if (!VerifyKeyData(employee)) return false;
            return true;
        }

        private bool VerifyKeyData(Employee employee)
        {
            if (this.viewModel.Employees.Where(x => x.PESEL == employee.PESEL).Any()) return ReturnErrorMsg("PESELdoubled");
            if (this.viewModel.Employees.Where(x => x.Login == employee.Login).Any()) return ReturnErrorMsg("LoginDoubled");
            if (employee is Physician)
            {
                if (viewModel.Employees.Where(x => x is Physician)
                     .Where(x => (x as Physician).LicenceNumber == ((Physician)employee).LicenceNumber)
                     .Any()) return ReturnErrorMsg("PWZdoubled");            
            }
            return true;
        }

        private bool VerifyEmptyFields(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name)) return ReturnErrorMsg("Name");
            if (string.IsNullOrEmpty(employee.Surname)) return ReturnErrorMsg("Surname");
            if (string.IsNullOrEmpty(employee.JobTitle)) return ReturnErrorMsg("JobTitle");
            if (employee.PESEL == 0 || employee.PESEL.ToString().Count() < 11) return ReturnErrorMsg("PESEL");
            if (employee is Physician)
            {
                if (string.IsNullOrEmpty(((Physician)employee).Specialization)) return ReturnErrorMsg("Specialization");
                if (((Physician)employee).LicenceNumber == 0 || ((Physician)employee).LicenceNumber.ToString().Count() < 7) return ReturnErrorMsg("LicenceNumber");
            }
            if (string.IsNullOrEmpty(employee.Login)) return ReturnErrorMsg("Login");
            if (string.IsNullOrEmpty(employee.Password)) return ReturnErrorMsg("Password");
            return true;
        }

        private bool ReturnErrorMsg(string fieldWithError)
        {
            string msgContent = "";
            switch (fieldWithError)
            {
                case "Name":
                    msgContent = "Imię pracownika jest wymagane";
                    break;
                case "Surname":
                    msgContent = "Nazwisko pracownika jest wymagane";
                    break;
                case "JobTitle":
                    msgContent = "Stanowisko pracownika jest wymagane";
                    break;
                case "PESEL":
                    msgContent = "Brak identyfikatora PESEL lub PESEL za krótki (wymagane 11 cyfr)";
                    break;
                case "Specialization":
                    msgContent = "Proszę wskazać specjalizację lekarza";
                    break;
                case "LicenceNumber":
                    msgContent = "Brak numeru PWZ lekarza lub numer PWZ za krótki (wymagane 7 cyfr)";
                    break;
                case "Login":
                    msgContent = "Login pracownika jest wymagany";
                    break;
                case "Password":
                    msgContent = "Hasło pracownika jest wymagane";
                    break;
                case "PESELdoubled":
                    msgContent = "W systemie jest już pracownik o takim numerze PESEL.";
                    break;
                case "LoginDoubled":
                    msgContent = "W systemie jest już pracownik o takim loginie.";
                    break;
                case "PWZdoubled":
                    msgContent = "W systemie jest już lekarz o takim numerze PWZ.";
                    break;
            }
            MessageBox.Show($"{msgContent}", "Operacja wstrzymana", MessageBoxButton.OK, MessageBoxImage.Stop);
            return false;
        }
        #endregion
    }
}
