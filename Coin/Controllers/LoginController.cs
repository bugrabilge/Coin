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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Users user)
        {
            using (var context = new Context())
            {
                var loginUser = context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

                if (loginUser != null) 
                {
                    var sessionName = "userId";
                    HttpContext.Session.SetInt32(sessionName, loginUser.UserID);
                    ViewBag.Log = "Login Succeeded";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Log = "Access Denied";
                    return View();
                }
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
