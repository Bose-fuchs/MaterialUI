using MaterialUI.Class;
using MaterialUI.DateBase;
using MaterialUI.Pages;
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
            Connect.Model = new DateBase.GymDBEntities();
            AppFrame.FrameMain = MainFraim;

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
            AppFrame.FrameMain.Navigate(new MainPage());
        }

        private void ClientWindow_Click(object sender, RoutedEventArgs e)
        { 
            AppFrame.FrameMain.Navigate(new ClientList());
        }

        private void Service_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new ServiceListPage());
        }

        private void TrainerWindow_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new EmployeeList());
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

        private void GymMembership_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new GymmembershipListPage());
        }
    }
}
