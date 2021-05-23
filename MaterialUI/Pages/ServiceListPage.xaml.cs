using MaterialUI.Class;
using MaterialUI.Database;
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
    /// Логика взаимодействия для ServiceListPage.xaml
    /// </summary>
    public partial class ServiceListPage : Page
    {
        public ServiceListPage()
        {
            InitializeComponent();
            ServiceDataGrid.ItemsSource = Connect.Model.Услуга.ToList();
            RemoveButton.Visibility = Visibility.Hidden;
        }

        // Открытие окна добавления записи
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Услуга услуга = new Услуга();
            ServiceWindow service = new ServiceWindow(услуга);
            service.ShowDialog();
            ServiceDataGrid.ItemsSource = Connect.Model.Услуга.ToList();
        }

        // Удаление записей
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удаление услуги приведет к потере соответствующих данных клубных карт. Продолжить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    while (ServiceDataGrid.SelectedItems.Count > 0)
                    {
                        Услуга item = ServiceDataGrid.SelectedItem as Услуга;
                        Connect.Model.Услуга.Remove(item);
                        Connect.Model.SaveChanges();
                        ServiceDataGrid.ItemsSource = Connect.Model.Услуга.ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        // Управление отображением кнопки удаления
        private void ServiceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceDataGrid.SelectedItems.Count > 0)
            {
                RemoveButton.Visibility = Visibility.Visible;
            }
            else
            {
                RemoveButton.Visibility = Visibility.Hidden;
            }
        }

        // Кнопка печати
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();
        }

        // Функция поиска
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

            ServiceDataGrid.ItemsSource = Connect.Model.Услуга.Where(x => x.Название.Contains(SearchString.Text)).ToList();
        }

        // Очистка строки поиска
        private void ClearSearchStrin_Click(object sender, RoutedEventArgs e)
        {
            SearchString.Text = "";
        }

        // Открытие редактора двойным кликом
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Услуга услуга = ServiceDataGrid.SelectedItem as Услуга;
            ServiceWindow service = new ServiceWindow(услуга);
            service.ShowDialog();
            ServiceDataGrid.ItemsSource = Connect.Model.Услуга.ToList();
        }

        private void EditGMItem_Click(object sender, RoutedEventArgs e)
        {
            Услуга услуга = ServiceDataGrid.SelectedItem as Услуга;
            ServiceWindow service = new ServiceWindow(услуга);
            service.ShowDialog();
            ServiceDataGrid.ItemsSource = Connect.Model.Услуга.ToList();
        }
    }
}