using System.ComponentModel.DataAnnotations;

namespace FullstackService.Models
{
    public class Bilde
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
    }
}