using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface IEmployeeAccessor
    {
        void CreateEmployeeAccount(string FirstName, string LastName, string Password, string PhoneNumber, string Email, string Role);
        EmployeeVM AuthenticateEmployeeWithEmailAndPassword(string email, string password);
        EmployeeVM SelectEmployeeVMByEmail(string email);
        EmployeeVM SelectEmployeeVMByID(int EmployeeID);
        string SelectRoleByEmployee(int  EmployeeID);
        int ChangeEmployeePassword(string email, string oldPassword, string newPassword);
        void ResetEmployeePassword(string email, string newPassword);
        void DeleteEmployeeAccountByEmail(string email);
        void UpdateEmployee(int EmployeeID, string FirstName, string LastName, string Password, string PhoneNumber, string Email, string Role);
    }
}
