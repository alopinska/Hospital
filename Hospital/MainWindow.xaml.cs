using Hospital_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        private string statusInfoHelper;
        private ViewModel _viewModel;
        public MainWindow(ViewModel vm)
        {
            InitializeComponent();
            this._viewModel = vm;
            this.DataContext = _viewModel;
            SetInitialWindowState();
        }

        #region Button actions
        private void DeleteDutyButtonClick(object sender, RoutedEventArgs e)
        {
            this.statusInfoHelper = $"anulowanie dyżuru z dn. {_viewModel.SelectedDuty.DateStringFormat}";
            DisplayStatusChange();
            _viewModel.RemoveDuty();
        }

        private void Logout_ButtonClick(object sender, RoutedEventArgs e)
        {           
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

        private void DatePickerClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_viewModel.ValidateDutyTerm((DateTime)this.addDuty_dtPicker.SelectedDate))
                {
                    this.statusInfoHelper = $"nowy dyżur w dniu {(DateTime)this.addDuty_dtPicker.SelectedDate}";
                    DisplayStatusChange();
                    _viewModel.AddDuty((DateTime)this.addDuty_dtPicker.SelectedDate);                    
                }
                else return;
            }
            catch (Exception)
            {
                MessageBox.Show("Aby zapisać dyżur, należy wskazać datę");
            }
            
        }

        private void DisplayStatusChange()
        {
            Thread shortlyDisplayedInfo = new Thread(ChangeStatusBarContent);
            shortlyDisplayedInfo.Start();
        }

        private async void ChangeStatusBarContent()
        {
            await Dispatcher.BeginInvoke(new Action(() => UpdateStatusInfo($"Przetwarzanie zmian: {statusInfoHelper}")));
            await Task.Delay(2500);
            await Dispatcher.BeginInvoke(new Action(() => UpdateStatusInfo($"Wprowadzono zmiany: {statusInfoHelper}")));
            await Task.Delay(2000);
            await Dispatcher.BeginInvoke(new Action(() => UpdateStatusInfo(_viewModel.IsLoggedUserAdmin ? "Wybierz pracownika z listy by edytować jego dane i/ lub dyżury (dyżury pełnią tylko lekarze i pielęgniarki)." :
                "Wybierz pracownika z listy, by wyświetlić terminy jego dyżurów (dyżurują tylko lekarze i pielęgniarki naszego szpitala).")));
        }

        private void UpdateStatusInfo(string msg)
        {
            statusTextBlock.Text = msg;
        }

        #endregion

        #region Layout manipulation logic
        private void ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedEmployee = _viewModel.IsLoggedUserAdmin ? this.listView.SelectedItem as Employee 
                : this.listViewDefault.SelectedItem as Employee;
            
            this.Edit_Button.IsEnabled = this.listView.SelectedItem == null ? false : true;           
            SetDatePickerState();
            _viewModel.SetListOfDutiesForSelectedEmployee();
            SetDutyLabel();
        }      

        private void DutySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.deleteDuty_Button.IsEnabled = this.dutiesListView.SelectedItem == null ? false : true;
            _viewModel.SelectedDuty = this.dutiesListView.SelectedItem as Duty;
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            _viewModel.SerializeAllData();
        }
        #endregion

        #region Window view settings
        private void SetInitialWindowState()
        {
            this.deleteDuty_Button.IsEnabled = this.Edit_Button.IsEnabled = false;
            SetDatePickerState();
            _viewModel.SetListOfDutiesForSelectedEmployee();
            statusTextBlock.Text = _viewModel.IsLoggedUserAdmin ? "Wybierz pracownika z listy by edytować jego dane i/ lub dyżury (dyżury pełnią tylko lekarze i pielęgniarki)." :
                "Wybierz pracownika z listy, by wyświetlić terminy jego dyżurów (dyżurują tylko lekarze i pielęgniarki naszego szpitala).";
            SetDutyLabel();
        }

        private void SetDatePickerState()
        {
            if (_viewModel.SelectedEmployee is Physician || _viewModel.SelectedEmployee is Nurse)
            {
                this.addDuty_dtPicker.IsEnabled = true;
            }
            else this.addDuty_dtPicker.IsEnabled = false;
        }       

        private void SetDutyLabel()
        {
            if(_viewModel.SelectedEmployee != null)
            {
                if (_viewModel.SelectedEmployee is Physician || _viewModel.SelectedEmployee is Nurse)
                {
                    this.dutyLabel.Content = string.Format("Dyżury: {0} {1}", _viewModel.SelectedEmployee.Name, _viewModel.SelectedEmployee.Surname);
                }
                else
                {
                    this.dutyLabel.Content = string.Format("{0} {1} (nie pełni dyżurów)", _viewModel.SelectedEmployee.Name, _viewModel.SelectedEmployee.Surname);
                }
            }           
        }
        #endregion

       
    }
}
