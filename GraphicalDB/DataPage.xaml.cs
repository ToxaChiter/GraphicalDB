using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace GraphicalDB;

/// <summary>
/// Логика взаимодействия для DataPage.xaml
/// </summary>
public partial class DataPage : Page
{
    internal ObservableCollection<Participant> Participants { get; set; }

    public DataPage()
    {
        InitializeComponent();

        using var context = new MyDbContext();
        Participants = new(context.Participants);
        TableDataGrid.ItemsSource = Participants;
    }
}
