using MaterialUI.Class;
using MaterialUI.Database;
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
using System.Windows.Shapes;

namespace MaterialUI.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServiceReportWindow.xaml
    /// </summary>
    public partial class ServiceReportWindow : Window
    {
        public string CountP { get; set; }
        public string Amount { get; set; }

        public ServiceReportWindow()
        {
            InitializeComponent();
            ServiceDG.ItemsSource = Connect.Model.Услуга.ToList();

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

        private void ServiceDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Услуга услуга = ServiceDG.SelectedItem as Услуга;
            ClientDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Услуга == услуга.Id).ToList();

            List<Посещения> list = Connect.Model.Посещения.Where(x => x.Услуга == услуга.Id).ToList();

            CountP = "Всего продано: " + list.Count.ToString();

            int count = 0;
            foreach (var item in list)
            {
                count += Convert.ToInt32(item.Услуга1.Стоимость);
            }
            Amount = "Продано на " + count.ToString() + " рублей";

            DataContext = this;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Посещения посещения = ClientDG.SelectedItem as Посещения;
            Клиент клиент = Connect.Model.Клиент.Where(x => x.Id == посещения.Клиент).FirstOrDefault();
            AppFrame.FrameMain.Navigate(new ClubCard(клиент));
        }
    }
}
