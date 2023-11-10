using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IEmployeeManager
    {               
        string HashSha256(string source);
        EmployeeVM AuthenticateEmployee(string email, string password);
        EmployeeVM GetEmployeeVMByEmail(string email);
        string GetRolesByEmployeeID(int employeeID);
        bool ResetPassword(string email, string oldPassword, string newPassword);
    }
}
