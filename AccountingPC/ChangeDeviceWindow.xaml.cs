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
    public enum TypeChange
    {
        Change,
        Add,
    }
    /// <summary>
    /// Логика взаимодействия для ChangeDeviceWindow.xaml
    /// </summary>
    public partial class ChangeDeviceWindow : Window
    {
        public static readonly RoutedCommand ExitCommand = new RoutedUICommand(
            "Exit", "ExitCommand", typeof(ChangeDeviceWindow),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F4, ModifierKeys.Alt) }));
        TypeDevice typeDevice;
        TypeChange typeChange;
        int DeviceID;
        SqlDataAdapter adapter;
        DataSet set;

        public ChangeDeviceWindow(TypeDevice typeD, int ID, TypeChange change)
        {
            InitializeComponent();
            string commandString;
            typeDevice = typeD;
            typeChange = change;
            DeviceID = ID;
            SqlDataReader reader;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            if (typeChange == TypeChange.Change)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    switch (typeDevice)
                    {
                        case TypeDevice.PC:
                            commandString = "SELECT PC.ID as 'ID', " +
                                "PC.InventoryNumber as 'Инвентарный номер', " +
                                "PC.Name as 'Наименование', " +
                                "PC.Cost as 'Цена', " +
                                "MotherBoard as 'Материнская плата', " +
                                "CPUModel as 'Процессор', " +
                                "PC.RAMGB as 'ОЗУ', " +
                                "VideoCard as 'Видеокарта', " +
                                "PC.HDDCapacityGB as 'Объем HDD', " +
                                "dbo.GetNameOS(OSID) as 'Операционная система', " +
                                "dbo.GetNumberInvoice(InvoiceID) as 'Номер накладной', " +
                                "dbo.GetLocalion(PlaceID) as 'Расположение' " +
                                $"from PC where PC.ID={DeviceID}";
                            reader = new SqlCommand(commandString, connection).ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    uint invN = Convert.ToUInt32(reader["Инвентарный номер"]);
                                    string n = reader["Наименование"].ToString();
                                    uint cost = Convert.ToUInt32(reader["Цена"]);
                                    string mb = reader["Материнская плата"].ToString();
                                    string cpu = reader["Процессор"].ToString();
                                    pc = new PC(invN, n, cost, mb, cpu);
                                    inventoryNumberPC.Text = pc.InventoryNumber.ToString();
                                    namePC.Text = pc.Name;
                                    costPC.Text = pc.Cost.ToString();
                                    motherboardPC.Text = pc.Motherboard;
                                    cpuPC.Text = pc.CPU;
                                }
                            }
                            break;
                        case TypeDevice.Notebook:
                            commandString = "SELECT Notebook.ID as 'ID', " +
                                "Notebook.InventoryNumber as 'Инвентарный номер', " +
                                "dbo.GetTypeNotebook(TypeNotebookID) as 'Тип', " +
                                "Notebook.Name as 'Наименование', " +
                                "Notebook.Cost as 'Цена', " +
                                "CPUModel as 'Процессор', " +
                                "RAMGB as 'ОЗУ', " +
                                "VideoCard as 'Видеокарта', " +
                                "ScreenDiagonal as 'Диагональ экрана', " +
                                "HDDCapacityGB as 'Объем HDD', " +
                                "dbo.GetNameOS(OSID) as 'Операционная система', " +
                                "dbo.GetNumberInvoice(InvoiceID) as 'Номер накладной', " +
                                "dbo.GetLocalion(PlaceID) as 'Расположение' " +
                                $"from Notebook where Notebook.ID={DeviceID}";
                            reader = new SqlCommand(commandString, connection).ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    
                                }
                            }
                            break;
                        case TypeDevice.Monitor:
                            commandString = "SELECT Monitor.ID as 'ID', " +
                                "Monitor.InventoryNumber as 'Инвентарный номер', " +
                                "Monitor.Name as 'Наименование', " +
                                "Monitor.Cost as 'Цена', " +
                                "ScreenDiagonal as 'Диагональ экрана', " +
                                "dbo.GetNumberInvoice(InvoiceID) as 'Номер накладной', " +
                                "dbo.GetLocalion(PlaceID) as 'Расположение' " +
                                $"from Monitor where Monitor.ID={DeviceID}";
                            reader = new SqlCommand(commandString, connection).ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    
                                }
                            }
                            break;
                        case TypeDevice.Projector:
                            commandString = "SELECT Projector.ID as 'ID', " +
                                "Projector.InventoryNumber as 'Инвентарный номер', " +
                                "Projector.Name as 'Наименование', " +
                                "Projector.Cost as 'Цена', " +
                                "MaxDiagonal as 'Максимальная диагональ', " +
                                "dbo.GetProjectorTechnology(ProjectorTechnologyID) as 'Технология проецирования', " +
                                "Invoice.Number as 'Номер накладной', " +
                                "dbo.GetLocalion(PlaceID) as 'Расположение' " +
                                $"from Projector where Projector.ID={DeviceID}";
                            reader = new SqlCommand(commandString, connection).ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    
                                }
                            }
                            break;
                        case TypeDevice.InteractiveWhiteboard:
                            commandString = "SELECT InteractiveWhiteboard.ID as 'ID', " +
                                "InteractiveWhiteboard.InventoryNumber as 'Инвентарный номер', " +
                                "InteractiveWhiteboard.Name as 'Наименование', " +
                                "InteractiveWhiteboard.Cost as 'Цена', " +
                                "Diagonal as 'Диагональ', " +
                                "dbo.GetNumberInvoice(InvoiceID) as 'Номер накладной', " +
                                "dbo.GetLocalion(PlaceID) as 'Расположение' " +
                                $"from InteractiveWhiteboard whete InteractiveWhiteboard.ID={DeviceID}";
                            reader = new SqlCommand(commandString, connection).ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    
                                }
                            }
                            break;
                        case TypeDevice.ProjectorScreen:
                            commandString = "SELECT ProjectorScreen.ID as 'ID', " +
                                "ProjectorScreen.InventoryNumber as 'Инвентарный номер', " +
                                "ProjectorScreen.Name as 'Наименование', " +
                                "ProjectorScreen.Cost as 'Цена', " +
                                "Diagonal as 'Диагональ', " +
                                "IsElectronicDrive as 'С электроприводом', " +
                                "dbo.GetNumberInvoice(InvoiceID) as 'Номер накладной', " +
                                "dbo.GetLocalion(PlaceID) as 'Расположение' " +
                                $"from ProjectorScreen where ProjectorScreen.ID={DeviceID}";
                            reader = new SqlCommand(commandString, connection).ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    
                                }
                            }
                            break;
                        case TypeDevice.PrinterScanner:
                            commandString = "SELECT PrinterScanner.ID as 'ID', " +
                                "dbo.GetTypePrinter(TypePrinterID) as 'Тип', " +
                                "PrinterScanner.InventoryNumber as 'Инвентарный номер', " +
                                "PrinterScanner.Name as 'Наименование', " +
                                "PrinterScanner.Cost as 'Цена', " +
                                "dbo.GetPaperSize(PaperSizeID) as 'Максимальный формат', " +
                                "dbo.GetNumberInvoice(InvoiceID) as 'Номер накладной', " +
                                "dbo.GetLocalion(PlaceID) as 'Расположение' " +
                                $"from PrinterScanner where PrinterScanner.ID={DeviceID}";
                            reader = new SqlCommand(commandString, connection).ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    
                                }
                            }
                            break;
                        case TypeDevice.NetworkSwitch:
                            commandString = "SELECT NetworkSwitch.ID as 'ID', " +
                                "NetworkSwitch.InventoryNumber as 'Инвентарный номер', " +
                                "NetworkSwitch.Name as 'Наименование', " +
                                "NetworkSwitch.Cost as 'Цена', " +
                                "NumberOfPorts as 'Количество портов', " +
                                "dbo.GetNumberInvoice(InvoiceID) as 'Номер накладной', " +
                                "dbo.GetLocalion(PlaceID) as 'Расположение' " +
                                $"from NetworkSwitch where NetworkSwitch.ID={DeviceID}";
                            reader = new SqlCommand(commandString, connection).ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    
                                }
                            }
                            break;
                        case TypeDevice.OtherEquipment:
                            commandString = "SELECT OtherEquipment.ID as 'ID', " +
                                "OtherEquipment.InventoryNumber as 'Инвентарный номер', " +
                                "OtherEquipment.Name as 'Наименование', " +
                                "OtherEquipment.Cost as 'Цена', " +
                                "dbo.GetNumberInvoice(InvoiceID) as 'Номер накладной', " +
                                "dbo.GetLocalion(PlaceID) as 'Расположение' " +
                                $"from OtherEquipment where OtherEquipment.ID={DeviceID}";
                            reader = new SqlCommand(commandString, connection).ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    
                                }
                            }
                            break;
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
                //int res = command.ExecuteNonQuery();
            }
        }

        private void ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            GetComboBoxSourcePC(sender);
        }

        private void GetComboBoxSourcePC(object sender)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            if (((ComboBox)sender).Name == "cpuPC")
            {

            }
            switch (((ComboBox)sender).Name)
            {
                case "motherboardPC":
                    adapter = new SqlDataAdapter($"SELECT Motherboard from PC Where Motherboard LIKE N'{motherboardPC.Text}%'", connectionString);
                    set = new DataSet();
                    adapter.Fill(set);
                    motherboardPC.ItemsSource = set.Tables[0].DefaultView;
                    motherboardPC.DisplayMemberPath = "Motherboard";
                    motherboardPC.IsDropDownOpen = true;
                    break;
                case "cpuPC":
                    adapter = new SqlDataAdapter($"SELECT CPUModel from PC Where CPUModel LIKE N'{cpuPC.Text}%'", connectionString);
                    set = new DataSet();
                    adapter.Fill(set);
                    cpuPC.ItemsSource = set.Tables[0].DefaultView;
                    cpuPC.DisplayMemberPath = "CPUModel";
                    cpuPC.IsDropDownOpen = true;
                    break;
            }
        }
    }
}
