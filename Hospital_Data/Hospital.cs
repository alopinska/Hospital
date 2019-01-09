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


        public Hospital()
        {
            Staff = DataDispatcher.DeserializeData();
        }

        public void SerializeData()
        {
            DataDispatcher.SerializeData(this.Staff);
        }

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;  

        private void OnPropertyChanged(string _property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_property));
        }
    }
}
