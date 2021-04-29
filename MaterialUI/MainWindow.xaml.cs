using MaterialUI.Class;
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
            Connect.Model = new DateBase.GymDBEntities2();
            AppFrame.FrameMain = MainFraim;
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

        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            //AppFrame.FrameMain.Navigate(new GymMembership());
        }

        private void ClientWindow_Click(object sender, RoutedEventArgs e)
        { 
            AppFrame.FrameMain.Navigate(new ClientList());
        }

        private void Exercise_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TrainerWindow_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new EmployeeList());
        }
    }
}
