using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjectsLayer;

namespace DataAccessInterface
{
    public interface ISalesAccessor 
    {
        int CreateSale(int EmployeeID, int CarID, int CustomerID, DateTime SaleDate, Double SalePrice);
        List<SalesVM> ViewSales();
        List<SalesVM> ViewSalesForEmployee(int EmployeeID);
    }
}
