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
        public ParametersWindow()
        {
            InitializeComponent();
        }

        private void SelectedOption(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            /*switch (list.SelectedIndex)
            {
                case 0:
                    frameSettings.Source = new Uri(@"ParametersPages\ParametersBasicPage.xaml", UriKind.Relative);
                    //frameSettings.Source = new Uri("/AccountingPC;component/ParametersPages/ParametersBasicPage.xaml", UriKind.Relative);
                    break;
                case 1:
                    frameSettings.Source = new Uri(@"ParametersPages\ParametersStylesPage.xaml", UriKind.Relative);
                    //frameSettings.Source = new Uri("/AccountingPC;component/ParametersPages/ParametersStylePage.xaml", UriKind.Relative);
                    break;
                case 2:
                    frameSettings.Source = new Uri(@"ParametersPages\ParametersSecurityPage.xaml", UriKind.Relative);
                    //frameSettings.Source = new Uri("/AccountingPC;component/ParametersPages/ParametersSecurityPage.xaml", UriKind.Relative);
                    break;
                default:
                    break;
            }*/
            if (frameSettings == null)
                frameSettings = new Frame();
            switch (item.Content)
            {
                case "Основное":
                    frameSettings.Source = new Uri("ParametersPages/ParametersBasicPage.xaml", UriKind.RelativeOrAbsolute);
                    //frameSettings.Source = new Uri("/AccountingPC;component/ParametersPages/ParametersBasicPage.xaml", UriKind.Relative);
                    break;
                case "Стили":
                    frameSettings.Source = new Uri("ParametersPages/ParametersStylesPage.xaml", UriKind.RelativeOrAbsolute);
                    //frameSettings.Source = new Uri("/AccountingPC;component/ParametersPages/ParametersStylePage.xaml", UriKind.Relative);
                    break;
                case "Безопасность":
                    frameSettings.Source = new Uri("ParametersPages/ParametersSecurityPage.xaml", UriKind.RelativeOrAbsolute);
                    //frameSettings.Source = new Uri("/AccountingPC;component/ParametersPages/ParametersSecurityPage.xaml", UriKind.Relative);
                    break;
                default:
                    break;
            }
        }
    }
}
