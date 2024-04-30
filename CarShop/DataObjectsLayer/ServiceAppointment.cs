using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataObjectsLayer
{
    public class ServiceAppointment
    {
        public int AppointmentID { get; set; }
        [Required]
        [Display(Name = "Car ID")]
        public int CarID { get; set; }
        [Required]
        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; }
        [Display(Name = "Service Type")]
        public int ServiceTypeID { get; set; }
        [Display(Name = "Comments (optional)")]
        public string CustomerComments { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Scheduled Date")]
        public DateTime ScheduledDate { get; set; }
    }
    public class ServiceAppointmentVM : ServiceAppointment
    {
        [Display(Name = "Car Model")]
        public string CarModel { get; set; }
        [Required]
        [Display(Name = "Service Name")]
        public string ServiceTypeName { get; set; }
        public IEnumerable<SelectListItem> ServiceTypeSelectionList { get; set; }
    }
}
