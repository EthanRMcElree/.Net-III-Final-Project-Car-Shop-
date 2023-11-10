using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class Sales
    {
        /*
         SaleID	int		
        EmployeeID	int		
        CarID	int		
        CustomerID	int		
        SaleDate	datetime		
        SalePrice	float		
         */
        public int SaleID { get; set; }
        public int EmployeeID { get; set; }
        public int CarID { get; set; }
        public int CustomerID { get; set; }
        public DateTime SaleDate { get; set; }
        public float SalePrice { get; set; }
    }
    public class SalesVM : Sales
    {
        public Employee Employee { get; set; }
        public CarInventory Car {  get; set; }
        public Customer Customer { get; set; }
    }
}
