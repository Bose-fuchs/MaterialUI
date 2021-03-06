using MaterialUI.Class;
using MaterialUI.Database;
using MaterialUI.Pages;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MaterialUI.Windows
{
    /// <summary>
    /// Логика взаимодействия для DistributionWindow.xaml
    /// </summary>
    public partial class DistributionWindow : Window
    {
        public string NamePl { get; set; }

        public DistributionWindow(Помещение place)
        {
            InitializeComponent();

            NamePl = place.Название;
            DistributionDG.ItemsSource = Connect.Model.Тренер.Where(x => x.МестоРаботы == place.Id).ToList();

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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Тренер тренер = DistributionDG.SelectedItem as Тренер;
            EditEmployeeWindow editEmployee = new EditEmployeeWindow(тренер);
            editEmployee.ShowDialog();
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            Тренер тренер = DistributionDG.SelectedItem as Тренер;
            AppFrame.FrameMain.Navigate(new WorkPlanEmployee(тренер));
        }
    }
}
