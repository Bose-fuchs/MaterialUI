using MaterialUI.Class;
using MaterialUI.Database;
using MaterialUI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaterialUI.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClubCard.xaml
    /// </summary>
    public partial class ClubCard : Page
    {

        public string FIO { get; set; }
        public string Date { get; set; }
        public string AmountG { get; set; }
        public string AmountS { get; set; }

        public ClubCard(Клиент клиент)
        {
            InitializeComponent();

            DataContext = this;
            Helper.client = клиент;


            int amount = 0;
            int count = клиент.К_Карта.Count;

            List<К_Карта> list = Connect.Model.К_Карта.Where(x => x.Клиент == клиент.Id).ToList();

            foreach (var item in list)
            {
                amount += Convert.ToInt32(item.Абонемент1.Стоимость);
            }

            AmountG = "Количество преобретенных абонементов: " + count + ", общая стоимость: " + amount + " рублей";

            count = 0;
            amount = 0;

            List<Посещения> visit = Connect.Model.Посещения.Where(x => x.Клиент == клиент.Id).Where(x => x.Услуга != null).ToList();

            count = visit.Count;

            foreach (var item in visit)
            {
                amount += Convert.ToInt32(item.Услуга1.Стоимость);
            }
            AmountS = "Количество преобретенных услуг: " + count + ", общая стоимость: " + amount + " рублей";

            GymmembershipDataGrid.ItemsSource = Connect.Model.К_Карта.Where(x => x.Клиент == клиент.Id).OrderBy(x => x.Статус1.Название).ToList();
            ServicesDataGrid.ItemsSource = Connect.Model.Посещения.Where(x => x.Клиент == клиент.Id).Where(x => x.Услуга != null).ToList();
            FIO = клиент.Фамилия.Trim() + " " + клиент.Имя.Trim() + " " + клиент.Отчество;
            Date = клиент.ДатаРегистрации.ToString("yyyy.MM.dd");
        }

        private void BackBatton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AppFrame.FrameMain.GoBack();
        }

        private void AddGymmembership_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            К_Карта test = Connect.Model.К_Карта.Where(x => x.Клиент == Helper.client.Id).ToList().LastOrDefault();

            if (test == null || test.Статус == 2)
                if (MessageBox.Show("Активного абонемента не найдено. Добавить новый?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AddGymmembershipWindow gymmembershipWindow = new AddGymmembershipWindow();
                    gymmembershipWindow.ShowDialog();
                    GymmembershipDataGrid.ItemsSource = Connect.Model.К_Карта.Where(x => x.Клиент == Helper.client.Id).OrderBy(x => x.Статус1.Название).ToList();
                }
                    

            

            if (test != null && test.Статус == 1)
                MessageBox.Show("Есть действующий абонемент", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void StopGMSButton_Click(object sender, RoutedEventArgs e)
        {
            К_Карта card = GymmembershipDataGrid.SelectedItem as К_Карта;
            card.Статус = 2;
            Connect.Model.SaveChanges();

            GymmembershipDataGrid.ItemsSource = Connect.Model.К_Карта.Where(x => x.Клиент == Helper.client.Id).OrderBy(x => x.Статус1.Название).ToList();
        }
    }
}
