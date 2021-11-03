using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FullstackService.Controllers;
using FullstackService.DAL;
using FullstackService.DTO;
using FullstackService.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace UnitTest
{
    public class BrukerTest
    {
        private readonly string _loggetInn = "logget inn";
        private readonly string _ikkeLoggetInn = "";

        private readonly Mock<IBrukerRepo> mockRep = new Mock<IBrukerRepo>();
        // put logger her

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHTTPSession mockSession = new MockHTTPSession();
        

        [Fact]
        public async Task HentAlleTestOk()
        {
            
            var bruker1 = new BrukerDTO {Id = 1, Brukernavn = "Per", Passord = "Askeladden"};
            var bruker2 = new BrukerDTO {Id = 2, Brukernavn = "Pål", Passord = "Askeladden"};
            var bruker3 = new BrukerDTO {Id = 3, Brukernavn = "Espen", Passord = "Askeladden"};

            var brukerListe = new List<BrukerDTO>();
            brukerListe.Add(bruker1);
            brukerListe.Add(bruker2);
            brukerListe.Add(bruker3);

            mockRep.Setup(b => b.HentAlleBrukereAsync()).ReturnsAsync(brukerListe);
            
            var brukerController = new BrukerController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await brukerController.HentAlle() as OkObjectResult;
            
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<List<BrukerDTO>>(brukerListe,(List<BrukerDTO>)resultat.Value);
        }
        
        [Fact]
        public async Task HentAlleTestFail()
        {
            // Arrange
            var bruker1 = new BrukerDTO {Id = 1, Brukernavn = "Per", Passord = "Askeladden"};
            var bruker2 = new BrukerDTO {Id = 2, Brukernavn = "Pål", Passord = "Askeladden"};
            var bruker3 = new BrukerDTO {Id = 3, Brukernavn = "Espen", Passord = "Askeladden"};

            var brukerListe = new List<BrukerDTO>();
            brukerListe.Add(bruker1);
            brukerListe.Add(bruker2);
            brukerListe.Add(bruker3);

            mockRep.Setup(b => b.HentAlleBrukereAsync()).ReturnsAsync(brukerListe);
            
            var brukerController = new BrukerController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await brukerController.HentAlle() as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized,resultat.StatusCode);
            Assert.Equal("Ikke logget inn",resultat.Value);
        }

        [Fact]
        public async Task HentBrukerTestOk()
        {
            // Arrange
            var bruker1 = new BrukerDTO() {Id = 1, Brukernavn = "Per", Passord = "Askeladden"};
            mockRep.Setup(b => b.HentEnBrukerByIdAsync(bruker1.Id)).ReturnsAsync(bruker1);
            var brukerController = new BrukerController(mockRep.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await brukerController.HentBruker(1) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<BrukerDTO>(bruker1, (BrukerDTO) resultat.Value);

        }
        
        [Fact]
        public async Task HentBrukerTestFail()
        {
            // Arrange
            var bruker1 = new BrukerDTO() {Id = 1, Brukernavn = "Per", Passord = "Askeladden"};
            mockRep.Setup(b => b.HentEnBrukerByIdAsync(bruker1.Id)).ReturnsAsync(bruker1);
            var brukerController = new BrukerController(mockRep.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await brukerController.HentBruker(1) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);

        }

        [Fact]
        public async Task VerifiserBrukerTestOk()
        {
            // Arrange
            var bruker1 = new BrukerDTO() {Id = 1, Brukernavn = "Per", Passord = "Askeladden"};
            mockRep.Setup(b => b.VerifiserBrukerAsync(bruker1)).ReturnsAsync(bruker1);
            var brukerController = new BrukerController(mockRep.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await brukerController.VerifiserBruker(bruker1) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<BrukerDTO>(bruker1, (BrukerDTO)resultat.Value);

        }
        
        [Fact]
        public async Task VerifiserBrukerTestFail()
        {
            // Arrange
            var bruker1 = new BrukerDTO() {Id = 1, Brukernavn = "Per", Passord = "Askeladden"};
            mockRep.Setup(b => b.VerifiserBrukerAsync(bruker1)).ReturnsAsync(()=> null);
            var brukerController = new BrukerController(mockRep.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await brukerController.VerifiserBruker(bruker1) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Username or password is wrong", resultat.Value);

        }

        [Fact]
        public void LoggUtTestOk()
        {
            // Arrange
            var brukerController = new BrukerController(mockRep.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = brukerController.LoggUt() as OkResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
        }
        

        [Fact]
        public void AutoriserTest()
        {
            // Arrange
            var brukerController = new BrukerController(mockRep.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = brukerController.AutoriserBruker() as OkResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);

        }
        
        [Fact]
        public async Task AddBrukerTestOk()
        {
            // Arrange
            var bruker1 = new BrukerDTO() {Id = 1, Brukernavn = "Per", Passord = "Askeladden"};
            var bruker = new Bruker() {Id = 1, Brukernavn = "Per", Salt = null, PassordHash = null};
            
            mockRep.Setup(b => b.LeggTilBrukerAsync(bruker1)).ReturnsAsync(bruker);
            
            var brukerController = new BrukerController(mockRep.Object);
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await brukerController.AddBruker(bruker1) as CreatedAtRouteResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Created, resultat.StatusCode);
            Assert.Equal<Bruker>(bruker, (Bruker)resultat.Value); // fungerer ikke

        }
        
        [Fact]
        public async Task AddBrukerTestFail()
        {
            // Arrange
            var bruker1 = new BrukerDTO() {Id = 1, Brukernavn = "Per", Passord = "Askeladden"};
            var bruker = new Bruker() {Id = 1, Brukernavn = "Per", Salt = null, PassordHash = null};
            
            mockRep.Setup(b => b.LeggTilBrukerAsync(bruker1)).ReturnsAsync(bruker);
            
            var brukerController = new BrukerController(mockRep.Object);
            
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await brukerController.AddBruker(bruker1) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);

        }
        
        [Fact]
        public async Task EndreBrukerTestOk()
        {
            // Ikke vits siden den ikke er implementert
        }

        [Fact]
        public async Task SlettBrukerTestOk()
        {
            // Arrange
            var bruker = new Bruker() {Id = 1, Brukernavn = "Per", Salt = null, PassordHash = null};

            mockRep.Setup(b => b.SlettBrukerAsync(bruker.Id)).ReturnsAsync(1);
            
            var brukerController = new BrukerController(mockRep.Object);
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await brukerController.DeleteBruker(bruker.Id) as NoContentResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.NoContent, resultat.StatusCode);
            
        }
        
        [Fact]
        public async Task SlettBrukerTestFail()
        {
            // Arrange
            var bruker = new Bruker() {Id = 1, Brukernavn = "Per", Salt = null, PassordHash = null};

            mockRep.Setup(b => b.SlettBrukerAsync(bruker.Id)).ReturnsAsync(1);
            
            var brukerController = new BrukerController(mockRep.Object);
            
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await brukerController.DeleteBruker(bruker.Id) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
            
        }
        
    }
    
    
    
}