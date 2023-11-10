using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataObjectsLayer;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests
{
    [TestClass]
    public class CustomerManagerTests
    {
        ICustomerManager _customerManager = null;

        [TestInitialize]
        public void TestSetUp()
        {
            _customerManager = new CustomerManager(new CustomerAccessorFake());
        }

        [TestMethod]
        public void TestHashSha256ReturnsACorrectHashValue()
        {
            // Arrange 
            CustomerManager customerManager = new CustomerManager();
            string testString = "test123";
            string actualHash = "";
            string expectedHash = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae";

            // Act 
            actualHash = customerManager.HashSha256(testString);

            // Assert
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void TestHashSha256FailsABadHashValue()
        {
            // Arrange 
            CustomerManager customerManager = new CustomerManager();
            string testString = "test321";
            string actualHash = "";
            string expectedHash = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae";

            // Act 
            actualHash = customerManager.HashSha256(testString);

            // Assert
            Assert.AreNotEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void TestAuthenticateCustomerByCorrectEmailAndPassword()
        {
            // Arrange             
            CustomerVM customerVM = null;
            string email = "sam@email.com";
            string password = "test123";

            // Act 
            customerVM = _customerManager.AuthenticateCustomer(email, password);

            // Assert
            Assert.IsNotNull(customerVM);
        }

        [TestMethod]
        public void TestFailAuthenticateCustomerByWrongEmailAndPassword()
        {
            // Arrange             
            CustomerVM customerVM = null;
            string email = "sam@email.com";
            string password = "test321";

            // Act 
            customerVM = _customerManager.AuthenticateCustomer(email, password);

            // Assert
            Assert.IsNull(customerVM);
        }

        [TestMethod]
        public void TestGetCustomerVMByCorrectEmail()
        {
            // Arrange             
            CustomerVM customerVM = null;
            string email = "sam@email.com";

            // Act 
            customerVM = _customerManager.GetCustomerVMByEmail(email);

            // Assert
            Assert.AreEqual(customerVM.Email, email);
        }

        [TestMethod]
        public void TestFailCustomerVMByWrongEmail()
        {
            // Arrange             
            CustomerVM customerVM = null;
            string email = "3";

            // Act 
            customerVM = _customerManager.GetCustomerVMByEmail(email);

            // Assert
            Assert.AreNotEqual(customerVM.Email, email);
        }

        [TestMethod]
        public void TestSuccessfullyResetPassword()
        {
            // Arrange
            string email = "sam@email.com";
            string oldPassword = "test123";
            string newPassword = "test321";

            // Act
            bool result = _customerManager.ResetPassword(email, oldPassword, newPassword);
            
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestFailsToResetPassword()
        {
            // Arrange
            string email = "sam@email.com";
            string oldPassword = "test321";
            string newPassword = "test123";

            // Act
            bool result = _customerManager.ResetPassword(email, oldPassword, newPassword);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
