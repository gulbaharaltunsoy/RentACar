using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.Areas.Admin.Data
{
    public class VehicleDto
    {
        public int? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string OldImage { get; set; }
        public double DailyPrice { get; set; }
        public int VehicleTypeId { get; set; }
    }
}