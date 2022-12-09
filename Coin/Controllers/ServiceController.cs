using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coin.Controllers
{
    [MyAuthentication]
    public class ServiceController : Controller
    {
        Context c = new Context();
        UsersManager um = new UsersManager(new EfUsersRepository());
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public IActionResult Convert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Convert(double amount)
        {
            var userId = HttpContext.Session.GetInt32("userId");

            var dataValue = c.Users.FirstOrDefault(x => x.UserID == userId);
            
            if (dataValue != null)
            {
                double coin=dataValue.CarbonPoint / 10 + dataValue.RecycleCoin;
                dataValue.RecycleCoin= coin;
                um.UserUpdate(dataValue);
            }
            ViewBag.Coin = dataValue.RecycleCoin;
            return View();
        }


    }
}
