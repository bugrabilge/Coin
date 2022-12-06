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
        ObjectRecycleManager om = new ObjectRecycleManager(new EfObjectRecycleRepository());
        public IActionResult Index()
        {
            var values = om.GetAllList();
            return View(values);
        }
    }
}
