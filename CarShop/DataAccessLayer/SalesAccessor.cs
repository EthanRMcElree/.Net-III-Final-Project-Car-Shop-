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
        public int CreateSale(int UserID, int CarID, DateTime SaleDate, Double SalePrice)
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
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@CarID", SqlDbType.Int);
            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@SalePrice", SqlDbType.Float);            

            // parameter values
            cmd.Parameters["@UserID"].Value = UserID;
            cmd.Parameters["@CarID"].Value = CarID;
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
            var cmdText = "sp_view_all_sales_data";

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
                    while (reader.Read())
                    {
                        SalesVM salesVM = new SalesVM();
                        salesVM.SaleID = reader.GetInt32(0);
                        salesVM.UserID = reader.GetInt32(1);
                        salesVM.CarID = reader.GetInt32(2);
                        salesVM.SaleDate = reader.GetDateTime(3);
                        salesVM.SalePrice = reader.GetDouble(4);
                        User user = new User();
                        user.FirstName = reader.GetString(5);
                        user.LastName = reader.GetString(6);
                        salesVM.User = user;
                        CarInventory car = new CarInventory();
                        car.Model = reader.GetString(7);
                        car.Year = reader.GetInt32(8);
                        salesVM.Car = car;
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

        public SalesVM ViewSaleByID(int SaleID)
        {
            // Make return variable if appropriate
            SalesVM salesVM = null;

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_sale_by_id";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@SaleID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@SaleID"].Value = SaleID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    salesVM = new SalesVM();
                    while (reader.Read())
                    {
                        salesVM.SaleID = reader.GetInt32(0);
                        salesVM.UserID = reader.GetInt32(1);
                        salesVM.CarID = reader.GetInt32(2);                                            
                        salesVM.SaleDate = reader.GetDateTime(3);
                        salesVM.SalePrice = reader.GetDouble(4);                                                
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
            return salesVM;
        }

        public int DeleteSaleByID(int SaleID)
        {
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_delete_sale";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@SaleID", SqlDbType.Int);

            // parameter values            
            cmd.Parameters["@SaleID"].Value = SaleID;
            var rows = 0;
            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    Console.WriteLine("Cannot Delete Sale.");
                    return rows;
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

        public List<SalesVM> ViewSalesForUser(int UserID)
        {
            // Make return variable if appropriate
            var List = new List<SalesVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_view_sales_for_user";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            // parameter values            
            cmd.Parameters["@UserID"].Value = UserID;

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
                        salesVM.UserID = reader.GetInt32(1);
                        salesVM.CarID = reader.GetInt32(2);
                        salesVM.UserID = reader.GetInt32(3);
                        salesVM.SaleDate = reader.GetDateTime(4);
                        salesVM.SalePrice = reader.GetDouble(5);
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
