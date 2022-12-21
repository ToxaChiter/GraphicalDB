using GraphicalDB.DataBase;
using GraphicalDB.DialogWindows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GraphicalDB.Pages;

/// <summary>
/// Логика взаимодействия для AdminDataPage.xaml
/// </summary>
public partial class AdminDataPage : Page
{
    public AdminDataPage()
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

    private void AddParticipantButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        AddParticipantWindow addParticipantWindow = new AddParticipantWindow(new Participant());
        if (addParticipantWindow.ShowDialog() == true)
        {
            App.MainWindow.Context.Participants.Add(addParticipantWindow.Participant);
            App.MainWindow.Context.SaveChanges();
        }
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        Participant? participant = (sender as Button)?.DataContext as Participant;

        if (participant is null) return;

        AddParticipantWindow addParticipantWindow = new AddParticipantWindow(participant);
        if (addParticipantWindow.ShowDialog() == true)
        {
            participant.Name = addParticipantWindow.Participant.Name;
            participant.BirthYear = addParticipantWindow.Participant.BirthYear;
            participant.Country = addParticipantWindow.Participant.Country;
            participant.Instrument = addParticipantWindow.Participant.Instrument;
            participant.Place = addParticipantWindow.Participant.Place;

            App.MainWindow.Context.Participants.Update(participant);
            App.MainWindow.Context.SaveChanges();
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show(
            "Это действие необратимо. Вы уверены, что хотите удалить данного участника?",
            "Внимание!",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning,
            MessageBoxResult.No,
            MessageBoxOptions.DefaultDesktopOnly);

        if (result != MessageBoxResult.Yes) return;

        Participant? participant = (sender as Button)?.DataContext as Participant;

        if (participant is null) return;

        App.MainWindow.Context.Participants.Remove(participant);
        App.MainWindow.Context.SaveChanges();

        MessageBox.Show(
            "Участник успешно удален",
            "Успех!",
            MessageBoxButton.OK,
            MessageBoxImage.Information,
            MessageBoxResult.OK,
            MessageBoxOptions.DefaultDesktopOnly);
    }

    private void YoungersButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        int year = DateTime.Today.Year;

        TableDataGrid.ItemsSource = from p in MainCollection
                                    where year - p.BirthYear < 12
                                    orderby p.BirthYear
                                    select p;

        AddParticipantButton.Visibility = Visibility.Hidden;
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

        
        AddParticipantButton.Visibility = Visibility.Hidden;
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

        AddParticipantButton.Visibility = Visibility.Hidden;
        SearchButton.Visibility = Visibility.Hidden;
        YoungersButton.Visibility = Visibility.Hidden;

        Top3Button.IsEnabled = false;

        CancelButton.Visibility = Visibility.Visible;


        TableDataGrid.ItemsSource = top3;
    }
}
