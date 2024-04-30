using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface IUserAccessor
    {
        void CreateUserAccount(string FirstName, string LastName, string Password, string PhoneNumber, string Email, string Role);
        UserVM AuthenticateUserWithEmailAndPassword(string email, string password);
        UserVM SelectUserVMByEmail(string email);
        UserVM SelectUserVMByID(int UserID);
        string SelectRoleByUser(int  UserID);
        int ChangeUserPassword(string email, string oldPassword, string newPassword);
        void ResetUserPassword(string email, string newPassword);
        int DeleteUserAccountByID(int UserID);
        void UpdateUser(string FirstName, string LastName, string PhoneNumber, string Email, string Role, bool IsLoggedIn, bool IsAdmin);
        List<UserVM> SelectAllUsers();
        List<string> GetCustomerEmails();
    }
}
