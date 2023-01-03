using GraphicalDB.DataBase;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GraphicalDB.DialogWindows;

/// <summary>
/// Логика взаимодействия для AddUserWindow.xaml
/// </summary>
public partial class AddUserWindow : Window
{
    internal User User { get; }

    internal AddUserWindow(User user)
    {
        InitializeComponent();

        User = new User();

        User.Login = user.Login;
        User.Role = user.Role;

        DataContext = User;
    }

    private void ConfirmButton_Click(object sender, RoutedEventArgs e)
    {
        User.Password = DbLogic.HashPassword(User.Password);
        User.Role = GetRoleFromString(RoleComboBox.Text);
        DialogResult = true;
    }

    private Roles GetRoleFromString(string role)
    {
        switch (role)
        {
            case "Пользователь":
                return Roles.Default;

            case "Администратор":
                return Roles.Admin;

            default:
                throw new Exception($"Can't convert string {role} to Role");
        }
    }

    private void InfoChanged(object sender, EventArgs e)
    {
        PasswordTextBox.Text = PasswordTextBox.Text.Trim();
        LoginTextBox.Text = LoginTextBox.Text.Trim();

        bool isBad = 
            string.IsNullOrWhiteSpace(PasswordTextBox.Text) || PasswordTextBox.Text.Length < 8 ||
            string.IsNullOrWhiteSpace(LoginTextBox.Text) || LoginTextBox.Text.Length < 3;

        if (ConfirmButton.IsEnabled == isBad) ConfirmButton.IsEnabled = !isBad;
    }

    private void RoleComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        if (User.Role == Roles.Admin) RoleComboBox.SelectedIndex = 1;
        User.Password = string.Empty;
    }

    private void ReturnButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
