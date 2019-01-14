using Hospital_Data;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Hospital_View
{
    /// <summary>
    /// Logika interakcji dla klasy Entry.xaml
    /// </summary>
    public partial class Entry : Window
    {
        private ViewModel _viewModel = new ViewModel();

        public Entry()
        {
            InitializeComponent();            
            statusTextblock.Text = "Podaj login i hasło";            
        }

        private void Exit_ButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_ButtonClick(object sender, RoutedEventArgs e)
        {
           if(_viewModel.VerifyPasswordAndLogin(this.loginTextbox.Text, this.passwordTextbox.Text))
            {
                // SetAdminMode();
                
                MainWindow mainWindow = new MainWindow(_viewModel);                
                mainWindow.Show();
                this.Close();
            }
            else
            {
                statusTextblock.Text = "Nieprawidłowy login lub hasło";
            }
        }

        private void SetAdminMode()
        {
            _viewModel.IsLoggedUserAdmin = _viewModel.Employees
                .Where(x => x.Login == this.loginTextbox.Text)
                .Select(x => (x as Employee).IsAdmin).Single();
        }
    }
}
