using Hospital_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_View
{
   
    public class EmployeeViewModel : BaseViewModel, INotifyPropertyChanged
    {
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

        public Employee EmployeeBackup { get; set; }        
        public Employee Employee { get; set; }
        public Physician Physician { get; set; }
        public Nurse Nurse { get; set; }

        public EmployeeViewModel(bool isEditModeOff, Employee employee = null)
        {
            IsCreatingNewEmployee = isEditModeOff;
            
            if(employee == null)
            {
                this.Employee = new Employee();
                TargetObjectType = null;
            }
            else
            {
                if (employee is Physician)
                {
                    this.Physician = new Physician((Physician)employee);
                    this.EmployeeBackup = DataDispatcher.CloneBySerialization<Physician>((Physician)employee);
                }
                else if (employee is Nurse)
                {
                    this.Nurse = new Nurse((Nurse)employee);
                    this.EmployeeBackup = DataDispatcher.CloneBySerialization<Nurse>((Nurse)employee);
                }
                else
                {
                    this.Employee = new Employee(employee);
                    this.EmployeeBackup = DataDispatcher.CloneBySerialization<Employee>((Employee)employee);
                }
                this.TargetObjectType = employee.JobTitle;     
               
            }      
           
        }      

        public void ReorganizeDataAccordingToEmployeeType(string mode, Employee _emp)
        {
            switch (mode)
            {
                case "lekarz":
                    this.Physician = new Physician(_emp);
                    break;
                case "pielęgniarka":
                    this.Nurse = new Nurse(_emp);
                    break;
                default:
                    this.Employee = new Employee(_emp);
                    break;
            }
        }
    }
}
