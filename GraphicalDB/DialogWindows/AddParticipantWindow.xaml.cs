using GraphicalDB.DataBase;
using System;
using System.Windows;

namespace GraphicalDB.DialogWindows;

/// <summary>
/// Логика взаимодействия для AddParticipantWindow.xaml
/// </summary>
public partial class AddParticipantWindow : Window
{
    // Экземпляр класса участника для биндинга
    internal Participant Participant { get; }

    internal AddParticipantWindow(Participant participant)
    {
        InitializeComponent();

        // Копирование полученного участника
        Participant = new Participant
        {
            Name = participant.Name,
            BirthYear = participant.BirthYear,
            Country = participant.Country,
            Instrument = participant.Instrument,
            Place = participant.Place
        };

        // Привязка данных
        DataContext = Participant;
    }

    // Метод передачи результата
    private void ConfirmButton_Click(object sender, RoutedEventArgs e)
    {
        // Заполнение полей результирующего участника
        Participant.Name = NameTextBox.Text;
        if (!int.TryParse(YearTextBox.Text, out int year)) throw new FormatException($"Can't convert string {YearTextBox.Text} to int");
        Participant.BirthYear = year;
        Participant.Country = CountryTextBox.Text;
        Participant.Instrument = GetInstrumentFromString(InstrumentComboBox.Text);
        if (!int.TryParse(PlaceTextBox.Text, out int place)) throw new FormatException($"Can't convert string {PlaceTextBox.Text} to int");
        Participant.Place = place;

        DialogResult = true;
    }

    // Метод преобразования строки в инструмент
    private static Instruments GetInstrumentFromString(string instrument) => instrument switch
    {
        "Гитара" => Instruments.Guitar,
        "Фортепиано" => Instruments.Piano,
        "Скрипка" => Instruments.Cello,
        "Виолончель" => Instruments.Violin,
        // В случае несовпадения со всеми вариантами – выбрасывает исключение
        _ => throw new FormatException($"Can't convert string {instrument} to Instrument")
    };

    // Метод отслеживания корректности введённых данных
    private void InfoChanged(object sender, EventArgs e)
    {
        // Очистка лишних пробельных символов 
        NameTextBox.Text = NameTextBox.Text.Trim(new char[] { '\n', '\t', '\r' });
        YearTextBox.Text = YearTextBox.Text.Trim();
        CountryTextBox.Text = CountryTextBox.Text.Trim(new char[] { '\n', '\t', '\r' });
        PlaceTextBox.Text = PlaceTextBox.Text.Trim();

        // Проверка корректности
        bool isBad =
            string.IsNullOrWhiteSpace(NameTextBox.Text) || NameTextBox.Text.Length < 4 ||
            string.IsNullOrWhiteSpace(YearTextBox.Text) || YearTextBox.Text.Length < 4 || 
            !int.TryParse(YearTextBox.Text, out int year) || year < DateTime.Today.Year - 150 || year > DateTime.Today.Year - 6 ||
            string.IsNullOrWhiteSpace(CountryTextBox.Text) || CountryTextBox.Text.Length < 3 ||
            string.IsNullOrWhiteSpace(PlaceTextBox.Text) || !int.TryParse(PlaceTextBox.Text, out int place) || place < 1;

        // Изменение текущего состояния кнопки подтверждения
        if (ConfirmButton.IsEnabled == isBad) ConfirmButton.IsEnabled = !isBad;
    }

    // Метод возврата, отменяет операцию
    private void ReturnButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    private void InstrumentComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        // Первоначально отображаемый инструмент – инструмент текущего участника
        InstrumentComboBox.SelectedIndex = (int)Participant.Instrument;
    }
}
