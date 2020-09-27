using EmreOrenRentACar.Code;
using EmreOrenRentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EmreOrenRentACar.Areas.Admin.Controllers
{
    [IsAdmin]
    public class HomeController : Controller
    {
        private RentalContext context = new RentalContext();

        // GET: Admin/Home
        public ActionResult Index()
        {
            var rentals = context.Rentals.Include(c => c.User).Include(c => c.Vehicle);
            return View(rentals.ToList());
        }
    }
}