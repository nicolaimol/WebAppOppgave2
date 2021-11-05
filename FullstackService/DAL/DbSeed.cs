using System;
using System.Collections.Generic;
using System.Linq;
using FullstackService.DTO;
using FullstackService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FullstackService.DAL
{
    public static class DbSeed
    {
        public static void SeedDb(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<MyDBContext>());
                SeedAdminUser(serviceScope.ServiceProvider.GetService<IBrukerRepo>());
            } 
        }

        private static void SeedData(MyDBContext db)
        {
            /***
             * Seeding 4 routes and lugar to all routes
             */
            if (!db.Reiser.Any())
            {
                var reise1 = new Reise
                {
                    Strekning = "Oslo-Kiel",
                    PrisPerGjest = 700,
                    PrisBil = 700,
                    BildeLink = new Bilde
                    {
                        Url = "./res/Color_Magic.jpeg",
                    },
                    Info = "Opplev herlige måltider i restaurantene, spektakulære show, spa og trening, " +
                           "taxfree-shopping, soldekk og mye mer. Forhåndbestill mat for å unngå fullbooket " +
                           "restaurant og få inntil 25% rabatt.",
                    MaLugar = true
                };
                var bilde = new Bilde
                {
                    Url = "./res/SuperSpeed_2.jpg"
                };
                var reise2 = new Reise
                {
                    Strekning = "Larvik-Hirtshals",
                    PrisPerGjest = 300,
                    PrisBil = 700,
                    BildeLink = bilde,
                    Info = "Overfarten med SuperSpeed" +
                           " fra Larvik tar kun 3 timer og 45 minutter. Det lønner seg å bestille tidlig, da sikrer du deg god pris" +
                           " og plass på ønsket avgang. Medlemmer av Color Club får de beste prisene på bilpakke til Danmark.",
                    MaLugar = false
                };
                var reise3 = new Reise
                {
                    Strekning = "Kristiansand-Hirtshals",
                    PrisPerGjest = 350,
                    PrisBil = 700,
                    BildeLink = new Bilde
                    {
                        Url = "./res/SuperSpeed_1.jpeg"
                    },
                    Info = "Det " +
                           "lønner seg å bestille tidlig, da sikrer du deg en god pris og plass på ønsket avgang. Overfarten med " +
                           "SuperSpeed fra Kristiansand tar kun 3 timer og 15 minutter. Medlemmer av Color Club får de beste prisene " +
                           "på bilpakke til Danmark.",
                    MaLugar = false
                };
                var reise4 = new Reise
                {
                    Strekning = "Sandefjord-Strömstad",
                    PrisPerGjest = 100,
                    PrisBil = 700,
                    BildeLink = new Bilde
                    {
                        Url = "./res/Color_Hybrid.jpeg"
                    },
                    Info = "Kjør " +
                           "bilen om bord og nyt overfarten fra Sandefjord til Strømstad på kun 2 ½ time. Underveis kan du slappe av," +
                           " kose deg med et godt måltid og handle taxfree-varer til svært gunstige priser. TIPS! Det lønner seg å " +
                           "være medlem av Color Club, da får du blant annet gratis reise med bil på flere avganger, og ytterligere " +
                           "10% rabatt på en mengde varer.",
                    MaLugar = false
                };

                db.Reiser.AddRange(reise1, reise2, reise3, reise4);

                var lugar1 = new Lugar
                {
                    Antall = 4,
                    Reise = reise1,
                    Pris = 500,
                    Type = "***"
                };
                
                var lugar2 = new Lugar
                {
                    Antall = 4,
                    Reise = reise1,
                    Pris = 800,
                    Type = "****"
                };
                
                var lugar3 = new Lugar
                {
                    Antall = 2,
                    Reise = reise1,
                    Pris = 1200,
                    Type = "*****"
                };
                
                var lugar4 = new Lugar
                {
                    Antall = 4,
                    Reise = reise2,
                    Pris = 500,
                    Type = "***"
                };
                
                var lugar5 = new Lugar
                {
                    Antall = 4,
                    Reise = reise3,
                    Pris = 500,
                    Type = "***"
                };
                
                var lugar6 = new Lugar
                {
                    Antall = 4,
                    Reise = reise4,
                    Pris = 400,
                    Type = "***"
                };
                
                db.Lugarer.AddRange(lugar1, lugar2, lugar3, lugar4, lugar5, lugar6);

                /***
                 * seeding all postnummer and poststed from file DAT/post.sql
                 */
                string[] linesPost = System.IO.File.ReadAllLines("DAL/post.sql");
                foreach (var s in linesPost)
                {
                    try
                    {
                        db.PostSteder.Add(new Post
                        {
                            PostNummer = s.Split("'")[1],
                            PostSted = s.Split("'")[3]
                        });
                    }
                    catch
                    {
                        Console.WriteLine(s);
                    }
                    
                }

                var barn = new List<Barn>();
                barn.Add(new Barn
                {
                    Fornavn = "Kari",
                    Etternavn = "Nordmann",
                    Foedselsdato = "2006-01-01"
                });

                var voksne = new List<Voksen>();
                voksne.Add(new Voksen
                {
                    Fornavn = "Jane",
                    Etternavn = "Doe",
                    Foedselsdato = "1960-01-01"
                });

                var bestilling = new Bestilling
                {
                    Reise = reise1,
                    Referanse = "a1b2c3d4",
                    Registreringsnummer = "DR 12345",
                    ReiseId = reise1.Id,
                    Pris = 1000,
                    AntallLugarer = 2,
                    LugarType = lugar3,
                    LugarTypeId = lugar3.Id,
                    KontaktPerson = new KontaktPerson
                    {
                        Fornavn = "Ola",
                        Etternavn = "Nordamnn",
                        Adresse = "Osloveien 82",
                        Epost = "ola.nordmann@gmail.com",
                        Telefon = "22225555",
                        Foedselsdato = "1998-01-01",
                        Post = db.PostSteder.Find("1275")
                    },
                    Barn = barn,
                    Voksne = voksne,
                    UtreiseDato = "2021-11-09",
                    HjemreiseDato = "2021-11-10"
                };

                db.Bestillinger.Add(bestilling);
                
                db.SaveChanges();
                Console.WriteLine("--> Seeding");
            }
            
        }

        private async static void SeedAdminUser(IBrukerRepo repo)
        {
            if ((await repo.HentAlleBrukereAsync()).Count == 0)
            {
                Console.WriteLine("--> SEEDING ADMIN USER");
                await repo.LeggTilBrukerAsync(new BrukerDTO {Brukernavn = "admin", Passord = "admin"});
            }
        }
    }
}