using CabBooking.DAL;
using CabBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CabBooking.Controllers
{
    //[SessionAuthorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.SoftwareName = "Cab Booking";
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Vehicles()
        {
            DbUtil dbUtil = new DbUtil();
            List<VehicleModel> details = new List<VehicleModel>();
            details = dbUtil.GetVehicles();
            return View(details);
        }
        
        // Vehicle methods starts here
        public ActionResult AddVehicle()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddVehicle(VehicleModel vehicleModel)
        {
            DbUtil dbUtil = new DbUtil();
            int row = dbUtil.AddVehicle(vehicleModel);
            if (row > 0)
            {
                Session["Notification"] = 1;
            }
            else
            {
                Session["Notification"] = 2;
            }
            return RedirectToAction("Vehicles");
        }
        
        [HttpGet]
        public ActionResult EditVehicle(int id)
        {
            DbUtil dbUtil = new DbUtil();
            VehicleModel vehicleModel = dbUtil.GetVehicleByID(id);
            return View(vehicleModel);
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditVehicle(VehicleModel vehicleModel)
        {
            DbUtil dbUtil = new DbUtil();
            int row = dbUtil.UpdateVehicle(vehicleModel);
            if (row > 0)
            {
                Session["Notification"] = 3;
            }
            else
            {
                Session["Notification"] = 4;
            }
            return RedirectToAction("Vehicles");
        }
        // Vehicle methods ENDS here

        // Trip methods STARTS here
        public ActionResult AddTrip()
        {
            DbUtil dbUtil = new DbUtil();

            ViewBag.Vehicals = new SelectList(dbUtil.GetVehicles(), "vehicleID", "regno");

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddTrip(TripModel tripModel)
        {
            tripModel.TripID = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            tripModel.Status = 1;
            tripModel.DateOfBooking = DateTime.Now;

            DbUtil db = new DbUtil();
            if (db.InsertTrip(tripModel))
            {
                RedirectToAction("PendingTrips");
            }
            else
            {
                Session["Notification"] = 1;
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult UpdateTrip(int id)
        {
            DbUtil dbUtil = new DbUtil();
            TripModel tripModel = dbUtil.GetTripByID(id);
            return View(tripModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateTrip(TripModel tripModel)
        {
            return View();
        }
        // Trip methods ENDS here

        // View Trip methods STARTS here
        public ActionResult PendingTrips()
        {
            DbUtil dbUtil = new DbUtil();
            List<TripModel> details = dbUtil.GetPendingTrips();
            return View(details);
        }
        
        public ActionResult CompletedTrips()
        {
            DbUtil dbUtil = new DbUtil();
            List<TripModel> details = dbUtil.GetCompletedTrips();
            return View(details);
        }
        // View Trip methods ENDS here
    }
}