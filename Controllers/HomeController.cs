using CabBooking.DAL;
using CabBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CabBooking.Controllers
{
    [SessionAuthorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.SoftwareName = "Cab Booking";
        }

        public ActionResult Index()
        {
            GeneralUtil generalUtil = new GeneralUtil();
            DbUtil dbUtil = new DbUtil();
            string Date = DateTime.Now.ToString("yyyy-MM-dd");

            ViewBag.CountPendingTrips = generalUtil.CountByArgs("trip", "status = 1");
            ViewBag.CountCompletedTrips = generalUtil.CountByArgs("trip", "status = 2");
            ViewBag.SumTotalIncome = generalUtil.SumByArgs("trip", "grandtotal", "status = 2");
            ViewBag.CountVehicles = generalUtil.Count("vehicle");

            TripBundle tripBundle = new TripBundle
            {
                BundleA = dbUtil.GetTripsByArgs("status = 1 AND datein < '" + Date+"'"),
                BundleB = dbUtil.GetTripsByArgs("status = 1 AND datein > '" + Date + "'")
            };

            return View(tripBundle);
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
        
        [HttpGet]
        public ActionResult AddTrip(int id = 0) // Making `id` optional parameter
        {
            DbUtil dbUtil = new DbUtil();
            GeneralUtil generalUtil = new GeneralUtil();
            ViewBag.Vehicals = new SelectList(dbUtil.GetVehicles(), "vehicleID", "regno");

            if (id != 0)
            {
                if (generalUtil.Validate("enquirys", "id", Convert.ToString(id)))
                {
                    TripModel tripModel = dbUtil.GetEnquiryByID(id);
                    tripModel.TripID = id;
                    tripModel.Status = 5;
                    return View(tripModel);
                }
                else
                {
                    ViewBag.Error = "Enquiry not found";
                    return View("Error");
                }
            }
            else
            {
                return View();
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddTrip(TripModel tripModel)
        {
            bool DeleteEnqAction = false;
            int EnqID = 0;
            
            if (tripModel.Status == 5)
            {
                EnqID = tripModel.TripID;
                DeleteEnqAction = true;
            }

            tripModel.TripID = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            tripModel.Status = 1;
            tripModel.DateOfBooking = DateTime.Now;

            DbUtil db = new DbUtil();
            if (db.InsertTrip(tripModel))
            {
                if (DeleteEnqAction)
                {
                    DbUtil dbUtil = new DbUtil();
                    dbUtil.DeleteEnquiry(EnqID);
                }
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
            ViewBag.Vehicals = new SelectList(dbUtil.GetVehicles(), "vehicleID", "regno");
            return View(tripModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateTrip(TripModel tripModel)
        {
            DbUtil dbUtil = new DbUtil();

            tripModel.DateOfInvoice = DateTime.Now;
            tripModel.Status = 2;

            if (dbUtil.Updatetrip(tripModel))
            {
                RedirectToAction("CompletedTrips");
            }
            else
            {
                Session["Notification"] = 1;
            }
            return View();
        }

        [HttpPost]
        public JsonResult FetchVehicalType(FormCollection formCollection)
        {
            DbUtil dbUtil = new DbUtil();
            try
            {
                int VehicleID = Convert.ToInt32(formCollection["id"]);
                VehicleModel vehicleModel = dbUtil.GetVehicleByID(VehicleID);
                return Json(new { rpk = vehicleModel.Km, rph = vehicleModel.Extrahr });
            }
            catch
            {
                return Json(new { rpk = 0, rph = 0 });
            }
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
        
        [HttpGet]
        public ActionResult ViewTrip(int id)
        {
            DbUtil dbUtil = new DbUtil();
            TripModel tripModel = dbUtil.GetTripByID(id);
            ViewBag.VehicleDetails = dbUtil.GetVehicleByID(tripModel.VehicleID);
            return View(tripModel);
        }

        // View Trip methods ENDS here

        public ActionResult Enquiries()
        {
            DbUtil dbUtil = new DbUtil();
            List<TripModel> details = dbUtil.GetEnquiryList();
            return View(details);
        }
        
        public ActionResult Reports()
        {
            DbUtil dbUtil = new DbUtil();
            string Date = DateTime.Now.ToString("yyyy-MM");
            ViewBag.SortedName = DateTime.Now.ToString("MMMM");
            List<TripModel> Trips = dbUtil.GetTripsByArgs("status = 2 AND dateofinvoice LIKE '"+ Date + "%'");
            return View(Trips);
        }
        
        [HttpPost]
        public ActionResult Reports(FormCollection formCollection)
        {
            //+-+
            string FormData = Convert.ToString(formCollection["sort"]);

            DateTime FromDate = Convert.ToDateTime(FormData.Substring(0, 10));
            DateTime ToDate = Convert.ToDateTime(FormData.Substring(13));

            string StrFromDate = FromDate.ToString("yyyy-MM-dd");
            string StrToDate = ToDate.ToString("yyyy-MM-dd");

            ViewBag.SortedName = FromDate.ToString("dd-MMMM-yyyy")+ " TO "+ ToDate.ToString("dd-MMMM-yyy");

            DbUtil dbUtil = new DbUtil();

            List<TripModel> Trips = dbUtil.GetTripsByArgs("status = 2 AND dateofinvoice >= '" + StrFromDate + "' AND dateofinvoice <= '" + StrToDate + "'");
            return View(Trips);
        }
        
        public ActionResult Error()
        {
            return View();
        }

    }
}