using DataAccessInterface;
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
    public class RepairInvoiceAccessor : IRepairInvoiceAccessor
    {
        public int CreateRepairInvoice(int CarID, int EmployeeID, string IssueDescription, DateTime RepairDate)
        {
            // Make return variable if appropriate
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_create_repair_invoice";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters            
            cmd.Parameters.Add("@CarID", SqlDbType.Int);
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@IssueDescription", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@RepairDate", SqlDbType.DateTime);            

            // parameter values
            cmd.Parameters["@CarID"].Value = CarID;
            cmd.Parameters["@EmployeeID"].Value = EmployeeID;
            cmd.Parameters["@IssueDescription"].Value = IssueDescription;
            cmd.Parameters["@RepairDate"].Value = RepairDate;

            var rows = 0;

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("SQL error creating repair invoice.");
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
    }
}
