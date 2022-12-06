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
    public class AboutManager : IAboutService
    {
        IAboutDal _AboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _AboutDal = aboutDal;
        }

        public void AddAbout(About about)
        {
            _AboutDal.Insert(about);
        }

        public void DeleteAbout(About about)
        {
            _AboutDal.Delete(about);
        }

        public List<About> GetAllList()
        {
            return _AboutDal.GetListAll();
        }

        public About GetByID(int id)
        {
            return _AboutDal.GetByID(id);
        }

        public void UpdateAbout(About about)
        {
            _AboutDal.Update(about);
        }
    }
}
