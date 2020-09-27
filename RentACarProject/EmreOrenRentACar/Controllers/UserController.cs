using EmreOrenRentACar.Models;
using EmreOrenRentACar.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmreOrenRentACar.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        RentalContext _context = new RentalContext();

        [HttpGet]
        public ActionResult Index()
        {
            var currentUser = HttpContext.Session["UserInfo"] as UserInfo;
            var query = _context.Rentals.AsQueryable().Include(y => y.Vehicle).Where(x => x.UserId == currentUser.Id);
            var rentals = query
                .Select(x => new RentalHistory()
                {
                    Id = x.VehicleId,
                    Brand = x.Vehicle.Brand,
                    Model = x.Vehicle.Model,
                    StartDate=x.StartDate,
                    EndDate=x.EndDate,
                    TotalDay = (DbFunctions.DiffDays(x.StartDate, x.EndDate) + 1),
                    TotalPrice = x.TotalPrice
                })
                .ToList();

            return View(rentals);
        }
    }
}