using GraphicalDB.DataBase;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GraphicalDB;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    internal MyDbContext Context { get; } = new MyDbContext();
    public MainWindow()
    {
        InitializeComponent();

        App.MainWindow = this;

        ChangePage(new Uri("Pages/LoginPage.xaml", UriKind.Relative));

        var borderStartAnimation = new PointAnimation
        {
            From = gradient.StartPoint,
            To = gradient.EndPoint,
            Duration = new Duration(TimeSpan.FromSeconds(10)),
            RepeatBehavior = RepeatBehavior.Forever,
            AutoReverse = true
        };

        var borderEndAnimation = new PointAnimation
        {
            From = gradient.EndPoint,
            To = gradient.StartPoint,
            Duration = new Duration(TimeSpan.FromSeconds(10)),
            RepeatBehavior = RepeatBehavior.Forever,
            AutoReverse = true
        };

        gradient.BeginAnimation(LinearGradientBrush.StartPointProperty, borderStartAnimation);
        gradient.BeginAnimation(LinearGradientBrush.EndPointProperty, borderEndAnimation);

        //DbLogic.Testing();

        //var backgroundStartAnimation = new PointAnimation
        //{
        //    From = backGradient.StartPoint,
        //    To = backGradient.EndPoint,
        //    Duration = new Duration(TimeSpan.FromSeconds(10)),
        //    RepeatBehavior = RepeatBehavior.Forever,
        //    AutoReverse = true
        //};

        //var backgroundEndAnimation = new PointAnimation
        //{
        //    From = backGradient.EndPoint,
        //    To = backGradient.StartPoint,
        //    Duration = new Duration(TimeSpan.FromSeconds(10)),
        //    RepeatBehavior = RepeatBehavior.Forever,
        //    AutoReverse = true
        //};

        //backGradient.BeginAnimation(LinearGradientBrush.StartPointProperty, borderStartAnimation);
        //backGradient.BeginAnimation(LinearGradientBrush.EndPointProperty, borderEndAnimation);
    }

    public void ChangePage(Uri source)
    {
        MainFrame.Navigate(source);
    }

    public void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.GoBack();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void HideButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void SizeButton_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
        }
        else
        {
            WindowState = WindowState.Maximized;
        }
    }

    public static void Message(string message)
    {
        MessageBox.Show(message);
    }
}
