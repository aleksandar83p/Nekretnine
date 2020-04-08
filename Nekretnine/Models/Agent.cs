using System.ComponentModel.DataAnnotations;

namespace Nekretnine.Models
{
    public class Agent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ImePrezime { get; set; }

        [Required]
        [MinLength(4), MaxLength(4)]
        public string Licenca { get; set; }

        [Range(1951, 1995)]
        public int? GodinaRodjenja { get; set; }

        [Required]
        [Range(0, 50)]
        public int BrojProdatih { get; set; }



    }
}