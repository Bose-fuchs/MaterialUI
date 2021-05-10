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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            //List<Посещения> посещения = Connect.Model.Посещения.ToList();

            //foreach (var item in посещения)
            //{
            //    if (item.Услуга != null)
            //        item.Услуга1.Название.Trim();
            //}

            MainDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Дата == DateTime.Today).OrderBy(x => x.Время).ToList();
        }

        private void OneDateFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MainDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Дата >= OneDateFilter.SelectedDate).ToList();
        }

        private void TwoDateFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MainDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Дата <= TwoDateFilter.SelectedDate).ToList();
        }

        private void TodayRecords_Click(object sender, RoutedEventArgs e)
        {

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

            NewVisit newVisit = new NewVisit();
            newVisit.ShowDialog();

            MainDG.ItemsSource = Connect.Model.Посещения.Where(x => x.Дата == DateTime.Today).OrderBy(x => x.Время).ToList();
        }
    }
}
