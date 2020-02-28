using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CabBooking.Models
{
    public class TripModel
    {
        public int TripID { get; set; }


        [Display(Name = "Client's Name")]
        [Required(ErrorMessage = "Enter Client's Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Enter a valid name")]
        public string ClientName { get; set; }


        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Enter Mobile Number")]
        [RegularExpression("^([6-9]{1})([0-9]{9})$", ErrorMessage = "Enter a valid mobile number")]
        public string Mobile { get; set; }


        [Display(Name = "Email ID")]
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }


        [Display(Name = "Pick up location")]
        [Required(ErrorMessage = "Enter Pick up location")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Enter a Pick up location")]
        public string PickupLoc { get; set; }


        [Display(Name = "Drop location")]
        [Required(ErrorMessage = "Enter Drop location")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Enter a valid Drop location")]
        public string DropLoc { get; set; }


        [Display(Name = "Vehicle details")]
        [Required(ErrorMessage = "Select a vehicle")]
        public int VehicleID { get; set; }


        [Display(Name = "Date of Booking")]
        public DateTime DateOfBooking { get; set; }


        [Display(Name = "Date of Invoice")]
        public DateTime DateOfInvoice { get; set; }


        [Display(Name = "Pickup date")]
        [Required(ErrorMessage = "Enter Date of pickup")]
        public DateTime DateIn { get; set; }


        [Display(Name = "Drop date")]
        [Required(ErrorMessage = "Enter Date of drop")]
        public DateTime DateOut { get; set; }


        [Display(Name = "Pickup time")]
        [Required(ErrorMessage = "Enter Pickup Time")]
        public TimeSpan PickupTime { get; set; }


        [Display(Name = "Drop time")]
        [Required(ErrorMessage = "Enter Drop Time")]
        public TimeSpan DropTime { get; set; }


        public int Status { get; set; }


        public float GrandTotal { get; set; }


        [Display(Name = "Extra KM")]
        [Required(ErrorMessage = "Enter Drop Time")]
        [RegularExpression(@"^(?:[1-9]\d*|0)?(?:\.\d+)?$", ErrorMessage = "Enter a valid number")]
        public float ExtraKm { get; set; }


        [Display(Name = "Extra hours")]
        [Required(ErrorMessage = "Enter Drop Time")]
        [RegularExpression(@"^(?:[1-9]\d*|0)?(?:\.\d+)?$", ErrorMessage = "Enter a valid number")]
        public float ExtraHr { get; set; }


        [Display(Name = "Package Charges")]
        [Required(ErrorMessage = "Enter Drop Time")]
        [RegularExpression(@"^(?:[1-9]\d*|0)?(?:\.\d+)?$", ErrorMessage = "Enter a valid number")]
        public float PkgCharges { get; set; }


        [Display(Name = "Package name")]
        [Required(ErrorMessage = "Enter Drop Time")]
        public string PkgDetails { get; set; }


        [Display(Name = "Pickup KM")]
        [Required(ErrorMessage = "Enter Drop Time")]
        [RegularExpression(@"^(\d)*\d*$", ErrorMessage = "Enter a valid number")]
        public long PickupKm { get; set; }


        [Display(Name = "Drop KM")]
        [Required(ErrorMessage = "Enter Drop Time")]
        [RegularExpression(@"^(\d)*\d*$", ErrorMessage = "Enter a valid number")]
        public long DropKm { get; set; }


        [Display(Name = "Toll Charges")]
        [RegularExpression(@"^(?:[1-9]\d*|0)?(?:\.\d+)?$", ErrorMessage = "Enter a valid number")]
        public float Toll { get; set; }


        [Display(Name = "Night Charges")]
        public float NightCharges { get; set; }


        [Display(Name = "Driver Bata")]
        [RegularExpression(@"^(?:[1-9]\d*|0)?(?:\.\d+)?$", ErrorMessage = "Enter a valid number")]
        public float Bata { get; set; }


        [Display(Name = "Intrastate Tax")]
        [RegularExpression(@"^(?:[1-9]\d*|0)?(?:\.\d+)?$", ErrorMessage = "Enter a valid number")]
        public float IntraTax { get; set; }


        [Display(Name = "CGST")]
        [RegularExpression(@"^(?:[1-9]\d*|0)?(?:\.\d+)?$", ErrorMessage = "Enter a valid number")]
        public float Cgst { get; set; }


        [Display(Name = "SGST")]
        [RegularExpression(@"^(?:[1-9]\d*|0)?(?:\.\d+)?$", ErrorMessage = "Enter a valid number")]
        public float Sgst { get; set; }

    }
}