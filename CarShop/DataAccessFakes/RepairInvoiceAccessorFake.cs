using DataAccessInterface;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class RepairInvoiceAccessorFake : IRepairInvoiceAccessor
    {
        public List<RepairInvoiceVM> fakeRepairInvoice = new List<RepairInvoiceVM>();

        public RepairInvoiceAccessorFake() 
        {
            fakeRepairInvoice.Add(new RepairInvoiceVM()
            {
                InvoiceID = 1,
                CarID = 1,
                EmployeeID = 1,
                IssueDescription = "Replace oxygen sensor",
                RepairDate = DateTime.Now
            });

            fakeRepairInvoice.Add(new RepairInvoiceVM()
            {
                InvoiceID = 2,
                CarID = 2,
                EmployeeID = 2,
                IssueDescription = "Replace catalytic converter",
                RepairDate = DateTime.Now.AddDays(16)
            });

            fakeRepairInvoice.Add(new RepairInvoiceVM()
            {
                InvoiceID = 3,
                CarID = 3,
                EmployeeID = 3,
                IssueDescription = "Replace ignition coil",
                RepairDate = DateTime.Now.AddYears(2)
            });          
        }

        public int CreateRepairInvoice(int CarID, int EmployeeID, string IssueDescription, DateTime RepairDate)
        {
            if (EmployeeID <= 0)
            {
                return 0;
            }
            fakeRepairInvoice.Add(new RepairInvoiceVM()
            {
                CarID = CarID,
                EmployeeID = EmployeeID,
                IssueDescription = IssueDescription,
                RepairDate = RepairDate
            });
            return 999;
        }
    }
}
