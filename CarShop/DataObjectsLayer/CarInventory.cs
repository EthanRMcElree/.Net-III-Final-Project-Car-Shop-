using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataObjectsLayer
{
    public class CarInventory
    {      
        public int CarID {  get; set; }
        [Display(Name = "Owner's Email")]
        public string CustomerEmail { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [Range(1980, 2025)]
        public int Year { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string VIN { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Range(1000, 1000000)]
        public Double Price { get; set; }
        [Required]
        [Range(50, 1000000)]
        public int Mileage { get; set; }
        [Required]
        [Display(Name = "Fuel Type")]
        public string FuelType { get; set; }
        [Required]
        [Display(Name = "Transmission Type")]
        public string TransmissionType { get; set; }
        [Required]
        [Display(Name = "Engine Size")]
        [Range(1.5, 10.0)]
        public Double EngineSize { get; set; }
        [Required]
        public string Description { get; set; }
        public override string ToString()
        {
            return Year + ", " + Model;
        }
    }

    public class CarInventoryVM : CarInventory
    {
        public CarInventoryVM() 
        {
            IsLoggedIn = false;
            IsAdmin = false;
        }
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<SelectListItem> FuelTypeSelectionList { get; set; }
        public IEnumerable<SelectListItem> TransmissionTypeSelectionList { get; set; }
        public IEnumerable<SelectListItem> CustomerEmailSelectionList { get; set; }
    }
}
