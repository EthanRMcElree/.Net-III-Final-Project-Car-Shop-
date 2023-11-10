using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class ServiceType
    {
        /*
         ServiceTypeID	int	
        Description	nvarchar	250
         */
        public int ServiceTypeID { get; set; }
        public string ServiceDescription { get; set; }
    }
    public class ServiceTypeVM : ServiceType
    {
    }
}
