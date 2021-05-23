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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            MainDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Дата == DateTime.Today).OrderBy(x => x.Время).ToList();

            TodayRecords.IsChecked = true;

        }

        private void OneDateFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TodayRecords.IsChecked = false;
            MainDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Дата >= OneDateFilter.SelectedDate).ToList();
        }

        private void TwoDateFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TodayRecords.IsChecked = false;
            MainDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Дата >= OneDateFilter.SelectedDate).Where(x => x.Дата <= TwoDateFilter.SelectedDate).ToList();
        }

        private void MoreRowItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClubCardItem_Click(object sender, RoutedEventArgs e)
        {
            Посещения card = MainDG.SelectedItem as Посещения;
            Клиент client = Connect.Model.Клиент.Where(x => x.Id == card.Клиент).FirstOrDefault();

            client.Id = (int)card.Клиент;
 
            AppFrame.FrameMain.Navigate(new ClubCard(client));
        }

        private void AddVisit_Click(object sender, RoutedEventArgs e)
        {
            Клиент клиент = null;

            NewVisit newVisit = new NewVisit(клиент);
            newVisit.ShowDialog();

            MainDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Дата == DateTime.Today).OrderBy(x => x.Время).ToList();
        }

        private void AddVisitInCM_Click(object sender, RoutedEventArgs e)
        {
            Посещения посещения = MainDG.SelectedItem as Посещения;

            Клиент клиент = Connect.Model.Клиент.Where(x => x.Id == посещения.Клиент).FirstOrDefault();

            NewVisit newVisit = new NewVisit(клиент);
            newVisit.ShowDialog();

            MainDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Дата >= DateTime.Today).OrderBy(x => x.Дата).ToList();
        }

        private void TodayRecords_Checked(object sender, RoutedEventArgs e)
        {
            OneDateFilter.SelectedDate = null;
            TwoDateFilter.SelectedDate = null;
            MainDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Дата == DateTime.Today).OrderBy(x => x.Дата).ToList();

        }

        private void GMsReport_Click(object sender, RoutedEventArgs e)
        {
            GMsReportWindow reportWindow = new GMsReportWindow();
            reportWindow.Show();
        }

        private void ServiceReport_Click(object sender, RoutedEventArgs e)
        {
            ServiceReportWindow serviceReport = new ServiceReportWindow();
            serviceReport.ShowDialog();
        }

        private void PrintVisitList_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.CurrentPageEnabled = true;
            printDialog.ShowDialog();
        }

    }
}
