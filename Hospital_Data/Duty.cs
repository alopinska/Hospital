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
    /// <para></para>
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    public class Duty : INotifyPropertyChanged 
    {
        /// <summary>The date</summary>
        private DateTime date;

        /// <summary>Gets or sets the date.</summary>
        /// <value>The date.</value>
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        /// <summary>Gets the date string format.</summary>
        /// <value>The date string format.</value>
        public string DateStringFormat
        {
            get
            {
                return string.Format("{0:D}", date);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="Duty"/> class.</summary>
        public Duty()
        {

        }
        /// <summary>Initializes a new instance of the <see cref="Duty"/> class.</summary>
        /// <param name="_date">The date.</param>
        public Duty(DateTime _date)
        {
            this.date = _date;               
        }


        /// <summary>Occurs when a property value changes.</summary>
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Called when [property changed].</summary>
        /// <param name="_property">The property.</param>
        private void OnPropertyChanged(string _property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_property));
        }
    }
}
