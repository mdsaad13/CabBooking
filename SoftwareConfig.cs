using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CabBooking
{

    public class SoftwareConfig
    {
        public static string SoftwareName = "Cab Booking";
        public static string DBName = "CabBooking";

        public static string Name = "Farjam Zafar";
        public static string Email = "farjamzafar@gmail.com";
        public static string Mobile = "+91 78921 03400";
        public static string ImgUrl = "/Images/farjam.jpg";

        //public static string Name = "Chanchal Jain";
        //public static string Email = "chanchal@gmail.com";
        //public static string Mobile = "+91 70141 12472";
        //public static string ImgUrl = "/Images/chanchal.jpg";

        public static string Address = "SURANA COLLEGE <br />16, S End Rd, Basavanagudi <br />BANGALORE 560004, KARNATAKA";

        public static List<SocialLinks> SocialMediaLinks = new List<SocialLinks>() {
                new SocialLinks(){ Name="twitter", URL="http://twitter.com"},
                new SocialLinks(){ Name="facebook", URL="http://facebook.com"},
                new SocialLinks(){ Name="instagram", URL="http://instagram.com"},
                new SocialLinks(){ Name="linkedin", URL="http://linkedin.com"}
        };
    }

    public class SocialLinks
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}