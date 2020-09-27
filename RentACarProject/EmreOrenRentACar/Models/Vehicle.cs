using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.Models
{
    [Table("Vehicle")]
    public class Vehicle:BaseEntity<int>
    {
        public Vehicle()
        {
            Rentals = new HashSet<Rental>();
        }

        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Image { get; set; }
        public double DailyPrice { get; set; }
        

        public int VehicleTypeId { get; set; }

        [ForeignKey("VehicleTypeId")]
        public VehicleType VehicleType { get; set; }

        public ICollection<Rental>Rentals { get; set; }
    }
}