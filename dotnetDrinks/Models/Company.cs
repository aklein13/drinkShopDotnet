using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetDrinks.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }
        public ICollection<Drinks.Models.Drink> Drinks { get; set; } 
    }
}
