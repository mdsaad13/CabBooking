using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CabBooking.Models
{
    public class AccountModel
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "What's your name?")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Enter a valid name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Kindly enter your email")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Kindly enter your username to login")]
        [Display(Name = "User Name")]
        [RegularExpression("^([a-zA-Z0-9_.-]{5,30})$", ErrorMessage = "Username should be atleast of 5 characters and can only include _ and . special characters")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Kindly enter your password")]
        [Display(Name = "Password")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Enter a valid password")]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!#$%^&_+-=]{6,16}$", ErrorMessage = "Enter a valid password")]
        public string Password { get; set; }


        [Display(Name = "Re-Enter Password")]
        [Compare("Password", ErrorMessage = "Entered Password did not match")]
        public string ConfirmPass { get; set; }

    }
}