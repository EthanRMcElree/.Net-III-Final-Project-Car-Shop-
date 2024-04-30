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
        public List<ServiceTypeVM> fakeServiceType = new List<ServiceTypeVM>();

        public ServiceAppointmentAccessorFake()
        {
            fakeServiceAppointment.Add(new ServiceAppointmentVM()
            {
                AppointmentID = 1,
                CarID = 1,
                CustomerEmail = "fake@email.com",
                ServiceTypeID = 1,
                CustomerComments = "My car's engine is making a strange noise.",
                ScheduledDate = DateTime.Now
            });

            fakeServiceAppointment.Add(new ServiceAppointmentVM()
            {
                AppointmentID = 2,
                CarID = 2,
                CustomerEmail = "notReal@email.com",
                ServiceTypeID = 2,
                CustomerComments = "My car is starting to get low on oil.",
                ScheduledDate = DateTime.Now.AddDays(23)
            });

            fakeServiceAppointment.Add(new ServiceAppointmentVM()
            {
                AppointmentID = 3,
                CarID = 3,
                CustomerEmail = "madeUp@email.com",
                ServiceTypeID = 3,
                CustomerComments = "Someone accidentally bumped into my car and put a dent in it.",
                ScheduledDate = DateTime.Now.AddYears(4)
            });

            fakeServiceType.Add(new ServiceTypeVM()
            {
                ServiceTypeID = 1,
                ServiceDescription = "Test1",
            });

            fakeServiceType.Add(new ServiceTypeVM()
            {
                ServiceTypeID = 2,
                ServiceDescription = "Test2",
            });

            fakeServiceType.Add(new ServiceTypeVM()
            {
                ServiceTypeID = 3,
                ServiceDescription = "Test3",
            });
        }

        public int CreateNewServiceAppointment(int CarID, string CustomerEmail, int ServiceTypeID, string CustomerComments, DateTime ScheduleDate)
        {
            if(ServiceTypeID <= 0)
            {
                return 0;
            }
            fakeServiceAppointment.Add(new ServiceAppointmentVM()
            {
                CarID = CarID,
                CustomerEmail = CustomerEmail,
                ServiceTypeID = ServiceTypeID,
                CustomerComments = CustomerComments,
                ScheduledDate = ScheduleDate
            });
            return 999;
        }

        public void DeleteServiceAppointmentByAppointmentID(int AppointmentID)
        {
            fakeServiceAppointment.Remove(RetrieveServiceAppointmentByAppointmentID(AppointmentID));
        }

        public ServiceAppointmentVM RetrieveServiceAppointmentByAppointmentID(int AppointmentID)
        {            
            foreach (ServiceAppointmentVM serviceAppointment in fakeServiceAppointment)
            {
                if(serviceAppointment.AppointmentID == AppointmentID)
                {
                    return serviceAppointment;                  
                }
            }
            return null;
        }

        public List<ServiceAppointmentVM> RetrieveServiceAppointments()
        {
            return fakeServiceAppointment;
        }

        public ServiceTypeVM RetrieveServiceTypeByID(int ServiceTypeID)
        {
            foreach (var serviceType in fakeServiceType)
            {
                if(serviceType.ServiceTypeID == ServiceTypeID)
                {
                    return serviceType;
                }
            }
            return null;
        }

        public ServiceTypeVM RetrieveServiceTypeByDescription(string Description)
        {
            return null;
        }

        public void UpdateServiceAppointment(int AppointmentID, int CarID, string CustomerEmail, int ServiceTypeID, string CustomerComments, DateTime ScheduledDate)
        {
            var serviceAppointmentToUpdate = RetrieveServiceAppointmentByAppointmentID(AppointmentID);

            if (serviceAppointmentToUpdate != null)
            {
                serviceAppointmentToUpdate.CarID = CarID;
                serviceAppointmentToUpdate.CustomerEmail = CustomerEmail;
                serviceAppointmentToUpdate.ServiceTypeID = ServiceTypeID;
                serviceAppointmentToUpdate.CustomerComments = CustomerComments;
                serviceAppointmentToUpdate.ScheduledDate = ScheduledDate;                
            }
        }
    }
}
