using Hospital_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;
        public MainWindow(ViewModel vm)
        {
            InitializeComponent();
            this._viewModel = vm;
            this.DataContext = _viewModel;
            SetInitialWindowState();

        }

        private void Exit_ButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetInitialWindowState()
        {
            this.deleteDuty_Button.IsEnabled = this.Edit_Button.IsEnabled = false;
            SetDatePickerState();
            statusTextBlock.Text = _viewModel.IsLoggedUserAdmin ? "Wybierz pracownika z listy by edytować jego dane i/ lub dyżury (dyżury pełnią tylko lekarze i pielęgniarki)." :
                "Wybierz pracownika z listy, by wyświetlić terminy jego dyżurów (dyżurują tylko lekarze i pielęgniarki naszego szpitala).";
        }


        private void Logout_ButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SerializeAllData();
            var entryView = new Entry();
            this.Close();
            entryView.Show();
        }

        private void Add_ButtonClick(object sender, RoutedEventArgs e)
        {
            this._viewModel.IsEditModeOff = true;
            var employeeData = new EmployeeView(this._viewModel);
            employeeData.ShowDialog();
        }

        private void Edit_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (this._viewModel.SelectedEmployee != null)
            {
                this._viewModel.IsEditModeOff = false;
                var employeeData = new EmployeeView(this._viewModel, _viewModel.SelectedEmployee);
                employeeData.ShowDialog();
            }

        } 
        
        private void ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Edit_Button.IsEnabled = this.listView.SelectedItem == null ? false : true;            
            _viewModel.SelectedEmployee = this.listView.SelectedItem as Employee;
            SetDatePickerState();
            _viewModel.SetListOfDutiesForSelectedEmployee();
        }        

        private void DutySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.deleteDuty_Button.IsEnabled = this.dutiesListView.SelectedItem == null ? false : true;
            _viewModel.SelectedDuty = this.dutiesListView.SelectedItem as Duty;
        }

        private void DatePickerClosed(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ValidateDutyTerm((DateTime)this.addDuty_dtPicker.SelectedDate))
            {
                _viewModel.AddDuty((DateTime)this.addDuty_dtPicker.SelectedDate);
            }
        }

        private void SetDatePickerState()
        {
            if (_viewModel.SelectedEmployee is Physician || _viewModel.SelectedEmployee is Nurse)
            {
                this.addDuty_dtPicker.IsEnabled = true;
            }
            else this.addDuty_dtPicker.IsEnabled = false;
        }
    }
}
