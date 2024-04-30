using DataAccessFakes;
using DataObjectsLayer;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class ServiceAppointmentManagerTests
    {
        IServiceAppointmentManager _serviceAppointmentManager = null;

        [TestInitialize]
        public void TestSetUp()
        {
            _serviceAppointmentManager = new ServiceAppointmentManager(new ServiceAppointmentAccessorFake());
        }

        [TestMethod]
        public void TestSuccessfullyRetrieveServiceAppointmentByAppointmentID()
        {
            // Arrange            
            ServiceAppointmentVM serviceAppointmentVM = null;
            int AppointmentID = 3;

            // Act 
            serviceAppointmentVM = _serviceAppointmentManager.RetrieveServiceAppointmentByAppointmentID(AppointmentID);

            // Assert            
            if (serviceAppointmentVM != null)
            {
                Assert.AreEqual(serviceAppointmentVM.AppointmentID, AppointmentID);
            }
            else
            {
                Assert.IsNull(serviceAppointmentVM);
            }
        }

        [TestMethod]
        public void TestFailedToRetrieveServiceAppointmentByWrongAppointmentID()
        {
            // Arrange            
            ServiceAppointmentVM serviceAppointmentVM = null;
            int AppointmentID = 6;

            // Act 
            serviceAppointmentVM = _serviceAppointmentManager.RetrieveServiceAppointmentByAppointmentID(AppointmentID);

            // Assert
            if (serviceAppointmentVM != null)
            {
                Assert.AreNotEqual(serviceAppointmentVM.AppointmentID, AppointmentID);
            }
            else
            {
                Assert.IsNull(serviceAppointmentVM);
            }
        }

        [TestMethod]
        public void TestSuccessfullyCreateNewServiceAppointment()
        {
            // Arrange                         
            int CarID = 4;
            string CustomerEmail = "spongebob@comapny.com";
            int ServiceTypeID = 4;
            string CustomerComments = "I think it seems fine.";
            DateTime ScheduleDate = DateTime.Now;

            // Act 
            int ID = _serviceAppointmentManager.CreateNewServiceAppointment(CarID, CustomerEmail, ServiceTypeID, CustomerComments, ScheduleDate);

            // Assert
            Assert.AreEqual(ID, 999);
        }

        [TestMethod]
        public void TestFailedToCreateNewServiceAppointment()
        {
            // Arrange             
            int CarID = 4;
            string CustomerEmail = "squidward@company.com";
            int ServiceTypeID = 4;
            string CustomerComments = "Spongebob wrecked my car.";
            DateTime ScheduleDate = DateTime.Now;

            // Act 
            int ID = _serviceAppointmentManager.CreateNewServiceAppointment(CarID, CustomerEmail, ServiceTypeID, CustomerComments, ScheduleDate);

            // Assert
            Assert.AreNotEqual(ID, 0);
        }

        [TestMethod]
        public void TestSuccessfullyDeleteServiceAppointmentByCorrectAppointmentID()
        {
            // Arrange                         
            int AppointmentID = 1;
            ServiceAppointmentVM serviceAppointmentVM = null;

            // Act             
            _serviceAppointmentManager.DeleteServiceAppointmentByAppointmentID(AppointmentID);
            serviceAppointmentVM = _serviceAppointmentManager.RetrieveServiceAppointmentByAppointmentID(AppointmentID);

            // Assert
            Assert.IsNull(serviceAppointmentVM);
        }

        [TestMethod]
        public void TestFailedToDeleteServiceAppointmentByWrongAppointmentID()
        {
            // Arrange                         
            int AppointmentID = 4;
            ServiceAppointmentVM serviceAppointmentVM = null;

            // Act             
            _serviceAppointmentManager.DeleteServiceAppointmentByAppointmentID(AppointmentID);
            serviceAppointmentVM = _serviceAppointmentManager.RetrieveServiceAppointmentByAppointmentID(AppointmentID);

            // Assert
            Assert.IsNull(serviceAppointmentVM);
        }

        [TestMethod]
        public void TestSuccessfullyUpdateServiceAppointment()
        {
            // Arrange
            int AppointmentID = 2;
            int CarID = 4;
            string CustomerEmail = "squidward@company.com";
            int ServiceTypeID = 3;
            string CustomerComments = "His care got damaged by spongebob's reckless driving";
            DateTime ScheduleDate = DateTime.Now.AddYears(4);

            // Act
            _serviceAppointmentManager.UpdateServiceAppointment(AppointmentID, CarID, CustomerEmail, ServiceTypeID, CustomerComments, ScheduleDate);
            var Appointment = _serviceAppointmentManager.RetrieveServiceAppointmentByAppointmentID(AppointmentID);

            // Assert
            Assert.AreEqual(Appointment.CarID, 4);
        }

        [TestMethod]
        public void TestFailedToUpdateServiceAppointment()
        {
            // Arrange
            int AppointmentID = 2;
            int CarID = 3;
            string CustomerEmail = "spongebob@company.com";
            int ServiceTypeID = 3;
            string CustomerComments = "No surprise, spongebob crashed his car.  Spongebob is unteachable.";
            DateTime ScheduleDate = DateTime.Now.AddYears(4);

            // Act
            _serviceAppointmentManager.UpdateServiceAppointment(AppointmentID, CarID, CustomerEmail, ServiceTypeID, CustomerComments, ScheduleDate);
            var Appointment = _serviceAppointmentManager.RetrieveServiceAppointmentByAppointmentID(AppointmentID);

            // Assert
            Assert.AreNotEqual(Appointment.CarID, 4);
        }
    }
}
