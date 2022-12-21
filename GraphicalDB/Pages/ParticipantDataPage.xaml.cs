using System;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GraphicalDB.DataBase;
using GraphicalDB.DialogWindows;
using Microsoft.EntityFrameworkCore;

namespace GraphicalDB;

/// <summary>
/// Логика взаимодействия для DataPage.xaml
/// </summary>
public partial class DataPage : Page
{
    public DataPage()
    {
        InitializeComponent();

        BackButton.Click += App.MainWindow.BackButton_Click;

        MyDbContext context = App.MainWindow.Context;
        context.Participants.Load();
        MainCollection = context.Participants.Local.ToObservableCollection();
    }

    private ObservableCollection<Participant> MainCollection { get; }

    private void ParticipantsTableDataGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        TableDataGrid.ItemsSource = MainCollection;
    }

    private void YoungersButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        int year = DateTime.Today.Year;

        TableDataGrid.ItemsSource = from p in MainCollection
                                    where year - p.BirthYear < 12
                                    orderby p.BirthYear
                                    select p;

        SearchButton.Visibility = Visibility.Hidden;
        Top3Button.Visibility = Visibility.Hidden;

        YoungersButton.IsEnabled = false;

        CancelButton.Visibility = Visibility.Visible;
    }

    private void SearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        SearchParticipantWindow searchParticipantWindow = new SearchParticipantWindow();
        if (searchParticipantWindow.ShowDialog() == false) return;

        string pattern = searchParticipantWindow.Pattern;

        TableDataGrid.ItemsSource = from p in MainCollection
                                    where p.Name.Contains(pattern, StringComparison.Ordinal)
                                    select p;


        Top3Button.Visibility = Visibility.Hidden;
        YoungersButton.Visibility = Visibility.Hidden;

        SearchButton.IsEnabled = false;

        CancelButton.Visibility = Visibility.Visible;
    }

    private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        foreach (Button button in buttonPanel.Children)
        {
            button.IsEnabled = true;
            button.Visibility = Visibility.Visible;
        }

        CancelButton.Visibility = Visibility.Hidden;


        TableDataGrid.ItemsSource = MainCollection;
    }

    private void Top3Button_Click(object sender, RoutedEventArgs e)
    {
        ObservableCollection<Participant> top3 = new ObservableCollection<Participant>();

        foreach (Instrument instrument in Enum.GetValues(typeof(Instrument)))
        {
            (from p in MainCollection
             where p.Instrument == instrument
             orderby p.Place
             select p)
            .Take(3).ToList().ForEach(p => top3.Add(p));
        }

        SearchButton.Visibility = Visibility.Hidden;
        YoungersButton.Visibility = Visibility.Hidden;

        Top3Button.IsEnabled = false;

        CancelButton.Visibility = Visibility.Visible;


        TableDataGrid.ItemsSource = top3;
    }
}
