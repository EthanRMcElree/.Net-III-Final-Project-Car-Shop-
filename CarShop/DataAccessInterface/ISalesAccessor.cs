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
        int CreateSale(int UserID, int CarID, DateTime SaleDate, Double SalePrice);
        int DeleteSaleByID(int SaleID);
        List<SalesVM> ViewSales();
        SalesVM ViewSaleByID(int SaleID);
        List<SalesVM> ViewSalesForUser(int UserID);
    }
}
