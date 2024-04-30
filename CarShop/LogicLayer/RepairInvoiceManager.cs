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
            int rows = 0;
            try
            {
                rows = _repairInvoiceAccessor.CreateRepairInvoice(CarID, EmployeeID, IssueDescription, RepairDate);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create a repair invoice.", ex);
            }
            return rows;
        }
    }
}
