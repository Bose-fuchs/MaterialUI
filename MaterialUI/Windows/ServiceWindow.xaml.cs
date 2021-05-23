using MaterialUI.Class;
using MaterialUI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MaterialUI.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public ServiceWindow(Услуга услуга)
        {
            InitializeComponent();

            Helper.service = услуга;
            DataContext = услуга;
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            IEnumerable<TextBox> collection = DataForm.Children.OfType<TextBox>();

            foreach (var item in collection)
            {
                if (item.Text == "")
                {
                    item.BorderBrush = new SolidColorBrush(Colors.Red);
                    item.BorderThickness = new Thickness(0, 0, 0, 2);
                    counter++;
                }
                else
                {
                    item.BorderBrush = new SolidColorBrush(Colors.Gray);
                    item.BorderThickness = new Thickness(0, 0, 0, 1);
                    counter--;
                }
            }

            if (counter == -2) SaveData();
        }

        private void SaveData()
        {
            if (Helper.service.Id != 0)
            {
                Услуга service = Connect.Model.Услуга.Where(x => x.Id == Helper.service.Id).FirstOrDefault();
                service.Название = NameService.Text.Trim();
                service.Стоимость = Convert.ToDecimal(PriceService.Text.Replace('.', ','));
                Connect.Model.SaveChanges();

                this.Close();
            }
            else
            {
                Услуга service = new Услуга()
                {
                    Название = NameService.Text,
                    Стоимость = Convert.ToDecimal(PriceService.Text.Replace('.', ','))
                };
                Connect.Model.Услуга.Add(service);
                Connect.Model.SaveChanges();

                this.Close();
            }
        }
    }
}
