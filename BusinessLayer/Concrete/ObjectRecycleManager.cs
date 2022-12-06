using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ObjectRecycleManager : IObjectRecycleService
    {
        IObjectRecycleDal _objectRecycleDal;

        public ObjectRecycleManager(IObjectRecycleDal objectRecycleDal)
        {
            _objectRecycleDal = objectRecycleDal;
        }

        public List<ObjectRecycle> GetAllList()
        {
            return _objectRecycleDal.GetListAll();
        }

        public ObjectRecycle GetByID(int id)
        {
            return _objectRecycleDal.GetByID(id);
        }

        public void ObjectAdd(ObjectRecycle objectRecycle)
        {
            _objectRecycleDal.Insert(objectRecycle);
        }

        public void ObjectDelete(ObjectRecycle objectRecycle)
        {
            _objectRecycleDal.Delete(objectRecycle);
        }

        public void ObjectUpdate(ObjectRecycle objectRecycle)
        {
            _objectRecycleDal.Update(objectRecycle);
        }
    }
}
