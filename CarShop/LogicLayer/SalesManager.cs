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
        public int CreateSale(int UserID, int CarID, DateTime SaleDate, double SalePrice)
        {
            int rows = 0;
            try
            {
                rows = _salesAccessor.CreateSale(UserID, CarID, SaleDate, SalePrice);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create sale.", ex);
            }
            return rows;
        }

        public int DeleteSaleByID(int SaleID)
        {
            int rows = 0;
            try
            {
                rows = _salesAccessor.DeleteSaleByID(SaleID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete sale.", ex);
            }
            return rows;
        }

        public List<SalesVM> ViewSales()
        {
            List<SalesVM> sale = new List<SalesVM>();
            try
            {
                sale = _salesAccessor.ViewSales();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to view sales.", ex);
            }
            return sale;
        }

        public SalesVM ViewSaleByID(int SaleID)
        {
            SalesVM result = null;
            try
            {
                result = _salesAccessor.ViewSaleByID(SaleID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to view sale by its ID.", ex);
            }
            return result;
        }

        public List<SalesVM> ViewSalesForUser(int UserID)
        {
            List<SalesVM> sale = new List<SalesVM>();
            try
            {
                sale = _salesAccessor.ViewSalesForUser(UserID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to view sales for user.", ex);
            }
            return sale;
        }
    }
}
