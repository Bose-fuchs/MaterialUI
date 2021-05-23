using MaterialUI.Class;
using MaterialUI.Database;
using System.Windows;
using System.Windows.Input;

namespace MaterialUI.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddPlaceWindow.xaml
    /// </summary>
    public partial class AddPlaceWindow : Window
    {
        public string NamePlaceProperty { get; set; }

        public AddPlaceWindow(Помещение place)
        {
            InitializeComponent();

            Helper.place = place;

            if (place != null)
            {
                NamePlaceProperty = place.Название.Trim();
            }

            DataContext = this;

        }

        private void NavigationButtons_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Helper.place == null)
            {
                Помещение place = new Помещение()
                {
                    Название = NamePlace.Text
                };

                Connect.Model.Помещение.Add(place);
                Connect.Model.SaveChanges();
            }

            if (Helper.place != null)
            {
                Helper.place.Название = NamePlace.Text;
                Connect.Model.SaveChanges();
            }


            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
