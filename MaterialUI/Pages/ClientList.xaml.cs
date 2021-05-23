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
    /// Логика взаимодействия для ClientList.xaml
    /// </summary>
    public partial class ClientList : Page
    {
        public ClientList()
        {
            InitializeComponent();
            ClientDataGrid.ItemsSource = Connect.Model.Клиент.ToList();
            RemoveButton.Visibility = Visibility.Hidden;
        }

        // Открытие окна добавления записи
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClient = new AddClientWindow();
            addClient.ShowDialog();
            ClientDataGrid.ItemsSource = Connect.Model.Клиент.ToList();
        }

        // Удаление записей
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            while (ClientDataGrid.SelectedItems.Count > 0)
            {
                Клиент service = ClientDataGrid.SelectedItem as Клиент;
                Connect.Model.Клиент.Remove(service);
                Connect.Model.SaveChanges();
                ClientDataGrid.ItemsSource = Connect.Model.Клиент.ToList();
            }
        }

        // Управление отображением кнопки удаления
        private void ClientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientDataGrid.SelectedItems.Count > 0)
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

            ClientDataGrid.ItemsSource = Connect.Model.Клиент.Where(x => x.Фамилия.Contains(SearchString.Text)).ToList();
        }

        // Очистка строки поиска
        private void ClearSearchStrin_Click(object sender, RoutedEventArgs e)
        {
            SearchString.Text = "";
        }

        // Открытие редактора двойным кликом
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Клиент клиент = ClientDataGrid.SelectedItem as Клиент;
            EditClientWindow clientWindow = new EditClientWindow(клиент);
            clientWindow.ShowDialog();
            ClientDataGrid.ItemsSource = Connect.Model.Клиент.ToList();
        }

        // Открытие редактора через ПКМ
        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного клиента
            Клиент клиент  = ClientDataGrid.SelectedItem as Клиент;
            // Создаем страницу и передаём параметр (выбранного клиента)
            EditClientWindow clientWindow = new EditClientWindow(клиент);
            // Открываем окно
            clientWindow.ShowDialog();
            // Обновление записей в "родительском" окне после изменения
            ClientDataGrid.ItemsSource = Connect.Model.Клиент.ToList();
        }

        // Открытие клубной карты через ПКМ 
        private void CardMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Клиент клиент = ClientDataGrid.SelectedItem as Клиент;
            AppFrame.FrameMain.Navigate(new ClubCard(клиент));
        }
    }
}
