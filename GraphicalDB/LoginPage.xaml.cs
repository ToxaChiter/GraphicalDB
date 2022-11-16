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
        ((MainWindow)Application.Current.MainWindow).ChangePage(new Uri("DataPage.xaml", UriKind.Relative));
    }
}
