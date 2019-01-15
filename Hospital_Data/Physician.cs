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

        public Physician(Physician _physician) :base(_physician)
        {
            this.specialization = _physician.Specialization;
            this.licenceNumber = _physician.LicenceNumber;
        }

        public Physician(Employee _employee) : base(_employee)
        {
            if(_employee is Physician)
            {
                this.specialization = ((Physician)_employee).Specialization;
                this.licenceNumber = ((Physician)_employee).LicenceNumber;
            }            
        }

        public void AddDuty(DateTime date)
        {
            this.Duties.Add(new Duty(date));
        }

        public void RemoveDuty(DateTime date)
        {
            this.Duties.Remove(Duties.Where(x => x.Date == date).Single());
        }

    }
}
