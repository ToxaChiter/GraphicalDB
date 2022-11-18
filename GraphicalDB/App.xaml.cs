using GraphicalDB.DataBase;
using System.Windows;

namespace GraphicalDB;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    internal static User AuthorizedUser { get; set; }
    internal static MainWindow MainWindow { get; set; }
}
