using MaterialUI.Class;
using MaterialUI.DateBase;
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

        private string _FIO;
        public string FIO
        {
            get 
            { 
                return _FIO; 
            }

            set
            {
                _FIO = value;
            }
        }

        private string _Date;
        public string Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
            }
        }

        public ClubCard(Клиент клиент)
        {
            InitializeComponent();
            DataContext = this;
            Helper.client = клиент;
            GymmembershipDataGrid.ItemsSource = Connect.Model.К_Карта.Where(x => x.Клиент == клиент.Id).OrderBy(x => x.Статус1.Название).ToList();
            _FIO = клиент.Фамилия.Trim() + " " + клиент.Имя.Trim() + " " + клиент.Отчество;
            _Date = клиент.ДатаРегистрации.ToString("yyyy.MM.dd");
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
    }
}
