using System.ComponentModel.DataAnnotations;
using FullstackService.Models;

namespace FullstackService.DTO
{
    public class BrukerDTO
    {
        public int Id { get; set; }
        
        [Required]
        public string Brukernavn { get; set; }
        
        [Required]
        public string Passord { get; set; }

        public override bool Equals(object? obj)
        {
            var objB = (BrukerDTO) obj;
            return Id == objB.Id && Brukernavn == objB.Brukernavn;
        }
    }
}