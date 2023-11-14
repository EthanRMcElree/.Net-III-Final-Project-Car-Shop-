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
            return _carInventoryAccessor.DeleteCarByID(CarID);            
        }

        public List<CarInventoryVM> FilterCarByFuelType(string FuelType)
        {
            return _carInventoryAccessor.FilterCarByFuelType(FuelType);
        }

        public List<CarInventoryVM> FilterCarByHighMileage()
        {
            return _carInventoryAccessor.FilterCarByHighMileage();
        }

        public List<CarInventoryVM> FilterCarByLowMileage()
        {
            return _carInventoryAccessor.FilterCarByLowMileage();
        }

        public List<CarInventoryVM> FilterCarByModerateMileage()
        {
            return _carInventoryAccessor.FilterCarByModerateMileage();
        }

        public int InsertNewCar(string Model, int Year, string Color, string VIN, Double Price, int Mileage, string FuelType, string TransmissionType, Double EngineSize, string Description)
        {
            return _carInventoryAccessor.InsertNewCar(Model, Year, Color, VIN, Price, Mileage, FuelType, TransmissionType, EngineSize, Description);
        }

        public void UpdateCar(int CarID, string Model, int Year, string Color, string VIN, Double Price, int Mileage, string FuelType, string TransmissionType, Double EngineSize, string Description)
        {
            _carInventoryAccessor.UpdateCar(CarID, Model, Year, Color, VIN, Price, Mileage, FuelType, TransmissionType, EngineSize, Description);
        }

        public CarInventoryVM ViewCarByID(int CarID)
        {
            return _carInventoryAccessor.ViewCarByID(CarID);
        }

        public List<CarInventoryVM> ViewCarInventory()
        {
            return _carInventoryAccessor.ViewCarInventory();
        }
    }
}
