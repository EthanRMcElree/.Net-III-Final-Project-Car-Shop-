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
        int DeleteEmployeeAccountByID(int EmployeeID);
        void UpdateEmployee(string FirstName, string LastName, string PhoneNumber, string Email, string Role);
        List<EmployeeVM> SelectAllEmployees();
    }
}
