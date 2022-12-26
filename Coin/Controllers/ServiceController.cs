using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using Coin.Models.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Coin.Controllers
{
    [MyAuthentication]
    public class ServiceController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IObjectRecycleService _ojService;

        public ServiceController(IUsersService userService, IObjectRecycleService ojService)
        {
            _userService = userService;
            _ojService = ojService;
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

            // islem yapan kullanici sistemde bulunuyorsa ve Carbon Pointi gerekli sayinin uzerindeyse C-RC donusumunu yapiyoruz
            if (user != null)
            {
                if (user.CarbonPoint > 100000000)
                {
                    var coinToAdd = user.CarbonPoint / 100000000;
                    Program.MainBlockChain.ConvertCarbonCoinToRecycleCoin(user.UsersAdress,coinToAdd);
                    user.RecycleCoin = Program.MainBlockChain.GetBalance(user.UsersAdress);
                    user.CarbonPoint = user.CarbonPoint - (coinToAdd * 100000000);
                    _userService.UserUpdate(user);
                    ViewBag.Coin = "İşlem Başarılı! "+ coinToAdd +" Recycle Coin bakiyenize eklenmiştir.";
                }

                else
                {
                    ViewBag.YetersizBakiye = "Carbon bakiyeniz bu işlemi gerçekleştirmek için yetersizdir!";
                }
            }
            GetAndSetUserInfo();
            return View();
        }

        public Users GetAndSetUserInfo()
        {
            // Kullanici bilgilerini ID uzerinden cekip viewlarda kullanilabilecek ViewBaglere atiyoruz.
            var userId = HttpContext.Session.GetInt32("userId").GetValueOrDefault();
            var user = _userService.GetByID(userId);

            if (user != null)
            {
                ViewBag.RecycleCoin = Program.MainBlockChain.GetBalance(user.UsersAdress);
                ViewBag.UserName = user.UserName;
                ViewBag.Mail = user.UserMail;
                ViewBag.Name = user.Name;
                ViewBag.Surname = user.Surname;
                ViewBag.Carbon = user.CarbonPoint;
                ViewBag.Adress = user.UsersAdress;
            }

            return user;
        }

        [HttpGet]
        public IActionResult Transfer()
        {
            GetAndSetUserInfo();
            return View();
        }

        [HttpPost]
        public IActionResult Transfer(string adress, int amount)
        {
            var sender = GetAndSetUserInfo();
            var receiver = _userService.GetAllList().FirstOrDefault(u => u.UsersAdress == adress);
            // eger gonderenin ve alicinin sha256 adresi sistemde bulunuyorsa ve gonderenin bakiyesi yeterliyse BlockChain uzerinden trasfer islemi gerceklesiyor
            if (sender != null && receiver != null)
            {
                if (sender.RecycleCoin > amount)
                {
                    try
                    {
                        // BlockChain uzerinden transaction olusturulup sisteme isleniyor
                        Program.MainBlockChain.CreateTransaction(new Transaction(sender.UsersAdress, receiver.UsersAdress, amount));
                        Program.MainBlockChain.ProcessPendingTransactions(sender.UsersAdress);

                        // Gonderenin ve alicinin balancelari BlockChain uzerinden cekiliyor
                        sender.RecycleCoin = Program.MainBlockChain.GetBalance(sender.UsersAdress);
                        receiver.RecycleCoin = Program.MainBlockChain.GetBalance(adress);

                        _userService.UserUpdate(sender);
                        _userService.UserUpdate(receiver);
                        ViewBag.Durum = "İşlem Başarılı! Mining ödülü 5 RC - Gönderim miktarı " + amount + " = " + (5-amount) +" hesabınıza eklenmiştir.";
                    }
                    catch (Exception)
                    {
                        ViewBag.Log = "Hata ile karşılaşıldı.";
                    }

                }
            }
            else
            {
                ViewBag.Durum = "Kullanıcı Bulunamadı veya Bakiyeniz yetersiz.";
            }

            GetAndSetUserInfo();
            return View();
        }

        [HttpGet]
        public IActionResult DoMining()
        {
            GetAndSetUserInfo();
            return View();
        }

        [HttpPost]
        public IActionResult DoMining(Users user)
        {
            // Mining islemi 5 RC odulu ile BlockChain uzerinden gerceklestiriliyor
            var userInfo = GetAndSetUserInfo();
            if (userInfo != null)
            {
                Program.MainBlockChain.ProcessPendingTransactions(userInfo.UsersAdress);
                ViewBag.Durum = "Mining Başarılı! Mining ödülü 5 RC hesabınıza eklenmiştir!";

                // Kullanicinin guncel balance'i BlockChain uzerinden cekilip guncelleniyor
                userInfo.RecycleCoin = Program.MainBlockChain.GetBalance(userInfo.UsersAdress);
                _userService.UserUpdate(userInfo);
                GetAndSetUserInfo();
            }
            return View();
        }

        public IActionResult TransactionHistory()
        {
            return View(Program.MainBlockChain);
        }

        public IActionResult ListObjectsRecycle()
        {
            var items = _ojService.GetAllList();
            return View(items);
        }


        public IActionResult ConvertObjectToCarbonCoin(int carbonPoint)
        {
            var user = GetAndSetUserInfo();
            if (user != null)
            {
                user.CarbonPoint = user.CarbonPoint + carbonPoint;
                _userService.UserUpdate(user);
            }
            return RedirectToAction("Index", "Service");

        }
    }
}
