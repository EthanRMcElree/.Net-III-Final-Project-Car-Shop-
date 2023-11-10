using DataAccessInterface;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class RepairInvoiceManager : IRepairInvoiceManager
    {
        private IRepairInvoiceAccessor _repairInvoiceAccessor = null;

        public RepairInvoiceManager()
        {
            _repairInvoiceAccessor = new RepairInvoiceAccessor();
        }

        public RepairInvoiceManager(IRepairInvoiceAccessor repairInvoiceAccessor)
        {
            _repairInvoiceAccessor = repairInvoiceAccessor;
        }
        public int CreateRepairInvoice(int CarID, int EmployeeID, string IssueDescription, DateTime RepairDate)
        {
            return _repairInvoiceAccessor.CreateRepairInvoice(CarID, EmployeeID, IssueDescription, RepairDate);
        }
    }
}
