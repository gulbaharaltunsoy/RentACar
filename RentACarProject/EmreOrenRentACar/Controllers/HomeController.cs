using EmreOrenRentACar.Dto;
using EmreOrenRentACar.Models;
using EmreOrenRentACar.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmreOrenRentACar.Controllers
{
  
    public class HomeController : Controller
    {
        RentalContext _context = new RentalContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View(new FilterInputDto());
        }

        [HttpPost]
        public ActionResult RenderList(FilterInputDto input)
        {
            var query = _context.Vehicles.Where(x => x.DailyPrice > input.minPrice && x.DailyPrice <= input.maxPrice).AsQueryable();

            if (input.filterText != null)
            {
                query = query.Where(x => x.Brand.Contains(input.filterText) || x.Model.Contains(input.filterText));
            }           
            if (input.orderByPrice)
            {
                query = query.OrderByDescending(x => x.DailyPrice);
            }
            else
            {
                query = query.OrderBy(x => x.DailyPrice);
            }
            List<VehicleDto> result = query.Select(x => new VehicleDto()
            {
                Id = x.Id,
                Brand = x.Brand,
                Model = x.Model,
                Image = x.Image,
                DailyPrice = x.DailyPrice,
                Year = x.Year
            }).ToList();
            return PartialView(new VehicleListViewModel()
            {
                Vehicles = result
            });
        }
    }
}