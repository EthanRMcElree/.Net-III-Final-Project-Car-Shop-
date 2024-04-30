using DataObjectsLayer;
using LogicLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace CarShopIdentityManagementMVC.Controllers
{
    [Authorize(Roles = "MANAGER, ADMIN, CUSTOMER")]
    public class ServiceAppointmentController : Controller
    {
        private IServiceAppointmentManager _serviceAppointmentManager;
        private IUserManager _userManager;
        private ICarInventoryManager _carInventoryManager;
        public ServiceAppointmentController()
        {
            _serviceAppointmentManager = new ServiceAppointmentManager();
            _userManager = new UserManager();
            _carInventoryManager = new CarInventoryManager();
        }

        public ServiceAppointmentVM SetUpViewModel()
        {
            ServiceAppointmentVM serviceAppointmentVM = new ServiceAppointmentVM();
            var serviceTypeNames = new List<string> { "Repairs", "Tires", "Oil Change", "Brakes" };
            serviceAppointmentVM.ServiceTypeSelectionList = serviceTypeNames.Select(f => new SelectListItem() { Value = f, Text = f });
            return serviceAppointmentVM;
        }

        public ServiceAppointmentVM AddSelectionListsViewModel(ServiceAppointmentVM serviceAppointmentVM)
        {
            var serviceTypeNames = new List<string> { "Repairs", "Tires", "Oil Change", "Brakes" };
            serviceAppointmentVM.ServiceTypeSelectionList = serviceTypeNames.Select(f => new SelectListItem() { Value = f, Text = f });
            return serviceAppointmentVM;
        }

        // GET: ServiceAppointment
        public ActionResult Index()
        {
            string UserEmail = User.Identity.GetUserName().ToString();
            List<ServiceAppointmentVM> serviceAppointmentVMs = null;
            if (User.IsInRole("CUSTOMER")) 
            {
                serviceAppointmentVMs = _serviceAppointmentManager.RetrieveServiceAppointments();
                foreach (ServiceAppointmentVM serviceAppointmentVM in serviceAppointmentVMs)
                {
                    serviceAppointmentVM.CarModel = _carInventoryManager.ViewCarByID(serviceAppointmentVM.CarID).Model;
                    serviceAppointmentVM.ServiceTypeName = _serviceAppointmentManager.RetrieveServiceTypeByID(serviceAppointmentVM.ServiceTypeID).ServiceDescription;
                }
                ViewBag.UserEmail = User.Identity.GetUserName().ToString();
                return View(serviceAppointmentVMs.Where(item => item.CustomerEmail == UserEmail));
            }
            serviceAppointmentVMs = _serviceAppointmentManager.RetrieveServiceAppointments();
            foreach (ServiceAppointmentVM serviceAppointmentVM in serviceAppointmentVMs)
            {
                serviceAppointmentVM.CarModel = _carInventoryManager.ViewCarByID(serviceAppointmentVM.CarID).Model;
                serviceAppointmentVM.ServiceTypeName = _serviceAppointmentManager.RetrieveServiceTypeByID(serviceAppointmentVM.ServiceTypeID).ServiceDescription;
            }
            return View(serviceAppointmentVMs);
        }

        // GET: ServiceAppointment/Details/5
        public ActionResult Details(int id)
        {
            ServiceAppointmentVM serviceAppointmentVM = _serviceAppointmentManager.RetrieveServiceAppointmentByAppointmentID(id);            
            return View(serviceAppointmentVM);
        }

        // GET: ServiceAppointment/Create
        public ActionResult Create(int id)
        {
            CarInventoryVM carInventoryVM = _carInventoryManager.ViewCarByID(id);
            ServiceAppointmentVM serviceAppointmentVM = new ServiceAppointmentVM();
            serviceAppointmentVM.CustomerEmail = carInventoryVM.CustomerEmail;
            serviceAppointmentVM.CarID = carInventoryVM.CarID;
            serviceAppointmentVM = AddSelectionListsViewModel(serviceAppointmentVM);
            return View(serviceAppointmentVM);
        }

        // POST: ServiceAppointment/Create
        [HttpPost]
        public ActionResult Create(ServiceAppointmentVM serviceAppointmentVM)
        {
            if (!ModelState.IsValid)
            {
                serviceAppointmentVM = AddSelectionListsViewModel(serviceAppointmentVM);
                return View(serviceAppointmentVM);
            }
            try
            {
                // TODO: Add insert logic here
                serviceAppointmentVM.ServiceTypeID = _serviceAppointmentManager.RetrieveServiceTypeByDescription(serviceAppointmentVM.ServiceTypeName).ServiceTypeID;
                _serviceAppointmentManager.CreateNewServiceAppointment(serviceAppointmentVM.CarID, serviceAppointmentVM.CustomerEmail, serviceAppointmentVM.ServiceTypeID, serviceAppointmentVM.CustomerComments, serviceAppointmentVM.ScheduledDate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceAppointment/Edit/5
        public ActionResult Edit(int id)
        {
            ServiceAppointmentVM serviceAppointmentVM = _serviceAppointmentManager.RetrieveServiceAppointmentByAppointmentID(id);
            serviceAppointmentVM = AddSelectionListsViewModel(serviceAppointmentVM);
            return View(serviceAppointmentVM);
        }

        // POST: ServiceAppointment/Edit/5
        [HttpPost]
        public ActionResult Edit(ServiceAppointmentVM serviceAppointmentVM)
        {
            if (!ModelState.IsValid)
            {                
                serviceAppointmentVM = AddSelectionListsViewModel(serviceAppointmentVM);
                return View(serviceAppointmentVM);
            }
            try
            {
                serviceAppointmentVM.ServiceTypeID = _serviceAppointmentManager.RetrieveServiceTypeByDescription(serviceAppointmentVM.ServiceTypeName).ServiceTypeID;
                _serviceAppointmentManager.UpdateServiceAppointment(serviceAppointmentVM.AppointmentID, serviceAppointmentVM.CarID,
                    serviceAppointmentVM.CustomerEmail, serviceAppointmentVM.ServiceTypeID, serviceAppointmentVM.CustomerComments,
                    serviceAppointmentVM.ScheduledDate);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceAppointment/Delete/5
        public ActionResult Delete(int id)
        {
            ServiceAppointmentVM serviceAppointmentVM = _serviceAppointmentManager.RetrieveServiceAppointmentByAppointmentID(id);
            return View(serviceAppointmentVM);
        }

        // POST: ServiceAppointment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ServiceAppointmentVM serviceAppointmentVM)
        {
            try
            {
                _serviceAppointmentManager.DeleteServiceAppointmentByAppointmentID(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
