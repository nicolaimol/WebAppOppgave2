using System.ComponentModel.DataAnnotations;

namespace FullstackService.DTO
{
    public class BrukerDTO
    {
        public int Id { get; set; }
        
        [Required]
        public string Brukernavn { get; set; }
        
        [Required]
        public string Passord { get; set; }
        
        
    }
}