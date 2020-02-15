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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccountingPC.ParametersPages
{
    /// <summary>
    /// Логика взаимодействия для ParametersBasicPage.xaml
    /// </summary>
    public partial class ParametersBasicPage : Page
    {
        public ParametersBasicPage()
        {
            InitializeComponent();
            switch (Settings.Default.SHUTDOWN_ON_EXPLICIT)
            {
                case true:
                    isOnExplicitShutdown.SelectedIndex = 0;
                    break;
                case false:
                    isOnExplicitShutdown.SelectedIndex = 1;
                    break;
            }
        }

        private void isOnExplicitShutdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (isOnExplicitShutdown.SelectedIndex)
            {
                case 0:
                    Settings.Default.SHUTDOWN_ON_EXPLICIT = true;
                    break;
                case 1:
                    Settings.Default.SHUTDOWN_ON_EXPLICIT = false;
                    break;
            }
        }
    }
}
