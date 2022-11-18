using System.Windows.Controls;

namespace GraphicalDB.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            AdminBackButton.Click += App.MainWindow.BackButton_Click;
        }
    }
}
