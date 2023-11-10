using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IServiceAppointmentManager
    {
        int CreateNewServiceAppointment(int CarID, int CustomerID, int ServiceTypeID);
        void DeleteServiceAppointmentByAppointmentID(int AppointmentID);
        ServiceAppointmentVM RetrieveServiceAppointmentByAppointmentID(int AppointmentID);
        void UpdateServiceAppointment(int AppointmentID, int CarID, int CustomerID, int ServiceTypeID, int SupplierID, DateTime ScheduledDate);
    }
}
