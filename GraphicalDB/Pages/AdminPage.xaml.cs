using System;
using System.Windows;
using System.Windows.Controls;

namespace GraphicalDB.Pages;

/// <summary>
/// Логика взаимодействия для AdminPage.xaml
/// </summary>
public partial class AdminPage : Page
{
    public AdminPage()
    {
        InitializeComponent();
        AdminBackButton.Click += App.MainWindow.BackButton_Click;
    }

    private void AdminUsersButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).ChangePage(new Uri("Pages/AdminUserDataPage.xaml", UriKind.Relative));
    }

    private void AdminParticipantsButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).ChangePage(new Uri("Pages/AdminParticipantDataPage.xaml", UriKind.Relative));
    }
}
