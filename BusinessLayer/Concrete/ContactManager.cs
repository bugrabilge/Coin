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
    public class ContactManager : IContactService
    {
        IContactDal _ContactDal;

        public ContactManager(IContactDal contactDal)
        {
            _ContactDal = contactDal;
        }

        public void AddContact(Contact contact)
        {
            _ContactDal.Insert(contact);
        }

        public void DeleteContact(Contact contact)
        {
            _ContactDal.Delete(contact);
        }

        public List<Contact> GetAllList()
        {
            return _ContactDal.GetListAll();
        }

        public Contact GetByID(int id)
        {
            return _ContactDal.GetByID(id);
        }

        public void UpdateContact(Contact contact)
        {
            _ContactDal.Update(contact);
        }
    }
}
