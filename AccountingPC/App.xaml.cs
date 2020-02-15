using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using AccountingPC.Properties;
using System.Security.Cryptography;
using System.Text;

namespace AccountingPC
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.ContextMenu notifyContextMenu;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Settings.Default.SHUTDOWN_ON_EXPLICIT)
            {
                App.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            }
            else
            {
                App.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
            }
            notify = new System.Windows.Forms.NotifyIcon(new System.ComponentModel.Container());
            notifyContextMenu = new System.Windows.Forms.ContextMenu( new System.Windows.Forms.MenuItem[] 
            { new System.Windows.Forms.MenuItem("Выход", new EventHandler(ShutdownCurrentApp)) });
            notify.Icon = new System.Drawing.Icon("images/icon.ico");
            notify.ContextMenu = notifyContextMenu;
            notify.Text = "AccountingPC";
            notify.DoubleClick += new System.EventHandler(NotifyDoubleClick);
            if (App.Current.ShutdownMode == ShutdownMode.OnExplicitShutdown)
            {
                notify.Visible = true;
            }
            else
            {

                notify.Visible = false;
            }
            SetUserCredentials();
            // CreateDB();
        }

        private void ShutdownCurrentApp(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void NotifyDoubleClick(object sender, EventArgs e)
        {
            bool isOpenWindow = false;
            foreach (Window w in App.Current.Windows.OfType<Window>())
            {
                if ((w.WindowState == WindowState.Minimized) || (!w.IsActive))
                {
                    isOpenWindow = true;
                }
            }
            if (!isOpenWindow)
            {
                new MainWindow().Show();
            }
        }

        private void SetUserCredentials(string login, string pass)
        {
            if (Settings.Default.USER_NAME == null || Settings.Default.USER_NAME == "")
                Settings.Default.USER_NAME = login;
            if (Settings.Default.PASSWORD_HASH == null || Settings.Default.PASSWORD_HASH == "")
                Settings.Default.PASSWORD_HASH = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(pass)));
            Settings.Default.Save();
        }

        private void SetUserCredentials(string userCredentials)
        {
            SetUserCredentials(userCredentials, userCredentials);
        }

        private void SetUserCredentials()
        {
            SetUserCredentials("admin");
        }

        private void CreateDB()
        {
            string str;
            SqlConnection myConn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True");
            str = "if not exists (select * from sys.databases where name=N'Accounting')"
                  + "begin"
                  + "CREATE DATABASE[Accounting]"
                  + "CONTAINMENT = NONE ON PRIMARY"
                  + "(NAME = N'Accounting', FILENAME = N'"
                  + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                  + "\\Accounting.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )"
                  + "LOG ON"
                  + "(NAME = N'Accounting_log', FILENAME = N'"
                  + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                  + "\\Accounting_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )"
                  + "IF(1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))"
                  + "begin"
                  + "EXEC[Accounting].[dbo].[sp_fulltext_database] @action = 'enable'"
                  + "end;"
                  + "end;";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                //MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButton.OK, MessageBoxImage.Error);
                Clipboard.SetText(ex.ToString());
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
            str = "ALTER DATABASE[Accounting] SET ANSI_NULL_DEFAULT OFF "
                  + "ALTER DATABASE[Accounting] SET ANSI_NULLS OFF "
                  + "ALTER DATABASE[Accounting] SET ANSI_PADDING OFF "
                  + "ALTER DATABASE[Accounting] SET ANSI_WARNINGS OFF "
                  + "ALTER DATABASE[Accounting] SET ARITHABORT OFF "
                  + "ALTER DATABASE[Accounting] SET AUTO_CLOSE ON "
                  + "ALTER DATABASE[Accounting] SET AUTO_SHRINK OFF "
                  + "ALTER DATABASE[Accounting] SET AUTO_UPDATE_STATISTICS ON "
                  + "ALTER DATABASE[Accounting] SET CURSOR_CLOSE_ON_COMMIT OFF "
                  + "ALTER DATABASE[Accounting] SET CURSOR_DEFAULT  GLOBAL "
                  + "ALTER DATABASE[Accounting] SET CONCAT_NULL_YIELDS_NULL OFF "
                  + "ALTER DATABASE[Accounting] SET NUMERIC_ROUNDABORT OFF "
                  + "ALTER DATABASE[Accounting] SET QUOTED_IDENTIFIER OFF "
                  + "ALTER DATABASE[Accounting] SET RECURSIVE_TRIGGERS OFF "
                  + "ALTER DATABASE[Accounting] SET DISABLE_BROKER "
                  + "ALTER DATABASE[Accounting] SET AUTO_UPDATE_STATISTICS_ASYNC ON "
                  + "ALTER DATABASE[Accounting] SET DATE_CORRELATION_OPTIMIZATION OFF "
                  + "ALTER DATABASE[Accounting] SET TRUSTWORTHY OFF "
                  + "ALTER DATABASE[Accounting] SET ALLOW_SNAPSHOT_ISOLATION OFF "
                  + "ALTER DATABASE[Accounting] SET PARAMETERIZATION SIMPLE "
                  + "ALTER DATABASE[Accounting] SET READ_COMMITTED_SNAPSHOT OFF "
                  + "ALTER DATABASE[Accounting] SET HONOR_BROKER_PRIORITY OFF "
                  + "ALTER DATABASE[Accounting] SET RECOVERY FULL "
                  + "ALTER DATABASE[Accounting] SET  MULTI_USER "
                  + "ALTER DATABASE[Accounting] SET PAGE_VERIFY CHECKSUM "
                  + "ALTER DATABASE[Accounting] SET DB_CHAINING OFF "
                  + "ALTER DATABASE[Accounting] SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF) "
                  + "ALTER DATABASE[Accounting] SET TARGET_RECOVERY_TIME = 60 SECONDS "
                  + "ALTER DATABASE[Accounting] SET DELAYED_DURABILITY = DISABLED "
                  + "ALTER DATABASE[Accounting] SET QUERY_STORE = OFF "
                  + "USE[Accounting]"
                  + "ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF; "
                  + "ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0; "
                  + "ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON; "
                  + "ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF; "
                  + "ALTER DATABASE[Accounting] SET READ_WRITE";
            myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                //MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButton.OK, MessageBoxImage.Error);
                Clipboard.SetText(ex.ToString());
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
    }
}
