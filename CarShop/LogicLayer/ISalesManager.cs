using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ISalesManager
    {
        int CreateSale(int EmployeeID, int CarID, int CustomerID, DateTime SaleDate, float SalePrice);
        List<SalesVM> ViewSales();
        List<SalesVM> ViewSalesForEmployee(int EmployeeID);
    }
}
