using MaterialUI.Class;
using System;
using System.Windows;
using System.Windows.Input;

namespace MaterialUI.Windows
{
    /// <summary>
    /// Логика взаимодействия для TimeWindow.xaml
    /// </summary>
    public partial class TimeWindow : Window
    {
        public string DefaultText;
        public TimeWindow(string defaultText)
        {
            InitializeComponent();

            DefaultText = defaultText;
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



        private void SaveTimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (TimeWork.SelectedTime != null)
            {
                DateTime dateTime = (DateTime)TimeWork.SelectedTime;
                Helper.time = dateTime - DateTime.Today;
            }
            else
            {
                Helper.time = new TimeSpan(Convert.ToInt32(DefaultText));
            }

            this.Close();
        }
    }
}
