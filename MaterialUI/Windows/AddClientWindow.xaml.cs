using MaterialUI.Class;
using MaterialUI.Database;
using MaterialUI.Pages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MaterialUI.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();

            try
            {
                Gender.SelectedValuePath = "Id";
                Gender.DisplayMemberPath = "Название";
                Gender.ItemsSource = Connect.Model.Пол.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошиба подключения к базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
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

        private void Rectangle_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Files(*.JPG; *.JPEG; *.PNG) | *.JPG; *.JPEG; *.PNG";
            if (fileDialog.ShowDialog() == true)
            {
                string file = fileDialog.FileName;

                BitmapImage image = new BitmapImage(new Uri(file));
                ProfilePhoto.ImageSource = image;

                //Bitmap img = new Bitmap(file);
                //ImageConverter converter = new ImageConverter();
                //byte[] bTemp = (byte[])converter.ConvertTo(img, typeof(byte[]));
                //Helper.Photo = bTemp;
            }
        }

        private void MenuItem_Click(object sender, MouseButtonEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://application:,,," + "/Resources/Images/profile-user.png");
            bitmapImage.EndInit();
            ProfilePhoto.ImageSource = bitmapImage;
        }

        private void CancelAddClient_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForm())
            {
                try
                {
                    Клиент клиент = new Клиент()
                    {
                        Имя = NameCl.Text,
                        Фамилия = Family.Text,
                        Отчество = Patronymic.Text,
                        Телефон = Phone.Text,
                        ДР = (DateTime)BirthDay.SelectedDate,
                        Почта = Email.Text,
                        Паспорт = Multipass.Text,
                        Адрес = Adres.Text,
                        Пол = Convert.ToByte(Gender.SelectedValue),
                        Фото = ImageSourceToBytes(new PngBitmapEncoder(), ProfilePhoto.ImageSource)
                    };

                    Connect.Model.Клиент.Add(клиент);
                    Connect.Model.SaveChanges();
                    MessageBox.Show("Запись добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.FrameMain.Navigate(new ClubCard(клиент));
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Заполните обязательные поля", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Костыль для валидации данных
        private bool CheckForm()
        {
            int counter = 0;
            IEnumerable<TextBox> collection = GridForms.Children.OfType<TextBox>();

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

            if (BirthDay.SelectedDate == null)
            {
                BirthDay.BorderBrush = new SolidColorBrush(Colors.Red);
                BirthDay.BorderThickness = new Thickness(0, 0, 0, 2);
                counter++;
            }
            else
            {
                BirthDay.BorderBrush = new SolidColorBrush(Colors.Gray);
                BirthDay.BorderThickness = new Thickness(0, 0, 0, 1);
                counter--;
            }

            if (Gender.SelectedItem == null)
            {
                Gender.BorderBrush = new SolidColorBrush(Colors.Red);
                Gender.BorderThickness = new Thickness(0, 0, 0, 2);
                counter++;
            }
            else
            {
                Gender.BorderBrush = new SolidColorBrush(Colors.Gray);
                Gender.BorderThickness = new Thickness(0, 0, 0, 1);
                counter--;
            }

            if (counter == -6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        {
            byte[] bytes = null;
            var bitmapSource = imageSource as BitmapSource;

            if (bitmapSource != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }

            return bytes;
        }
    }
}
