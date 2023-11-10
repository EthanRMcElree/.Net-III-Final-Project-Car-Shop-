using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class Employee
    {
        /*
            EmployeeID	int	
            FirstName	nvarchar	50
            LastName	nvarchar	50
            Password	nvarchar	100
            PhoneNumber	nvarchar	11
            Email	nvarchar	250
            Role	nvarchar	100
         */
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password {  get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
    public class EmployeeVM : Employee
    {
    }
}
