using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class Manufacturer
    {
        /*
         ManufacturerID	int	
        ManufacturerName	nvarchar	50
        CountryOrigin	nvarchar	50
         */
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
        public string CountryOrigin { get; set;}
    }
    public class ManufacturerVM : Manufacturer
    {
    }
}
