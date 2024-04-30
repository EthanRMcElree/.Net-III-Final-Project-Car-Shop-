using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class User
    {
        public int UserID {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public string Role { get; set; }
    }
    public class UserVM : User
    {
        public List<CarInventoryVM> cars {  get; set; }
        public CarInventoryVM car { get; set; }
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
