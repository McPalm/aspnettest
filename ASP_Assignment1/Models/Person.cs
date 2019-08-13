using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Assignment1.Models
{
    public class Person
    {
        [Required, Display(Name ="Name"), StringLength(70)]
        public string Name { get; set; }
        [Display(Name = "City"), StringLength(85)]
        public string City { get; set; }
        [Required, Range(0, 200), Display(Name = "Age")]
        public int Age { get; set; }
    }
}
