using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Assignment1.Models
{
    public class Temperature
    {
        [Required]
        public double Value { get; set; }

        public double Celcius => Value;

        public bool HasFever => Value > 38f;
        public bool HasTheChill => Value < 37f;
        public bool IsDead => Value > 42 || Value < 35;
    }
}
