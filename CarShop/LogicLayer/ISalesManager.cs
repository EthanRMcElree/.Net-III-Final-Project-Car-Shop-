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
        int CreateSale(int UserID, int CarID, DateTime SaleDate, double SalePrice);
        int DeleteSaleByID(int SaleID);
        List<SalesVM> ViewSales();
        SalesVM ViewSaleByID(int SaleID);
        List<SalesVM> ViewSalesForUser(int UserID);
    }
}
