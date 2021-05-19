using MaterialUI.Class;
using MaterialUI.DateBase;
using MaterialUI.Pages;
using QRCoder;
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
    /// Логика взаимодействия для GMsReport.xaml
    /// </summary>
    public partial class GMsReportWindow : Window
    {
        public string FIO { get; set; }
        public string Date { get; set; }
        public string ActiveGMs { get; set; }
        public string AllCountGMs { get; set; }

        public GMsReportWindow()
        {
            InitializeComponent();

            GymmembershipDataGrid.ItemsSource = Connect.Model.К_Карта.Where(x => x.Статус == 1).ToList();
            ActiveGMs = "Действующих абонементов: " + Connect.Model.К_Карта.Where(x => x.Статус == 1).Count().ToString();
            AllCountGMs = "Продано абонементов: " + Connect.Model.К_Карта.Count().ToString();

            //QRCodeGenerator QRCodeGenerator = new QRCodeGenerator();
            //QRCodeData qRCode = QRCodeGenerator.CreateQrCode("Хер тебе", QRCodeGenerator.ECCLevel.Q);
            //QRCode code = new QRCode(qRCode);

            this.DataContext = this;
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

        private void StopGMSButton_Click(object sender, RoutedEventArgs e)
        {
            К_Карта card = GymmembershipDataGrid.SelectedItem as К_Карта;
            card.Статус = 2;
            Connect.Model.SaveChanges();

            GymmembershipDataGrid.ItemsSource = Connect.Model.К_Карта.Where(x => x.Статус == 1).ToList();
        }

        private void ClubCardItem_Click(object sender, RoutedEventArgs e)
        {
            //К_Карта card = GymmembershipDataGrid.SelectedItem as К_Карта;
            //Клиент client = Connect.Model.Клиент.FirstOrDefault(x => x.Id == card.Клиент);
            //AppFrame.FrameMain.Navigate(new ClubCard(client));
        }


        private void CountDayOfEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CountDayOfEnd.Text == "")
            {
                ClearDayCount.Visibility = Visibility.Hidden;
            }
            else
            {
                ClearDayCount.Visibility = Visibility.Visible;
            }

            //GymmembershipDataGrid.ItemsSource = Connect.Model.К_Карта.Where(x => x.Статус == 1).Where(x => x.ДатаОкончания - DateTime.Today > Convert.ToInt32(CountDayOfEnd)))

            //ClientDataGrid.ItemsSource = Connect.Model.Клиент.Where(x => x.Фамилия.Contains(SearchString.Text)).ToList();
        }

        private void PrintButto1n_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();
        }

        private void ClearDayCount_Click(object sender, RoutedEventArgs e)
        {
            CountDayOfEnd.Text = "";
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

            //ClientDataGrid.ItemsSource = Connect.Model.Клиент.Where(x => x.Фамилия.Contains(SearchString.Text)).ToList();
        }

        // Очистка строки поиска
        private void ClearSearchStrin_Click(object sender, RoutedEventArgs e)
        {
            SearchString.Text = "";
        }
    }
}
