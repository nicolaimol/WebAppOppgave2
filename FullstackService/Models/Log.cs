using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullstackService.Models
{
    public class Log
    {
        public int Id { get; set; }
        [ForeignKey("BrukerId")]
        public virtual Bruker Bruker { get; set; }
        public int? BrukerId { get; set; }
        [Required]
        public string Beskrivelse { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DatoEndret { get; set; }
    }
}