using MaterialUI.Class;
using MaterialUI.DateBase;
using MaterialUI.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialUI.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Page
    {
        public EmployeeList()
        {
            InitializeComponent();
            EmployeeDataGrid.ItemsSource = Connect.Model.Тренер.ToList();
            RemoveButton.Visibility = Visibility.Hidden;
        }

        private void EmployeeDataGrid_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            grid.UnselectAll();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployee = new AddEmployeeWindow();
            addEmployee.ShowDialog();
            EmployeeDataGrid.ItemsSource = Connect.Model.Тренер.ToList();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            while (EmployeeDataGrid.SelectedItems.Count > 0)
            {
                Тренер service = EmployeeDataGrid.SelectedItem as Тренер;
                Connect.Model.Тренер.Remove(service);
                Connect.Model.SaveChanges();
                EmployeeDataGrid.ItemsSource = Connect.Model.Клиент.ToList();
            }
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItems.Count > 0)
            {
                RemoveButton.Visibility = Visibility.Visible;
            }
            else
            {
                RemoveButton.Visibility = Visibility.Hidden;
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();
        }

        private void SearchString_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchString.Text == "")
            {
                ClearSearchStrin.Visibility = Visibility.Hidden;
            }
            else
            {
                ClearSearchStrin.Visibility = Visibility.Visible;
            }

            EmployeeDataGrid.ItemsSource = Connect.Model.Клиент.Where(x => x.Фамилия.Contains(SearchString.Text)).ToList();
        }

        private void ClearSearchStrin_Click(object sender, RoutedEventArgs e)
        {
            SearchString.Text = "";
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Тренер тренер = EmployeeDataGrid.SelectedItem as Тренер;
            EditEmployeeWindow clientWindow = new EditEmployeeWindow(тренер);
            clientWindow.ShowDialog();
        }
    }
}
