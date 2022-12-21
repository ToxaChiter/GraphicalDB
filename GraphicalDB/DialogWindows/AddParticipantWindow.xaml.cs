using GraphicalDB.DataBase;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GraphicalDB.DialogWindows;

/// <summary>
/// Логика взаимодействия для AddParticipantWindow.xaml
/// </summary>
public partial class AddParticipantWindow : Window
{
    internal Participant Participant { get; }

    internal AddParticipantWindow(Participant participant)
    {
        InitializeComponent();

        Participant = new Participant();

        Participant.Name = participant.Name;
        Participant.BirthYear = participant.BirthYear;
        Participant.Country = participant.Country;
        Participant.Instrument = participant.Instrument;
        Participant.Place = participant.Place;

        DataContext = Participant;
    }

    private void ConfirmButton_Click(object sender, RoutedEventArgs e)
    {
        Participant.Name = NameTextBox.Text;
        if (!int.TryParse(YearTextBox.Text, out int year)) throw new Exception($"Can't convert string {YearTextBox.Text} to int");
        Participant.BirthYear = year;
        Participant.Country = CountryTextBox.Text;
        Participant.Instrument = GetInstrumentFromString(InstrumentComboBox.Text);
        if (!int.TryParse(PlaceTextBox.Text, out int place)) throw new Exception($"Can't convert string {PlaceTextBox.Text} to int");
        Participant.Place = place;

        DialogResult = true;
    }

    private Instrument GetInstrumentFromString(string instrument) => instrument switch
    {
        "Гитара" => Instrument.Guitar,
        "Фортепиано" => Instrument.Piano,
        "Скрипка" => Instrument.Cello,
        "Виолончель" => Instrument.Violin,
        _ => throw new Exception($"Can't convert string {instrument} to Instrument")
    };

    private void InfoChanged(object sender, EventArgs e)
    {
        NameTextBox.Text = NameTextBox.Text.Trim(new char[] { '\n', '\t', '\r' });
        YearTextBox.Text = YearTextBox.Text.Trim();
        CountryTextBox.Text = CountryTextBox.Text.Trim(new char[] { '\n', '\t', '\r' });
        PlaceTextBox.Text = PlaceTextBox.Text.Trim();

        bool isBad =
            string.IsNullOrWhiteSpace(NameTextBox.Text) || NameTextBox.Text.Length < 4 ||
            string.IsNullOrWhiteSpace(YearTextBox.Text) || YearTextBox.Text.Length < 4 || 
            !int.TryParse(YearTextBox.Text, out int year) || year < DateTime.Today.Year - 150 || year > DateTime.Today.Year - 6 ||
            string.IsNullOrWhiteSpace(CountryTextBox.Text) || CountryTextBox.Text.Length < 3 ||
            string.IsNullOrWhiteSpace(PlaceTextBox.Text) || !int.TryParse(PlaceTextBox.Text, out int place) || place < 1;

        if (ConfirmButton.IsEnabled == isBad) ConfirmButton.IsEnabled = !isBad;
    }

    private void ReturnButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    private void InstrumentComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        InstrumentComboBox.SelectedIndex = (int)Participant.Instrument;
    }
}
