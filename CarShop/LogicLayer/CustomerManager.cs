using DataAccessInterface;
using DataAccessLayer;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class CustomerManager : ICustomerManager
    {
        // dependency inversion for the data provider
        private ICustomerAccessor _customerAccessor = null;

        // the default constructor will use the database
        public CustomerManager() 
        {
            _customerAccessor = new CustomerAccessor();
        }       
        public CustomerManager(ICustomerAccessor customerAccessor)
        {
            _customerAccessor = customerAccessor;
        }
        public CustomerVM AuthenticateCustomer(string email, string password)
        {
            password = HashSha256(password);

            CustomerVM customerVM = _customerAccessor.AuthenticateCustomerWithEmailAndPassword(email, password);

            return customerVM;
        }


        public CustomerVM GetCustomerVMByEmail(string email)
        {
            CustomerVM customerVM = null;

            try
            {
                customerVM = _customerAccessor.SelectCustomerVMByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Customer not found", ex);
            }

            return customerVM;
        }

        public string HashSha256(string source)
        {
            string hashValue = "";

            // hash functions run at the bits and bytes level
            // so we create a byte array
            byte[] data;

            // create a .NET hash provider object
            using (SHA256 sha256hasher = SHA256.Create())
            {
                // hash the source string
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // create an output stringbuilder object
            var s = new StringBuilder();

            // loop through the byte array and build a return string
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2")); // outputs the byte as two hex digits
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
                result = (1 == _customerAccessor.ChangeCustomerPassword(email, oldPassword, newPassword));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("User or password not found.", ex);
            }

            return result;
        }
    }
}
