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
    /// <para>Nurse class with methods and properties allows you to create a class instance and add/remove duty.</para>
    /// </summary>
    /// <seealso cref="Hospital_Data.Employee" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    public class Nurse : Employee, INotifyPropertyChanged
    {
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

        /// <summary>Initializes a new instance of the <see cref="Nurse"/> class.</summary>
        public Nurse()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="Nurse"/> class.</summary>
        /// <param name="nurse">The nurse.</param>
        public Nurse(Nurse nurse) : base(nurse)
        {

        }
        /// <summary>Initializes a new instance of the <see cref="Nurse"/> class.</summary>
        /// <param name="_employee">The employee.</param>
        public Nurse(Employee _employee) : base(_employee)
        {

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
