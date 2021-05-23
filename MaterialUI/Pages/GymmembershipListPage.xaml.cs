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
    /// Логика взаимодействия для GymmembershipListPage.xaml
    /// </summary>
    public partial class GymmembershipListPage : Page
    {
        public GymmembershipListPage()
        {
            InitializeComponent();
            GMsDataGrid.ItemsSource = Connect.Model.Абонемент.ToList();
            RemoveButton.Visibility = Visibility.Hidden;
        }

        // Открытие окна добавления записи
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Абонемент абонемент = new Абонемент();
            GMshipWindow clientWindow = new GMshipWindow(абонемент);
            clientWindow.ShowDialog();
            GMsDataGrid.ItemsSource = Connect.Model.Абонемент.ToList();
        }

        // Удаление записей
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удаление абонемента приведет к потере соответствующих данных клубных карт. Продолжить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    while (GMsDataGrid.SelectedItems.Count > 0)
                    {
                        Абонемент item = GMsDataGrid.SelectedItem as Абонемент;
                        Connect.Model.Абонемент.Remove(item);
                        Connect.Model.SaveChanges();
                        GMsDataGrid.ItemsSource = Connect.Model.Абонемент.ToList();
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
            if (GMsDataGrid.SelectedItems.Count > 0)
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

            GMsDataGrid.ItemsSource = Connect.Model.Абонемент.Where(x => x.Название.Contains(SearchString.Text)).ToList();
        }

        // Очистка строки поиска
        private void ClearSearchStrin_Click(object sender, RoutedEventArgs e)
        {
            SearchString.Text = "";
        }

        // Открытие редактора двойным кликом
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Абонемент абонемент = GMsDataGrid.SelectedItem as Абонемент;
            GMshipWindow clientWindow = new GMshipWindow(абонемент);
            clientWindow.ShowDialog();
            GMsDataGrid.ItemsSource = Connect.Model.Абонемент.ToList();
        }

        // Открытие редактора через ПКМ
        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Абонемент абонемент = GMsDataGrid.SelectedItem as Абонемент;
            GMshipWindow clientWindow = new GMshipWindow(абонемент);
            clientWindow.ShowDialog();
            GMsDataGrid.ItemsSource = Connect.Model.Абонемент.ToList();
        }
    }
}
