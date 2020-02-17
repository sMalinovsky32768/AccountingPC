using AccountingPC.Properties;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AccountingPC
{
    /// <summary>
    /// Логика взаимодействия для ParametersWindow.xaml
    /// </summary>
    public partial class ParametersWindow : Window
    {
        public static readonly RoutedCommand ExitCommand = new RoutedUICommand(
            "Exit", "ExitCommand", typeof(AccountingPCWindow),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F4, ModifierKeys.Alt) }));

        public ParametersWindow()
        {
            InitializeComponent();
        }

        private void SelectedOption(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            if (frameSettings == null)
                frameSettings = new Frame();
            switch (item.Content)
            {
                case "Основное":
                    frameSettings.Source = new Uri("ParametersPages/ParametersBasicPage.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "Стили":
                    frameSettings.Source = new Uri("ParametersPages/ParametersStylesPage.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "Безопасность":
                    frameSettings.Source = new Uri("ParametersPages/ParametersSecurityPage.xaml", UriKind.RelativeOrAbsolute);
                    break;
                default:
                    break;
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).IsDefault)
                Settings.Default.Save();
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();// Для перемещение ока
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
