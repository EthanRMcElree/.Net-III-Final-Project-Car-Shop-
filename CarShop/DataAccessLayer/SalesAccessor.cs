using DataAccessInterface;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SalesAccessor : ISalesAccessor
    {
        public int CreateSale(int EmployeeID, int CarID, int CustomerID, DateTime SaleDate, float SalePrice)
        {
            // Make return variable if appropriate
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_create_sale";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters            
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@CarID", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@SalePrice", SqlDbType.Float);            

            // parameter values
            cmd.Parameters["@EmployeeID"].Value = EmployeeID;
            cmd.Parameters["@CarID"].Value = CarID;
            cmd.Parameters["@CustomerID"].Value = CustomerID;
            cmd.Parameters["@SaleDate"].Value = SaleDate;
            cmd.Parameters["@SalePrice"].Value = SalePrice;

            var rows = 0;

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("SQL error creating sale.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public List<SalesVM> ViewSales()
        {
            // Make return variable if appropriate
            var List = new List<SalesVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_view_all_sales";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters            
            // parameter values            

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    SalesVM salesVM = new SalesVM();
                    while (reader.Read())
                    {
                        salesVM.SaleID = reader.GetInt32(0);
                        salesVM.EmployeeID = reader.GetInt32(1);
                        salesVM.CarID = reader.GetInt32(2);
                        salesVM.CustomerID = reader.GetInt32(3);
                        salesVM.SaleDate = reader.GetDateTime(4);
                        salesVM.SalePrice = reader.GetFloat(5);                       
                        List.Add(salesVM);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return List;
        }

        public List<SalesVM> ViewSalesForEmployee(int EmployeeID)
        {
            // Make return variable if appropriate
            var List = new List<SalesVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_view_sales_for_employee";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);

            // parameter values            
            cmd.Parameters["@EmployeeID"].Value = EmployeeID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    SalesVM salesVM = new SalesVM();
                    while (reader.Read())
                    {
                        salesVM.SaleID = reader.GetInt32(0);
                        salesVM.EmployeeID = reader.GetInt32(1);
                        salesVM.CarID = reader.GetInt32(2);
                        salesVM.CustomerID = reader.GetInt32(3);
                        salesVM.SaleDate = reader.GetDateTime(4);
                        salesVM.SalePrice = reader.GetFloat(5);
                        List.Add(salesVM);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return List;
        }
    }
}
