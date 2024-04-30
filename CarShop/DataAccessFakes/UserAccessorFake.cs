using DataAccessInterface;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class UserAccessorFake : IUserAccessor
    {        
        private List<UserVM> fakeUsers = new List<UserVM>();        

        public UserAccessorFake()
        {
            // Password for all users initially set to test123
            fakeUsers.Add(new UserVM()
            {
                UserID = 1,
                FirstName = "Sam",
                LastName = "Hill",
                PhoneNumber = "1234567890",
                Email = "sam@carshop.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae",
                Role = "Customer"
            });
            fakeUsers.Add(new UserVM()
            {
                UserID = 2,
                FirstName = "Beth",
                LastName = "Luck",
                PhoneNumber = "1234567890",
                Email = "beth@carshop.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae",
                Role = "Manager"
            });
            fakeUsers.Add(new UserVM()
            {
                UserID = 3,
                FirstName = "Dan",
                LastName = "Mills",
                PhoneNumber = "1234567890",
                Email = "dan@carshop.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae",
                Role = "Administrator"
            });
            fakeUsers.Add(new UserVM()
            {
                UserID = 4,
                FirstName = "Max",
                LastName = "Lock",
                PhoneNumber = "1234567890",
                Email = "max@carshop.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae",
                Role = "Service Representative"
            });
        }

        public UserVM AuthenticateUserWithEmailAndPassword(string email, string password)
        {
            UserVM authenticatedUser = null;

            foreach (UserVM user in fakeUsers)
            {
                if (user.Email == email && user.Password == password)
                {
                    authenticatedUser = user;
                    break;
                }
            }            
            return authenticatedUser;
        }

        public int ChangeUserPassword(string email, string oldPassword, string newPassword)
        {
            int rows = 0;           

            foreach(UserVM user in fakeUsers)
            {
                if (user.Email.Equals(email) && user.Password.Equals(oldPassword))
                {
                    user.Password = newPassword;
                    rows++;
                    break;
                }
            }        
            return rows;
        }

        public void CreateUserAccount(string FirstName, string LastName, string Password, string PhoneNumber, string Email, string Role)
        {
            fakeUsers.Add(new UserVM()
            {                
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Password = Password,
                Role = Role
            });
        }

        public void DeleteUserAccountByEmail(string email)
        {         
            fakeUsers.Remove(SelectUserVMByEmail(email));
        }

        public int DeleteUserAccountByID(int UserID)
        {
            return 1;
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

        public void ResetUserPassword(string email, string newPassword)
        {
            UserVM userVM = SelectUserVMByEmail(email);
            userVM.Password = HashSha256(newPassword);
        }

        public List<UserVM> SelectAllUsers()
        {
            return fakeUsers;
        }

        public UserVM SelectUserVMByEmail(string email)
        {
            UserVM userVM = new UserVM();

            foreach(UserVM user in fakeUsers) 
            {
                if(user.Email == email)
                {
                    userVM = user;
                    break;
                };
            }
            return userVM;
        }

        public UserVM SelectUserVMByID(int UserID)
        {
            UserVM userVM = new UserVM();

            foreach (UserVM user in fakeUsers)
            {
                user.UserID = UserID;
            }
            return userVM;
        }

        public string SelectRoleByUser(int UserID)
        {
            string role = "";           

            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.UserID == UserID)
                {
                    role = fakeUser.Role;
                    break;
                }
            }
            return role;
        }

        public void UpdateUser(string FirstName, string LastName, string PhoneNumber, string Email, string Role, bool IsLoggedIn, bool IsAdmin)
        {            
            var userToUpdate = fakeUsers.FirstOrDefault(e => e.Email == Email);

            if (userToUpdate != null)
            {
                userToUpdate.FirstName = FirstName;
                userToUpdate.LastName = LastName;
                userToUpdate.PhoneNumber = PhoneNumber;
                userToUpdate.Email = Email;
                userToUpdate.Role = Role;
            }
        }

        public List<string> GetCustomerEmails()
        {
            List<string> customerEmails = new List<string>();
            foreach (var user in fakeUsers)
            {
                if(user.Role == "Customer")
                {
                    customerEmails.Add(user.Email);
                }
            }
            return customerEmails;
        }
    }
}
