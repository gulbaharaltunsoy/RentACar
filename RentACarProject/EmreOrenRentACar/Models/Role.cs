using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.Models
{
    public class Role : BaseEntity<int>
    {
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}