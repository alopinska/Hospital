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
        public List<Employee> Staff = new List<Employee>();     


        public Hospital()
        {
            //Staff = DataDispatcher.DeserializeData();
        }

        
        public event PropertyChangedEventHandler PropertyChanged;  

        private void OnPropertyChanged(string _property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_property));
        }
    }
}
