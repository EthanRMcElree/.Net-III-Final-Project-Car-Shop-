using DataAccessInterface;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomerAccessor : ICustomerAccessor
    {
        public CustomerVM AuthenticateCustomerWithEmailAndPassword(string Email, string password)
        {
            CustomerVM customerVM = new CustomerVM();

            // start connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set command text
            var commandText = "sp_authenticate_customer";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to the command
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            // set the parameters values
            cmd.Parameters["@Password"].Value = password;
            cmd.Parameters["@Email"].Value = Email;

            // open the connection and execute the command 
            try
            {
                // open the connection
                conn.Open();

                // execute the command and capture the result
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customerVM.CustomerID = reader.GetInt32(0);
                        customerVM.FirstName = reader.GetString(1);
                        customerVM.LastName = reader.GetString(2);
                        customerVM.Password = reader.GetString(3);
                        customerVM.PhoneNumber = reader.GetString(4);
                        customerVM.Email = reader.GetString(5);                        
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

            return customerVM;
        }

        public int ChangeCustomerPassword(string email, string newPassword, string oldPassword)
        {
            int rows = 0;

            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_change_customer_password";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to the command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPassword", SqlDbType.NVarChar, 100);

            // set the parameters values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@NewPassword"].Value = newPassword;
            cmd.Parameters["@OldPassword"].Value = oldPassword;

            try
            {
                // open connection
                conn.Open();

                rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("Failed to change password");
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

        public void CreateCustomerAccount(string FirstName, string LastName, string Password, string PhoneNumber, string Email)
        {
            // connect object
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var commandText = "sp_create_customer_account";

            // command 
            var cmd = new SqlCommand(commandText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            // parameters values
            cmd.Parameters["@FirstName"].Value = FirstName;
            cmd.Parameters["@LastName"].Value = LastName;
            cmd.Parameters["@Password"].Value = Password;
            cmd.Parameters["@PhoneNumber"].Value = PhoneNumber;
            cmd.Parameters["@Email"].Value = Email;

            try
            {
                // open connection
                conn.Open();

                var rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("Failed to create account");
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
        }

        public void DeleteCustomerAccountByEmail(string email)
        {
            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_delete_customer_account";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to the command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            // set the parameters values
            cmd.Parameters["@Email"].Value = email;

            try
            {
                // open connection
                conn.Open();

                var rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("Failed to delete account");
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
        }

        public void ResetCustomerPassword(string Email, string newPassword)
        {
            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_reset_customer_password";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to the command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 100);

            // set the parameters values
            cmd.Parameters["@Email"].Value = Email;
            cmd.Parameters["@NewPassword"].Value = newPassword;

            try
            {
                // open connection
                conn.Open();

                var rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("Failed to reset password");
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
        }

        public CustomerVM SelectCustomerVMByID(int CustomerID)
        {
            CustomerVM customerVM = new CustomerVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_customer_by_id";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@CustomerID"].Value = CustomerID;

            try
            {
                // open the connection
                conn.Open();

                // execute
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        customerVM.CustomerID = reader.GetInt32(0);
                        customerVM.FirstName = reader.GetString(1);
                        customerVM.LastName = reader.GetString(2);
                        customerVM.Password = reader.GetString(3);
                        customerVM.PhoneNumber = reader.GetString(4);
                        customerVM.Email = reader.GetString(5);
                    }
                }
                else
                {
                    throw new ArgumentException("Unable to find customer");
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

            return customerVM;
        }

        public CustomerVM SelectCustomerVMByEmail(string Email)
        {
            CustomerVM customerVM = new CustomerVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_customer_by_email";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            // parameter values
            cmd.Parameters["@Email"].Value = Email;

            try
            {
                // open the connection
                conn.Open();

                // execute
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        customerVM.CustomerID = reader.GetInt32(0);
                        customerVM.FirstName = reader.GetString(1);
                        customerVM.LastName = reader.GetString(2);
                        customerVM.Password = reader.GetString(3);
                        customerVM.PhoneNumber = reader.GetString(4);
                        customerVM.Email = reader.GetString(5);
                    }
                }
                else
                {
                    throw new ArgumentException("Unable to find customer");
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

            return customerVM;
        }

        public void UpdateCustomer(int CustomerID, string FirstName, string LastName, string Password, string PhoneNumber, string Email)
        {
            // Make return variable if appropriate
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_update_customer";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            // parameters values
            cmd.Parameters["@CustomerID"].Value = CustomerID;
            cmd.Parameters["@FirstName"].Value = FirstName;
            cmd.Parameters["@LastName"].Value = LastName;
            cmd.Parameters["@Password"].Value = Password;
            cmd.Parameters["@PhoneNumber"].Value = PhoneNumber;
            cmd.Parameters["@Email"].Value = Email;

            try
            {
                conn.Open();

                var rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("SQL error updating employee.");
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
        }
    }
}
