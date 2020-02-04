using AccountingPC.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccountingPC
{
    class SettingsPassword
    {
        public List<SettingsPasswordParameter> Passwords { get; private set; }
        public SettingsPassword()
        {
            Passwords = new List<SettingsPasswordParameter>();
            Passwords.Add(new SettingsPasswordParameter() { Name = "Старый пароль" });
            Passwords.Add(new SettingsPasswordParameter() { Name = "Новый пароль" });
            Passwords.Add(new SettingsPasswordParameter() { Name = "Повторите пароль" });
        }

        public string ChangePassword()
        {
            string enPass = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(Passwords[0].Value)));
            string setPass = Settings.Default.PASSWORD_HASH;
            if (enPass == setPass)
            {
                if(Passwords[1].Value== Passwords[2].Value)
                {
                    Settings.Default.PASSWORD_HASH = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(Passwords[2].Value)));
                    Settings.Default.Save();
                    return "Пароль успешно изменен";
                }
                else
                {
                    return "Пароли не совпадают";
                }
            }
            else
            {
                return "Неверный пароль";
            }
        }
    }

    class SettingsPasswordParameter
    {
        public string Name { get; internal set; }
        public string Value { get; set; }
    }
}
