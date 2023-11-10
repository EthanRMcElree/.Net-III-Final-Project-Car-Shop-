using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface ICustomerAccessor
    {
        void CreateCustomerAccount(string FirstName, string LastName, string Password, string PhoneNumber, string Email);
        CustomerVM SelectCustomerVMByID(int CustomerID);
        CustomerVM SelectCustomerVMByEmail(string Email);
        CustomerVM AuthenticateCustomerWithEmailAndPassword(string email, string password);
        int ChangeCustomerPassword(string email, string newPassword, string oldPassword);
        void ResetCustomerPassword(string email, string newPassword);
        void DeleteCustomerAccountByEmail(string email);
        void UpdateCustomer(int CustomerID, string FirstName, string LastName, string Password, string PhoneNumber, string Email);

    }
}
