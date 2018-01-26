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
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name can't be longer then 100 chars or shorder then 3 chars.")]
        public string Name { get; set; }
        public string Country { get; set; }
        [Display(Name = "Zip Code")]
        [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Zip code format is not valid. It should be XX-XXX.")]
        public string ZipCode { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }
        public virtual ICollection<Drinks.Models.Drink> Drinks { get; set; } 
    }
}
