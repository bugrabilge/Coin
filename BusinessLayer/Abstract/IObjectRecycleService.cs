using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IObjectRecycleService
    {
        void ObjectAdd(ObjectRecycle objectRecycle);
        void ObjectDelete(ObjectRecycle objectRecycle);
        void ObjectUpdate(ObjectRecycle objectRecycle);
        List<ObjectRecycle> GetAllList();
        ObjectRecycle GetByID(int id);
    }
}
