using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital_Data
{
    /// <summary></summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    public class Hospital : INotifyPropertyChanged
    {
        private List<Employee> staff = new List<Employee>();
        public List<Employee> Staff
        {
            get { return staff; }
            set
            {
                staff = value;
                OnPropertyChanged("Staff");
            }
        }


        /// <summary>Initializes a new instance of the <see cref="Hospital"/> class.</summary>
        public Hospital()
        {
            Staff = DataDispatcher.DeserializeData();
        }

        /// <summary>Serializes the data.</summary>
        public void SerializeData()
        {
            DataDispatcher.SerializeData(this.Staff);
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
