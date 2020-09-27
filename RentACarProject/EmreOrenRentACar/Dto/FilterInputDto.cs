using EmreOrenRentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.Dto
{
    public class FilterInputDto
    {
        public string filterText { get; set; } = null;
        public double minPrice { get; set; } = double.MinValue;
        public double maxPrice { get; set; } = double.MaxValue;
        public bool orderByPrice { get; set; } = false;
    }
}