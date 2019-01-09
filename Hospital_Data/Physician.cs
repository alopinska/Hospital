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
        private int licenceNumber;

        private List<Duty> duties = new List<Duty>();
        public List<Duty> Duties
        {
            get { return duties; }
            set
            {
                duties = value;
                OnPropertyChanged("Duties");
            }
        }

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

        public int LicenceNumber
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
        //public Physician(string _name, string _surname, long _pesel, string _login, string _password, string _jobtitle, bool _isAdmin, string _specialization, int _licNumber)
        //    : base(_name, _surname, _pesel, _login, _password, _jobtitle, _isAdmin)
        //{
        //    this.Specialization = _specialization;
        //    this.LicenceNumber = _licNumber;
        //}

        public Physician(Physician _physician) :base(_physician)
        {
            this.specialization = _physician.Specialization;
            this.licenceNumber = _physician.LicenceNumber;
        }

        public Physician(Employee _employee, string _specialization = null, string _licNumber = null) : base(_employee)
        {
            this.specialization = _specialization;
            this.licenceNumber = _licNumber == null ? 0 : int.Parse(_licNumber);
        }


    }
}
