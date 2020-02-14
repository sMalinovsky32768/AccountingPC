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
using System.Security.Cryptography;

namespace AccountingPC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Выход при Esc
        public static readonly RoutedCommand ExitCommand = new RoutedUICommand(
            "Exit", "ExitCommand", typeof(MainWindow),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.Escape) }));

        public MainWindow()
        {
            InitializeComponent();
            loginTextBox.Focus();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string uName = Settings.Default.USER_NAME;
            string enPass = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(passwordTextBox.Password)));
            string setPass = Settings.Default.PASSWORD_HASH;
            bool isTrueLogin = login == uName;
            bool isTruePassword = enPass == setPass;
            if (isTrueLogin && isTruePassword)
            {
                new AccountingPCWindow().Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
            App.Current.Shutdown();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();// Для перемещение ока
        }
    }
}
