using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class Warranty
    {
        /*
         WarrantyID	int	
        CarID	int	
        WarrantyTypeID	int	
        WarrantyStartDate	datetime	
        WarrantyEndDate	datetime	
         */
        public int WarrantyID { get; set; }
        public int CarID { get; set; }
        public int WarrantyTypeID { get; set; }
        public DateTime WarrantyStartDate { get; set; }
        public DateTime WarrantyEndDate { get; set; }
    }
    public class WarrantyVM : Warranty
    {
        public CarInventory Car { get; set; }
        public WarrantyType WarrantyType { get; set; }
    }
}
