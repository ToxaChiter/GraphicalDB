using GraphicalDB.DataBase;
using System;
using System.Windows;

namespace GraphicalDB.DialogWindows;

/// <summary>
/// Логика взаимодействия для SearchParticipantWindow.xaml
/// </summary>
public partial class SearchParticipantWindow : Window
{
    internal string Pattern { get; private set; }

    internal SearchParticipantWindow()
    {
        InitializeComponent();
    }

    private void ConfirmButton_Click(object sender, RoutedEventArgs e)
    {
        Pattern = NameTextBox.Text;

        DialogResult = true;
    }

    private void InfoChanged(object sender, EventArgs e)
    {
        NameTextBox.Text = NameTextBox.Text.Trim(new char[] { '\n', '\t', '\r' });

        bool isBad =
            string.IsNullOrWhiteSpace(NameTextBox.Text) || NameTextBox.Text.Length < 1;

        if (ConfirmButton.IsEnabled == isBad) ConfirmButton.IsEnabled = !isBad;
    }

    private void ReturnButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
