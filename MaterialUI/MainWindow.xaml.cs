using MaterialUI.Class;
using MaterialUI.Database;
using MaterialUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MaterialUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // При необходимости изменять GymDBEntities()
            Connect.Model = new GymDBEntities();

            // Остальное не трогать
            AppFrame.FrameMain = MainFraim;

            ActiveButton.Text = "Главная страница";
            AppFrame.FrameMain.Navigate(new MainPage());
            UpdateStatusAsync();
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
            Application.Current.Shutdown();
        }

        private void MainWindowApp_Click(object sender, RoutedEventArgs e)
        {
            ActiveButton.Text = "Главная страница";
            AppFrame.FrameMain.Navigate(new MainPage());
        }

        private void ClientWindow_Click(object sender, RoutedEventArgs e)
        {
            ActiveButton.Text = "Список клиентов";
            AppFrame.FrameMain.Navigate(new ClientList());
        }

        private void Service_Click(object sender, RoutedEventArgs e)
        {
            ActiveButton.Text = "Список услуг";
            AppFrame.FrameMain.Navigate(new ServiceListPage());
        }

        private void TrainerWindow_Click(object sender, RoutedEventArgs e)
        {
            ActiveButton.Text = "Список тренеров";
            AppFrame.FrameMain.Navigate(new EmployeeList());
        }

        private void GymMembership_Click(object sender, RoutedEventArgs e)
        {
            ActiveButton.Text = "Список абонементов";
            AppFrame.FrameMain.Navigate(new GymmembershipListPage());
        }

        private async void UpdateStatusAsync()
        {
            await Task.Run(() =>
            {
                List<К_Карта> card = Connect.Model.К_Карта.ToList();

                foreach (var item in card)
                {
                    if (item.ДатаОкончания == DateTime.Now.Date) item.Статус = 2;
                }

                Connect.Model.SaveChanges();
            });
        }

        private void Place_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new ListPacePage());
        }
    }
}
