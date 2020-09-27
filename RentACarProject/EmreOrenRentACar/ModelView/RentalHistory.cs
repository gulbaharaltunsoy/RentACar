using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.ModelView
{
    public class RentalHistory
    {
        public int Id { get; set; }
        [DisplayName("Marka")]
        public string Brand { get; set; }
        [DisplayName("Model")]
        public string Model { get; set; }
        [DisplayName("Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }
        [DisplayName("Bitiş Tarihi")]
        public DateTime EndDate { get; set; }
        [DisplayName("Gün Sayısı")]
        public int? TotalDay { get; set; }
        [DisplayName("Ücret")]
        public double TotalPrice { get; set; }
    }
}