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

            DataContext = this;
            FIO = тренер.Фамилия.Trim() + " " + тренер.Имя.Trim() + " " + тренер.Отчество.Trim();
            Helper.employee = тренер;
            WorkPlanDataGrid.ItemsSource = Connect.Model.Расписание.Where(x => x.Id == тренер.Id).ToList();
        }

        private void BackBatton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AppFrame.FrameMain.GoBack();
        }
    }
}
