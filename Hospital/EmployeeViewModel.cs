using Hospital_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_View
{
    public class EmployeeViewModel : BaseViewModel
    {
        public bool IsCreatingNewEmployee { get; set; }
        public Employee EmployeeBackup { get; set; }
        public EmployeeViewModel(Employee employee = null)
        {
            if (employee == null)
            {
                IsCreatingNewEmployee = true;
                employee = new Employee();
            }
            else
            {
                IsCreatingNewEmployee = true;//Todo usunac
                EmployeeBackup = DataDispatcher.CloneBySerialization(employee);               
            }
                
            Employee = employee;
        }

        public Employee Employee { get; set; }

    }
}
