using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ObjectRecycleManagerTests
    {
        private readonly Mock<IObjectRecycleService> _ojManager;
        private readonly Mock<IObjectRecycleDal> _ojRepo;

        public ObjectRecycleManagerTests()
        {
            this._ojManager = new Mock<IObjectRecycleService>();
            this._ojRepo = new Mock<IObjectRecycleDal>();
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ObjectRecycleManagerGetByIdTest()
        {
            var objectRecyle = new ObjectRecycle
            {
                ObjectID = 1,
                ObjectName = "test"
            };

            _ojRepo.Setup(u => u.GetByID(objectRecyle.ObjectID)).Returns(objectRecyle);

            var objectManager = new ObjectRecycleManager(_ojRepo.Object);

            var result = objectManager.GetByID(1);
            Assert.AreEqual(result.ObjectName, objectRecyle.ObjectName);
        }

        [Test]
        public void ObjectRecycleManagerGetAllListTest() 
        {
            List<ObjectRecycle> ojList = new List<ObjectRecycle>()
            {
                new ObjectRecycle
                {
                    ObjectID = 1,
                    ObjectName = "test"
                },
                new ObjectRecycle
                {
                    ObjectID = 2,
                    ObjectName = "test2"
                },

                new ObjectRecycle
                {
                    ObjectID = 3,
                    ObjectName = "test3"
                }
            };

            _ojRepo.Setup(u => u.GetListAll()).Returns(ojList);

            var ojManager = new ObjectRecycleManager(_ojRepo.Object);

            var result = ojManager.GetAllList();

            Assert.AreEqual(result[0].ObjectName, ojList[0].ObjectName);
            Assert.AreEqual(result[1].ObjectName, ojList[1].ObjectName);
            Assert.AreEqual(result[2].ObjectName, ojList[2].ObjectName);
        }
    }
}
