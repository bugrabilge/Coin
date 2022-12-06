using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coin.Controllers
{
    public class UsersController : Controller
    {
        UsersManager um = new UsersManager(new EfUsersRepository());
        public IActionResult Index()
        {
            return View();
        }
    }
}
