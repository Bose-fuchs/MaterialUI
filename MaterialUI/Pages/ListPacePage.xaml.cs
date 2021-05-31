using MaterialUI.Class;
using MaterialUI.Database;
using MaterialUI.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaterialUI.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListPacePage.xaml
    /// </summary>
    public partial class ListPacePage : Page
    {
        public ListPacePage()
        {
            InitializeComponent();

            PlaceList.ItemsSource = Connect.Model.Помещение.ToList();
        }

        private void PlaceEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlaceList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Помещение помещение = PlaceList.SelectedItem as Помещение;
            AddPlaceWindow addPlace = new AddPlaceWindow(помещение);
            addPlace.ShowDialog();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPlace_Click(object sender, RoutedEventArgs e)
        {
            Помещение place = null;
            AddPlaceWindow addPlace = new AddPlaceWindow(place);
            addPlace.ShowDialog();

            PlaceList.ItemsSource = Connect.Model.Помещение.ToList();
        }

        private void EditItemClick(object sender, RoutedEventArgs e)
        {
            Помещение помещение = PlaceList.SelectedItem as Помещение;
            AddPlaceWindow addPlace = new AddPlaceWindow(помещение);
            addPlace.ShowDialog();

            PlaceList.ItemsSource = Connect.Model.Помещение.ToList();
        }

        private void DeleItemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Удаление данной записи может привести к потере данных. Продолжить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Помещение помещение = PlaceList.SelectedItem as Помещение;
                    Connect.Model.Помещение.Remove(помещение);
                    Connect.Model.SaveChanges();

                    PlaceList.ItemsSource = Connect.Model.Помещение.ToList();
                }
            }
            catch (Exception)
            {

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Помещение помещение = PlaceList.SelectedItem as Помещение;
            DistributionWindow distribution = new DistributionWindow(помещение);
            distribution.ShowDialog();
        }
    }
}
