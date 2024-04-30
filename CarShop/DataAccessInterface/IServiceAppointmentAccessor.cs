using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface IServiceAppointmentAccessor
    {
        int CreateNewServiceAppointment(int CarID, string CustomerEmail, int ServiceTypeID, string customerComments, DateTime scheduledDate);
        void UpdateServiceAppointment(int AppointmentID, int CarID, string CustomerEmail, int ServiceTypeID, string CustomerComments, DateTime ScheduledDate);
        ServiceAppointmentVM RetrieveServiceAppointmentByAppointmentID(int AppointmentID);
        List<ServiceAppointmentVM> RetrieveServiceAppointments();
        ServiceTypeVM RetrieveServiceTypeByID(int ServiceTypeID);
        void DeleteServiceAppointmentByAppointmentID(int AppointmentID);
        ServiceTypeVM RetrieveServiceTypeByDescription(string Description);
    }
}
