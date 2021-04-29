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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {

        public EditClientWindow(Клиент клиент)
        {
            InitializeComponent();
            Helper.client = клиент;

            SetData(клиент);

            try
            {
                Gender.SelectedValuePath = "Id";
                Gender.DisplayMemberPath = "Название";
                Gender.ItemsSource = Connect.Model.Пол.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошиба подключения к базе данных","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
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

        public static ImageSource ByteToImageSource(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        private void SaveClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = Connect.Model.Клиент.SingleOrDefault(x => x.Id == Helper.client.Id);
                result.Фамилия = Family.Text;
                result.Имя = NameCl.Text;
                result.Отчество = Patronymic.Text;


                Connect.Model.SaveChanges();
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CancelEditClient_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SetData(Клиент client)
        {
            try
            {
                Family.Text = client.Фамилия.Trim();
                NameCl.Text = client.Имя.Trim();
                Patronymic.Text = client.Отчество.Trim();
                BirthDay.SelectedDate = client.ДР;
                Phone.Text = client.Телефон.Trim();
                Gender.SelectedValue = client.Пол;
                Email.Text = client.Почта.Trim();
                Multipass.Text = client.Паспорт.Trim();
                Adres.Text = client.Адрес.Trim();
                if (client.Фото != null)
                {
                    ProfilePhoto.ImageSource = ByteToImageSource(client.Фото);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
