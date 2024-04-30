using DataAccessFakes;
using DataObjectsLayer;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class UserManagerTests
    {
        IUserManager _userManager = null;

        [TestInitialize]
        public void TestSetUp()
        {
            _userManager = new UserManager(new UserAccessorFake());
        }

        [TestMethod]
        public void TestHashSha256ReturnsACorrectHashValue()
        {
            // Arrange 
            UserManager userManager = new UserManager();
            string testString = "test123";
            string actualHash = "";
            string expectedHash = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae";

            // Act 
            actualHash = userManager.HashSha256(testString);

            // Assert
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void TestHashSha256FailsABadHash()
        {
            // Arrange 
            UserManager userManager = new UserManager();
            string testString = "test321";
            string actualHash = "";
            string expectedHash = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae";

            // Act 
            actualHash = userManager.HashSha256(testString);

            // Assert
            Assert.AreNotEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void TestAuthenticateUserByCorrectEmailAndPassword()
        {
            // Arrange             
            UserVM userVM = null;
            string email = "max@carshop.com";
            string password = "test123";

            // Act 
            userVM = _userManager.AuthenticateUser(email, password);

            // Assert
            Assert.IsNotNull(userVM);
        }

        [TestMethod]
        public void TestFailAuthenticateUserByWrongEmailAndPassword()
        {
            // Arrange             
            UserVM userVM = null;
            string email = "max@carshop.com";
            string password = "test321";

            // Act 
            userVM = _userManager.AuthenticateUser(email, password);

            // Assert
            Assert.IsNull(userVM);
        }

        [TestMethod]
        public void TestGetUserVMByCorrectEmail()
        {
            // Arrange             
            UserVM userVM = null;
            string email = "max@carshop.com";

            // Act 
            userVM = _userManager.GetUserVMByEmail(email);

            // Assert
            Assert.AreEqual(userVM.Email, email);
        }

        [TestMethod]
        public void TestFailUserVMByWrongEmail()
        {
            // Arrange             
            UserVM userVM = null;
            string email = "4";

            // Act 
            userVM = _userManager.GetUserVMByEmail(email);

            // Assert
            Assert.AreNotEqual(userVM.Email, email);
        }

        [TestMethod]
        public void TestGetRoleByCorrectUserID()
        {
            // Arrange             
            string role = "";
            int userID = 4;

            // Act 
            role = _userManager.GetRolesByUserID(userID);

            // Assert
            Assert.AreEqual("Service Representative", role);
        }

        [TestMethod]
        public void TestFailRoleByWrongUserID()
        {
            // Arrange             
            string role = "";
            int userID = 3;

            // Act 
            role = _userManager.GetRolesByUserID(userID);

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
            bool result = _userManager.ResetPassword(email, oldPassword, newPassword);

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
            bool result = _userManager.ResetPassword(email, oldPassword, newPassword);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestSuccessfullyGetCustomerEmails()
        {
            // Arrange             
            List<string> customerEmails = new List<string>();

            // Act 
            customerEmails = _userManager.GetCustomerEmails();

            // Assert
            Assert.AreEqual(customerEmails.Count, 1);
        }

        [TestMethod]
        public void TestFailedToGetCustomerEmails()
        {
            // Arrange             
            List<string> customerEmails = new List<string>();

            // Act 
            customerEmails = _userManager.GetCustomerEmails();

            // Assert
            Assert.AreNotEqual(customerEmails.Count, 2);
        }
    }
}
