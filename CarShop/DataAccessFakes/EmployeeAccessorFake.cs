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
    public class EmployeeAccessorFake : IEmployeeAccessor
    {        
        private List<EmployeeVM> fakeEmployees = new List<EmployeeVM>();        

        public EmployeeAccessorFake()
        {
            // Password for all users initially set to test123
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 1,
                FirstName = "Sam",
                LastName = "Hill",
                PhoneNumber = "1234567890",
                Email = "sam@carshop.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae",
                Role = "Sales Representative"
            });
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 2,
                FirstName = "Beth",
                LastName = "Luck",
                PhoneNumber = "1234567890",
                Email = "beth@carshop.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae",
                Role = "Manager"
            });
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 3,
                FirstName = "Dan",
                LastName = "Mills",
                PhoneNumber = "1234567890",
                Email = "dan@carshop.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae",
                Role = "Administrator"
            });
            fakeEmployees.Add(new EmployeeVM()
            {
                EmployeeID = 4,
                FirstName = "Max",
                LastName = "Lock",
                PhoneNumber = "1234567890",
                Email = "max@carshop.com",
                Password = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae",
                Role = "Service Representative"
            });
        }

        public EmployeeVM AuthenticateEmployeeWithEmailAndPassword(string email, string password)
        {
            EmployeeVM authenticatedEmployee = null;

            foreach (EmployeeVM employee in fakeEmployees)
            {
                if (employee.Email == email && employee.Password == password)
                {
                    authenticatedEmployee = employee;
                    break;
                }
            }            
            return authenticatedEmployee;
        }

        public int ChangeEmployeePassword(string email, string oldPassword, string newPassword)
        {
            int rows = 0;           

            foreach(EmployeeVM employee in fakeEmployees)
            {
                if (employee.Email.Equals(email) && employee.Password.Equals(oldPassword))
                {
                    employee.Password = newPassword;
                    rows++;
                    break;
                }
            }        
            return rows;
        }

        public void CreateEmployeeAccount(string FirstName, string LastName, string Password, string PhoneNumber, string Email, string Role)
        {
            fakeEmployees.Add(new EmployeeVM()
            {                
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Password = Password,
                Role = Role
            });
        }

        public void DeleteEmployeeAccountByEmail(string email)
        {         
            fakeEmployees.Remove(SelectEmployeeVMByEmail(email));
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

        public void ResetEmployeePassword(string email, string newPassword)
        {
            EmployeeVM employeeVM = SelectEmployeeVMByEmail(email);
            employeeVM.Password = HashSha256(newPassword);
        }

        public EmployeeVM SelectEmployeeVMByEmail(string email)
        {
            EmployeeVM employeeVM = new EmployeeVM();

            foreach(EmployeeVM employee in fakeEmployees) 
            {
                if(employee.Email == email)
                {
                    employeeVM = employee;
                    break;
                };
            }
            return employeeVM;
        }

        public EmployeeVM SelectEmployeeVMByID(int EmployeeID)
        {
            EmployeeVM employeeVM = new EmployeeVM();

            foreach (EmployeeVM employee in fakeEmployees)
            {
                employee.EmployeeID = EmployeeID;
            }
            return employeeVM;
        }

        public string SelectRoleByEmployee(int EmployeeID)
        {
            string role = "";           

            foreach (var fakeEmployee in fakeEmployees)
            {
                if (fakeEmployee.EmployeeID == EmployeeID)
                {
                    role = fakeEmployee.Role;
                    break;
                }
            }
            return role;
        }

        public void UpdateEmployee(int EmployeeID, string FirstName, string LastName, string Password, string PhoneNumber, string Email, string Role)
        {            
            var employeeToUpdate = fakeEmployees.FirstOrDefault(e => e.EmployeeID == EmployeeID);

            if (employeeToUpdate != null)
            {
                employeeToUpdate.FirstName = FirstName;
                employeeToUpdate.LastName = LastName;
                employeeToUpdate.PhoneNumber = PhoneNumber;
                employeeToUpdate.Email = Email;
                employeeToUpdate.Password = Password;
                employeeToUpdate.Role = Role;
            }
        }
    }
}
