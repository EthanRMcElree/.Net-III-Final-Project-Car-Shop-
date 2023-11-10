using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class RepairInvoice
    {
        /*
         InvoiceID	int	
        CarID	int	
        EmployeeID	int	
        IssueDescription	nvarchar	250
        RepairDate	datetime	
         */
        public int InvoiceID {  get; set; }
        public int CarID { get; set; }
        public int EmployeeID { get; set; }
        public string IssueDescription { get; set; }
        public DateTime RepairDate { get; set; }
    }
    public class RepairInvoiceVM : RepairInvoice
    {
        public CarInventory Car { get; set; }
        public Employee Employee { get; set; }
    }
}
