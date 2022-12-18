using BusinessLayer.Abstract;
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
        private readonly IUsersService _usersService;

        public SignUpController(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Users users)
        {
            users.UsersAdress = 10;
            users.UserStatus = true;
            users.Coin = 100;
            users.RecycleCoin = 100;
            users.CarbonPoint = 1000;
            _usersService.UserAdd(users);
            return RedirectToAction("Index","Login");
        }
    }
}
