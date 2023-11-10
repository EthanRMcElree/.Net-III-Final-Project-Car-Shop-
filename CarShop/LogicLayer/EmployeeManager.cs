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
    public class EmployeeManager : IEmployeeManager
    {
        // dependency inversion for the data provider
        private IEmployeeAccessor _employeeAccessor = null;

        // the default constructor will use the database
        public EmployeeManager() // uses the database
        {
            _employeeAccessor = new EmployeeAccessor();
        }
        // the optional constructor can accept any data provider (fake)
        public EmployeeManager(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
        }

        public EmployeeVM AuthenticateEmployee(string email, string password)
        {
            EmployeeVM employeeVM = _employeeAccessor.AuthenticateEmployeeWithEmailAndPassword(email, password);

            return employeeVM;
        }

        public EmployeeVM GetEmployeeVMByEmail(string email)
        {
            EmployeeVM employeeVM = null;

            employeeVM = _employeeAccessor.SelectEmployeeVMByEmail(email);

            return employeeVM;
        }

        public string GetRolesByEmployeeID(int employeeID)
        {
            string role = "";

            try
            {
                role = _employeeAccessor.SelectRoleByEmployee(employeeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return role;
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

        public bool ResetPassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;

            oldPassword = HashSha256(oldPassword);
            newPassword = HashSha256(newPassword);

            try
            {
                result = (1 == _employeeAccessor.ChangeEmployeePassword(email, oldPassword, newPassword));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("User or password not found.", ex);
            }

            return result;
        }
    }
}
