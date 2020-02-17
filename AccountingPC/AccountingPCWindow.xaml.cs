using AccountingPC.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AccountingPC
{
    /// <summary>
    /// Логика взаимодействия для AccountingPCWindow.xaml
    /// </summary>
    public partial class AccountingPCWindow : Window
    {
        double lastHeight;
        double lastWidth;
        public static readonly RoutedCommand ParametersCommand = new RoutedUICommand(
            "Parameters", "ParametersCommand", typeof(AccountingPCWindow),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F12) }));
        public static readonly RoutedCommand ExitCommand = new RoutedUICommand(
            "Exit", "ExitCommand", typeof(AccountingPCWindow),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F4, ModifierKeys.Alt) }));
        public static readonly RoutedCommand MaximazedCommand = new RoutedUICommand(
            "Maximazed", "MaximazedCommand", typeof(AccountingPCWindow),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F3, ModifierKeys.Alt) }));
        public static readonly RoutedCommand MinimazedCommand = new RoutedUICommand(
            "Minimazed", "MinimazedCommand", typeof(AccountingPCWindow),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F2, ModifierKeys.Alt) }));

        public AccountingPCWindow()
        {
            InitializeComponent();
            lastHeight = Height;
            lastWidth = Width;
        }

        private void OpenParameters(object sender, RoutedEventArgs e)
        {
            new ParametersWindow().ShowDialog();
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(MenuItem))
                if (((MenuItem)e.OriginalSource).Name == "menuExit")
                    if (Settings.Default.SHUTDOWN_ON_EXPLICIT)
                        App.Current.Shutdown();
                    else
                        Close();
                else { }
            else if (e.OriginalSource.GetType() == typeof(Button))
                if (((Button)e.OriginalSource).Name == "buttonExit")
                    Close();
                else { }
            else
                Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();// Для перемещение ока
        }

        private void MazimazeApp(object sender, ExecutedRoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                Height = lastHeight;
                Width = lastWidth;
                maximazeButtonImage.Source = new BitmapImage(new Uri("pack://application:,,,/AccountingPC;component/images/maximazed.png"));
            }
            else if (WindowState == WindowState.Normal)
            {
                lastHeight = Height;
                lastWidth = Width;
                WindowState = WindowState.Maximized;
                maximazeButtonImage.Source = new BitmapImage(new Uri("pack://application:,,,/AccountingPC;component/images/maximaze.png"));
            }
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            Height = AccountingPCWindowSettings.Default.Height > MinHeight ? AccountingPCWindowSettings.Default.Height : MinHeight;
            Width = AccountingPCWindowSettings.Default.Width > MinWidth ? AccountingPCWindowSettings.Default.Width : MinWidth;
            WindowState = AccountingPCWindowSettings.Default.WindowState != string.Empty ? 
                (WindowState)Enum.Parse(typeof(WindowState), AccountingPCWindowSettings.Default.WindowState) : WindowState.Normal;
            /*switch (AccountingPCWindowSettings.Default.WindowState)
            {
                case "Normal":
                    WindowState = WindowState.Normal;
                    break;
                case "Maximazed":
                    WindowState = WindowState.Maximized;
                    break;
            }*/
            if (WindowState == WindowState.Maximized)
            {
                maximazeButtonImage.Source = new BitmapImage(new Uri("pack://application:,,,/AccountingPC;component/images/maximazed.png"));
            }
            else if (WindowState == WindowState.Normal)
            {
                maximazeButtonImage.Source = new BitmapImage(new Uri("pack://application:,,,/AccountingPC;component/images/maximaze.png"));
            }
        }

        private void window_Closed(object sender, EventArgs e)
        {
            AccountingPCWindowSettings.Default.WindowState = Enum.GetName(typeof(WindowState), WindowState);
            AccountingPCWindowSettings.Default.Width = Width;
            AccountingPCWindowSettings.Default.lastWidth = lastWidth;
            AccountingPCWindowSettings.Default.lastHeignt = lastHeight;
            AccountingPCWindowSettings.Default.Height = Height;
            AccountingPCWindowSettings.Default.Save();
        }

        private void MinimazeApp(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
