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
                _employeeViewModel = new EmployeeViewModel(this._viewModel.IsEditModeOff);
            }
            else
            {
                _employeeViewModel = new EmployeeViewModel(this._viewModel.IsEditModeOff, employee);
            }
            this.DataContext = _employeeViewModel;
            SetBindingForControls(this._employeeViewModel.TargetObjectType);
            SetOptionOfPhysicianPropertiesEdit();
        }

        private void OnlyNumbersAllowed(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void OnlyLettersAllowed(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Z łćśńźżóęą]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ConfirmAdd_ButtonClick(object sender, RoutedEventArgs e)
        {           
            switch (this._employeeViewModel.TargetObjectType)
            {
                case "lekarz":
                    _viewModel.Employees.Add(this._employeeViewModel.Physician);
                    break;
                case "pielęgniarka":
                    _viewModel.Employees.Add(this._employeeViewModel.Nurse);
                    break;
                default:
                    _viewModel.Employees.Add(this._employeeViewModel.Employee);
                    break;
            }
            MessageBox.Show("Dodano nowego pracownika do systemu", "Operacja zakończona", MessageBoxButton.OK, MessageBoxImage.Information);
            //dodać walidację : taki pracownik już istnieje, by edytować jego dane wybierz w widoku głównym... blabla                      
        }

        private void ConfirmEdit_Button_Click(object sender, RoutedEventArgs e)
        {
            //walidacja: czy cokolwiek się zmieniło?
            //przeładować aktualnego jako nowego, usunąć starego
            this._viewModel.Employees.Remove(this._viewModel.Employees.Where(x => x.PESEL == this._employeeViewModel.EmployeeBackup.PESEL).Single());
            ConfirmAdd_ButtonClick(this, e);
            //this._viewModel.Employees.Add(this._employeeViewModel.Employee);
        }

        private void Cancel_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (this._viewModel.IsEditModeOff == false)
            {     
                //walidacja: czy cokolwiek się zmieniło?
                //jeśli tak - pytajka, czy zachować wprowadzone zmiany
                //tak: confirm edit, nie: poniższe
                this._employeeViewModel.Employee = this._employeeViewModel.EmployeeBackup;
            }
            this.Close();
        }

        private void JobTitleCB_DropDownClosed(object sender, EventArgs e)
        {
            this._employeeViewModel.TargetObjectType = this.JobTitle_CB.Text; // tutaj jest problem z przejściem z lekarza na cokolwiek innego
            this._employeeViewModel.ReorganizeDataAccordingToEmployeeType(this._employeeViewModel.TargetObjectType, this._employeeViewModel.Employee);
            SetBindingForControls(this._employeeViewModel.TargetObjectType);
            SetOptionOfPhysicianPropertiesEdit();
        }

        private void Delete_ButtonClick(object sender, RoutedEventArgs e)
        {
            switch(MessageBox.Show("Ta operacja spowoduje trwałe usunięcie danych pracownika z systemu. Czy kontynuować?", "Usuwanie danych", MessageBoxButton.YesNo, MessageBoxImage.Warning))
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

        private void SetOptionOfPhysicianPropertiesEdit()
        {
            this.Specialization_CB.IsEnabled = this.LicNumber_TB.IsEnabled =
                this._employeeViewModel.TargetObjectType == "lekarz" ? true : false;            
        }

        private void SetBindingForControls(string mode = null)
        {
            string targetObjectType = mode == null ? " " : mode;

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
    }
}
