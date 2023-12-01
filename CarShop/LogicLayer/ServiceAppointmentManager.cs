using DataAccessInterface;
using DataAccessLayer;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ServiceAppointmentManager : IServiceAppointmentManager
    {
        private IServiceAppointmentAccessor _serviceAppointmentAccessor = null;

        public ServiceAppointmentManager()
        {
            _serviceAppointmentAccessor = new ServiceAppointmentAccessor();
        }

        public ServiceAppointmentManager(IServiceAppointmentAccessor serviceAppointmentAccessor)
        {
            _serviceAppointmentAccessor = serviceAppointmentAccessor;
        }
        public int CreateNewServiceAppointment(int CarID, int CustomerID, int ServiceTypeID)
        {
            return _serviceAppointmentAccessor.CreateNewServiceAppointment(CarID, CustomerID, ServiceTypeID);
        }

        public void DeleteServiceAppointmentByAppointmentID(int AppointmentID)
        {
            _serviceAppointmentAccessor.DeleteServiceAppointmentByAppointmentID(AppointmentID);
        }

        public List<ServiceAppointmentVM> RetrieveServiceAppointments()
        {
            return _serviceAppointmentAccessor.RetrieveServiceAppointments();
        }

        public ServiceAppointmentVM RetrieveServiceAppointmentByAppointmentID(int AppointmentID)
        {
            return _serviceAppointmentAccessor.RetrieveServiceAppointmentByAppointmentID(AppointmentID);
        }

        public void UpdateServiceAppointment(int AppointmentID, int CarID, int CustomerID, int ServiceTypeID, int SupplierID, DateTime ScheduledDate)
        {
            _serviceAppointmentAccessor.UpdateServiceAppointment(AppointmentID, CarID, CustomerID, ServiceTypeID, SupplierID, ScheduledDate);
        }
    }
}
