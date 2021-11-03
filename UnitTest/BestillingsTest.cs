using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FullstackService.Controllers;
using FullstackService.DAL;
using FullstackService.DTO;
using FullstackService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using Xunit;

namespace UnitTest
{
    public class BestillingsTest
    {
        private readonly string _loggetInn = "logget inn";
        private readonly string _ikkeLoggetInn = "";

        private readonly Mock<IBestillingRepo> mockRep = new Mock<IBestillingRepo>();
        private readonly Mock<ILogger<BestillingsController>> mockLog = new Mock<ILogger<BestillingsController>>();


        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHTTPSession mockSession = new MockHTTPSession();

        [Fact]
        public async Task HentAlleTestOk()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            var bestilling2 = new Bestilling
            {
                Id = 6, Pris = 3000, Referanse = "abc123df", Registreringsnummer = "UR 34234", AntallLugarer = 1,
                UtreiseDato = "11-10-2021", HjemreiseDato = "12-10-2021", ReiseId = 1, Voksne = new List<Voksen>(),
                Barn = new List<Barn>(), Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };
            var bestilling3 = new Bestilling
            {
                Id = 7, Pris = 4000, Referanse = "12ab34cd", Registreringsnummer = "UR 32234", AntallLugarer = 1,
                UtreiseDato = "09-09-2021", HjemreiseDato = "10-11-2021", ReiseId = 1, Voksne = new List<Voksen>(),
                Barn = new List<Barn>(), Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };



            var bestillingsListe = new List<Bestilling>();
            bestillingsListe.Add(bestilling);
            bestillingsListe.Add(bestilling2);
            bestillingsListe.Add(bestilling3);


            mockRep.Setup(b => b.HentAlleBestillingerAsync()).ReturnsAsync(bestillingsListe);

            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.HentAlleBestillingerAsync() as OkObjectResult;

            // Assert
            Assert.Equal((int) HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Bestilling>>(bestillingsListe, (List<Bestilling>) resultat.Value);
        }

        [Fact]
        public async Task HentAlleTestFail()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            var bestilling2 = new Bestilling
            {
                Id = 6, Pris = 3000, Referanse = "abc123df", Registreringsnummer = "UR 34234", AntallLugarer = 1,
                UtreiseDato = "11-10-2021", HjemreiseDato = "12-10-2021", ReiseId = 1, Voksne = new List<Voksen>(),
                Barn = new List<Barn>(), Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };
            var bestilling3 = new Bestilling
            {
                Id = 7, Pris = 4000, Referanse = "12ab34cd", Registreringsnummer = "UR 32234", AntallLugarer = 1,
                UtreiseDato = "09-09-2021", HjemreiseDato = "10-11-2021", ReiseId = 1, Voksne = new List<Voksen>(),
                Barn = new List<Barn>(), Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };



            var bestillingsListe = new List<Bestilling>();
            bestillingsListe.Add(bestilling);
            bestillingsListe.Add(bestilling2);
            bestillingsListe.Add(bestilling3);


            mockRep.Setup(b => b.HentAlleBestillingerAsync()).ReturnsAsync(bestillingsListe);

            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.HentAlleBestillingerAsync() as UnauthorizedObjectResult;

            // Assert
            Assert.Equal((int) HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentByIdOk()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            mockRep.Setup(b => b.HentEnBestillingAsync(bestilling.Id)).ReturnsAsync(bestilling);

            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.HentBestillingByIdAsync(bestilling.Id) as OkObjectResult;

            // Assert
            Assert.Equal((int) HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Bestilling>(bestilling, (Bestilling) resultat.Value);
        }

        [Fact]
        public async Task HentByIdFail()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            mockRep.Setup(b => b.HentEnBestillingAsync(bestilling.Id)).ReturnsAsync(bestilling);

            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat =
                await bestillingController.HentBestillingByIdAsync(bestilling.Id) as UnauthorizedObjectResult;

            // Assert
            Assert.Equal((int) HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentByRefOk()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            mockRep.Setup(b => b.HentEnBestillingByRefAsync(bestilling.Referanse)).ReturnsAsync(bestilling);
            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bestillingController.HentBestillingByRefAsync(bestilling.Referanse) as OkObjectResult;

            // Assert
            Assert.Equal((int) HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Bestilling>(bestilling, (Bestilling) resultat.Value);
        }
        
        [Fact]
        public async Task HentByRefFail()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            mockRep.Setup(b => b.HentEnBestillingByRefAsync(bestilling.Referanse)).ReturnsAsync(()=>null);
            
            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat =
                await bestillingController.HentBestillingByRefAsync("abcd1234") as NotFoundObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound,resultat.StatusCode);
            Assert.Equal("Bestilling ikke funnet på referanse: abcd1234",resultat.Value);
        }

        [Fact]
        public async Task EditBestillingOk()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            mockRep.Setup(b => b.EndreBestillingAsync(bestilling.Id, bestilling)).ReturnsAsync(bestilling);
            
            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await bestillingController.EditBestillingAsync(bestilling, bestilling.Id) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Bestilling>(bestilling, (Bestilling)resultat.Value);
        }
        
        [Fact]
        public async Task EditBestillingBad()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            mockRep.Setup(b => b.EndreBestillingAsync(bestilling.Id, bestilling)).ReturnsAsync(()=>null);
            
            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await bestillingController.EditBestillingAsync(bestilling, bestilling.Id) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("No bestilling found with id 5", resultat.Value);
        }
        
        [Fact]
        public async Task EditBestillingFail()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            mockRep.Setup(b => b.EndreBestillingAsync(bestilling.Id, bestilling)).ReturnsAsync(bestilling);
            
            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await bestillingController.EditBestillingAsync(bestilling, bestilling.Id) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        
        [Fact]
        public async Task SlettBestillingOk()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            mockRep.Setup(b => b.SlettBestillingAsync(bestilling.Id)).ReturnsAsync(1);

            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await bestillingController.DeleteBestillingAsync(bestilling.Id) as OkResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
        }
        
        [Fact]
        public async Task SlettBestillingFail()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            mockRep.Setup(b => b.SlettBestillingAsync(bestilling.Id)).ReturnsAsync(1);

            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await bestillingController.DeleteBestillingAsync(bestilling.Id) as UnauthorizedObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        
        [Fact]
        public async Task SlettBestillingBad()
        {
            // Arrange
            var reise = new Reise
            {
                Id = 1, Info = "Heisann", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 200, BildeLink = new Bilde {Id = 1, Url = "res/bild.jpg"}
            };
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };
            var enLugar = new Lugar {Antall = 1, Id = 1, Pris = 200, Reise = reise, ReiseId = 1, Type = "***"};

            var bestilling = new Bestilling
            {
                Id = 5, Pris = 2000, Referanse = "ab12cd34", Registreringsnummer = "EL 11777", AntallLugarer = 1,
                UtreiseDato = "10-10-2021", HjemreiseDato = "10-11-2021", ReiseId = 1,
                Voksne = new List<Voksen>(), Barn = new List<Barn>(),
                Reise = reise, KontaktPerson = kontaktPerson, LugarType = enLugar
            };

            mockRep.Setup(b => b.SlettBestillingAsync(bestilling.Id)).ReturnsAsync(-1);

            var bestillingController = new BestillingsController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bestillingController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await bestillingController.DeleteBestillingAsync(bestilling.Id) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("No changes made", resultat.Value);
        }
    }
}