using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Data
{
    [Serializable]
    public class Nurse : Employee, INotifyPropertyChanged
    {
        private List<Duty> duties = new List<Duty>();
        public Nurse()
        {

        }
        public Nurse (string _name, string _surname, long _pesel, string _login, string _password, string _jobtitle, bool _isAdmin) 
            : base (_name, _surname, _pesel, _login, _password, _jobtitle, _isAdmin)
        {
            
        }       

    }
}
