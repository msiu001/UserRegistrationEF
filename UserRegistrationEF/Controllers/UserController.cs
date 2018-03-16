using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRegistrationEF.Models;

namespace UserRegistrationEF.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult AddOrEdit( int id=0 )
        {
            User userModel = new User();
            return View(userModel);
        }


        [HttpPost]
        public ActionResult AddOrEdit(User userModel)
        {
            using (DBModels dbModel = new DBModels())
            {
                if(dbModel.Users.Any(x => x.Username == userModel.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exits";
                    return View("AddOrEdit", userModel);
                }


                dbModel.Users.Add(userModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful.";
            return View("AddOrEdit", new User());
        }
    }
}