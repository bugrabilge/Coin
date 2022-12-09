using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Coin.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Users users)
        {
            var dataValue = c.Users.FirstOrDefault(x =>x.UserName==users.UserName && x.Password == users.Password);

            var sessionName = "userId";

            HttpContext.Session.SetInt32(sessionName, dataValue.UserID);

            if (dataValue != null)
            {
                return RedirectToAction("Index", "Home");

            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }



    }
}
