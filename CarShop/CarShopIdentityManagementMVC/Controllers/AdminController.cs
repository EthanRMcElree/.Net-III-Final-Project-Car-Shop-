using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarShopIdentityManagementMVC;
using CarShopIdentityManagementMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MVCPresentation.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private ApplicationUserManager userManager;        


        // GET: Admin
        public ActionResult Index()
        {
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(userManager.Users.OrderBy(n => n.Email).ToList());            
        }


        // GET: Admin/Details/5
        public ActionResult Details(string id, string errorMessage = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            if (errorMessage.Length > 0)
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            var allRoles = new string[] { "ADMIN", "CUSTOMER", "EMPLOYEE", "MANAGER", "USER" };
            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View(applicationUser);
        }

        public ActionResult RemoveRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            if (role == "ADMIN")
            {
                var adminUsers = userManager.Users.ToList().Where(u => userManager.IsInRole(u.Id, "ADMIN")).ToList().Count();
                if (adminUsers < 2)
                {
                    return RedirectToAction("Details", "Admin", new { id = user.Id, errorMessage = "Cannot remove last administrator." });
                }
            }

            userManager.RemoveFromRole(id, role);

            var allRoles = new string[] {"ADMIN", "CUSTOMER", "EMPLOYEE", "MANAGER", "USER"};
            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View("Details", user);
        }


        public ActionResult AddRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            userManager.AddToRole(id, role);

            var allRoles = new string[] { "ADMIN", "CUSTOMER", "EMPLOYEE", "MANAGER", "USER" };
            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View("Details", user);
        }       
    }
}
