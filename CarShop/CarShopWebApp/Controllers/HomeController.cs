using DataObjectsLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IUserManager userManager;
        private ICarInventoryManager carInventoryManager;
        private User currentUser;
        public HomeController()
        {
            userManager = new UserManager();
            carInventoryManager = new CarInventoryManager();
            currentUser = new User();
        }

        public ActionResult Index(string userEmail = "")
        {
            if (TempData["userEmail"] != null)
            {
                string loggedInUserEmail = TempData["userEmail"] as string;
                UserVM user = userManager.GetUserVMByEmail(loggedInUserEmail);
                ViewData["user"] = user;
                return View();
            }
            else if (userEmail != "")
            {
                UserVM user = userManager.GetUserVMByEmail(userEmail);
                ViewData["user"] = user;
                return View();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            this.currentUser = userManager.AuthenticateUser(Email, Password);
            if(this.currentUser != null)
            {
                if (this.currentUser.Role.Equals("manager") || this.currentUser.Role.Equals("admin"))
                {
                    this.currentUser.IsAdmin = true;
                }
                this.currentUser.IsLoggedIn = true;
                UserVM currentUserVM = new UserVM();
                currentUserVM.Email = this.currentUser.Email;
                currentUserVM.Password = this.currentUser.Password;
                currentUserVM.FirstName = this.currentUser.FirstName;
                currentUserVM.LastName = this.currentUser.LastName;
                currentUserVM.PhoneNumber = this.currentUser.PhoneNumber;
                currentUserVM.IsLoggedIn = true;
                currentUserVM.IsAdmin = this.currentUser.IsAdmin;
                currentUserVM.Role = this.currentUser.Role;
                userManager.UpdateUser(currentUserVM);
                TempData["userEmail"] = currentUserVM.Email;
                return this.RedirectToAction("Index", "CarInventory", new {area = ""});
            }    
            else
            {
                TempData["error"] = "That user and password combination is not valid.";
                return this.RedirectToAction("Index");
            }
        }

        public ActionResult Logout() 
        {
            this.currentUser = new User();
            return RedirectToAction("Index");
        }
    }
}