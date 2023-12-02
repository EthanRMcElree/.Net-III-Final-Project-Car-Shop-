using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterface;
using DataObjectsLayer;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;

namespace DataAccessLayer
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        public EmployeeVM AuthenticateEmployeeWithEmailAndPassword(string email, string password)
        {
            EmployeeVM employeeVM = null;

            // start connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set command text
            var commandText = "sp_authenticate_employee";

            // create command object
            var cmd = new SqlCommand(commandText, conn);

            // set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to the command
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // set the parameters values
            cmd.Parameters["@Password"].Value = password;
            cmd.Parameters["@Email"].Value = email;            

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
                        employeeVM = new EmployeeVM();
                        employeeVM.EmployeeID = reader.GetInt32(0);
                        employeeVM.FirstName = reader.GetString(1);
                        employeeVM.LastName = reader.GetString(2);
                        employeeVM.Password = reader.GetString(3);
                        employeeVM.PhoneNumber = reader.GetString(4);
                        employeeVM.Email = reader.GetString(5);
                        employeeVM.Role = reader.GetString(6);
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

            return employeeVM;
        }

        public EmployeeVM SelectEmployeeVMByID(int EmployeeID)
        {
            EmployeeVM employeeVM = new EmployeeVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_employee_by_id";

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
                // open the connection
                conn.Open();

                // execute
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        employeeVM.EmployeeID = reader.GetInt32(0);
                        employeeVM.FirstName = reader.GetString(1);
                        employeeVM.LastName = reader.GetString(2);
                        employeeVM.Password = reader.GetString(3);
                        employeeVM.PhoneNumber = reader.GetString(4);
                        employeeVM.Email = reader.GetString(5);
                        employeeVM.Role = reader.GetString(6);
                    }
                }
                else
                {
                    throw new ArgumentException("Unable to find employee");
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

            return employeeVM;
        }

        public EmployeeVM SelectEmployeeVMByEmail(string email)
        {
            EmployeeVM employeeVM = new EmployeeVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_employee_by_email";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            // parameter values
            cmd.Parameters["@Email"].Value = email;

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
                        employeeVM.EmployeeID = reader.GetInt32(0);
                        employeeVM.FirstName = reader.GetString(1);
                        employeeVM.LastName = reader.GetString(2);
                        employeeVM.Password = reader.GetString(3);
                        employeeVM.PhoneNumber = reader.GetString(4);
                        employeeVM.Email = reader.GetString(5);
                        employeeVM.Role = reader.GetString(6);
                    }
                }
                else
                {
                    throw new ArgumentException("Unable to find employee");
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

            return employeeVM;
        }    

        public int ChangeEmployeePassword(string email, string oldPassword, string newPassword)
        {
            int rows = 0;

            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_change_employee_password";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to the command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPassword", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 100);           

            // set the parameters values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPassword"].Value = oldPassword;
            cmd.Parameters["@NewPassword"].Value = newPassword;            
            
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

        public void CreateEmployeeAccount(string FirstName, string LastName, string Password, string PhoneNumber, string Email, string Role)
        {            
            // connect object
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var commandText = "sp_create_employee_account";

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
            cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 100);

            // parameters values
            cmd.Parameters["@FirstName"].Value = FirstName;
            cmd.Parameters["@LastName"].Value = LastName;
            cmd.Parameters["@Password"].Value = Password;
            cmd.Parameters["@PhoneNumber"].Value = PhoneNumber;
            cmd.Parameters["@Email"].Value = Email;
            cmd.Parameters["@Role"].Value = Role;

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

        public void ResetEmployeePassword(string Email, string newPassword)
        {
            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_reset_employee_password";

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

        public void DeleteEmployeeAccountByEmail(string email)
        {
            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_delete_employee_account";

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

        public void UpdateEmployee(string FirstName, string LastName, string PhoneNumber, string Email, string Role)
        {
            // Make return variable if appropriate
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_update_employee";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 100);

            // parameters values
            cmd.Parameters["@FirstName"].Value = FirstName;
            cmd.Parameters["@LastName"].Value = LastName;            
            cmd.Parameters["@PhoneNumber"].Value = PhoneNumber;
            cmd.Parameters["@Email"].Value = Email;
            cmd.Parameters["@Role"].Value = Role;

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

        public string SelectRoleByEmployee(int EmployeeID)
        {
            EmployeeVM employeeVM = SelectEmployeeVMByID(EmployeeID);
            return employeeVM.Role;
        }
    }
}
