using Hospital_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital_View
{
    /// <summary>
    /// Logika interakcji dla klasy EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        ViewModel _viewModel;
        EmployeeViewModel _employeeViewModel;

        public EmployeeView(ViewModel viewModel, Employee employee = null)
        {
            InitializeComponent();
            _viewModel = viewModel;
            if (employee == null)
            {
                _employeeViewModel = new EmployeeViewModel(_viewModel);
            }
            else
            {
                _employeeViewModel = new EmployeeViewModel(_viewModel, employee);
            }
            this.DataContext = _employeeViewModel;
            SetBindingForControls(this._employeeViewModel.TargetObjectType);
            SetOptionOfPhysicianPropertiesEdit();
        }

        #region Controls settings
        private void SetOptionOfPhysicianPropertiesEdit()
        {
            this.Specialization_CB.IsEnabled = this.LicNumber_TB.IsEnabled =
                this._employeeViewModel.TargetObjectType == "lekarz" ? true : false;
        }

        private void OnlyNumbersAllowed(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OnlyLettersAllowed(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Z łćśńźżóęąŁŚŻŹ]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion

        #region Buttons and ComboBoxes actions
        private void ConfirmAdd_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (_employeeViewModel.AddNewEmployee())
            {
                MessageBox.Show("Dodano nowego pracownika do systemu", "Operacja zakończona", MessageBoxButton.OK, MessageBoxImage.Information);                
                Reset_ButtonClick(this, e);
            }
        }

        private void ConfirmEdit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_employeeViewModel.HasTextChanged)
            {
                int index = _viewModel.Employees.IndexOf(_viewModel.Employees.Where(x => x.PESEL == this._employeeViewModel.EmployeeBackup.PESEL).Single());
               
                this._viewModel.Employees.RemoveAt(index);
                _employeeViewModel.AddNewEmployee();
            }
            this.Close();
        }

        private void Cancel_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (this._viewModel.IsEditModeOff == false && this._employeeViewModel.HasTextChanged)
            {
                switch(MessageBox.Show("Czy zachować wprowadzone zmiany?", "Wprowadzono zmiany w formularzu", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        this._viewModel.Employees.Remove(this._viewModel.Employees.Where(x => x.PESEL == this._employeeViewModel.EmployeeBackup.PESEL).Single());
                        _employeeViewModel.AddNewEmployee();
                        break;
                    case MessageBoxResult.No:
                        this._employeeViewModel.Employee = this._employeeViewModel.EmployeeBackup;
                        break;
                }                
            }
            this.Close();
        }

        private void JobTitleCB_DropDownClosed(object sender, EventArgs e)
        {
            this._employeeViewModel.HasTextChanged = true;
            this._employeeViewModel.TargetObjectType = this.JobTitle_CB.Text;
            this._employeeViewModel.ReorganizeDataAccordingToEmployeeType(this._employeeViewModel.TargetObjectType, this._employeeViewModel.Employee);
            SetBindingForControls(this._employeeViewModel.TargetObjectType);
            SetOptionOfPhysicianPropertiesEdit();
        }

        private void Delete_ButtonClick(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Ta operacja spowoduje trwałe usunięcie danych pracownika z systemu. Czy kontynuować?", "Usuwanie danych", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    this._viewModel.Employees.Remove(this._viewModel.Employees
                        .Where(x => x.PESEL == this._employeeViewModel.EmployeeBackup.PESEL).Single());
                    Reset_ButtonClick(this, e);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void Reset_ButtonClick(object sender, RoutedEventArgs e)
        {
            this._employeeViewModel.Employee = new Employee();
            SetBindingForControls(this._employeeViewModel.TargetObjectType = null);
        }
        #endregion

        #region Binding setters
        private void SetBindingForControls(string mode = null)
        {
            string targetObjectType = mode ?? " ";

            BindingOperations.SetBinding(this.Name_TB, TextBox.TextProperty, AddNewBindingWithOptions(targetObjectType, "Name"));
            BindingOperations.SetBinding(this.Surname_TB, TextBox.TextProperty, AddNewBindingWithOptions(targetObjectType, "Surname"));
            BindingOperations.SetBinding(this.JobTitle_CB, ComboBox.TextProperty, AddNewBindingWithOptions(targetObjectType, "JobTitle"));
            BindingOperations.SetBinding(this.Pesel_TB, TextBox.TextProperty, AddNewBindingWithOptions(targetObjectType, "PESEL"));
            BindingOperations.SetBinding(this.Login_TB, TextBox.TextProperty, AddNewBindingWithOptions(targetObjectType, "Login"));
            BindingOperations.SetBinding(this.Password_TB, TextBox.TextProperty, AddNewBindingWithOptions(targetObjectType, "Password"));
            BindingOperations.SetBinding(this.IsAdmin_CheckBox, CheckBox.IsCheckedProperty, AddNewBindingWithOptions(targetObjectType, "IsAdmin"));
            if (targetObjectType == "lekarz")
            {
                BindingOperations.SetBinding(this.Specialization_CB, ComboBox.TextProperty, AddNewBindingWithOptions(targetObjectType, "Specialization"));
                BindingOperations.SetBinding(this.LicNumber_TB, TextBox.TextProperty, AddNewBindingWithOptions(targetObjectType, "LicenceNumber"));
            }
            else
            {
                BindingOperations.ClearAllBindings(this.Specialization_CB);
                BindingOperations.ClearAllBindings(this.LicNumber_TB);
            }
        }
        private Binding AddNewBindingWithOptions(string mode, string propertyPath)
        {
            Binding bd = new Binding();
            switch (mode)
            {
                case "lekarz":
                    bd.Source = _employeeViewModel.Physician;
                    break;
                case "pielęgniarka":
                    bd.Source = _employeeViewModel.Nurse;
                    break;
                default:
                    bd.Source = _employeeViewModel.Employee;
                    break;
            }
            bd.Path = new PropertyPath(propertyPath);
            bd.Mode = BindingMode.TwoWay;
            bd.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            return bd;
        }
        #endregion

        private void TextChangedEventHandler(object sender, TextChangedEventArgs e)
        {
            _employeeViewModel.HasTextChanged = true;
        }

        private void SpecializationChanged(object sender, DataTransferEventArgs e)
        {
            _employeeViewModel.HasTextChanged = true;
        }
    }
}
