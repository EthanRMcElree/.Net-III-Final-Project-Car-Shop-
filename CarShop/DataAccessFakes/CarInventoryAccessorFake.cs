using DataAccessInterface;
using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class CarInventoryAccessorFake : ICarInventoryAccessor
    {
        public List<CarInventoryVM> fakeCars = new List<CarInventoryVM>();

        public CarInventoryAccessorFake() 
        {
            fakeCars.Add(new CarInventoryVM()
            {
                CarID = 1,
                Model = "crosstrek",
                Year = 2023,
                Color = "black",
                VIN = "4HDAS138",
                Price = 24995.00F,
                Mileage = 0,
                FuelType = "regular",
                TransmissionType = "automatic",
                EngineSize = 2.5F,
                Description = "do not miss out on this car"
            });

            fakeCars.Add(new CarInventoryVM()
            {
                CarID = 2,
                Model = "forester",
                Year = 2024,
                Color = "blue",
                VIN = "9F1J2AG9",
                Price = 25800.00F,
                Mileage = 76000,
                FuelType = "E85",
                TransmissionType = "manual",
                EngineSize = 2.5F,
                Description = "another great car"
            });

            fakeCars.Add(new CarInventoryVM()
            {
                CarID = 3,
                Model = "ascent",
                Year = 2024,
                Color = "red",
                VIN = "1A8HWE40",
                Price = 27550.00F,
                Mileage = 12000,
                FuelType = "hybrid",
                TransmissionType = "automatic",
                EngineSize = 2.5F,
                Description = "awesome car"
            });
        }

        public int DeleteCarByID(int CarID)
        {
            CarInventoryVM carInventoryVM = ViewCarByID(CarID);
            if(carInventoryVM == null)
            {
                return 0;
            }
            fakeCars.Remove(carInventoryVM);
            return 1;
        }

        public List<CarInventoryVM> FilterCarByFuelType(string FuelType)
        {
            List<CarInventoryVM> cars = new List<CarInventoryVM>();
            foreach(CarInventoryVM car in fakeCars)
            {
                if (car.FuelType.Equals(FuelType))
                {
                    cars.Add(car);
                }              
            }
            return cars;
        }

        public List<CarInventoryVM> FilterCarByHighMileage()
        {
            List<CarInventoryVM> cars = new List<CarInventoryVM>();
            foreach (CarInventoryVM car in fakeCars)
            {
                if (car.Mileage > 75000)
                {
                    cars.Add(car);
                }
            }
            return cars;
        }

        public List<CarInventoryVM> FilterCarByLowMileage()
        {
            List<CarInventoryVM> cars = new List<CarInventoryVM>();
            foreach (CarInventoryVM car in fakeCars)
            {
                if (car.Mileage < 10000)
                {
                    cars.Add(car);
                }
            }
            return cars;
        }

        public List<CarInventoryVM> FilterCarByModel(string Model)
        {
            List<CarInventoryVM> cars = new List<CarInventoryVM>();
            foreach (CarInventoryVM car in fakeCars)
            {
                if (car.Model.Equals(Model))
                {
                    cars.Add(car);
                }
            }
            return cars;
        }

        public List<CarInventoryVM> FilterCarByModerateMileage()
        {
            List<CarInventoryVM> cars = new List<CarInventoryVM>();
            foreach (CarInventoryVM car in fakeCars)
            {
                if (car.Mileage < 75000 && car.Mileage > 10000)
                {
                    cars.Add(car);
                }
            }
            return cars;
        }

        public List<CarInventoryVM> FilterCarByYear(int Year)
        {
            List<CarInventoryVM> cars = new List<CarInventoryVM>();
            foreach (CarInventoryVM car in fakeCars)
            {
                if (car.Year.Equals(Year))
                {
                    cars.Add(car);
                }
            }
            return cars;
        }

        public int InsertNewCar(string Model, int Year, string Color, string VIN, Double Price, int Mileage, string FuelType, string TransmissionType, Double EngineSize, string Description)
        {
            if (Year <= 0)
            {
                return 0;
            }
            fakeCars.Add(new CarInventoryVM()
            {
                Model = Model,
                Year = Year,
                Color = Color,
                VIN = VIN,
                Price = Price,
                Mileage = Mileage,
                FuelType = FuelType,
                TransmissionType = TransmissionType,
                EngineSize = EngineSize,
                Description = Description
            });
            return 1;
        }

        public void UpdateCar(int CarID, string CustomerEmail, string Model, int Year, string Color, string VIN, Double Price, int Mileage, string FuelType, string TransmissionType, Double EngineSize, string Description)
        {                        
            var carToUpdate = ViewCarByID(CarID);

            if (carToUpdate != null)
            {
                carToUpdate.Model = Model;
                carToUpdate.CustomerEmail = CustomerEmail;
                carToUpdate.Year = Year;
                carToUpdate.Color = Color;
                carToUpdate.VIN = VIN;
                carToUpdate.Price = Price;
                carToUpdate.Mileage = Mileage;
                carToUpdate.FuelType = FuelType;
                carToUpdate.TransmissionType = TransmissionType;
                carToUpdate.EngineSize = EngineSize;
                carToUpdate.Description = Description;
            }
        }

        public CarInventoryVM ViewCarByID(int CarID)
        {
            CarInventoryVM carInventoryVM = new CarInventoryVM();

            foreach (CarInventoryVM carInventory in fakeCars)
            {
                if(carInventory.CarID == CarID)
                {
                    return carInventory;
                }              
            }
            return null;
        }

        public List<CarInventoryVM> ViewCarInventory()
        {
            return fakeCars;
        }
    }
}
