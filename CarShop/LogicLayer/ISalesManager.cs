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
        int CreateSale(int EmployeeID, int CarID, int CustomerID, DateTime SaleDate, double SalePrice);
        int DeleteSaleByID(int SaleID);
        List<SalesVM> ViewSales();
        SalesVM ViewSaleByID(int SaleID);
        List<SalesVM> ViewSalesForEmployee(int EmployeeID);
    }
}
