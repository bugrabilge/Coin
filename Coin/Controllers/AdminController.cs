using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Coin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IObjectRecycleService _ojService;

        public AdminController(IObjectRecycleService ojService)
        {
            _ojService = ojService;
        }

        public IActionResult AdminPage()
        {
            var items = _ojService.GetAllList();
            return View(items);
        }

        public IActionResult ChangeRecycleObjectCarbonValue(int id)
        {
            var objectToChange = _ojService.GetByID(id);
            return View(objectToChange);
        }

        [HttpPost]
        public IActionResult ChangeRecycleObjectCarbonValue(ObjectRecycle objectRecycle)
        {
            var objectToChange = _ojService.GetByID(objectRecycle.ObjectID);
            objectToChange.ObjectCarbonPoint = objectRecycle.ObjectCarbonPoint;
            _ojService.ObjectUpdate(objectToChange);
            return RedirectToAction("AdminPage", "Admin");
        }

    }
}
