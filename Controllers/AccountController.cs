using CabBooking.DAL;
using CabBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CabBooking.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public AccountController()
        {
            ViewBag.SoftwareName = SoftwareConfig.SoftwareName;
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(AccountModel accountModel)
        {
            /*
             * Creating object of DAL class Account
             */
            AccountUtil db = new AccountUtil();

            /*
             * Creating object of Model class UserInfo
             */
            AccountModel result = new AccountModel();

            /*
             * Passing parameters to Login method of Account class
             * @param Email (Which we are fetching from UserInfo Modal)
             * @param Passwd (Which we are fetching from UserInfo Modal)
             * 
             * @return object
             */
            result = db.Login(accountModel.UserName, accountModel.Password);

            if (result.ID != 0)
            {
                Session["UserID"] = result.ID;
                Session["UserName"] = result.Name;
                Session["UserEmail"] = result.Email;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = @"
                    <div class=""alert alert-danger"" role=""alert"">
                      Incorrect Email or Password!
                    </div>
                ";
                return View();
            }
        }

        
    }
}