using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicalDB;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

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
}
