using DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjectsLayer;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;

namespace DataAccessLayer
{
    public class ServiceAppointmentAccessor : IServiceAppointmentAccessor
    {
        public int CreateNewServiceAppointment(int CarID, string CustomerEmail, int ServiceTypeID, string CustomerComments, DateTime ScheduleDate)
        {
            // Make return variable if appropriate
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_create_service_appointment";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters            
            cmd.Parameters.Add("@CarID", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerEmail", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@ServiceTypeID", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerComments", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@ScheduleDate", SqlDbType.DateTime);

            // parameter values
            cmd.Parameters["@CarID"].Value = CarID;
            cmd.Parameters["@CustomerEmail"].Value = CustomerEmail;
            cmd.Parameters["@ServiceTypeID"].Value = ServiceTypeID;
            if(CustomerComments == null)
            {
                CustomerComments = "No comments at this time.";
            }
            cmd.Parameters["@CustomerComments"].Value = CustomerComments;
            cmd.Parameters["@ScheduleDate"].Value = ScheduleDate;
            int ID = 0;
            try
            {
                conn.Open();

                ID = (int)cmd.ExecuteNonQuery();

                if (ID != -1)
                {
                    return 0;
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
            return ID;
        }

        public void DeleteServiceAppointmentByAppointmentID(int AppointmentID)
        {
            // Make return variable if appropriate
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_delete_service_appointment_by_appointment_id";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@AppointmentID", SqlDbType.Int);

            // parameter values            
            cmd.Parameters["@AppointmentID"].Value = AppointmentID;

            try
            {
                conn.Open();

                var rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("SQL error deleting appointment.");
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

        public List<ServiceAppointmentVM> RetrieveServiceAppointments()
        {
            List<ServiceAppointmentVM> serviceAppointmentVMs = new List<ServiceAppointmentVM>();

            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_view_all_service_appointments_data";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                // open connection
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ServiceAppointmentVM serviceAppointmentVM = new ServiceAppointmentVM();
                        serviceAppointmentVM.AppointmentID = reader.GetInt32(0);
                        serviceAppointmentVM.CarID = reader.GetInt32(1);
                        serviceAppointmentVM.CustomerEmail = reader.GetString(2);
                        serviceAppointmentVM.ServiceTypeID = reader.GetInt32(3);
                        if (reader.IsDBNull(4))
                        {
                            serviceAppointmentVM.CustomerComments = "No comment at this time.";
                        }
                        else
                        {
                            serviceAppointmentVM.CustomerComments = reader.GetString(4);
                        }
                        serviceAppointmentVM.ScheduledDate = reader.GetDateTime(5);
                        serviceAppointmentVMs.Add(serviceAppointmentVM);
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

            return serviceAppointmentVMs;
        }

        public ServiceAppointmentVM RetrieveServiceAppointmentByAppointmentID(int AppointmentID)
        {
            ServiceAppointmentVM serviceAppointmentVM = new ServiceAppointmentVM();

            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_retrieve_service_appointment_by_appointment_id";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to the command
            cmd.Parameters.Add("@AppointmentID", SqlDbType.Int);

            // set the parameters values
            cmd.Parameters["@AppointmentID"].Value = AppointmentID;

            try
            {
                // open connection
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        serviceAppointmentVM.AppointmentID = reader.GetInt32(0);
                        serviceAppointmentVM.CarID = reader.GetInt32(1);
                        serviceAppointmentVM.CustomerEmail = reader.GetString(2);
                        serviceAppointmentVM.ServiceTypeID = reader.GetInt32(3);
                        if (reader.IsDBNull(4))
                        {
                            serviceAppointmentVM.CustomerComments = "No comment at this time.";
                        }
                        else
                        {
                            serviceAppointmentVM.CustomerComments = reader.GetString(4);
                        }                                            
                        serviceAppointmentVM.ScheduledDate = reader.GetDateTime(5);
                        serviceAppointmentVM.CarModel = reader.GetString(6);
                        serviceAppointmentVM.ServiceTypeName = reader.GetString(7);
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

            return serviceAppointmentVM;
        }

        public void UpdateServiceAppointment(int AppointmentID, int CarID, string CustomerEmail, int ServiceTypeID, string CustomerComments, DateTime ScheduleDate)
        {
            // Make return variable if appropriate
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_update_service_appointment";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@AppointmentID", SqlDbType.Int);
            cmd.Parameters.Add("@CarID", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerEmail", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@ServiceTypeID", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerComments", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@ScheduleDate", SqlDbType.DateTime);

            // parameter values
            cmd.Parameters["@AppointmentID"].Value = AppointmentID;
            cmd.Parameters["@CarID"].Value = CarID;
            cmd.Parameters["@CustomerEmail"].Value = CustomerEmail;
            cmd.Parameters["@ServiceTypeID"].Value = ServiceTypeID;
            cmd.Parameters["@CustomerComments"].Value = CustomerComments;
            cmd.Parameters["@ScheduleDate"].Value = ScheduleDate;

            try
            {
                conn.Open();

                var rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("SQL error updating service appointment.");
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

        public ServiceTypeVM RetrieveServiceTypeByID(int ServiceTypeID)
        {
            ServiceTypeVM serviceType = null;

            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_retrieve_service_type_by_type_id";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to the command
            cmd.Parameters.Add("@ServiceTypeID", SqlDbType.Int);

            // set the parameters values
            cmd.Parameters["@ServiceTypeID"].Value = ServiceTypeID;

            try
            {
                // open connection
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        serviceType = new ServiceTypeVM();
                        serviceType.ServiceTypeID = reader.GetInt32(0);
                        serviceType.ServiceDescription = reader.GetString(1);
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

            return serviceType;
        }

        public ServiceTypeVM RetrieveServiceTypeByDescription(string Description)
        {
            ServiceTypeVM serviceType = null;

            // start a connect object
            var conn = SqlConnectionProvider.GetConnection();

            // set the command text
            var commandText = "sp_retrieve_service_type_by_description";

            // create the command object
            var cmd = new SqlCommand(commandText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters to the command
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);

            // set the parameters values
            cmd.Parameters["@Description"].Value = Description;

            try
            {
                // open connection
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        serviceType = new ServiceTypeVM();
                        serviceType.ServiceTypeID = reader.GetInt32(0);
                        serviceType.ServiceDescription = reader.GetString(1);
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

            return serviceType;
        }
    }
}
