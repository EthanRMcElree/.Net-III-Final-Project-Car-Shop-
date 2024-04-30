using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IUserManager
    {               
        string HashSha256(string source);
        UserVM AuthenticateUser(string email, string password);
        UserVM SelectUserVMByID(int UserID);
        UserVM GetUserVMByEmail(string email);
        string GetRolesByUserID(int employeeID);
        bool ResetPassword(string email, string oldPassword, string newPassword);
        int DeleteUserAccountByID(int UserID);
        void UpdateUser(UserVM user);
        List<string> GetCustomerEmails();
    }
}
