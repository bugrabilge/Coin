using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ObjectRecycle
    {
        [Key]
        public int ObjectID { get; set; }
        public string ObjectName { get; set; }
        public double ObjectQuantity { get; set; }
        public int ObjectCarbonPoint { get; set; }
        public bool ObjectStatus { get; set; }

    }
}
