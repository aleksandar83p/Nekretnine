using System.ComponentModel.DataAnnotations;

namespace Nekretnine.Models
{
    public class Pretraga
    {
        [Required]
        [Range(2.01, double.MaxValue)]
        public decimal Mini { get; set; }

        [Required]
        [Range(2.01, double.MaxValue)]
        public decimal Maksi { get; set; }
    }
}