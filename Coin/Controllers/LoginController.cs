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
using DataAccessLayer.EntityFramework;
using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Server.IIS.Core;
using Grpc.Net.Client;
using MailGrpcService;
using System.Net.Http;
using System.Net.Security;
using NewPasswordCreatorService;
using BusinessLayer.Abstract;

namespace Coin.Controllers
{
    public class LoginController : Controller
    {       
        private readonly IUsersService _usersService;

        public LoginController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Users user)
        {
            // Girilen kullanici adi ve sifre kontrolu yapilip gecis saglaniyor
            using (var context = new Context())
            {
                var loginUser = context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

                if (loginUser != null) 
                {
                    var sessionName = "userId";
                    HttpContext.Session.SetInt32(sessionName, loginUser.UserID);
                    ViewBag.Log = "Login Succeeded";
                    return RedirectToAction("Index", "Service");
                }
                else
                {
                    ViewBag.Log = "Access Denied";
                    return View();
                }
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(Users users)
        {
            var dataValue = _usersService.GetUserByMail(users.UserMail);

            if (dataValue != null)
            {
                NewPasswordCreatorServiceSoapClient client =
                    new NewPasswordCreatorServiceSoapClient(NewPasswordCreatorServiceSoapClient.EndpointConfiguration.NewPasswordCreatorServiceSoap);
                string yeniSifre = client.NewPasswordCreator();
                dataValue.Password = yeniSifre;
                _usersService.UserUpdate(dataValue);

                var channel = GrpcChannel.ForAddress("https://localhost:7280");  // gRPC servisi ayaktayken dinlenen localhost yazılır 
                var clientGrpc = new SendMailService.SendMailServiceClient(channel);
                var MailRequest = new MailRequest { ToMailAddress = users.UserMail, NewPassword = yeniSifre };
                var response = clientGrpc.SendMail(MailRequest).Message;
             
                return RedirectToAction("Index", "Login");

            }
            else
            {
                ViewBag.Log = "Girilen mail'e ait bir hesap bulunamamıştır";
                return View();
            }

        }


    }
}
