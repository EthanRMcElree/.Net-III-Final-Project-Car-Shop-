using DataAccessFakes;
using DataObjectsLayer;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class CarInventoryManagerTests
    {
        ICarInventoryManager _carInventoryManager = null;

        [TestInitialize]
        public void TestSetUp() 
        {
            _carInventoryManager = new CarInventoryManager(new CarInventoryAccessorFake());
        }

        [TestMethod]
        public void TestSuccessfullyInsertedNewCar()
        {
            // Arrange
            string Model = "ascent";
            int Year = 2024;
            string Color = "red";
            string VIN = "1E8H8A40";
            float Price = 27550.00F;
            int Mileage = 12000;
            string FuelType = "hybrid";
            string TransmissionType = "automatic";
            float EngineSize = 2.5F;
            string Description = "awesome car";

            // Act
            int rows = _carInventoryManager.InsertNewCar(Model, Year, Color, VIN, Price, Mileage, FuelType, TransmissionType, EngineSize, Description);

            // Assert
            Assert.AreEqual(1, rows);
        }

        [TestMethod]
        public void TestFailedToInsertNewCar()
        {
            // Arrange
            string Model = "ascent";
            int Year = 0;
            string Color = null;
            string VIN = "1A8HWE40";
            float Price = 27550.00F;
            int Mileage = 12000;
            string FuelType = "hybrid";
            string TransmissionType = "automatic";
            float EngineSize = 2.5F;
            string Description = "awesome car";

            // Act
            int rows = _carInventoryManager.InsertNewCar(Model, Year, Color, VIN, Price, Mileage, FuelType, TransmissionType, EngineSize, Description);

            // Assert
            Assert.AreEqual(0, rows);
        }

        [TestMethod]
        public void TestSuccessfullyViewCarByCorrectID()
        {
            // Arrange
            int CarID = 1;
            CarInventoryVM carInventoryVM = null;

            // Act
            carInventoryVM = _carInventoryManager.ViewCarByID(CarID);

            // Assert
            Assert.AreEqual(CarID, carInventoryVM.CarID);
        }

        [TestMethod]
        public void TestFailedToViewCarByCorrectID()
        {
            // Arrange
            int CarID = 999999999;
            CarInventoryVM carInventoryVM = null;

            // Act
            carInventoryVM = _carInventoryManager.ViewCarByID(CarID);

            // Assert
            Assert.IsNull(carInventoryVM);
        }

        [TestMethod]
        public void TestSuccessfullyDeletedCarByID()
        {
            // Arrange
            int CarID = 1;
            CarInventoryVM carInventoryVM = null;

            // Act
            _carInventoryManager.DeleteCarByID(CarID);
            carInventoryVM = _carInventoryManager.ViewCarByID(CarID);

            // Assert
            Assert.IsNull(carInventoryVM);
        }

        [TestMethod]
        public void TestFailedToDeleteCarByID()
        {
            // Arrange
            int CarID = 999999999;
            CarInventoryVM carInventoryVM = null;

            // Act
            _carInventoryManager.DeleteCarByID(CarID);
            carInventoryVM = _carInventoryManager.ViewCarByID(CarID);

            // Assert
            Assert.IsNull(carInventoryVM);
        }

        [TestMethod]
        public void TestSuccessfullyViewedCarInventory()
        {
            // Arrange
            List<CarInventoryVM> carInventoryVMs = new List<CarInventoryVM>();

            // Act
            carInventoryVMs = _carInventoryManager.ViewCarInventory();

            // Assert
            Assert.AreEqual(carInventoryVMs.Count, 3);
        }

        [TestMethod]
        public void TestFailedToViewCarInventory()
        {
            // Arrange
            List<CarInventoryVM> carInventoryVMs = new List<CarInventoryVM>();

            // Act
            carInventoryVMs = _carInventoryManager.ViewCarInventory();
            carInventoryVMs.Remove(carInventoryVMs[0]);

            // Assert
            Assert.AreNotEqual(carInventoryVMs.Count, 3);
        }

        [TestMethod]
        public void TestSuccessfullyUpdateCar()
        {
            // Arrange
            int CarID = 3;
            string Model = "ascent";
            int Year = 2024;
            string Color = "red";
            string VIN = "1A8HWE40";
            float Price = 27550.00F;
            int Mileage = 15000;
            string FuelType = "hybrid";
            string TransmissionType = "automatic";
            float EngineSize = 2.5F;
            string Description = "awesome car";

            // Act
            _carInventoryManager.UpdateCar(CarID, Model, Year, Color, VIN, Price, Mileage, FuelType, TransmissionType, EngineSize, Description);
            var Car = _carInventoryManager.ViewCarByID(CarID);

            // Assert
            Assert.AreEqual(Car.Mileage, 15000);
        }

        [TestMethod]
        public void TestFailedToUpdateCar()
        {
            // Arrange
            int CarID = 3;
            string Model = "ascent";
            int Year = 2024;
            string Color = "red";
            string VIN = "1A8HWE40";
            float Price = 27550.00F;
            int Mileage = 12000;
            string FuelType = "hybrid";
            string TransmissionType = "automatic";
            float EngineSize = 2.5F;
            string Description = "awesome car";

            // Act
            _carInventoryManager.UpdateCar(CarID, Model, Year, Color, VIN, Price, Mileage, FuelType, TransmissionType, EngineSize, Description);
            var Car = _carInventoryManager.ViewCarByID(CarID);

            // Assert
            Assert.AreNotEqual(Car.Mileage, 15000);
        }

        [TestMethod]
        public void TestSuccessfullyFilterCarByFuelType()
        {
            // Arrange
            string FuelType = "regular";
            List<CarInventoryVM> carInventoryVMs = new List<CarInventoryVM>();

            // Act
            carInventoryVMs = _carInventoryManager.FilterCarByFuelType(FuelType);

            // Assert
            Assert.AreEqual(carInventoryVMs.Count, 1);
        }

        [TestMethod]
        public void TestFailedToFilterCarByFuelType()
        {
            // Arrange
            string FuelType = "hybrid";
            List<CarInventoryVM> carInventoryVMs = new List<CarInventoryVM>();

            // Act
            carInventoryVMs = _carInventoryManager.FilterCarByFuelType(FuelType);

            // Assert
            Assert.AreNotEqual(carInventoryVMs.Count, 3);
        }

        [TestMethod]
        public void TestSuccessfullyFilterCarByHighMileage()
        {
            // Arrange
            List<CarInventoryVM> carInventoryVMs = new List<CarInventoryVM>();

            // Act
            carInventoryVMs = _carInventoryManager.FilterCarByHighMileage();

            // Assert            
            Assert.AreEqual(carInventoryVMs.Count, 1);                       
        }

        [TestMethod]
        public void TestFailedToFilterCarByHighMileage()
        {
            // Arrange            
            List<CarInventoryVM> carInventoryVMs = new List<CarInventoryVM>();

            // Act
            carInventoryVMs = _carInventoryManager.FilterCarByHighMileage();

            // Assert
            Assert.AreNotEqual(carInventoryVMs.Count, 3);           
        }

        [TestMethod]
        public void TestSuccessfullyFilterCarByLowMileage()
        {
            // Arrange
            List<CarInventoryVM> carInventoryVMs = new List<CarInventoryVM>();

            // Act
            carInventoryVMs = _carInventoryManager.FilterCarByLowMileage();

            // Assert            
            Assert.AreEqual(carInventoryVMs.Count, 1);
        }

        [TestMethod]
        public void TestFailedToFilterCarByLowMileage()
        {
            // Arrange            
            List<CarInventoryVM> carInventoryVMs = new List<CarInventoryVM>();

            // Act
            carInventoryVMs = _carInventoryManager.FilterCarByLowMileage();

            // Assert
            Assert.AreNotEqual(carInventoryVMs.Count, 3);
        }

        [TestMethod]
        public void TestSuccessfullyFilterCarByModerateMileage()
        {
            // Arrange
            List<CarInventoryVM> carInventoryVMs = new List<CarInventoryVM>();

            // Act
            carInventoryVMs = _carInventoryManager.FilterCarByModerateMileage();

            // Assert            
            Assert.AreEqual(carInventoryVMs.Count, 1);
        }

        [TestMethod]
        public void TestFailedToFilterCarByModerateMileage()
        {
            // Arrange            
            List<CarInventoryVM> carInventoryVMs = new List<CarInventoryVM>();

            // Act
            carInventoryVMs = _carInventoryManager.FilterCarByModerateMileage();

            // Assert
            Assert.AreNotEqual(carInventoryVMs.Count, 3);
        }
    }
}
