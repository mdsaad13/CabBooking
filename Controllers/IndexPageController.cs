using CabBooking.DAL;
using CabBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CabBooking.Controllers
{
    public class IndexPageController : Controller
    {
        // GET: IndexPage
        public ActionResult Index()
        {
            ViewBag.SoftwareName = SoftwareConfig.SoftwareName;
            ViewBag.Name = SoftwareConfig.Name;
            ViewBag.Email = SoftwareConfig.Email;
            ViewBag.Mobile = SoftwareConfig.Mobile;
            ViewBag.ImgUrl = SoftwareConfig.ImgUrl;
            ViewBag.Address = SoftwareConfig.Address;

            foreach (var item in SoftwareConfig.SocialMediaLinks)
            {
                ViewData[item.Name] = item.URL;
            }

            return View();
        }
        
        [HttpPost]
        public JsonResult AjaxAddEnquiry(TripModel tripModel)
        {
            DbUtil dbUtil = new DbUtil();
            tripModel.DateOfBooking = DateTime.Now;
            bool status = dbUtil.InsertEnquiry(tripModel);
            return Json(new { status });
        }
    }
}