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
        public string UsersAdress { get; set; }
        public int RecycleCoin { get; set; }
        public int CarbonPoint { get; set; }
        public int UserType { get; set; }
        public bool UserStatus { get; set; }
    }
}
