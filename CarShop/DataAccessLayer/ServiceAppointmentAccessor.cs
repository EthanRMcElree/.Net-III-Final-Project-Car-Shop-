﻿using DataAccessInterface;
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
        public int CreateNewServiceAppointment(int CarID, int CustomerID, int ServiceTypeID)
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
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@ServiceTypeID", SqlDbType.Int);            

            // parameter values
            cmd.Parameters["@CarID"].Value = CarID;
            cmd.Parameters["@CustomerID"].Value = CustomerID;
            cmd.Parameters["@ServiceTypeID"].Value = ServiceTypeID;
            int ID = 0;
            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ID = reader.GetInt32(0);                        
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
                        serviceAppointmentVM.CustomerID = reader.GetInt32(2);
                        serviceAppointmentVM.ServiceTypeID = reader.GetInt32(3);
                        serviceAppointmentVM.SupplierID = reader.GetInt32(4);
                        serviceAppointmentVM.ScheduledDate = reader.GetDateTime(5);
                        Customer customer = new Customer();
                        customer.FirstName = reader.GetString(6);
                        customer.LastName = reader.GetString(7);
                        serviceAppointmentVM.Customer = customer;
                        CarInventory car = new CarInventory();
                        car.Model = reader.GetString(8);
                        car.Year = reader.GetInt32(9);
                        serviceAppointmentVM.Car = car;
                        ServiceType serviceType = new ServiceType();
                        serviceType.ServiceDescription = reader.GetString(10);
                        serviceAppointmentVM.ServiceType = serviceType;
                        Supplier supplier = new Supplier();
                        supplier.SupplierName = reader.GetString(11);
                        serviceAppointmentVM.Supplier = supplier;
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
                        serviceAppointmentVM.CustomerID = reader.GetInt32(2);
                        serviceAppointmentVM.ServiceTypeID = reader.GetInt32(3);
                        serviceAppointmentVM.SupplierID = reader.GetInt32(4);
                        serviceAppointmentVM.ScheduledDate = reader.GetDateTime(5);                        
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

        public void UpdateServiceAppointment(int AppointmentID, int CarID, int CustomerID, int ServiceTypeID, int SupplierID, DateTime ScheduledDate)
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
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@ServiceTypeID", SqlDbType.Int);
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters.Add("@ScheduledDate", SqlDbType.DateTime);            

            // parameter values
            cmd.Parameters["@AppointmentID"].Value = AppointmentID;
            cmd.Parameters["@CarID"].Value = CarID;
            cmd.Parameters["@CustomerID"].Value = CustomerID;
            cmd.Parameters["@ServiceTypeID"].Value = ServiceTypeID;
            cmd.Parameters["@SupplierID"].Value = SupplierID;
            cmd.Parameters["@ScheduledDate"].Value = ScheduledDate;         

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
    }
}
