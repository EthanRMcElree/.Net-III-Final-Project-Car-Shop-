using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class CarInventory
    {      
        public int CarID {  get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public Double Price { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }
        public Double EngineSize { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return Year + ", " + Model;
        }
    }
    public class CarInventoryVM : CarInventory
    {
    }
}
