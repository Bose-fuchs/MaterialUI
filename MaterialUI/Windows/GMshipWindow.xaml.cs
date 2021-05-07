using MaterialUI.Class;
using MaterialUI.DateBase;
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
    /// Логика взаимодействия для EditGMWindow.xaml
    /// </summary>
    public partial class GMshipWindow : Window
    {
        public GMshipWindow(Абонемент абонемент)
        {
            InitializeComponent();

            Helper.gmb = абонемент;
            DataContext = абонемент;
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

            if (counter == -3) SaveData();
        }

        private void SaveData()
        {
            if (Helper.service.Id != 0)
            {
                Абонемент gmb = Connect.Model.Абонемент.Where(x => x.Id == Helper.gmb.Id).FirstOrDefault();
                gmb.Название = NameGMs.Text;
                gmb.Длительность = Convert.ToInt16(DurationGMs.Text);
                gmb.Стоимость = Convert.ToDecimal(PriceGMs.Text.Replace('.', ','));
                Connect.Model.SaveChanges();

                this.Close();
            }
            else
            {
                Абонемент gmb = new Абонемент()
                {
                    Название = NameGMs.Text,
                    Длительность = Convert.ToInt16(DurationGMs.Text),
                    Стоимость = Convert.ToDecimal(PriceGMs.Text.Replace('.', ','))
                };
                Connect.Model.Абонемент.Add(gmb);
                Connect.Model.SaveChanges();

                this.Close();
            }
        }
    }
}
