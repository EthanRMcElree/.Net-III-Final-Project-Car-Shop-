﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class Employee
    {        
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password {  get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
    public class EmployeeVM : Employee
    {
    }
}
