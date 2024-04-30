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
        public int CreateNewServiceAppointment(int CarID, string CustomerEmail, int ServiceTypeID, string CustomerComments, DateTime ScheduledDate)
        {
            int rows = 0;
            try
            {
                rows = _serviceAppointmentAccessor.CreateNewServiceAppointment(CarID, CustomerEmail, ServiceTypeID, CustomerComments, ScheduledDate);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create a new service appointment.", ex);
            }
            return rows;           
        }

        public void DeleteServiceAppointmentByAppointmentID(int AppointmentID)
        {
            try
            {
                _serviceAppointmentAccessor.DeleteServiceAppointmentByAppointmentID(AppointmentID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete service appointment by id.", ex);
            }           
        }

        public List<ServiceAppointmentVM> RetrieveServiceAppointments()
        {
            List<ServiceAppointmentVM> serviceAppointment = new List<ServiceAppointmentVM>();
            try
            {
                serviceAppointment = _serviceAppointmentAccessor.RetrieveServiceAppointments();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to view the car's inventory", ex);
            }
            return serviceAppointment; 
        }

        public ServiceAppointmentVM RetrieveServiceAppointmentByAppointmentID(int AppointmentID)
        {
            ServiceAppointmentVM result = null;
            try
            {
                result = _serviceAppointmentAccessor.RetrieveServiceAppointmentByAppointmentID(AppointmentID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve service appointment by its id.", ex);
            }
            return result;
        }

        public void UpdateServiceAppointment(int AppointmentID, int CarID, string CustomerEmail, int ServiceTypeID, string CustomerComments, DateTime ScheduledDate)
        {
            try
            {
                _serviceAppointmentAccessor.UpdateServiceAppointment(AppointmentID, CarID, CustomerEmail, ServiceTypeID, CustomerComments, ScheduledDate);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update service appointment.", ex);
            }           
        }

        public ServiceTypeVM RetrieveServiceTypeByID(int ServiceTypeID)
        {
            ServiceTypeVM result = null;
            try
            {
                result = _serviceAppointmentAccessor.RetrieveServiceTypeByID(ServiceTypeID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve service type by its id.", ex);
            }
            return result;
            
        }

        public ServiceTypeVM RetrieveServiceTypeByDescription(string Description)
        {
            ServiceTypeVM result = null;
            try
            {
                result = _serviceAppointmentAccessor.RetrieveServiceTypeByDescription(Description);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve service type by its description.", ex);
            }
            return result; 
        }
    }
}
