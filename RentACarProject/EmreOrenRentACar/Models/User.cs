using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmreOrenRentACar.Models
{
    [Table("User")]
    public class User : BaseEntity<int>
    {
        public User()
        {
            Rentals = new HashSet<Rental>();
        }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Rental> Rentals { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public int RoleId { get; set; }
    }
}