using DataAccessInterface;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CarInventoryAccessor : ICarInventoryAccessor
    {
        public int DeleteCarByID(int CarID)
        {            
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_delete_car";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@CarID", SqlDbType.Int);            

            // parameter values            
            cmd.Parameters["@CarID"].Value = CarID;
            var rows = 0;
            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {                   
                    Console.WriteLine("Cannot Delete Car.");
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

        public List<CarInventoryVM> FilterCarByFuelType(string FuelType)
        {
            // Make return variable if appropriate
            var List = new List<CarInventoryVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_filter_car_inventory_by_fuel_type";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@FuelType", SqlDbType.NVarChar, 50);

            // parameter values            
            cmd.Parameters["@FuelType"].Value = FuelType;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    CarInventoryVM carInventoryVM = null;
                    while (reader.Read())
                    {
                        carInventoryVM = new CarInventoryVM();
                        carInventoryVM.CarID = reader.GetInt32(0);
                        carInventoryVM.Model = reader.GetString(1);
                        carInventoryVM.Year = reader.GetInt32(2);
                        carInventoryVM.Color = reader.GetString(3);
                        carInventoryVM.VIN = reader.GetString(4);
                        carInventoryVM.Price = reader.GetDouble(5);
                        carInventoryVM.Mileage = reader.GetInt32(6);
                        carInventoryVM.FuelType = reader.GetString(7);
                        carInventoryVM.TransmissionType = reader.GetString(8);
                        carInventoryVM.EngineSize = reader.GetDouble(9);
                        carInventoryVM.Description = reader.GetString(10);
                        List.Add(carInventoryVM);
                        carInventoryVM = null;
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

        public List<CarInventoryVM> FilterCarByHighMileage()
        {
            // Make return variable if appropriate
            var List = new List<CarInventoryVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_filter_car_inventory_by_high_mileage";

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
                    CarInventoryVM carInventoryVM = new CarInventoryVM();
                    while (reader.Read())
                    {
                        carInventoryVM.CarID = reader.GetInt32(0);
                        carInventoryVM.Model = reader.GetString(1);
                        carInventoryVM.Year = reader.GetInt32(2);
                        carInventoryVM.Color = reader.GetString(3);
                        carInventoryVM.VIN = reader.GetString(4);
                        carInventoryVM.Price = reader.GetDouble(5);
                        carInventoryVM.Mileage = reader.GetInt32(6);
                        carInventoryVM.FuelType = reader.GetString(7);
                        carInventoryVM.TransmissionType = reader.GetString(8);
                        carInventoryVM.EngineSize = reader.GetDouble(9);
                        carInventoryVM.Description = reader.GetString(10);
                        List.Add(carInventoryVM);
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

        public List<CarInventoryVM> FilterCarByLowMileage()
        {
            // Make return variable if appropriate
            var List = new List<CarInventoryVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_filter_car_inventory_by_low_mileage";

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
                CarInventoryVM carInventoryVM = null;
                if (reader.HasRows)
                {                  
                    while (reader.Read())
                    {
                        carInventoryVM = new CarInventoryVM();
                        carInventoryVM.CarID = reader.GetInt32(0);
                        carInventoryVM.Model = reader.GetString(1);
                        carInventoryVM.Year = reader.GetInt32(2);
                        carInventoryVM.Color = reader.GetString(3);
                        carInventoryVM.VIN = reader.GetString(4);
                        carInventoryVM.Price = reader.GetDouble(5);
                        carInventoryVM.Mileage = reader.GetInt32(6);
                        carInventoryVM.FuelType = reader.GetString(7);
                        carInventoryVM.TransmissionType = reader.GetString(8);
                        carInventoryVM.EngineSize = reader.GetDouble(9);
                        carInventoryVM.Description = reader.GetString(10);
                        List.Add(carInventoryVM);
                        carInventoryVM = null;
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

        public List<CarInventoryVM> FilterCarByModel(string Model)
        {
            // Make return variable if appropriate
            var List = new List<CarInventoryVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_filter_car_inventory_by_model";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 50);

            // parameter values            
            cmd.Parameters["@Model"].Value = Model;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    CarInventoryVM carInventoryVM = new CarInventoryVM();
                    while (reader.Read())
                    {
                        carInventoryVM.CarID = reader.GetInt32(0);
                        carInventoryVM.Model = reader.GetString(1);
                        carInventoryVM.Year = reader.GetInt32(2);
                        carInventoryVM.Color = reader.GetString(3);
                        carInventoryVM.VIN = reader.GetString(4);
                        carInventoryVM.Price = reader.GetDouble(5);
                        carInventoryVM.Mileage = reader.GetInt32(6);
                        carInventoryVM.FuelType = reader.GetString(7);
                        carInventoryVM.TransmissionType = reader.GetString(8);
                        carInventoryVM.EngineSize = reader.GetDouble(9);
                        carInventoryVM.Description = reader.GetString(10);
                        List.Add(carInventoryVM);
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

        public List<CarInventoryVM> FilterCarByModerateMileage()
        {
            // Make return variable if appropriate
            var List = new List<CarInventoryVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_filter_car_inventory_by_moderate_mileage";

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
                    CarInventoryVM carInventoryVM = new CarInventoryVM();
                    while (reader.Read())
                    {
                        carInventoryVM.CarID = reader.GetInt32(0);
                        carInventoryVM.Model = reader.GetString(1);
                        carInventoryVM.Year = reader.GetInt32(2);
                        carInventoryVM.Color = reader.GetString(3);
                        carInventoryVM.VIN = reader.GetString(4);
                        carInventoryVM.Price = reader.GetDouble(5);
                        carInventoryVM.Mileage = reader.GetInt32(6);
                        carInventoryVM.FuelType = reader.GetString(7);
                        carInventoryVM.TransmissionType = reader.GetString(8);
                        carInventoryVM.EngineSize = reader.GetDouble(9);
                        carInventoryVM.Description = reader.GetString(10);
                        List.Add(carInventoryVM);
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

        public List<CarInventoryVM> FilterCarByYear(int Year)
        {
            // Make return variable if appropriate
            var List = new List<CarInventoryVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_filter_car_inventory_by_year";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Year", SqlDbType.NVarChar, 50);

            // parameter values            
            cmd.Parameters["@Year"].Value = Year;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    CarInventoryVM carInventoryVM = new CarInventoryVM();
                    while (reader.Read())
                    {
                        carInventoryVM.CarID = reader.GetInt32(0);
                        carInventoryVM.Model = reader.GetString(1);
                        carInventoryVM.Year = reader.GetInt32(2);
                        carInventoryVM.Color = reader.GetString(3);
                        carInventoryVM.VIN = reader.GetString(4);
                        carInventoryVM.Price = reader.GetDouble(5);
                        carInventoryVM.Mileage = reader.GetInt32(6);
                        carInventoryVM.FuelType = reader.GetString(7);
                        carInventoryVM.TransmissionType = reader.GetString(8);
                        carInventoryVM.EngineSize = reader.GetDouble(9);
                        carInventoryVM.Description = reader.GetString(10);
                        List.Add(carInventoryVM);
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

        public int InsertNewCar(string Model, int Year, string Color, string VIN, Double Price, int Mileage, string FuelType, string TransmissionType, Double EngineSize, string Description)
        {
            // Make return variable if appropriate
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_insert_new_car";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters            
            cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Year", SqlDbType.Int);
            cmd.Parameters.Add("@Color", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@VIN", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@Price", SqlDbType.Float);
            cmd.Parameters.Add("@Mileage", SqlDbType.Int);
            cmd.Parameters.Add("@FuelType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@TransmissionType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EngineSize", SqlDbType.Float);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);

            // parameter values
            cmd.Parameters["@Model"].Value = Model;
            cmd.Parameters["@Year"].Value = Year;
            cmd.Parameters["@Color"].Value = Color;
            cmd.Parameters["@VIN"].Value = VIN;
            cmd.Parameters["@Price"].Value = Price;
            cmd.Parameters["@Mileage"].Value = Mileage;
            cmd.Parameters["@FuelType"].Value = FuelType;
            cmd.Parameters["@TransmissionType"].Value = TransmissionType;
            cmd.Parameters["@EngineSize"].Value = EngineSize;
            cmd.Parameters["@Description"].Value = Description;

            var rows = 0;

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();

                if (rows == 0)
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
            return rows;
        }

        public void UpdateCar(int CarID, string CustomerEmail, string Model, int Year, string Color, string VIN, Double Price, int Mileage, string FuelType, string TransmissionType, Double EngineSize, string Description)
        {
            // Make return variable if appropriate
            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_update_car";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@CarID", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerEmail", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Year", SqlDbType.Int);
            cmd.Parameters.Add("@Color", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@VIN", SqlDbType.NVarChar, 17);
            cmd.Parameters.Add("@Price", SqlDbType.Float);
            cmd.Parameters.Add("@Mileage", SqlDbType.Int);
            cmd.Parameters.Add("@FuelType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@TransmissionType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EngineSize", SqlDbType.Float);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250);

            // parameter values
            cmd.Parameters["@CarID"].Value = CarID;
            cmd.Parameters["@CustomerEmail"].Value = CustomerEmail;
            cmd.Parameters["@Model"].Value = Model;
            cmd.Parameters["@Year"].Value = Year;
            cmd.Parameters["@Color"].Value = Color;
            cmd.Parameters["@VIN"].Value = VIN;
            cmd.Parameters["@Price"].Value = Price;
            cmd.Parameters["@Mileage"].Value = Mileage;
            cmd.Parameters["@FuelType"].Value = FuelType;
            cmd.Parameters["@TransmissionType"].Value = TransmissionType;
            cmd.Parameters["@EngineSize"].Value = EngineSize;
            cmd.Parameters["@Description"].Value = Description;

            try
            {
                conn.Open();
               
                var rows = cmd.ExecuteNonQuery();                

                if (rows == 0)
                {                    
                    throw new ArgumentException("SQL error updating car.");
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

        public CarInventoryVM ViewCarByID(int CarID)
        {
            // Make return variable if appropriate
            CarInventoryVM carInventoryVM = null;

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_select_car_by_id";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@CarID", SqlDbType.Int);

            // parameter values
            cmd.Parameters["@CarID"].Value = CarID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    carInventoryVM = new CarInventoryVM();
                    while (reader.Read())
                    {
                        carInventoryVM.CarID = reader.GetInt32(0);
                        carInventoryVM.Model = reader.GetString(1);
                        carInventoryVM.Year = reader.GetInt32(2);
                        carInventoryVM.Color = reader.GetString(3);
                        carInventoryVM.VIN = reader.GetString(4);
                        carInventoryVM.Price = reader.GetDouble(5);
                        carInventoryVM.Mileage = reader.GetInt32(6);
                        carInventoryVM.FuelType = reader.GetString(7);
                        carInventoryVM.TransmissionType = reader.GetString(8);
                        carInventoryVM.EngineSize = reader.GetDouble(9);
                        carInventoryVM.Description = reader.GetString(10);
                        carInventoryVM.CustomerEmail = reader.GetString(11);
                        if (carInventoryVM.CustomerEmail == "")
                        {
                            carInventoryVM.CustomerEmail = "No one has bought his car yet.";
                        }
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
            return carInventoryVM;
        }

        public List<CarInventoryVM> ViewCarInventory()
        {
            // Make return variable if appropriate
            var List = new List<CarInventoryVM>();

            // connection
            var conn = SqlConnectionProvider.GetConnection();

            // command text
            var cmdText = "sp_view_car_inventory";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;           

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {                   
                    while (reader.Read())
                    {
                        CarInventoryVM carInventoryVM = new CarInventoryVM();
                        carInventoryVM.CarID = reader.GetInt32(0);
                        carInventoryVM.Model = reader.GetString(1);
                        carInventoryVM.Year = reader.GetInt32(2);
                        carInventoryVM.Color = reader.GetString(3);
                        carInventoryVM.VIN = reader.GetString(4);
                        carInventoryVM.Price = reader.GetDouble(5);
                        carInventoryVM.Mileage = reader.GetInt32(6);
                        carInventoryVM.FuelType = reader.GetString(7);
                        carInventoryVM.TransmissionType = reader.GetString(8);
                        carInventoryVM.EngineSize = reader.GetDouble(9);
                        carInventoryVM.Description = reader.GetString(10);
                        carInventoryVM.CustomerEmail = reader.GetString(11);
                        if (carInventoryVM.CustomerEmail == "")
                        {
                            carInventoryVM.CustomerEmail = "No one has bought his car yet.";
                        }
                        List.Add(carInventoryVM);
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
