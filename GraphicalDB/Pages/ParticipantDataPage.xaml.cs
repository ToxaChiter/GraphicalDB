using System.Collections.ObjectModel;
using System.Windows.Controls;
using GraphicalDB.DataBase;

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

        //using var context = new MyDbContext();
        //Participants = new(context.Participants);
        //TableDataGrid.ItemsSource = Participants;
        Participants = new ObservableCollection<Participant>
        {
            new Participant { Id = 0, Name = "Anthony", BirthYear = 2003, Country = "Belarus", Instrument = Instrument.Piano, Place = 0 },
            new Participant { Id = 1, Name = "Antony", BirthYear = 2004, Country = "Russia", Instrument = Instrument.Cello, Place = 1 },
            new Participant { Id = 2, Name = "Anton", BirthYear = 2005, Country = "USA", Instrument = Instrument.Guitar, Place = 2 },
            new Participant { Id = 3, Name = "Лобарев Антон Михайлович", BirthYear = 2001, Country = "Беларусь", Instrument = Instrument.Guitar, Place = 3 }
        };
        TableDataGrid.ItemsSource = Participants;

        BackButton.Click += App.MainWindow.BackButton_Click;
    }
}
