using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Data
{
    /// <summary>
    /// <para>Class marked for serialization</para>
    /// <para>Physician class with methods and properties allows you to create a class instance and add/remove duty</para>
    /// </summary>
    /// <seealso cref="Hospital_Data.Employee" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    public class Physician : Employee, INotifyPropertyChanged
    {
        /// <summary>The specialization</summary>
        private string specialization;

        /// <summary>The licence number</summary>
        private int licenceNumber;

        /// <summary>The duties</summary>
        private List<Duty> duties = new List<Duty>();

        /// <summary>Gets or sets the duties.</summary>
        /// <value>The duties.</value>
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

        /// <summary>Initializes a new instance of the <see cref="Physician"/> class.</summary>
        public Physician()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="Physician"/> class.</summary>
        /// <param name="_physician">The physician.</param>
        public Physician(Physician _physician) :base(_physician)
        {
            this.specialization = _physician.Specialization;
            this.licenceNumber = _physician.LicenceNumber;
        }

        /// <summary>Initializes a new instance of the <see cref="Physician"/> class.</summary>
        /// <param name="_employee">The employee.</param>
        public Physician(Employee _employee) : base(_employee)
        {
            if(_employee is Physician)
            {
                this.specialization = ((Physician)_employee).Specialization;
                this.licenceNumber = ((Physician)_employee).LicenceNumber;
            }            
        }

        /// <summary>Adds the duty.</summary>
        /// <param name="date">The date.</param>
        public void AddDuty(DateTime date)
        {
            this.Duties.Add(new Duty(date));
        }

        /// <summary>Removes the duty.</summary>
        /// <param name="date">The date.</param>
        public void RemoveDuty(DateTime date)
        {
            this.Duties.Remove(Duties.Where(x => x.Date == date).Single());
        }

    }
}
