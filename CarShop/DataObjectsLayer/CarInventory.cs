using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class CarInventory
    {
        /*
         CarID	int	
        Model	nvarchar	50
        Year	int	
        Color	nvarchar	50
        VIN	nvarchar	17
        Price	float	
        Mileage	int	
        FuelType	nvarchar	50
        TransmissionType	nvarchar	50
        EngineSize	float	
        Description	nvarchar	250
         */
        public int CarID {  get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public float Price { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }
        public float EngineSize { get; set; }
        public string Description { get; set; }
    }
    public class CarInventoryVM : CarInventory
    {
    }
}
