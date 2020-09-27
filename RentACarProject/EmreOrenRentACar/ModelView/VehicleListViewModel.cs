using EmreOrenRentACar.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.ModelView
{
    public class VehicleListViewModel
    {
        public List<VehicleDto> Vehicles { get; set; }
        public FilterInputDto FilterInputDto { get; set; }
        public VehicleListViewModel()
        {
            Vehicles = new List<VehicleDto>();
            FilterInputDto = new FilterInputDto();
        }
    }
}