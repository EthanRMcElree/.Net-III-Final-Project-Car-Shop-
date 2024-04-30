using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ICarInventoryManager 
    {
        // Insert new car inventory vm 
        int InsertNewCar(string Model, int Year, string Color, string VIN, Double Price, int Mileage, string FuelType, string TransmissionType, Double EngineSize, string Description);
        // Update new car inventory vm 
        void UpdateCar(int CarID, string CustomerEmail, string Model, int Year, string Color, string VIN, Double Price, int Mileage, string FuelType, string TransmissionType, Double EngineSize, string Description);
        // Select new car inventory vm 
        CarInventoryVM ViewCarByID(int CarID);
        // Select all new car inventory vm 
        List<CarInventoryVM> ViewCarInventory();
        // Delete new car inventory vm 
        int DeleteCarByID(int CarID);
        // Select high mileage
        List<CarInventoryVM> FilterCarByHighMileage();
        // Select low mileage
        List<CarInventoryVM> FilterCarByLowMileage();
        // Select moderate mileage
        List<CarInventoryVM> FilterCarByModerateMileage();
        // Select by fueltype
        List<CarInventoryVM> FilterCarByFuelType(string FuelType);
    }
}
