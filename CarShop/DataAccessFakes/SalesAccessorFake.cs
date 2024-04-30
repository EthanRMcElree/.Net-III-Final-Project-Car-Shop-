using DataAccessInterface;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class SalesAccessorFake : ISalesAccessor
    {
        public List<SalesVM> fakeSales = new List<SalesVM>();

        public SalesAccessorFake() 
        {
            fakeSales.Add(new SalesVM()
            {
                SaleID = 1,
                UserID = 1,
                CarID = 1,
                SaleDate = DateTime.Now,
                SalePrice = 22500.00F,
            });

            fakeSales.Add(new SalesVM()
            {
                SaleID = 2,
                UserID = 2,
                CarID = 2,
                SaleDate = DateTime.Now.AddYears(2),
                SalePrice = 19295.50F,
            });

            fakeSales.Add(new SalesVM()
            {
                SaleID = 3,
                UserID = 2,
                CarID = 3,
                SaleDate = DateTime.Now.AddDays(10),
                SalePrice = 20750.00F,
            });
        }
        public int CreateSale(int UserID, int CarID, DateTime SaleDate, Double SalePrice)
        {
            if (CarID <= 0)
            {
                return 0;
            }
            fakeSales.Add(new SalesVM()
            {
                UserID = UserID,
                CarID = CarID,
                SaleDate = SaleDate,
                SalePrice = SalePrice
            });
            return 999;
        }

        public List<SalesVM> ViewSales()
        {
            return fakeSales;
        }

        public List<SalesVM> ViewSalesForUser(int UserID)
        {
            List<SalesVM> salesVM = new List<SalesVM>();

            foreach (SalesVM sales in fakeSales)
            {                
                if (sales.UserID == UserID)
                {
                    salesVM.Add(sales);
                }
            }

            return salesVM;
        }

        public SalesVM ViewSaleByID(int SaleID)
        {
            SalesVM salesVM = new SalesVM();

            foreach (SalesVM sales in fakeSales)
            {
                if (sales.SaleID == SaleID)
                {
                    return sales;
                }
            }
            return null;
        }

        public int DeleteSaleByID(int SaleID)
        {
            SalesVM salesVM = ViewSaleByID(SaleID);
            if (salesVM == null)
            {
                return 0;
            }
            fakeSales.Remove(salesVM);
            return 1;
        }
    }
}
