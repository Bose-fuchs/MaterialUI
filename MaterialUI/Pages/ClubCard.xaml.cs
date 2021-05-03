using MaterialUI.Class;
using MaterialUI.DateBase;
using System;
using System.Linq;
using System.Windows.Controls;

namespace MaterialUI.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClubCard.xaml
    /// </summary>
    public partial class ClubCard : Page
    {

        private string _FIO;
        public string FIO
        {
            get 
            { 
                return _FIO; 
            }

            set
            {
                _FIO = value;
            }
        }

        private string _Date;
        public string Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
            }
        }

        public ClubCard(Клиент клиент)
        {
            InitializeComponent();
            DataContext = this;
            TestDG.ItemsSource = Connect.Model.К_Карта.Where(x => x.Клиент == клиент.Id).ToList();
            Combo.ItemsSource = Connect.Model.Статус.ToList();
            _FIO = клиент.Фамилия.Trim() + " " + клиент.Имя.Trim() + " " + клиент.Отчество;
            _Date = клиент.ДатаРегистрации.ToString("yyyy.MM.dd");
        }
    }
}
