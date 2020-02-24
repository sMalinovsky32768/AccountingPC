using AccountingPC.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        SqlDataAdapter adapter;
        DataSet set;

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

        /// <summary>
        /// Если метод был вызван кнопкой в верхнем правом углу, то, если задан SHUTDOWN_ON_EXPLICIT, то происходит завершение программы.
        /// В остальных случаях просто закрывается окно.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                maximazeButtonImage.Source = new BitmapImage(new Uri("pack://application:,,,/AccountingPC;component/images/window-maximize.png"));
            }
            else if (WindowState == WindowState.Normal)
            {
                lastHeight = Height;
                lastWidth = Width;
                WindowState = WindowState.Maximized;
                maximazeButtonImage.Source = new BitmapImage(new Uri("pack://application:,,,/AccountingPC;component/images/window-restore.png"));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
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
                maximazeButtonImage.Source = new BitmapImage(new Uri("pack://application:,,,/AccountingPC;component/images/window-restore.png"));
            }
            else if (WindowState == WindowState.Normal)
            {
                maximazeButtonImage.Source = new BitmapImage(new Uri("pack://application:,,,/AccountingPC;component/images/window-maximize.png"));
            }
            list.SelectedIndex = 0;
        }

        private void Window_Closed(object sender, EventArgs e)
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void AddDevice(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
            }
            UpdateDataGrid();
        }

        private void ChangeDevice(object sender, RoutedEventArgs e)
        {
            DataRow row = ((DataRowView)view.SelectedItem).Row;
            int id = Convert.ToInt32(row[0]);
            new ChangeDeviceWindow(TypeDevice.PC, id).ShowDialog();
            UpdateDataGrid();
        }

        private void DeleteDevice(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (object obj in view.SelectedItems)
                {
                    DataRow row = ((DataRowView)obj).Row;
                    int id = Convert.ToInt32(row[0]);
                    SqlCommand command = new SqlCommand($"Delete from PC where ID={id}", connection);
                    int res = command.ExecuteNonQuery();
                }
            }
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            switch (list.SelectedIndex)
            {
                case 0:
                    adapter = new SqlDataAdapter("SELECT PC.ID as 'ID', PC.InventoryNumber as 'Инвентарный номер', " +
                        "PC.Name as 'Наименование', str(PC.Cost, 10, 2) + N' ₽' as 'Цена', " +
                        "(select dbo.getSocketNameByID(SocketID)) as 'Сокет', " +
                        "(select dbo.getFullProcessorName(PC.CPUID)) as 'Процессор', " +
                        "str(PC.RAM, 3, 0) + N' ГБ' as 'ОЗУ', VideoCard as 'Видеокарта', " +
                        "str(PC.HDDCapacityTB, 4, 1) + N' ТБ' as 'Объем HDD' from PC",
                        ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    set = new DataSet();
                    adapter.Fill(set);
                    view.ItemsSource = set.Tables[0].DefaultView;
                    view.Columns[0].Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
