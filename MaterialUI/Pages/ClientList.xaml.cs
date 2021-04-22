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

        private void ClientDataGrid_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            grid.UnselectAll();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClient = new AddClientWindow();
            addClient.ShowDialog();
            ClientDataGrid.ItemsSource = Connect.Model.Клиент.ToList();
        }

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

            ClientDataGrid.ItemsSource = Connect.Model.Клиент.Where(x => x.Фамилия.Contains(SearchString.Text)).ToList();
        }

        private void ClearSearchStrin_Click(object sender, RoutedEventArgs e)
        {
            SearchString.Text = "";
        }
    }
}
