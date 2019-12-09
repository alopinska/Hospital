using Hospital_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital_View
{
    /// <summary>Base view model for <see cref="Hospital_View.EmployeeViewModel"/> and <see cref="Hospital_View.ViewModel"/></summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>Represents the method that will handle the <see cref="System.ComponentModel.INotifyPropertyChanged.PropertyChanged"/></summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Called when [property changed].</summary>
        /// <param name="_property">A <see cref="System.ComponentModel.PropertyChangedEventArgs"/> that contains the event data.</param>
        protected void OnPropertyChanged([CallerMemberName] string _property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_property));
        }       

    }
}
