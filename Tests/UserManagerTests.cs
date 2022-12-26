using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class UserManagerTests
    {
        private readonly Mock<IUsersService> _userManager;
        private readonly Mock<IUsersDal> _userRepo;

        public UserManagerTests()
        {
            this._userManager = new Mock<IUsersService>();
            this._userRepo = new Mock<IUsersDal>();
        }

        [SetUp]
        public void Setup()
        {

        }

        // Testlerde kullanabilmek icin private 3 adet user barindiran bir user list donuyor
        private List<Users> GetTestUsersList()
        {
            List<Users> usersList = new List<Users>()
            {
                new Users
                {
                    UserID = 1,
                    Name = "TestName",
                    Surname = "TestSurname",
                    UserName = "Test"
                },


                new Users
                {
                    UserID = 2,
                    Name = "TestName2",
                    Surname = "TestSurname2",
                    UserName = "Test2"
                },


                new Users
                {
                    UserID = 3,
                    Name = "TestName3",
                    Surname = "TestSurname3",
                    UserName = "Test3"
                },
            };

            return usersList;
        }

        [Test]
        public void UserManagerGetByIdTest()
        {
            var user = new Users
            {
                UserID = 1,
                UserName = "Test"
            };

            _userRepo.Setup(u => u.GetByID(user.UserID)).Returns(user);

            var userManager = new UsersManager(_userRepo.Object);

            var result = userManager.GetByID(1);
            Assert.AreEqual(result.Name, user.Name);
        }

        [Test]
        public void UserManagerGetUserByMailTest()
        {
            var user = new Users
            {
                UserID = 1,
                UserName = "Test",
                UserMail = "test@test.com"
            };

            _userManager.Setup(u => u.GetUserByMail(user.UserMail)).Returns(user);

            var result = _userManager.Object.GetUserByMail(user.UserMail);

            Assert.AreEqual(result.UserName, user.UserName);

        }

        [Test]
        public void UserManagerGetAllListTest()
        {
            var userList = GetTestUsersList();

            _userRepo.Setup(u => u.GetListAll()).Returns(userList);

            var userManager = new UsersManager(_userRepo.Object);

            var result = userManager.GetAllList();

            Assert.AreEqual(result[0].UserName, userList[0].UserName);
        }
    }
}