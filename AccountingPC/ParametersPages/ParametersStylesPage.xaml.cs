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
    /// Логика взаимодействия для ParametersStylesPage.xaml
    /// </summary>
    public partial class ParametersStylesPage : Page
    {
        public ParametersStylesPage()
        {
            InitializeComponent();
            Theme.SelectedIndex = Settings.Default.THEME;
        }

        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.THEME = Theme.SelectedIndex;
        }
    }
}
