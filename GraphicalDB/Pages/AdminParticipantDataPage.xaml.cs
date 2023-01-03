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
/// Логика взаимодействия для AdminParticipantDataPage.xaml
/// </summary>
public partial class AdminParticipantDataPage : Page
{
    public AdminParticipantDataPage()
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
            DbLogic.AddParticipant(addParticipantWindow.Participant);
            MainCollection.Add(addParticipantWindow.Participant);
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

        DbLogic.RemoveParticipant(participant);
        MainCollection.Remove(participant);

        MessageBox.Show(
            "Участник успешно удален",
            "Успех!",
            MessageBoxButton.OK,
            MessageBoxImage.Information,
            MessageBoxResult.OK,
            MessageBoxOptions.DefaultDesktopOnly);
    }

    // Метод отображения в таблице самых юных (до 12 лет) участников 
    private void YoungersButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        // Получение текущего года
        int year = DateTime.Today.Year;

        // Из всех участников взять тех, чей возраст меньше 12 и отсортировать по возрасту по возрастанию
        TableDataGrid.ItemsSource = from p in MainCollection
                                    where year - p.BirthYear < 12
                                    orderby p.BirthYear
                                    select p;

        // Блок изменений в интерфейсе, в основном исчезновение кнопок
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

    // Метод отображения в таблице Топ-3 участников по инструментам
    private void Top3Button_Click(object sender, RoutedEventArgs e)
    {
        Top3ParticipantWindow top3ParticipantWindow = new Top3ParticipantWindow();
        if (top3ParticipantWindow.ShowDialog() == false) return;

        Instruments instrument = top3ParticipantWindow.Instrument;

        // Новая коллекция
        ObservableCollection<Participant> top3 = new ObservableCollection<Participant>();

        // Из всех участников взять тех, чей инструмент соответсвует текущему,
        // отсортировать по занятому месту, взять 3 лучших и добавить в коллекцию
        (from p in MainCollection
         where p.Instrument == instrument
         orderby p.Place
         select p)
        .Take(3).ToList().ForEach(p => top3.Add(p));

        // Блок изменений в интерфейсе, в основном исчезновение кнопок
        AddParticipantButton.Visibility = Visibility.Hidden;
        SearchButton.Visibility = Visibility.Hidden;
        YoungersButton.Visibility = Visibility.Hidden;

        Top3Button.IsEnabled = false;

        CancelButton.Visibility = Visibility.Visible;

        // Привязка данных коллекции к таблице
        TableDataGrid.ItemsSource = top3;
    }
}
