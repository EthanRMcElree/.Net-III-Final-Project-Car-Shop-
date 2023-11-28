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
                EmployeeID = 1,
                CarID = 1,
                CustomerID = 1,
                SaleDate = DateTime.Now,
                SalePrice = 22500.00F,
            });

            fakeSales.Add(new SalesVM()
            {
                SaleID = 2,
                EmployeeID = 2,
                CarID = 2,
                CustomerID = 2,
                SaleDate = DateTime.Now.AddYears(2),
                SalePrice = 19295.50F,
            });

            fakeSales.Add(new SalesVM()
            {
                SaleID = 3,
                EmployeeID = 2,
                CarID = 3,
                CustomerID = 3,
                SaleDate = DateTime.Now.AddDays(10),
                SalePrice = 20750.00F,
            });
        }
        public int CreateSale(int EmployeeID, int CarID, int CustomerID, DateTime SaleDate, Double SalePrice)
        {
            if (CarID <= 0)
            {
                return 0;
            }
            fakeSales.Add(new SalesVM()
            {
                EmployeeID = EmployeeID,
                CarID = CarID,
                CustomerID = CustomerID,
                SaleDate = SaleDate,
                SalePrice = SalePrice
            });
            return 999;
        }

        public List<SalesVM> ViewSales()
        {
            return fakeSales;
        }

        public List<SalesVM> ViewSalesForEmployee(int EmployeeID)
        {
            List<SalesVM> salesVM = new List<SalesVM>();

            foreach (SalesVM sales in fakeSales)
            {                
                if (sales.EmployeeID == EmployeeID)
                {
                    salesVM.Add(sales);
                }
            }

            return salesVM;
        }
    }
}
