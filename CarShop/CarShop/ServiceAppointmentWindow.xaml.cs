using DataObjectsLayer;
using LogicLayer;
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
using System.Windows.Shapes;

namespace CarShop
{
    /// <summary>
    /// Interaction logic for ServiceAppointmentWindow.xaml
    /// </summary>
    public partial class ServiceAppointmentWindow : Window
    {
        ServiceAppointmentManager serviceAppointmentManager = null;
        public ServiceAppointmentWindow()
        {
            InitializeComponent();
            serviceAppointmentManager = new ServiceAppointmentManager();           
            setUpServiceData();
        }

        private void mnuServAppExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void setUpServiceData()
        {
            List<ServiceAppointmentVM> serviceAppointmentVMs = serviceAppointmentManager.RetrieveServiceAppointments();
            List<ServiceDisplayItem> display = new List<ServiceDisplayItem>();
            foreach (ServiceAppointmentVM s in serviceAppointmentVMs)
            {
                ServiceDisplayItem lineItem = new ServiceDisplayItem();
                lineItem.AppointmentID = s.AppointmentID;
                lineItem.customer = s.Customer;
                lineItem.car = s.Car;
                lineItem.serviceType = s.ServiceType;
                lineItem.supplier = s.Supplier;
                lineItem.scheduleDate = s.ScheduledDate;
                display.Add(lineItem);
            }
            MyServApps.ItemsSource = display;
        }

        private void MyServApps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = MyServApps.SelectedItem as ServiceDisplayItem;            
            serviceAppointmentManager.DeleteServiceAppointmentByAppointmentID(item.AppointmentID);
            setUpServiceData();
        }
    }
    class ServiceDisplayItem
    {
        public int AppointmentID { get; set; }

        public Customer customer { get; set; }

        public CarInventory car { get; set; }

        public ServiceType serviceType { get; set; }

        public Supplier supplier { get; set; }

        public DateTime scheduleDate { get; set; }


    }
}
