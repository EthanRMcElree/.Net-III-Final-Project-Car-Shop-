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
    public class UserAccessor : IUserAccessor
    {
        public UserVM AuthenticateUserWithEmailAndPassword(string email, string password)
        {
            UserVM userVM = null;

            // start connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set command text
            var commandText = "sp_authenticate_user";

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
                        userVM = new UserVM();
                        userVM.UserID = reader.GetInt32(0);
                        userVM.FirstName = reader.GetString(1);
                        userVM.LastName = reader.GetString(2);
                        userVM.Password = reader.GetString(3);
                        userVM.PhoneNumber = reader.GetString(4);
                        userVM.Email = reader.GetString(5);
                        userVM.Role = reader.GetString(6);
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

            return userVM;
        }

        public UserVM SelectUserVMByID(int UserID)
        {
            UserVM userVM = new UserVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_user_by_id";

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
                // open the connection
                conn.Open();

                // execute
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        userVM.UserID = reader.GetInt32(0);
                        userVM.FirstName = reader.GetString(1);
                        userVM.LastName = reader.GetString(2);
                        userVM.Password = reader.GetString(3);
                        userVM.PhoneNumber = reader.GetString(4);
                        userVM.Email = reader.GetString(5);
                        userVM.Role = reader.GetString(6);
                    }
                }
                else
                {
                    throw new ArgumentException("Unable to find user");
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

            return userVM;
        }

        public UserVM SelectUserVMByEmail(string email)
        {
            UserVM userVM = new UserVM();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_user_by_email";

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
                        userVM.UserID = reader.GetInt32(0);
                        userVM.FirstName = reader.GetString(1);
                        userVM.LastName = reader.GetString(2);
                        userVM.Password = reader.GetString(3);
                        userVM.PhoneNumber = reader.GetString(4);
                        userVM.Email = reader.GetString(5);
                        userVM.Role = reader.GetString(6);
                        userVM.IsLoggedIn = reader.GetBoolean(7);
                        userVM.IsAdmin = reader.GetBoolean(8);
                    }
                }
                else
                {
                    throw new ArgumentException("Unable to find user");
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

            return userVM;
        }

        public List<UserVM> SelectAllUsers()
        {
            List<UserVM> userVMs = new List<UserVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_all_user";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;            

            try
            {
                // open the connection
                conn.Open();

                // execute
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserVM userVM = new UserVM();
                        userVM.UserID = reader.GetInt32(0);
                        userVM.FirstName = reader.GetString(1);
                        userVM.LastName = reader.GetString(2);
                        userVM.Password = reader.GetString(3);
                        userVM.PhoneNumber = reader.GetString(4);
                        userVM.Email = reader.GetString(5);
                        userVM.Role = reader.GetString(6);
                        userVMs.Add(userVM);
                    }
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Unable to find user");
            }
            finally
            {
                conn.Close();
            }

            return userVMs;
        }

        public int ChangeUserPassword(string email, string oldPassword, string newPassword)
        {
            int rows = 0;

            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_change_user_password";

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

        public void CreateUserAccount(string FirstName, string LastName, string Password, string PhoneNumber, string Email, string Role)
        {            
            // connect object
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var commandText = "sp_create_user_account";

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

        public void ResetUserPassword(string Email, string newPassword)
        {
            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_reset_user_password";

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

        public int DeleteUserAccountByID(int UserID)
        {
            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_delete_user_account";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to the command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);            

            // set the parameters values
            cmd.Parameters["@UserID"].Value = UserID;            

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
            return UserID;
        }

        public void UpdateUser(string FirstName, string LastName, string PhoneNumber, string Email, string Role, bool IsLoggedIn, bool IsAdmin)
        {
            // Make return variable if appropriate
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_update_user";

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
            cmd.Parameters.Add("@IsLoggedIn", SqlDbType.Bit);
            cmd.Parameters.Add("@IsAdmin", SqlDbType.Bit);

            // parameters values
            cmd.Parameters["@FirstName"].Value = FirstName;
            cmd.Parameters["@LastName"].Value = LastName;            
            cmd.Parameters["@PhoneNumber"].Value = PhoneNumber;
            cmd.Parameters["@Email"].Value = Email;
            cmd.Parameters["@Role"].Value = Role;
            cmd.Parameters["@IsLoggedIn"].Value = IsLoggedIn;
            cmd.Parameters["@IsAdmin"].Value = IsAdmin;

            try
            {
                conn.Open();

                var rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("SQL error updating user.");
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

        public string SelectRoleByUser(int UserID)
        {
            UserVM userVM = SelectUserVMByID(UserID);
            return userVM.Role;
        }

        public List<string> GetCustomerEmails()
        {
            List<string> userEmails = new List<string>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_get_all_customer_emails";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                // open the connection
                conn.Open();

                // execute
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userEmails.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Unable to find user");
            }
            finally
            {
                conn.Close();
            }

            return userEmails;
        }
    }
}
