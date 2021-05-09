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
    /// Логика взаимодействия для EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        public EditEmployeeWindow(Тренер тренер)
        {
            InitializeComponent();
            Helper.employee = тренер;

            SetData(тренер);

            try
            {
                Gender.SelectedValuePath = "Id";
                Gender.DisplayMemberPath = "Название";
                Gender.ItemsSource = Connect.Model.Пол.ToList();

                PlaceWork.SelectedValuePath = "Id";
                PlaceWork.DisplayMemberPath = "Название";
                PlaceWork.ItemsSource = Connect.Model.Место_Работы.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошиба подключения к базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                var result = Connect.Model.Тренер.SingleOrDefault(x => x.Id == Helper.employee.Id);
                result.Фамилия = Family.Text;
                result.Имя = NameCl.Text;
                result.Отчество = Patronymic.Text;
                result.Телефон = Phone.Text;
                result.Почта = Email.Text;
                result.Паспорт = Multipass.Text;
                result.ДР = (DateTime)BirthDay.SelectedDate;
                result.Пол = Convert.ToByte(Gender.SelectedValue);
                result.Адрес = Adres.Text;
                result.МестоРаботы = Convert.ToByte(PlaceWork.SelectedValue);
                result.Фото = ImageSourceToBytes(new PngBitmapEncoder(), ProfilePhoto.ImageSource);
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

        public void SetData(Тренер employee)
        {
            try
            {
                Family.Text = employee.Фамилия.Trim();
                NameCl.Text = employee.Имя.Trim();
                Patronymic.Text = employee.Отчество.Trim();
                BirthDay.SelectedDate = employee.ДР;
                Phone.Text = employee.Телефон.Trim();
                Gender.SelectedValue = employee.Пол;
                Email.Text = employee.Почта.Trim();
                Multipass.Text = employee.Паспорт.Trim();
                PlaceWork.SelectedValue = employee.МестоРаботы;
                Adres.Text = employee.Адрес.Trim();
                if (employee.Фото != null)
                {
                    ProfilePhoto.ImageSource = ByteToImageSource(employee.Фото);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
