using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataObjectsLayer
{
    public class Sales
    {
        public int SaleID { get; set; }
        [Required]
        public int CarID { get; set; }
        public int UserID { get; set; }
        [DisplayName("Sale Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime SaleDate { get; set; }
        [Required]
        [DisplayName("Sale Price")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Range(1000.00, 1000000.00)]
        public Double SalePrice { get; set; }
    }
    public class SalesVM : Sales
    {
        public User User { get; set; }
        public CarInventory Car { get; set; }
        public IEnumerable<SelectListItem> CarIDSelectionList { get; set; }
    }
}
