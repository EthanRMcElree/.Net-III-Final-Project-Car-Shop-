using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class WarrantyType
    {
        /*
         WarrantyTypeID	int	
        Description	nvarchar	250
         */
        public int WarrantyTypeID { get; set; }
        public string Description { get; set; }
    }
    public class WarrantyTypeVM : WarrantyType
    {
    }
}
