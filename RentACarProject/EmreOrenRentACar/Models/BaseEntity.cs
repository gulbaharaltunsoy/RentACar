using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.Models
{
    public class BaseEntity<TId> where TId : IEquatable<TId>
    {
        public TId Id { get; set; }
    }
}