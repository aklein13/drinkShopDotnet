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
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }
    }
}
