using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Логика взаимодействия для ChangeDeviceWindow.xaml
    /// </summary>
    public partial class ChangeDeviceWindow : Window
    {
        public static readonly RoutedCommand ExitCommand = new RoutedUICommand(
            "Exit", "ExitCommand", typeof(ChangeDeviceWindow),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F4, ModifierKeys.Alt) }));
        TypeDevice typeDevice;
        int DeviceID;

        public ChangeDeviceWindow(TypeDevice typeD, int ID)
        {
            InitializeComponent();
            typeDevice = typeD;
            DeviceID = ID;
            SqlDataReader reader;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString = $"Select * from {typeDevice.ToString()} where ID={DeviceID}";
                reader = new SqlCommand(commandString, connection).ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        uint invN = Convert.ToUInt32(reader["InventoryNumber"]);
                        string n = reader["Name"].ToString();
                        uint cost = Convert.ToUInt32(reader["Cost"]);
                        pc = new PC(invN, n, cost);
                        inventoryNumberPC.Text = pc.InventoryNumber.ToString();
                        namePC.Text = pc.Name;
                        costPC.Text = pc.Cost.ToString();
                    }
                }
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();// Для перемещение ока
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString = $"Update {typeDevice} set InventoryNumber = {Convert.ToUInt32(inventoryNumberPC.Text)}, " +
                    $"Name = N'{namePC.Text}', Cost = {Convert.ToUInt32(costPC.Text)} where ID={DeviceID}";
                SqlCommand command = new SqlCommand(commandString, connection);
                int res = command.ExecuteNonQuery();
            }
        }
    }
}
