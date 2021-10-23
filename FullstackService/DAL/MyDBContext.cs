using Microsoft.EntityFrameworkCore;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Barn> Barn { get; set; }
        public DbSet<Voksen> Voksne { get; set; }
        public DbSet<KontaktPerson> KontaktPersoner { get; set; }
        public DbSet<Bestilling> Bestillinger { get; set; }
        public DbSet<Reise> Reiser { get; set; }
        public DbSet<Lugar> Lugarer { get; set; }
        public DbSet<Post> PostSteder { get; set; }
        
        public DbSet<Bilde> Bilder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}