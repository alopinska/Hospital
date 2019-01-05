using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Data
{
    [Serializable]
    public class Duty : INotifyPropertyChanged 
    {
        private DateTime date;      

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public Duty()
        {

        }
        public Duty(DateTime _date)
        {
            this.Date = _date;               
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string _property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_property));
        }
    }
}
