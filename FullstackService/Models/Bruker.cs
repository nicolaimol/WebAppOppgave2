namespace FullstackService.Models
{
    public class Bruker
    {
        public int Id { get; set; }
        
        public string Brukernavn { get; set; }
        
        public string PassordHash { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} Brukernavn: {Brukernavn} Hash: {PassordHash}";
        }
    }
}