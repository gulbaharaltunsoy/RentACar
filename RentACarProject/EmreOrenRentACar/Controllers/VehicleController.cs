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
    public class VehicleController : Controller
    {
        RentalContext _context = new RentalContext();

        [HttpGet]
        public ActionResult Index(int id, DateTime? startDate = null, DateTime? endDate = null)
        {
            var sDate = startDate == null ? DateTime.Now : (DateTime)startDate;
            var eDate = endDate == null ? DateTime.Now : (DateTime)endDate;

            var query = _context.Vehicles.AsQueryable().Include(x => x.VehicleType)
                .Select(y => new VehicleDetayViewModel()
                {
                    Id = y.Id,
                    Brand = y.Brand,
                    DailyPrice = y.DailyPrice,
                    Image = y.Image,
                    Model = y.Model,
                    Year = y.Year,
                    Type = y.VehicleType.Type,
                    StartDate = sDate,
                    EndDate = eDate
                }).FirstOrDefault(x => x.Id == id);
            if (query != null)
                return View(query);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateRetail(int vehicleId, DateTime startDate, DateTime endDate)
        {
            var user = HttpContext.Session["UserInfo"] as UserInfo;
            
            var checkRentals = _context.Rentals
                .Where(x => x.VehicleId == vehicleId &&
                    (((x.StartDate >= startDate && x.EndDate >= endDate)) || ((x.EndDate >= startDate))))
                .FirstOrDefault();

            if (checkRentals != null)
            {
                TempData["RentalError"] = "Arac bu " + checkRentals.StartDate.ToString("dd-MM-yyyy") + "-" + checkRentals.EndDate.ToString("dd-MM-yyyy") + " tarihler arasında dolu";
                return RedirectToAction("Index", "Vehicle", new { id = vehicleId, startDate = startDate, endDate = endDate });
            }
            var arac = _context.Vehicles.Find(vehicleId);
            _context.Rentals.Add(new Rental()
            {
                UserId = user.Id,
                VehicleId = vehicleId,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = ((endDate - startDate).TotalDays + 1) * arac.DailyPrice
            });
            _context.SaveChanges();
            TempData["RentalSuccess"] = "Arac bu " + startDate.ToString("dd-MM-yyyy") + "-" + endDate.ToString("dd-MM-yyyy") + " tarihler arasında başarıyla kiralandı";
            return RedirectToAction("Index", "Home");
        }
    }
}