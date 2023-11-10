using DataAccessFakes;
using DataObjectsLayer;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class EmployeeManagerTests
    {
        IEmployeeManager _employeeManager = null;

        [TestInitialize]
        public void TestSetUp()
        {
            _employeeManager = new EmployeeManager(new EmployeeAccessorFake());
        }

        [TestMethod]
        public void TestHashSha256ReturnsACorrectHashValue()
        {            
            // Arrange 
            EmployeeManager employeeManager = new EmployeeManager();
            string testString = "test123";
            string actualHash = "";
            string expectedHash = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae";

            // Act 
            actualHash = employeeManager.HashSha256(testString);

            // Assert
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void TestHashSha256FailsABadHash()
        {
            // Arrange 
            EmployeeManager employeeManager = new EmployeeManager();
            string testString = "test321";
            string actualHash = "";
            string expectedHash = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae";

            // Act 
            actualHash = employeeManager.HashSha256(testString);

            // Assert
            Assert.AreNotEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void TestAuthenticateEmployeeByCorrectEmailAndPassword()
        {            
            // Arrange             
            EmployeeVM employeeVM = null;
            string email = "max@carshop.com";            
            string password = "test123";

            // Act 
            employeeVM = _employeeManager.AuthenticateEmployee(email, password);

            // Assert
            Assert.IsNotNull(employeeVM);
        }
        
        [TestMethod]
        public void TestFailAuthenticateEmployeeByWrongEmailAndPassword()
        {
            // Arrange             
            EmployeeVM employeeVM = null;
            string email = "max@carshop.com";
            string password = "test321";

            // Act 
            employeeVM = _employeeManager.AuthenticateEmployee(email, password);

            // Assert
            Assert.IsNull(employeeVM);
        }

        [TestMethod]
        public void TestGetEmployeeVMByCorrectEmail()
        {           
            // Arrange             
            EmployeeVM employeeVM = null;
            string email = "max@carshop.com";            

            // Act 
            employeeVM = _employeeManager.GetEmployeeVMByEmail(email);

            // Assert
            Assert.AreEqual(employeeVM.Email, email);
        }

        [TestMethod]
        public void TestFailEmployeeVMByWrongEmail()
        {
            // Arrange             
            EmployeeVM employeeVM = null;
            string email = "4";

            // Act 
            employeeVM = _employeeManager.GetEmployeeVMByEmail(email);

            // Assert
            Assert.AreNotEqual(employeeVM.Email, email);
        }

        [TestMethod]
        public void TestGetRoleByCorrectEmployeeID()
        {
            // Arrange             
            string role = "";
            int employeeID = 4;

            // Act 
            role = _employeeManager.GetRolesByEmployeeID(employeeID);

            // Assert
            Assert.AreEqual("Service Representative", role);
        }

        [TestMethod]
        public void TestFailRoleByWrongEmployeeID()
        {
            // Arrange             
            string role = "";
            int employeeID = 3;

            // Act 
            role = _employeeManager.GetRolesByEmployeeID(employeeID);

            // Assert
            Assert.AreNotEqual("Service Representative", role);
        }

        [TestMethod]
        public void TestSuccessfullyResetPassword()
        {
            // Arrange
            string email = "max@carshop.com";
            string oldPassword = "test123";
            string newPassword = "test321";

            // Act
            bool result = _employeeManager.ResetPassword(email, oldPassword, newPassword);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestFailedToResetPassword()
        {
            // Arrange
            string email = "max@carshop.com";
            string oldPassword = "test321";
            string newPassword = "test123";

            // Act
            bool result = _employeeManager.ResetPassword(email, oldPassword, newPassword);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
