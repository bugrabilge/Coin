using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserMail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UsersAdress { get; set; }
        public double RecycleCoin  { get; set; }
        public double Coin { get; set; }
        public int CarbonPoint { get; set; }
        public bool UserStatus { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
