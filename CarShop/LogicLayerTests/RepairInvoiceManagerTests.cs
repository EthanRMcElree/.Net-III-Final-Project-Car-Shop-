using DataAccessFakes;
using DataAccessInterface;
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
    public class RepairInvoiceManagerTests
    {
        IRepairInvoiceManager _repairInvoiceManager = null;

        [TestInitialize]
        public void TestSetUp()
        {
            _repairInvoiceManager = new RepairInvoiceManager(new RepairInvoiceAccessorFake());
        }

        [TestMethod]
        public void TestSuccessfullyCreateNewRepairInvoice()
        {
            // Arrange
            int CarID = 4;
            int EmployeeID = 4;
            string IssueDescription = "Replace oxygen sensor";
            DateTime RepairDate = DateTime.Now;

            // Act
            int ID = _repairInvoiceManager.CreateRepairInvoice(CarID, EmployeeID, IssueDescription, RepairDate);

            // Assert
            Assert.AreEqual(999, ID);
        }

        [TestMethod]
        public void TestFailedToCreateNewRepairInvoice()
        {
            // Arrange
            int CarID = 4;
            int EmployeeID = 0;
            string IssueDescription = "Replace oxygen sensor";
            DateTime RepairDate = DateTime.Now;

            // Act
            int ID = _repairInvoiceManager.CreateRepairInvoice(CarID, EmployeeID, IssueDescription, RepairDate);

            // Assert
            Assert.AreEqual(0, ID);
        }
    }
}
