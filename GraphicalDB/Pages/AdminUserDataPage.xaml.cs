using GraphicalDB.DataBase;
using GraphicalDB.DialogWindows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace GraphicalDB.Pages;

/// <summary>
/// Логика взаимодействия для AdminUserDataPage.xaml
/// </summary>
public partial class AdminUserDataPage : Page
{
    public AdminUserDataPage()
    {
        InitializeComponent();
        BackButton.Click += App.MainWindow.BackButton_Click;
    }

    private void UsersTableDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = App.MainWindow;
        mainWindow.Context.Users.Load();
        TableDataGrid.ItemsSource = mainWindow.Context.Users.Local.ToObservableCollection();
    }

    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
        AddUserWindow addUserWindow = new AddUserWindow(new User());
        if (addUserWindow.ShowDialog() == true)
        {
            DbLogic.AddUser(addUserWindow.User);
            (TableDataGrid.ItemsSource as ObservableCollection<User>)?.Add(addUserWindow.User);
        }
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        User? user = (sender as Button)?.DataContext as User;

        if (user is null) return;

        AddUserWindow addUserWindow = new AddUserWindow(user);
        if (addUserWindow.ShowDialog() == true)
        {
            user.Login = addUserWindow.User.Login;
            user.Password = addUserWindow.User.Password;
            user.Role = addUserWindow.User.Role;

            App.MainWindow.Context.Users.Update(user);
            App.MainWindow.Context.SaveChanges();
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show(
            "Это действие необратимо. Вы уверены, что хотите удалить данного пользователя?",
            "Внимание!",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning,
            MessageBoxResult.No,
            MessageBoxOptions.DefaultDesktopOnly);

        if (result != MessageBoxResult.Yes) return;



        User? user = (sender as Button)?.DataContext as User;

        if (user is null) return;

        DbLogic.RemoveUser(user);
        (TableDataGrid.ItemsSource as ObservableCollection<User>)?.Remove(user);

        MessageBox.Show(
            "Пользователь успешно удален",
            "Успех!",
            MessageBoxButton.OK,
            MessageBoxImage.Information,
            MessageBoxResult.OK,
            MessageBoxOptions.DefaultDesktopOnly);
    }
}
