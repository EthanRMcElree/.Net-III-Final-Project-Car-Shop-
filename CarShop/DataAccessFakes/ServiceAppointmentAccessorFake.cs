using DataAccessInterface;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class ServiceAppointmentAccessorFake : IServiceAppointmentAccessor
    {        
        public List<ServiceAppointmentVM> fakeServiceAppointment = new List<ServiceAppointmentVM>();

        public ServiceAppointmentAccessorFake()
        {
            fakeServiceAppointment.Add(new ServiceAppointmentVM()
            {
                AppointmentID = 1,
                CarID = 1,
                CustomerID = 1,
                ServiceTypeID = 1,
                SupplierID = 1,
                ScheduledDate = DateTime.Now
            });

            fakeServiceAppointment.Add(new ServiceAppointmentVM()
            {
                AppointmentID = 2,
                CarID = 2,
                CustomerID = 2,
                ServiceTypeID = 2,
                SupplierID = 2,
                ScheduledDate = DateTime.Now.AddDays(23)
            });

            fakeServiceAppointment.Add(new ServiceAppointmentVM()
            {
                AppointmentID = 3,
                CarID = 3,
                CustomerID = 3,
                ServiceTypeID = 3,
                SupplierID = 3,
                ScheduledDate = DateTime.Now.AddYears(4)
            });
        }

        public int CreateNewServiceAppointment(int CarID, int CustomerID, int ServiceTypeID)
        {
            if(CustomerID <= 0)
            {
                return 0;
            }
            fakeServiceAppointment.Add(new ServiceAppointmentVM()
            {
                CarID = CarID,
                CustomerID = CustomerID,
                ServiceTypeID = ServiceTypeID,
            });
            return 999;
        }

        public void DeleteServiceAppointmentByAppointmentID(int AppointmentID)
        {
            fakeServiceAppointment.Remove(RetrieveServiceAppointmentByAppointmentID(AppointmentID));
        }

        public ServiceAppointmentVM RetrieveServiceAppointmentByAppointmentID(int AppointmentID)
        {
            // ServiceAppointmentVM serviceAppointmentVM = new ServiceAppointmentVM();

            foreach (ServiceAppointmentVM serviceAppointment in fakeServiceAppointment)
            {
                if(serviceAppointment.AppointmentID == AppointmentID)
                {
                    return serviceAppointment;                  
                }
            }
            return null;
        }

        public void UpdateServiceAppointment(int AppointmentID, int CarID, int CustomerID, int ServiceTypeID, int SupplierID, DateTime ScheduledDate)
        {
            var serviceAppointmentToUpdate = RetrieveServiceAppointmentByAppointmentID(AppointmentID);

            if (serviceAppointmentToUpdate != null)
            {
                serviceAppointmentToUpdate.CarID = CarID;
                serviceAppointmentToUpdate.CustomerID = CustomerID;
                serviceAppointmentToUpdate.ServiceTypeID = ServiceTypeID;
                serviceAppointmentToUpdate.SupplierID = SupplierID;
                serviceAppointmentToUpdate.ScheduledDate = ScheduledDate;                
            }
        }
    }
}
