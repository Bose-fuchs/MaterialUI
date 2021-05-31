using MaterialUI.Class;
using MaterialUI.Database;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MaterialUI.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddGymmembership.xaml
    /// </summary>
    public partial class AddGymmembershipWindow : Window
    {
        public AddGymmembershipWindow()
        {
            InitializeComponent();
            try
            {
                NameGymMS.ItemsSource = Connect.Model.Абонемент.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void AddGMSButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameGymMS.SelectedItem != null)
            {
                Абонемент абонемент = NameGymMS.SelectedItem as Абонемент;
                К_Карта card = new К_Карта
                {
                    Клиент = Helper.client.Id,
                    Абонемент = абонемент.Id,
                    ДатаНачала = DateTime.Now,
                    ДатаОкончания = DateTime.Now.AddDays(абонемент.Длительность),
                    Статус = 1
                };
                Connect.Model.К_Карта.Add(card);
                Connect.Model.SaveChanges();

                this.Close();
            }
            else
                MessageBox.Show("Не выбран абонемент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
