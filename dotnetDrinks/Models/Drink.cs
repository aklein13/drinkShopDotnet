using dotnetDrinks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks.Models
{
    public class Drink
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name can't be longer then 100 chars or shorder then 3 chars.")]
        [Display(Name = "Drink name")]
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 10000, ErrorMessage = "Amount can't be negative or be higher then 10 000.")]
        public int Amount { get; set; }
        [Display(Name = "Company")]
        public int? CompanyID { get; set; }
        public virtual Company Company { get; set; }
    }
}
