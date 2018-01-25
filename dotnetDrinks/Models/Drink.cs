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
        [StringLength(100, ErrorMessage = "Name can't be longer then 100 chars.")]
        [Display(Name = "Drink name")]
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
        [Range(0, 999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Display(Name = "Company")]
        public int? CompanyID { get; set; }
        public virtual Company Company { get; set; }
    }
}
