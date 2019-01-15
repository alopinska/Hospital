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
        public List<Duty> Duties
        {
            get { return duties; }
            set
            {
                duties = value;
                OnPropertyChanged("Duties");
            }
        }

        public Nurse()
        {

        } 

        public Nurse(Nurse nurse) : base(nurse)
        {

        }
        public Nurse(Employee _employee) : base(_employee)
        {

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
