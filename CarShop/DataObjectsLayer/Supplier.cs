using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class Supplier
    {
        /*
         SupplierID	int	
        SupplierName	nvarchar	50
        ContactPerson	nvarchar	50
        PhoneNumber	nvarchar	11
         */
        public int SupplierID {  get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class SupplierVM : Supplier
    {
    }
}
