using MaterialUI.Class;
using MaterialUI.DateBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public AddEmployeeWindow()
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
            try
            {
                Тренер тренер = new Тренер()
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

                Connect.Model.Тренер.Add(тренер);
                Connect.Model.SaveChanges();
                MessageBox.Show("Запись добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
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
