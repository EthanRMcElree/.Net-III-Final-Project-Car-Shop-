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
    public class SalesManagerTests
    {
        ISalesManager _salesManager = null;

        [TestInitialize]
        public void TestSetUp()
        {
            _salesManager = new SalesManager(new SalesAccessorFake());
        }

        [TestMethod]
        public void TestSuccessfullyCreateSale()
        {
            // Arrange
            int EmployeeID = 4;
            int CarID = 4;
            int CustomerID = 4;
            DateTime SaleDate = DateTime.Now.AddDays(23);
            float SalePrice = 22500.50F;

            // Act
            int ID = _salesManager.CreateSale(EmployeeID, CarID, CustomerID, SaleDate, SalePrice);

            // Assert
            Assert.AreEqual(999, ID);
        }

        [TestMethod]
        public void TestFailedToCreateSale()
        {
            // Arrange
            int EmployeeID = 4;
            int CarID = 0;
            int CustomerID = 4;
            DateTime SaleDate = DateTime.Now.AddDays(23);
            float SalePrice = 22500.50F;

            // Act
            int ID = _salesManager.CreateSale(EmployeeID, CarID, CustomerID, SaleDate, SalePrice);

            // Assert
            Assert.AreEqual(0, ID);
        }

        [TestMethod]
        public void TestSuccessfullyDeletedCarByID()
        {
            // Arrange
            int SaleID = 1;
            SalesVM salesVM = null;

            // Act
            _salesManager.DeleteSaleByID(SaleID);
            salesVM = _salesManager.ViewSaleByID(SaleID);

            // Assert
            Assert.IsNull(salesVM);
        }

        [TestMethod]
        public void TestFailedToDeleteCarByID()
        {
            // Arrange
            int SaleID = 999999999;
            SalesVM salesVM = null;

            // Act
            _salesManager.DeleteSaleByID(SaleID);
            salesVM = _salesManager.ViewSaleByID(SaleID);

            // Assert
            Assert.IsNull(salesVM);
        }

        [TestMethod]
        public void TestSuccessfullyViewedSales()
        {
            // Arrange
            List<SalesVM> salesVMs = new List<SalesVM>();

            // Act
            salesVMs = _salesManager.ViewSales();

            // Assert
            Assert.AreEqual(salesVMs.Count, 3);
        }

        [TestMethod]
        public void TestFailedToViewSales()
        {
            // Arrange
            List<SalesVM> salesVMs = new List<SalesVM>();

            // Act
            salesVMs = _salesManager.ViewSales();
            salesVMs.Remove(salesVMs[0]);

            // Assert
            Assert.AreNotEqual(salesVMs.Count, 3);
        }

        [TestMethod]
        public void TestSuccessfullyViewSaleByCorrectID()
        {
            // Arrange
            int SaleID = 1;
            SalesVM salesVM = null;

            // Act
            salesVM = _salesManager.ViewSaleByID(SaleID);

            // Assert
            Assert.AreEqual(SaleID, salesVM.SaleID);
        }

        [TestMethod]
        public void TestFailedToViewSaleByCorrectID()
        {
            // Arrange
            int SaleID = 999999999;
            SalesVM salesVM = null;

            // Act
            salesVM = _salesManager.ViewSaleByID(SaleID);

            // Assert
            Assert.IsNull(salesVM);
        }

        [TestMethod]
        public void TestSuccessfullyViewedSalesForEmployee()
        {
            // Arrange
            List<SalesVM> salesVMs = new List<SalesVM>();
            int EmployeeID = 1;

            // Act
            salesVMs = _salesManager.ViewSalesForEmployee(EmployeeID);

            // Assert
            Assert.AreEqual(salesVMs.Count, 1);
        }

        [TestMethod]
        public void TestFailedToViewSalesForEmployee()
        {
            // Arrange
            List<SalesVM> salesVMs = new List<SalesVM>();

            // Act
            salesVMs = _salesManager.ViewSalesForEmployee(2);

            // Assert
            Assert.AreNotEqual(salesVMs.Count, 1);
        }
    }
}
