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

namespace Vohmencev_ComputerSoft.Pages
{
    /// <summary>
    /// Логика взаимодействия для StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        private Database.Vohmencev_ComputerSoftEntities Connection;
        public Database.ComputerEquipment SelectedEquipment { get; set; }
        public Database.Client SelectedClient { get; set; }
        public Database.OrderInfo NewOrder { get; set; }
        public List<Database.ComputerEquipment> Equipments { get; set; }
        public List<Database.Client> Clients { get; set; }
        public List<Database.EquipmentType> Types { get; set; }
        public List<Database.RepairType> Repairs { get; set; }

        public StaffPage()
        {
            InitializeComponent();
            Connection = Pages.Connector.GetModel();
            Types = Connection.EquipmentType.ToList();
            EquipmentTypeCombo.ItemsSource = Types;
            Repairs = Connection.RepairType.ToList();
            RepairTypeCombo.ItemsSource = Repairs;
            DataContext = this;
            EquipmentListUpdate();
            ClientListUpdate();
        }

        //Элемент Техника

        private void EquipmentAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (EquipmentNumberText == null || EquipmentNameText == null || EquipmentTypeCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            var NewEquipment = new Database.ComputerEquipment();
            NewEquipment.EquipmentSerialNumber = EquipmentNumberText.Text.ToString();
            NewEquipment.EquipmentName = EquipmentNameText.Text.ToString();
            NewEquipment.EquipmentType = (EquipmentTypeCombo.SelectedItem as Database.EquipmentType).EquipmentTypeName;
            Connection.ComputerEquipment.Add(NewEquipment);
            Connection.SaveChanges();
            EquipmentListUpdate();
            EquipmentNumberText.Text = "";
            EquipmentNameText.Text = "";
            EquipmentTypeCombo.SelectedIndex = -1;
            SelectedEquipment = null;
            MessageBox.Show("Новый экземпляр техники успешно добавлен!");
        }

        private void EquipmentRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (EquipmentNumberText == null || EquipmentNameText == null || EquipmentTypeCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            Connection.SaveChanges();
            MessageBox.Show("Данные о технике успешно обновлены!");
        }

        private void EquipmentDropButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentAddButton.IsEnabled = true;
            EquipmentRefreshButton.IsEnabled = false;
            EquipmentList.SelectedIndex = -1;
            EquipmentNumberText.Text = "";
            EquipmentNameText.Text = "";
            EquipmentTypeCombo.SelectedIndex = -1;
            SelectedEquipment = null;
            EquipmentBindingUpdate();
        }

        private void EquipmentListUpdate()
        {
            Equipments = Connection.ComputerEquipment.OrderBy(emp => new { emp.EquipmentSerialNumber }).ToList();
            EquipmentList.ItemsSource = Equipments;
            EquipmentCombo.ItemsSource = Equipments;
        }

        private void EquipmentListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EquipmentList.SelectedIndex != -1)
            {
                EquipmentAddButton.IsEnabled = false;
                EquipmentRefreshButton.IsEnabled = true;
                SelectedEquipment = EquipmentList.SelectedItem as Database.ComputerEquipment;
                EquipmentBindingUpdate();
            }
            else
            {
                EquipmentAddButton.IsEnabled = true;
                EquipmentRefreshButton.IsEnabled = false;
                EquipmentList.SelectedIndex = -1;
                EquipmentNumberText.Text = "";
                EquipmentNameText.Text = "";
                EquipmentTypeCombo.SelectedIndex = -1;
                SelectedEquipment = null;
                EquipmentBindingUpdate();
            }
        }

        private void EquipmentBindingUpdate()
        {
            EquipmentNumberText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            EquipmentNameText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            EquipmentTypeCombo.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateTarget();
        }

        //Элемент Клиент

        private void ClientAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientfNameText == null || ClientPhoneText == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            var NewClient = new Database.Client();
            NewClient.ClientName = ClientfNameText.Text.ToString();
            NewClient.Phone = ClientPhoneText.Text.ToString();
            Connection.Client.Add(NewClient);
            Connection.SaveChanges();
            ClientListUpdate();
            ClientfNameText.Text = "";
            ClientPhoneText.Text = "";
            SelectedClient = null;
            MessageBox.Show("Новый клиент успешно добавлен!");
        }

        private void ClientRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientfNameText == null || ClientPhoneText == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            Connection.SaveChanges();
            MessageBox.Show("Данные о клиенте успешно обновлены!");
        }

        private void ClientDropButton_Click(object sender, RoutedEventArgs e)
        {
            ClientAddButton.IsEnabled = true;
            ClientRefreshButton.IsEnabled = false;
            ClientList.SelectedIndex = -1;
            ClientfNameText.Text = "";
            ClientPhoneText.Text = "";
            SelectedClient = null;
            ClientBindingUpdate();
        }

        private void ClientListUpdate()
        {
            Clients = Connection.Client.OrderBy(emp => new { emp.ClientName }).ToList();
            ClientList.ItemsSource = Clients;
            ClientCombo.ItemsSource = Clients;
        }

        private void ClientListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EquipmentList.SelectedIndex != -1)
            {
                ClientAddButton.IsEnabled = false;
                ClientRefreshButton.IsEnabled = true;
                SelectedClient = ClientList.SelectedItem as Database.Client;
                ClientBindingUpdate();
            }
            else
            {
                ClientAddButton.IsEnabled = true;
                ClientRefreshButton.IsEnabled = false;
                ClientList.SelectedIndex = -1;
                ClientfNameText.Text = "";
                ClientPhoneText.Text = "";
                SelectedClient = null;
                ClientBindingUpdate();
            }
        }

        private void ClientBindingUpdate()
        {
            ClientfNameText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            ClientPhoneText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }

        //Элемент Заказ

        private void OrderAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали клиента!");
                return;
            }
            if (EquipmentCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали технику для ремонта!");
                return;
            }
            if (RepairTypeCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали тип ремонта для техники!");
                return;
            }
            int NewOrderNumber;
            var LastOrder = Connection.OrderInfo.OrderBy(o => o.OrderNumber).ToList().LastOrDefault();
            if (LastOrder == null)
            {
                NewOrderNumber = 1;
            }
            else
            {
                NewOrderNumber = LastOrder.OrderNumber + 1;
            }
            Database.OrderInfo NewOrderInfo = new Database.OrderInfo();
            NewOrderInfo.OrderNumber = NewOrderNumber;
            NewOrderInfo.Client = (ClientCombo.SelectedItem as Database.Client).Phone;
            NewOrderInfo.Equipment= (EquipmentCombo.SelectedItem as Database.ComputerEquipment).EquipmentSerialNumber;
            NewOrderInfo.Repair = (RepairTypeCombo.SelectedItem as Database.RepairType).RepairTypeName;
            NewOrderInfo.OrderStatus = "В работе";
            NewOrderInfo.OrderDate = DateTime.Today;
            Connection.OrderInfo.Add(NewOrderInfo);
            Connection.SaveChanges();
            ClientCombo.SelectedIndex = -1;
            EquipmentCombo.SelectedIndex = -1;
            RepairTypeCombo.SelectedIndex = -1;
            MessageBox.Show("Заказ успешно добавлен!");
        }

        private void OrderDropButton_Click(object sender, RoutedEventArgs e)
        {
            ClientCombo.SelectedIndex = -1;
            EquipmentCombo.SelectedIndex = -1;
            RepairTypeCombo.SelectedIndex = -1;
        }
    }
}
