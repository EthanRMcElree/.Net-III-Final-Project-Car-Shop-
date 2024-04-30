using DataObjectsLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShopWebApp.Controllers
{
    public class CarInventoryController : Controller
    {
        private ICarInventoryManager carInventoryManager;
        private IUserManager userManager;
        public CarInventoryController()
        {
            carInventoryManager = new CarInventoryManager();
            userManager = new UserManager();
        }

        // GET: CarInventory
        public ActionResult Index(string userEmail = "")
        {
            UserVM user = null;
            if (userEmail != "")
            {
                user = userManager.GetUserVMByEmail(userEmail);
                
            } 
            else if (TempData["userEmail"] != null)
            {
                user = userManager.GetUserVMByEmail((string)TempData["userEmail"]);
            }
            
            if (user != null)
            {
                List<CarInventoryVM> cars = carInventoryManager.ViewCarInventory();
                ViewData["user"] = user;
                return View(cars);
            }
            return RedirectToAction("Index", "Home", new {area=""});
        }

        public ActionResult Create()
        {
            CarInventoryVM carInventoryVM = new CarInventoryVM();
            //carInventoryVM.IsLoggedIn = true;
            //UserVM userVM = userManager.GetUserVMByEmail(Email);
            //userVM.car = carInventoryVM;
            return View(carInventoryVM);
        }

        [HttpPost]
        public ActionResult Create(CarInventoryVM carInventoryVM)
        {
            if (carInventoryVM != null) 
            {
                Console.WriteLine("There was an error creating a car.");
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult Delete(int CarID, string Email) 
        {
            carInventoryManager.DeleteCarByID(CarID);
            UserVM userVM = userManager.GetUserVMByEmail(Email);
            TempData["user"] = userVM;
            return this.RedirectToAction("Index", "CarInventory", new {area=""});
        }
    }
}