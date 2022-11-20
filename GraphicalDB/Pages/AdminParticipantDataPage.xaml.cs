using GraphicalDB.DataBase;
using GraphicalDB.DialogWindows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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
    }

    private void ParticipantsTableDataGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        MainWindow mainWindow = App.MainWindow;
        mainWindow.Context.Database.EnsureCreated();
        mainWindow.Context.Participants.Load();
        TableDataGrid.ItemsSource = mainWindow.Context.Participants.Local.ToObservableCollection();
    }

    private void AddParticipantButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        
    }
}
