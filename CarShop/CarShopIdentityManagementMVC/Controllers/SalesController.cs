using DataObjectsLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShopIdentityManagementMVC.Controllers
{
    public class SalesController : Controller
    {
        SalesManager _salesManager;
        CarInventoryManager _carInventoryManager;
        public SalesController()
        {
            _salesManager = new SalesManager();
            _carInventoryManager = new CarInventoryManager();
        }

        // GET: Sales
        public ActionResult Index()
        {
            List<SalesVM> salesList = _salesManager.ViewSales();
            return View(salesList);
        }

        public SalesVM AddSelectionListViewModels(SalesVM salesVM)
        {
            var cars = _carInventoryManager.ViewCarInventory();
            List<string> carIDs = new List<string>();
            foreach (var car in cars)
            {
                carIDs.Add(car.CarID.ToString());
            }
            salesVM.CarIDSelectionList = carIDs.Select(f => new SelectListItem() { Value = f, Text = f });
            return salesVM;
        }

        // GET: Sales/Create
        [Authorize(Roles = "MANAGER, ADMIN")]
        public ActionResult Create()
        {
            SalesVM salesVM = new SalesVM();
            salesVM = AddSelectionListViewModels(salesVM);
            return View(salesVM);
        }

        // POST: Sales/Create
        [HttpPost]
        [Authorize(Roles = "MANAGER, ADMIN")]
        public ActionResult Create(SalesVM salesVM)
        {
            if (!ModelState.IsValid)
            {
                salesVM = AddSelectionListViewModels(salesVM);
                return View(salesVM);
            }
            CarInventory carInventory = _carInventoryManager.ViewCarByID(salesVM.CarID);    
            if (salesVM.SalePrice >= carInventory.Price || _salesManager.ViewSales().Where(item => item.CarID == salesVM.CarID).Count() > 0)
            {
                ViewBag.Error = "Sorry.  There can only be one sale per car and the price must be lower than the car's current price.";
                salesVM = AddSelectionListViewModels(salesVM);
                return View(salesVM);
            }
            try
            {
                _salesManager.CreateSale(1, salesVM.CarID, DateTime.Now, salesVM.SalePrice);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sales/Delete/5
        [Authorize(Roles = "MANAGER, ADMIN")]
        public ActionResult Delete(int id)
        {
            SalesVM salesVM = _salesManager.ViewSaleByID(id);
            return View(salesVM);
        }

        // POST: Sales/Delete/5
        [HttpPost]
        [Authorize(Roles = "MANAGER, ADMIN")]
        public ActionResult Delete(int id, SalesVM salesVM)
        {
            try
            {
                _salesManager.DeleteSaleByID(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
