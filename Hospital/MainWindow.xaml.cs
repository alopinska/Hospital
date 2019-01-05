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
            this.DataContext = this;
            this.listView.DataContext = _viewModel._Hospital.Staff;
            
        }

        private void Exit_ButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Refresh()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(listView.ItemsSource);
            view.Refresh();
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
            var employeeData = new EmployeeView(this._viewModel);
            employeeData.ShowDialog();
            
        }
    }
}
