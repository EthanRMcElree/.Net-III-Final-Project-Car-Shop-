using DataAccessInterface;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class CustomerAccessorFake : ICustomerAccessor
    {
        private List<CustomerVM> fakeCustomers = new List<CustomerVM>();

        public CustomerAccessorFake() 
        {
            // Password for all users initially set to test123
            fakeCustomers.Add(new CustomerVM()
            {
                CustomerID = 1,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "3191012222",
                Email = "john@email.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae"
            });
            fakeCustomers.Add(new CustomerVM()
            {
                CustomerID = 2,
                FirstName = "Kate",
                LastName = "Hillard",
                PhoneNumber = "3191023333",
                Email = "kate@email.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae"
            });
            fakeCustomers.Add(new CustomerVM()
            {
                CustomerID = 3,
                FirstName = "Sam",
                LastName = "Spacer",
                PhoneNumber = "3191034444",
                Email = "sam@email.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae"
            });            
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

        public CustomerVM AuthenticateCustomerWithEmailAndPassword(string email, string password)
        {
            CustomerVM authenticatedCustomer = null;

            foreach (CustomerVM customer in fakeCustomers)
            {
                if (customer.Email == email && customer.Password == password)
                {
                    authenticatedCustomer = customer;
                    break;
                }
            }
            return authenticatedCustomer;
        }

        public int ChangeCustomerPassword(string email, string oldPassword, string newPassword)
        {
            int rows = 0;

            foreach (CustomerVM customer in fakeCustomers)
            {
                if (customer.Email.Equals(email) && customer.Password.Equals(oldPassword))
                {
                    customer.Password = newPassword;
                    rows++;
                    break;
                }
            }
            return rows;
        }

        public void CreateCustomerAccount(string FirstName, string LastName, string Password, string PhoneNumber, string Email)
        {
            fakeCustomers.Add(new CustomerVM()
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Password = Password
            });
        }

        public void DeleteCustomerAccountByEmail(string email)
        {
            fakeCustomers.Remove(SelectCustomerVMByEmail(email));
        }

        public void ResetCustomerPassword(string email, string newPassword)
        {
            CustomerVM customerVM = SelectCustomerVMByEmail(email);
            customerVM.Password = newPassword;
        }

        public CustomerVM SelectCustomerVMByEmail(string Email)
        {
            CustomerVM customerVM = new CustomerVM();

            foreach (CustomerVM customer in fakeCustomers)
            {
                if (customer.Email == Email)
                {
                    customerVM = customer;
                    break;
                };
            }
            return customerVM;
        }

        public CustomerVM SelectCustomerVMByID(int CustomerID)
        {
            CustomerVM customerVM = new CustomerVM();

            foreach (CustomerVM customer in fakeCustomers)
            {
                customer.CustomerID = CustomerID;
            }
            return customerVM;
        }

        public void UpdateCustomer(int CustomerID, string FirstName, string LastName, string Password, string PhoneNumber, string Email)
        {
            var customerToUpdate = fakeCustomers.FirstOrDefault(c => c.CustomerID == CustomerID);

            if (customerToUpdate != null)
            {
                customerToUpdate.FirstName = FirstName;
                customerToUpdate.LastName = LastName;
                customerToUpdate.PhoneNumber = PhoneNumber;
                customerToUpdate.Email = Email;
                customerToUpdate.Password = Password;
            }
        }
    }
}
