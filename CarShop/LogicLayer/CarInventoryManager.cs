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
    public class CarInventoryManager : ICarInventoryManager
    {        
        private ICarInventoryAccessor _carInventoryAccessor = null;
        
        public CarInventoryManager() 
        {
            _carInventoryAccessor = new CarInventoryAccessor();
        }
        
        public CarInventoryManager(ICarInventoryAccessor carInventoryAccessor)
        {
            _carInventoryAccessor = carInventoryAccessor;
        }

        public int DeleteCarByID(int CarID)
        {
            int rows = 0;
            try
            {
                rows = _carInventoryAccessor.DeleteCarByID(CarID);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Failed to delete car", ex);
            }
            return rows;            
        }

        public List<CarInventoryVM> FilterCarByFuelType(string FuelType)
        {
            List<CarInventoryVM> carInventory = new List<CarInventoryVM>();
            try
            {
                carInventory = _carInventoryAccessor.FilterCarByFuelType(FuelType);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to filter by fuel type", ex);
            }
            return carInventory;
        }

        public List<CarInventoryVM> FilterCarByHighMileage()
        {
            List<CarInventoryVM> carInventory = new List<CarInventoryVM>();
            try
            {
                carInventory = _carInventoryAccessor.FilterCarByHighMileage();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to filter by high mileage", ex);
            }
            return carInventory;
        }

        public List<CarInventoryVM> FilterCarByLowMileage()
        {
            List<CarInventoryVM> carInventory = new List<CarInventoryVM>();
            try
            {
                carInventory = _carInventoryAccessor.FilterCarByLowMileage();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to filter by low mileage", ex);
            }
            return carInventory;
        }

        public List<CarInventoryVM> FilterCarByModerateMileage()
        {
            List<CarInventoryVM> carInventory = new List<CarInventoryVM>();
            try
            {
                carInventory = _carInventoryAccessor.FilterCarByModerateMileage();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to filter by low mileage", ex);
            }
            return carInventory;
        }

        public int InsertNewCar(string Model, int Year, string Color, string VIN, Double Price, int Mileage, string FuelType, string TransmissionType, Double EngineSize, string Description)
        {
            int rows = 0;
            try
            {
                rows = _carInventoryAccessor.InsertNewCar(Model, Year, Color, VIN, Price, Mileage, FuelType, TransmissionType, EngineSize, Description);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Failed to insert a new car.", ex);
            }
            return rows;
        }

        public void UpdateCar(int CarID, string CustomerEmail, string Model, int Year, string Color, string VIN, Double Price, int Mileage, string FuelType, string TransmissionType, Double EngineSize, string Description)
        {
            try
            {
                _carInventoryAccessor.UpdateCar(CarID, CustomerEmail, Model, Year, Color, VIN, Price, Mileage, FuelType, TransmissionType, EngineSize, Description);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update a car.", ex);
            }      
        }

        public CarInventoryVM ViewCarByID(int CarID)
        {
            CarInventoryVM result = null;
            try
            {
                result = _carInventoryAccessor.ViewCarByID(CarID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to view a car.", ex);
            }
            return result;
        }

        public List<CarInventoryVM> ViewCarInventory()
        {
            List<CarInventoryVM> carInventory = new List<CarInventoryVM>();
            try
            {
                carInventory = _carInventoryAccessor.ViewCarInventory();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to view the car's inventory", ex);
            }
            return carInventory;
        }
    }
}
