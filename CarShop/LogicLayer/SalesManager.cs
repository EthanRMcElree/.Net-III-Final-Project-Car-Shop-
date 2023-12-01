using DataAccessInterface;
using DataAccessLayer;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class SalesManager : ISalesManager
    {
        private ISalesAccessor _salesAccessor = null;

        public SalesManager()
        {
            _salesAccessor = new SalesAccessor();
        }

        public SalesManager(ISalesAccessor salesAccessor)
        {
            _salesAccessor = salesAccessor;
        }
        public int CreateSale(int EmployeeID, int CarID, int CustomerID, DateTime SaleDate, double SalePrice)
        {
            return _salesAccessor.CreateSale(EmployeeID, CarID, CustomerID, SaleDate, SalePrice);
        }

        public int DeleteSaleByID(int SaleID)
        {
            return _salesAccessor.DeleteSaleByID(SaleID);
        }

        public List<SalesVM> ViewSales()
        {
            return _salesAccessor.ViewSales();
        }

        public SalesVM ViewSaleByID(int SaleID)
        {
            return _salesAccessor.ViewSaleByID(SaleID);
        }

        public List<SalesVM> ViewSalesForEmployee(int EmployeeID)
        {
            return _salesAccessor.ViewSalesForEmployee(EmployeeID);
        }
    }
}
