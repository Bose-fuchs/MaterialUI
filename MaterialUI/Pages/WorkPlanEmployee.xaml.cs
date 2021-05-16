using MaterialUI.Class;
using MaterialUI.DateBase;
using MaterialUI.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialUI.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkPlanEmployee.xaml
    /// </summary>
    public partial class WorkPlanEmployee : Page
    {
        public string FIO { get; set; }
        public WorkPlanEmployee(Тренер тренер)
        {
            InitializeComponent();
            FIO = тренер.Фамилия.Trim() + " " + тренер.Имя.Trim() + " " + тренер.Отчество.Trim();
            Helper.employee = тренер;
            WorkPlanDataGrid.ItemsSource = Connect.Model.Расписание.Where(x => x.Тренер == тренер.Id).OrderBy(x => x.День).ToList();

            SelectorDay.ItemsSource = Connect.Model.ДниНедели.ToList();
            SelectorDay.DisplayMemberPath = "Название";
            SelectorDay.SelectedValuePath = "Id";

            PlaceWorkText.Text = тренер.Помещение.Название;

            DataContext = this;

        }

        private void BackBatton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AppFrame.FrameMain.GoBack();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            TimeWindow timeWindow = new TimeWindow(textBlock.Text);
            timeWindow.ShowDialog();

            Расписание расписание = WorkPlanDataGrid.SelectedItem as Расписание;

            if (расписание != null)
            {
                switch (textBlock.Name)
                {
                    case "BreakFrom":
                        расписание.ПерерывС = Helper.time;
                        break;
                    case "BreakBefore":
                        расписание.ПерерывДо = Helper.time;
                        break;                    
                    case "WorkWith":
                        расписание.РаботаС = Helper.time;
                        break;                    
                    case "WorkBefore":
                        расписание.РаботаДо = Helper.time;
                        break;
                }

                Connect.Model.SaveChanges();
                WorkPlanDataGrid.ItemsSource = Connect.Model.Расписание.Where(x => x.Тренер == Helper.employee.Id).ToList();
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

            while (WorkPlanDataGrid.SelectedItems.Count > 0)
            {
                Расписание service = WorkPlanDataGrid.SelectedItem as Расписание;
                Connect.Model.Расписание.Remove(service);
                Connect.Model.SaveChanges();
                WorkPlanDataGrid.ItemsSource = Connect.Model.Расписание.Where(x => x.Тренер == Helper.employee.Id).OrderBy(x => x.День).ToList();
            }
            
        }

        private void AddDay_Click(object sender, RoutedEventArgs e)
        {
            if (SelectorDay.SelectedIndex != -1)
            {
                Расписание расписание = new Расписание()
                {
                    День = Convert.ToByte(SelectorDay.SelectedValue),
                    Тренер = Helper.employee.Id,
                    ПерерывС = new TimeSpan(13, 0, 0),
                    ПерерывДо = new TimeSpan(14, 0, 0),
                    РаботаС = new TimeSpan(9, 0, 0),
                    РаботаДо = new TimeSpan(18, 0, 0)
                };

                Connect.Model.Расписание.Add(расписание);
                Connect.Model.SaveChanges();
                WorkPlanDataGrid.ItemsSource = Connect.Model.Расписание.Where(x => x.Тренер == Helper.employee.Id).OrderBy(x => x.День).ToList();
            }
        }
    }
}
