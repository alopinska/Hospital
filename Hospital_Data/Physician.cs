using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Data
{
    [Serializable]
    public class Physician : Employee, INotifyPropertyChanged
    {
        private string specialization;
        private long licenceNumber;

        public List<Duty> Duties;

        #region Properties
        public string Specialization
        {
            get { return specialization; }
            set
            {
                specialization = value;
                OnPropertyChanged("Specialization");
            }
        }
        public long LicenceNumber
        {
            get { return licenceNumber; }
            set
            {
                licenceNumber = value;
                OnPropertyChanged("LicenceNumber");
            }
        }
        #endregion

        public Physician()
        {

        }
        public Physician(string _name, string _surname, long _pesel, string _login, string _password, string _jobtitle, bool _isAdmin, string _specialization, long _licNumber)
            : base(_name, _surname, _pesel, _login, _password, _jobtitle, _isAdmin)
        {
            this.Specialization = _specialization;
            this.LicenceNumber = _licNumber;
        }   
        

    }
}
