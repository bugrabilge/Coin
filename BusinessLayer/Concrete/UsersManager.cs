using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UsersManager : IUsersService
    {
        IUsersDal _usersDal;

        public UsersManager(IUsersDal usersDal)
        {
            _usersDal = usersDal;
        }

        public List<Users> GetAllList()
        {
            return _usersDal.GetListAll();
        }

        public Users GetByID(int id)
        {
            return _usersDal.GetByID(id);
        }

        public void UserAdd(Users users)
        {
            _usersDal.Insert(users);
        }

        public void UserDelete(Users users)
        {
            _usersDal.Delete(users);
        }

        public void UserUpdate(Users users)
        {
            _usersDal.Update(users);
        }

    }
}
