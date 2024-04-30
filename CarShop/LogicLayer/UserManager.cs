using DataAccessInterface;
using DataAccessLayer;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        // dependency inversion for the data provider
        private IUserAccessor _userAccessor = null;

        // the default constructor will use the database
        public UserManager() // uses the database
        {
            _userAccessor = new UserAccessor();
        }
        // the optional constructor can accept any data provider (fake)
        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public UserVM AuthenticateUser(string email, string password)
        {
            UserVM userVM = null;
            try
            {
                userVM = _userAccessor.AuthenticateUserWithEmailAndPassword(email, HashSha256(password));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to authenticate user.", ex);
            }
            return userVM;
        }

        public UserVM GetUserVMByID(int UserID)
        {
            UserVM userVM = null;
            try
            {
                userVM = _userAccessor.SelectUserVMByID(UserID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get user by id.", ex);
            }
            return userVM;      
        }

        public UserVM GetUserVMByEmail(string email)
        {
            UserVM userVM = null;
            try
            {
                userVM = _userAccessor.SelectUserVMByEmail(email);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get user by email.", ex);
            }
            return userVM;
        }

        public string GetRolesByUserID(int userID)
        {
            string role = "";

            try
            {
                role = _userAccessor.SelectRoleByUser(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get roles by user id", ex);
            }
            return role;
        }

        public string HashSha256(string source)
        {
            string hashValue = "";
            
            byte[] data;

            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2")); 
            }

            hashValue = s.ToString();
            return hashValue;
        }       

        public bool ResetPassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;

            oldPassword = HashSha256(oldPassword);
            newPassword = HashSha256(newPassword);

            try
            {
                result = (1 == _userAccessor.ChangeUserPassword(email, oldPassword, newPassword));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("User or password not found.", ex);
            }

            return result;
        }

        public void CreateCreateUserAccount(string FirstName, string LastName, string Password, string PhoneNumber, string Email, string Role)
        {
            try
            {
                _userAccessor.CreateUserAccount(FirstName, LastName, HashSha256(Password), PhoneNumber, Email, Role);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create a new user account.", ex);
            }           
        }

        public void UpdateUser(UserVM user)
        {
            try
            {
                _userAccessor.UpdateUser(user.FirstName, user.LastName, user.PhoneNumber, user.Email, user.Role, user.IsLoggedIn, user.IsAdmin);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update user.", ex);
            }        
        }
        public List<UserVM> SelectAllUser()
        {
            List<UserVM> user = new List<UserVM>();
            try
            {
                user = _userAccessor.SelectAllUsers();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to select all users.", ex);
            }
            return user;          
        }

        public int DeleteUserAccountByID(int UserID) 
        {
            int rows = 0;
            try
            {
                rows = _userAccessor.DeleteUserAccountByID(UserID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete user account by id.", ex);
            }
            return rows; 
        }

        public UserVM SelectUserVMByID(int UserID)
        {
            UserVM result = null;
            try
            {
                result = _userAccessor.SelectUserVMByID(UserID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to select a user by their id.", ex);
            }
            return result; 
        }

        public List<string> GetCustomerEmails()
        {
            List<string> customerEmails = new List<string>();
            try
            {
                customerEmails = _userAccessor.GetCustomerEmails();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get customer emails.", ex);
            }
            return customerEmails;
        }
    }
}
