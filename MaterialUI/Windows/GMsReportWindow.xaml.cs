using MaterialUI.Class;
using MaterialUI.Database;
using MaterialUI.Pages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


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
            К_Карта card = GymmembershipDataGrid.SelectedItem as К_Карта;
            Клиент client = Connect.Model.Клиент.FirstOrDefault(x => x.Id == card.Клиент);
            AppFrame.FrameMain.Navigate(new ClubCard(client));
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

            if (CountDayOfEnd.Text != "")
            {
                DateTime date = DateTime.Today;
                DateTime date1 = date.AddDays(Convert.ToInt32(CountDayOfEnd.Text));

                GymmembershipDataGrid.ItemsSource = Connect.Model.К_Карта.Where(x => x.Статус == 1).Where(x => x.ДатаОкончания >= date1).ToList();
            }
            else
            {
                GymmembershipDataGrid.ItemsSource = Connect.Model.К_Карта.Where(x => x.Статус == 1).Where(x => x.ДатаОкончания > DateTime.Today).ToList();
            }
            
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

            GymmembershipDataGrid.ItemsSource = Connect.Model.К_Карта.Where(x => x.Статус == 1).Where(x => x.Клиент1.Фамилия.Contains(SearchString.Text)).ToList();
        }

        // Очистка строки поиска
        private void ClearSearchStrin_Click(object sender, RoutedEventArgs e)
        {
            SearchString.Text = "";
        }
    }
}
