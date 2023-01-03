using GraphicalDB.DataBase;
using System;
using System.Windows;

namespace GraphicalDB.DialogWindows;

/// <summary>
/// Логика взаимодействия для SearchParticipantWindow.xaml
/// </summary>
public partial class Top3ParticipantWindow : Window
{
    internal Instruments Instrument { get; private set; }

    internal Top3ParticipantWindow()
    {
        InitializeComponent();
    }

    private void ConfirmButton_Click(object sender, RoutedEventArgs e)
    {
        Instrument = GetInstrumentFromString(InstrumentComboBox.Text);

        DialogResult = true;
    }

    private void ReturnButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    private static Instruments GetInstrumentFromString(string instrument) => instrument switch
    {
        "Гитара" => Instruments.Guitar,
        "Фортепиано" => Instruments.Piano,
        "Скрипка" => Instruments.Cello,
        "Виолончель" => Instruments.Violin,
        _ => throw new FormatException($"Can't convert string {instrument} to Instrument")
    };

    private void InstrumentComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        //InstrumentComboBox.SelectedIndex = 0;
    }
}
