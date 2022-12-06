using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coin.Controllers
{
    public class SignUpController : Controller
    {
        UsersManager um = new UsersManager(new EfUsersRepository());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Users users)
        {
            users.Name="Alperen";
            users.Surname = "Tan";
            users.UsersAdress = 10;
            users.UserStatus = true;
            users.Coin = 100;
            users.RecycleCoin = 100;
            um.UserAdd(users);
            return RedirectToAction("Index","Login");
        }
    }
}
