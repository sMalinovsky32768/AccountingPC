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
using AccountingPC;

namespace AccountingPC.ParametersPages
{
    /// <summary>
    /// Логика взаимодействия для ParametersSecurityPage.xaml
    /// </summary>
    public partial class ParametersSecurityPage : Page
    {
        SettingsPassword password;
        public ParametersSecurityPage()
        {
            InitializeComponent();
            password = new SettingsPassword();
            //changePassword.ItemsSource = password.Passwords;
            changePassword.DataContext = password;
            changePassword.ItemsSource = password.Passwords;
            ParameterNameGrid.Width = this.MaxWidth / 2;
            ParameterValueGrid.Width = this.MaxWidth / 2;
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            string res = password.ChangePassword();
            MessageBox.Show(res);
        }
    }
}
