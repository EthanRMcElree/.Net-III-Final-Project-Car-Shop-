using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class Customer
    {
        /*
         CustomerID	int	
        FirstName	nvarchar	50
        LastName	nvarchar	50
        Email	nvarchar	250
        Password	nvarchar	100
        PhoneNumber	nvarchar	11
         */
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class CustomerVM : Customer
    {
    }
}
