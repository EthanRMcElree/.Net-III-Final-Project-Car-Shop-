using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class ServiceAppointment
    {
        /*
         AppointmentID	int
        CarID	int
        CustomerID	int
        ServiceTypeID	int
        SupplierID	int
        ScheduledDate	datetime
         */
        public int AppointmentID { get; set; }
        public int CarID { get; set; }
        public int CustomerID { get; set; }
        public int ServiceTypeID { get; set; }
        public int SupplierID { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
    public class ServiceAppointmentVM : ServiceAppointment
    {
        public CarInventory Car { get; set; }
        public Customer Customer { get; set; }
        public ServiceType ServiceType { get; set; }
        public Supplier Supplier { get; set; }
    }
}
