using DataObjectsLayer;
using LogicLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShopIdentityManagementMVC.Controllers
{
    [Authorize]
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
        public ActionResult Index()
        {
            ViewBag.UserEmail = User.Identity.GetUserName().ToString();
            List<CarInventoryVM> cars = carInventoryManager.ViewCarInventory();         
            return View(cars);                       
        }

        // GET: CarInventory/Details/5
        public ActionResult Details(int id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            CarInventoryVM carInventoryVM = carInventoryManager.ViewCarByID(id);
            if(carInventoryVM == null)
            {
                return RedirectToAction("Index");
            }
            return View(carInventoryVM);
        }

        public CarInventoryVM SetUpViewModel()
        {
            CarInventoryVM carInventoryVM = new CarInventoryVM();
            var fuelTypeNames = new List<string> { "Gasoline", "Diesel", "E85", "Ethanl", "Hybrid" };
            var transmissionTypeNames = new List<string> { "Automatic", "Manual" };
            var customerEmails = userManager.GetCustomerEmails();
            carInventoryVM.TransmissionTypeSelectionList = transmissionTypeNames.Select(f => new SelectListItem() { Value = f, Text = f });
            carInventoryVM.FuelTypeSelectionList = fuelTypeNames.Select(f => new SelectListItem() { Value = f, Text = f });
            carInventoryVM.CustomerEmailSelectionList = customerEmails.Select(f => new SelectListItem() { Value = f, Text = f });
            return carInventoryVM;
        }

        public CarInventoryVM AddSelectionListsViewModel(CarInventoryVM carInventoryVM)
        {
            var fuelTypeNames = new List<string> { "Gasoline", "Diesel", "E85", "Ethanl", "Hybrid" };
            var transmissionTypeNames = new List<string> { "Automatic", "Manual" };
            var customerEmails = userManager.GetCustomerEmails();
            carInventoryVM.TransmissionTypeSelectionList = transmissionTypeNames.Select(f => new SelectListItem() { Value = f, Text = f });
            carInventoryVM.FuelTypeSelectionList = fuelTypeNames.Select(f => new SelectListItem() { Value = f, Text = f });
            carInventoryVM.CustomerEmailSelectionList = customerEmails.Select(f => new SelectListItem() { Value = f, Text = f });
            return carInventoryVM;
        }

        // GET: CarInventory/Create
        [Authorize(Roles = "MANAGER, ADMIN, EMPLOYEE")]
        public ActionResult Create()
        {
            CarInventoryVM carInventoryVM = SetUpViewModel();
            return View(carInventoryVM);
        }

        // POST: CarInventory/Create
        [HttpPost]
        [Authorize(Roles = "MANAGER, ADMIN, EMPLOYEE")]
        public ActionResult Create(CarInventoryVM carInventoryVM)
        {
            if (!ModelState.IsValid)
            {
                carInventoryVM = AddSelectionListsViewModel(carInventoryVM);
                return View(carInventoryVM);
            }
            try
            {
                carInventoryManager.InsertNewCar(carInventoryVM.Model, carInventoryVM.Year,
                    carInventoryVM.Color, carInventoryVM.VIN, carInventoryVM.Price,
                    carInventoryVM.Mileage, carInventoryVM.FuelType, carInventoryVM.TransmissionType,
                    carInventoryVM.EngineSize, carInventoryVM.Description);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CarInventory/Edit/5
        [Authorize(Roles = "MANAGER, ADMIN, EMPLOYEE")]
        public ActionResult Edit(Int32 id)
        {
            CarInventoryVM carInventoryVM = carInventoryManager.ViewCarByID(id);
            carInventoryVM = AddSelectionListsViewModel(carInventoryVM);
            return View(carInventoryVM);
        }

        // POST: CarInventory/Edit/5
        [HttpPost]
        [Authorize(Roles = "MANAGER, ADMIN, EMPLOYEE")]
        public ActionResult Edit(CarInventoryVM carInventoryVM)
        {

            if (!ModelState.IsValid)
            {
                carInventoryVM = AddSelectionListsViewModel(carInventoryVM);
                return View(carInventoryVM);
            }
            try
            {
                if (carInventoryVM.CustomerEmail == null)
                {
                    carInventoryVM.CustomerEmail = "";
                }
                carInventoryManager.UpdateCar(carInventoryVM.CarID, carInventoryVM.CustomerEmail, carInventoryVM.Model, carInventoryVM.Year,
                    carInventoryVM.Color, carInventoryVM.VIN, carInventoryVM.Price,
                    carInventoryVM.Mileage, carInventoryVM.FuelType, carInventoryVM.TransmissionType,
                    carInventoryVM.EngineSize, carInventoryVM.Description);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: CarInventory/Delete/5
        [Authorize(Roles = "MANAGER, ADMIN, EMPLOYEE")]
        public ActionResult Delete(int id)
        {
            CarInventoryVM carInventoryVM = carInventoryManager.ViewCarByID(id);
            return View(carInventoryVM);
        }

        // POST: CarInventory/Delete/5
        [HttpPost]
        [Authorize(Roles = "MANAGER, ADMIN, EMPLOYEE")]
        public ActionResult Delete(int id, CarInventoryVM carInventoryVM)
        {
            try
            {
                carInventoryManager.DeleteCarByID(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
