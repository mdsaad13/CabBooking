using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CabBooking.Models
{
    public class VehicleModel
    {
        public int VehicleID { get; set; }

        [Display(Name = "Vehicle Make")]
        [Required(ErrorMessage = "Enter Vehicle Make")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Enter a valid name")]
        public string Type { get; set; }

        [Display(Name = "Driver Name")]
        [Required(ErrorMessage = "Enter Driver's Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Enter a valid name")]
        public string Drivername { get; set; }

        [Display(Name = "Mobile No.")]
        [Required(ErrorMessage = "Enter Mobile No.")]
        [RegularExpression("^([6-9]{1})([0-9]{9})$", ErrorMessage = "Enter a valid mobile number")]
        public string Mobile { get; set; }


        [Display(Name = "Vehicle Reg No.")]
        [Required(ErrorMessage = "Enter Vehicle Reg No.")]
        [RegularExpression(@"^([A-Z]{2}(-)(\d{2})(-)([A-Z]{1,2})(-)(\d{4}))$", ErrorMessage = "Enter a valid mobile number")]
        public string Regno { get; set; }


        [Display(Name = "Rate per kilometer")]
        [Required(ErrorMessage = "Enter Rate per kilometer")]
        public long Km { get; set; }


        [Display(Name = "Rate per extra hour")]
        [Required(ErrorMessage = "Enter Rate per extra hour")]
        public long Extrahr { get; set; }
    }
}