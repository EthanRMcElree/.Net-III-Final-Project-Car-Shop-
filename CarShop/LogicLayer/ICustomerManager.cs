using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ICustomerManager
    {
        CustomerVM AuthenticateCustomer(string Email, string Password);
        string HashSha256(string source);        
        CustomerVM GetCustomerVMByEmail(string email);
        bool ResetPassword(string email, string oldPassword, string newPassword);
    }
}
