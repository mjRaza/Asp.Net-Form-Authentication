using FormAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace FormAuthentication.Controllers
{[AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }


        // Post: Account/Login

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {


            using (var context = new OfficeEntities())

            {
                bool isValid = context.Users.Any(x => x.UserName == model.UserName && x.password == model.Password);

                    if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }



                ModelState.AddModelError("", "Invalid userName or password");
                return View();

            }
            



        }



        // GET: Account/SignUp

        public ActionResult Signup()
        {


            return View();
        }


        // Post: Account/SignUp

        [HttpPost]
        public ActionResult Signup(User user)
        {
            using (var context = new OfficeEntities())

            {
                context.Users.Add(user);
                context.SaveChanges();

            }
         
            return RedirectToAction("Login");
        }




        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}