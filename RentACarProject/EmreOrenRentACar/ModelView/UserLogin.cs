using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.ModelView
{
    public class UserLogin
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}