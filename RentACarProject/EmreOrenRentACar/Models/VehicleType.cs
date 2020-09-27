using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.Models
{
    [Table("VehicleType")]
    public class VehicleType:BaseEntity<int>
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public string Type { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}