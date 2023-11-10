using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface ICarInventoryAccessor
    {
        // crud: create, retrieve, update, delete
        int InsertNewCar(string Model, int Year, string Color, string VIN, float Price, int Mileage, string FuelType, string TransmissionType, float EngineSize, string Description);
        void UpdateCar(int CarID, string Model, int Year, string Color, string VIN, float Price, int Mileage, string FuelType, string TransmissionType, float EngineSize, string Description);
        int DeleteCarByID(int CarID);
        List<CarInventoryVM> ViewCarInventory();
        CarInventoryVM ViewCarByID(int CarID);
        List<CarInventoryVM> FilterCarByModel(string Model);
        List<CarInventoryVM> FilterCarByYear(int Year);
        List<CarInventoryVM> FilterCarByFuelType(string FuelType);
        List<CarInventoryVM> FilterCarByHighMileage();
        List<CarInventoryVM> FilterCarByLowMileage();
        List<CarInventoryVM> FilterCarByModerateMileage();
    }
}
