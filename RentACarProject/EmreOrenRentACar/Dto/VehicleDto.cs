using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.Dto
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Image { get; set; }
        public double DailyPrice { get; set; }
        public int Year { get; set; }
    }
}