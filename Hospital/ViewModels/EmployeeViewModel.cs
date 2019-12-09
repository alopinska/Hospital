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
    /// <summary>Employee object data model.</summary>
    /// <remarks>
    /// <see cref="Employee.Name"/>
    /// <see cref="Employee.Surname"/>
    /// <see cref="Employee.PESEL"/>
    /// <see cref="Employee.Login"/>
    /// <see cref="Employee.Password"/>
    /// <see cref="Employee.JobTitle"/>
    /// </remarks>
    /// <seealso cref="Hospital_View.BaseViewModel" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class EmployeeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        /// <summary>The view model</summary>
        private ViewModel viewModel;

        /// <summary>The target object type</summary>
        private string targetObjectType;

        /// <summary>Gets or sets the type of the target object.</summary>
        /// <value>The type of the target object.</value>
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

        /// <summary>Gets or sets a value indicating whether this instance is creating new employee.</summary>
        /// <value>
        ///   <c>true</c> if this instance is creating new employee; otherwise, <c>false</c>.</value>
        public bool IsCreatingNewEmployee { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance has text changed.</summary>
        /// <value>
        ///   <c>true</c> if this instance has text changed; otherwise, <c>false</c>.</value>
        public bool HasTextChanged { get; set; }

        /// <summary>Gets or sets the employee backup.</summary>
        /// <value>The employee backup.</value>
        public Employee EmployeeBackup { get; set; }

        /// <summary>Gets or sets the employee.</summary>
        /// <value>The employee.</value>
        public Employee Employee { get; set; }

        /// <summary>Gets or sets the physician.</summary>
        /// <value>The physician.</value>
        public Physician Physician { get; set; }

        /// <summary>Gets or sets the nurse.</summary>
        /// <value>The nurse.</value>
        public Nurse Nurse { get; set; }

        /// <summary>Initializes a new instance of the <see cref="EmployeeViewModel"/> class.</summary>
        /// <param name="vm">The view parameter.</param>
        /// <param name="employee">The Employee object.</param>
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

        /// <summary>Reorganizes the type of the data according to employee.</summary>
        /// <param name="type">The type in string. 
        /// <para>Possible values: lekarz, pielęgniarka</para></param>
        /// <param name="_emp">The Employee object.</param>
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

        /// <summary>Validates the addition of the Employee.</summary>
        /// <returns></returns>
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
        /// <summary>Validates the input.</summary>
        /// <param name="Employees">The employees.</param>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public bool ValidateInput(ObservableCollection<Employee> Employees, Employee employee)
        {
            if (!VerifyEmptyFields(employee)) return false;
            if (!VerifyKeyData(employee)) return false;
            return true;
        }

        /// <summary>Verifies the key data PESEL and a login name .</summary>
        /// <param name="employee">The employee.</param>
        /// <returns>Information on validated fields.</returns>
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

        /// <summary>Verifies the empty fields.</summary>
        /// <param name="employee">The employee.</param>
        /// <returns>Information on validated fields.</returns>
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

        /// <summary>Returns the error MSG.</summary>
        /// <param name="fieldWithError">The field with error.</param>
        /// <returns>Errors</returns>
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
