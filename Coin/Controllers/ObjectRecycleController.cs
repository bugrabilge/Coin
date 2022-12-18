using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coin.Controllers
{
    public class ObjectRecycleController : Controller
    {
        private readonly IObjectRecycleService _ojService;

        public ObjectRecycleController(IObjectRecycleService ojService)
        {
            this._ojService = ojService;
        }

        public IActionResult Index()
        {
            var values = _ojService.GetAllList();
            return View(values);
        }
    }
}
