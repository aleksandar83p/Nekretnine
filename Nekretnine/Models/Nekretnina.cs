using System.ComponentModel.DataAnnotations;

namespace Nekretnine.Models
{
    public class Nekretnina
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Mesto { get; set; }

        [Required]
        [MinLength(1), MaxLength(5)]
        public string AgencijskaOznaka { get; set; }

        [Range(1900, 2018)]
        public int? GodinaIzgradnje { get; set; }

        [Required]
        [Range(2.01, double.MaxValue)]
        public decimal Kvadratura { get; set; }

        [Required]
        [Range(0.01, 100000.00)]
        public decimal Cena { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}