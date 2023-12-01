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
            List<ServiceAppointment> serviceAppointments = new List<ServiceAppointment>();
            foreach (ServiceAppointmentVM s in serviceAppointmentVMs)
            {
                ServiceAppointment serviceAppointment = new ServiceAppointment();
                serviceAppointment.SupplierID = s.SupplierID;
                serviceAppointment.AppointmentID = s.AppointmentID;
                serviceAppointment.CustomerID = s.CustomerID;
                serviceAppointment.ScheduledDate = s.ScheduledDate;
                serviceAppointment.CarID = s.CarID;
                serviceAppointments.Add(serviceAppointment);
            }
            MyServApps.ItemsSource = serviceAppointments;
        }

        private void MyServApps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = MyServApps.SelectedItem as ServiceAppointment;            
            serviceAppointmentManager.DeleteServiceAppointmentByAppointmentID(item.AppointmentID);
            setUpServiceData();
        }
    }    
}
