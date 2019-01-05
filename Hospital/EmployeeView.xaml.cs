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

        public EmployeeView(ViewModel viewModel, EmployeeViewModel employeeViewModel = null)
        {
            InitializeComponent();
            _viewModel = viewModel;
            if (employeeViewModel == null)
                employeeViewModel = new EmployeeViewModel();
            this.DataContext = _employeeViewModel = employeeViewModel;            
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
        
        private void Confirm_ButtonClick(object sender, RoutedEventArgs e)
        {
            //var added = _viewModel.AddNewEmployee(this.Name_TB.Text, this.Surname_TB.Text, this.JobTitle_CB.Text, long.Parse(this.Pesel_TB.Text),
            //    this.Specialization_CB.Text, long.Parse(this.LicNumber_TB.Text), this.Login_TB.Text, this.Password_TB.Text,
            //    (bool)this.IsAdmin_CheckBox.IsChecked);            
            //MessageBox.Show("Dodano nowego pracownika do systemu", "Operacja zakończona", MessageBoxButton.OK, MessageBoxImage.Information);

            _viewModel.Employees.Add(_employeeViewModel.Employee);
            
        }

        private void Cancel_ButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void JobTitleCB_DropDownClosed(object sender, EventArgs e)
        {
            if(JobTitle_CB.SelectedValue.ToString() == "lekarz")
            {
                this.Specialization_CB.IsEnabled = this.LicNumber_TB.IsEnabled = true;
            }
        }

        
    }
}
