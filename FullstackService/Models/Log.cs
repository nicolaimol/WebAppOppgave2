using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullstackService.Models
{
    public class Log
    {
        public int Id { get; set; }
        public virtual Bruker Bruker { get; set; }
        public string Beskrivelse { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DatoEndret { get; set; }
    }
}