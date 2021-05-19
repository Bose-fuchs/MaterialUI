using MaterialUI.Class;
using MaterialUI.DateBase;
using MaterialUI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MaterialUI.Windows
{
    /// <summary>
    /// Логика взаимодействия для NewVisit.xaml
    /// </summary>
    public partial class NewVisit : Window
    {
        public NewVisit(Клиент клиент)
        {
            InitializeComponent();


            if (клиент != null)
            {
                SelectorClient.IsExpanded = false;
                ClientDataGrid.SelectedItem = клиент;
            }

            //SelectorClient.IsExpanded = true;

            try
            {
                ClientDataGrid.ItemsSource = Connect.Model.Клиент.ToList();

                SelectorService.ItemsSource = Connect.Model.Услуга.ToList();
                SelectorService.DisplayMemberPath = "Название";
                SelectorService.SelectedValuePath = "Id";

                PlaceName.ItemsSource = Connect.Model.Помещение.ToList();
                PlaceName.SelectedValuePath = "Id";
                PlaceName.DisplayMemberPath = "Название";

                Place.Visibility = Visibility.Collapsed;

            }
            catch (Exception)
            {

            }

        }
        private void NavigationButtons_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Клиент клиент = ClientDataGrid.SelectedItem as Клиент;
            EditClientWindow clientWindow = new EditClientWindow(клиент);
            clientWindow.ShowDialog();
            ClientDataGrid.ItemsSource = Connect.Model.Клиент.ToList();
        }
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClient = new AddClientWindow();
            addClient.ShowDialog();
            ClientDataGrid.ItemsSource = Connect.Model.Клиент.ToList();
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

        private void ClientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Клиент client = ClientDataGrid.SelectedItem as Клиент;
            Helper.client = client;
            К_Карта GMs = Connect.Model.К_Карта.Where(x => x.Клиент == client.Id).ToList().LastOrDefault();

            if (GMs != null && GMs.Статус == 1)
            {
                GMsCheckBox.IsEnabled = true;
                GMsCheckBox.IsChecked = true;
            } else
            {
                GMsCheckBox.IsEnabled = false;
                GMsCheckBox.IsChecked = false;
            }

            if (ClientDataGrid.SelectedIndex != -1)
            {
                Purpose.IsExpanded = true;
                Purpose.IsEnabled = true;

                Thread.Sleep(150);
                SelectorClient.IsExpanded = false;
            }

        }

        private void SelectorService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GMsCheckBox.IsChecked = false;
            Place.Visibility = Visibility.Collapsed;
        }

        private void GMsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectorService.SelectedIndex = -1;
            GMsCheckBox.IsChecked = true;
            Place.Visibility = Visibility.Visible;
        }

        private void PlaceName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int numberday = Convert.ToByte(DateTime.Now.DayOfWeek.ToString("D"));
            int index = Convert.ToByte(PlaceName.SelectedIndex) + 1;
            EmployeeWork.ItemsSource = Connect.Model.Расписание.Where(x => x.День == numberday).Where(y => y.Тренер1.МестоРаботы == index).ToList();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            Посещения visit = null;

            if (GMsCheckBox.IsChecked == true 
                && ClientDataGrid.SelectedIndex != -1
                && DateVisit.SelectedDate != null
                && TimeVisit.SelectedTime != null
                && PlaceName.SelectedIndex != -1)
            {
                visit = new Посещения()
                {
                    Клиент = Helper.client.Id,
                    Дата = (DateTime)DateVisit.SelectedDate,
                    Время = (TimeSpan)(TimeVisit.SelectedTime - DateTime.Today),
                    Помещение = Convert.ToByte(PlaceName.SelectedValue),
                    Услуга = null
                };

                Connect.Model.Посещения.Add(visit);
                Connect.Model.SaveChanges();
                this.Close();
            } else
            {
                if (GMsCheckBox.IsChecked == false
                && ClientDataGrid.SelectedIndex != -1
                && SelectorService.SelectedIndex != -1
                && DateVisit.SelectedDate != null
                && TimeVisit.SelectedTime != null)
                {
                    visit = new Посещения()
                    {
                        Клиент = Helper.client.Id,
                        Дата = (DateTime)DateVisit.SelectedDate,
                        Время = (TimeSpan)(TimeVisit.SelectedTime - DateTime.Today),
                        Помещение = null,
                        Услуга = Convert.ToByte(SelectorService.SelectedValue)
                    };

                    Connect.Model.Посещения.Add(visit);
                    Connect.Model.SaveChanges();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Обязательные поля не заполнены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
    }
}
