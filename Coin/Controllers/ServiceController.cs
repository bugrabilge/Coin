using BusinessLayer.Abstract;
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
        private readonly IUsersService _userService;

        public ServiceController(IUsersService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Convert()
        {
            GetAndSetUserInfo();
            return View();
        }

        public IActionResult Profile()
        {
            GetAndSetUserInfo();
            return View();
        }

        [HttpPost]
        public IActionResult Convert(double amount)
        {
            var user = GetAndSetUserInfo();

            if (user != null)
            {
                double coinToAdd = user.CarbonPoint / 100000000;
                double currentCoin = coinToAdd + user.RecycleCoin;
                user.RecycleCoin = currentCoin;
                user.CarbonPoint = user.CarbonPoint - (int)(coinToAdd * 100000000);
                _userService.UserUpdate(user);
            }
            ViewBag.Coin = user.RecycleCoin;
            GetAndSetUserInfo();
            return View();
        }

        public Users GetAndSetUserInfo()
        {
                var userId = HttpContext.Session.GetInt32("userId").GetValueOrDefault();
                var user = _userService.GetByID(userId);

                if (user != null)
                {
                    ViewBag.UserName = user.UserName;
                    ViewBag.Mail = user.UserMail;
                    ViewBag.Name = user.Name;
                    ViewBag.Surname = user.Surname;
                    ViewBag.RecycleCoin = user.RecycleCoin;
                    ViewBag.Carbon = user.CarbonPoint;
                }

            return user;
        }

    }
}
