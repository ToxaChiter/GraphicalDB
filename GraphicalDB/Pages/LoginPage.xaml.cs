using GraphicalDB.DataBase;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace GraphicalDB;

/// <summary>
/// Логика взаимодействия для LoginPage.xaml
/// </summary>
public partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        string login = LoginBox.Text;
        string password = PasswordBox.Password;

        var user = DbLogic.FindUser(u => string.Equals(u.Login, login));

        if (user is null)
        {
            LoginBox.Clear();
            PasswordBox.Clear();

            MessageBox.Show(
                        "Пользователь с таким логином не найден",
                        "Ошибка!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error,
                        MessageBoxResult.None,
                        MessageBoxOptions.DefaultDesktopOnly);
            return;
        }

        if (!string.Equals(user.Password, DbLogic.HashPassword(user, password)))
        {
            PasswordBox.Clear();

            MessageBox.Show(
                        "Неверный пароль",
                        "Ошибка!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error,
                        MessageBoxResult.None,
                        MessageBoxOptions.DefaultDesktopOnly);
            return;
        }


        App.AuthorizedUser = user;

        if (user.Role == Role.Admin)
        {
            ((MainWindow)Application.Current.MainWindow).ChangePage(new Uri("Pages/AdminPage.xaml", UriKind.Relative));
        }
        else
        {
            ((MainWindow)Application.Current.MainWindow).ChangePage(new Uri("Pages/ParticipantDataPage.xaml", UriKind.Relative));
        }
    }

    private void Login_TextChanged(object sender, EventArgs e)
    {
        bool isBad = string.IsNullOrWhiteSpace(PasswordBox.Password) || PasswordBox.Password.Length < 8 ||
            string.IsNullOrWhiteSpace(LoginBox.Text) || LoginBox.Text.Length < 3;

        if (LoginButton.IsEnabled == isBad) LoginButton.IsEnabled = !isBad;
    }
}
