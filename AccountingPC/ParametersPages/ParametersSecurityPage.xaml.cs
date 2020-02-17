using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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
using AccountingPC.Properties;

namespace AccountingPC.ParametersPages
{
    /// <summary>
    /// Логика взаимодействия для ParametersSecurityPage.xaml
    /// </summary>
    public partial class ParametersSecurityPage : Page
    {
        public ParametersSecurityPage()
        {
            InitializeComponent();
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            KeyValuePair<bool, string> res = ChangePassword();
            if (res.Key)
            {
                changeStatus.Content = res.Value;
                Task task;
                task = new Task(() =>
                {
                    try
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            i++;
                            Thread.Sleep(1000);
                        }
                        Dispatcher.Invoke(() => changeStatus.Content = string.Empty);
                        
                    }
                    catch { }
                });
                task.Start();
            }
            else
            {
                MessageBox.Show(res.Value);
            }
        }

        /// <summary>
        /// Метод сравнивает старый пароль с сохраненным. 
        /// Если пароли равны, то сравнивается новый пароль с его повтором. 
        /// Если новый пароль совпадает, хешированный пароль сохраняется.
        /// </summary>
        /// <returns>Возвращает пару ключ-значение.
        /// Ключ представляет логическое значение. True - пароль изменен. False - Произошла ошибка.
        /// Значение представляет сообщение о статусе изменения пароля.
        /// </returns>
        public KeyValuePair<bool, string> ChangePassword()
        {
            string enPass = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(oldPass.Password)));
            string setPass = Settings.Default.PASSWORD_HASH;
            if (enPass == setPass)
            {
                if (newPass.Password == repeatPass.Password)
                {
                    Settings.Default.PASSWORD_HASH = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(repeatPass.Password)));
                    Settings.Default.Save();
                    return new KeyValuePair<bool, string>(true, "Пароль успешно изменен");
                }
                else
                {
                    return new KeyValuePair<bool, string>(false, "Пароли не совпадают");
                }
            }
            else
            {
                return new KeyValuePair<bool, string>(false, "Неверный пароль");
            }
        }
    }
}
