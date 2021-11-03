using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FullstackService.Controllers;
using FullstackService.DAL;
using FullstackService.DTO;
using FullstackService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTest
{
    public class ReisendeTest
    {
        private readonly string _loggetInn = "logget inn";
        private readonly string _ikkeLoggetInn = "";

        private readonly Mock<IReisendeRepo> mockRep = new Mock<IReisendeRepo>();
        // put logger her

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHTTPSession mockSession = new MockHTTPSession();
        
        [Fact]
        public async Task EndreKontaktOk()
        {
            // Arrange
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };

            mockRep.Setup(b => b.ChangeKontaktPersonAsync(kontaktPerson.Id, kontaktPerson)).ReturnsAsync(kontaktPerson);

            var reisendeController = new ReisendeController(mockRep.Object);
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reisendeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reisendeController.ChangeKontaktPerson(kontaktPerson.Id, kontaktPerson) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<KontaktPerson>(kontaktPerson, (KontaktPerson)resultat.Value);
        }
        
        [Fact]
        public async Task EndreKontaktFail()
        {
            // Arrange
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };

            mockRep.Setup(b => b.ChangeKontaktPersonAsync(kontaktPerson.Id, kontaktPerson)).ReturnsAsync(kontaktPerson);

            var reisendeController = new ReisendeController(mockRep.Object);
            
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reisendeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reisendeController.ChangeKontaktPerson(kontaktPerson.Id, kontaktPerson) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized,resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        
        [Fact]
        public async Task EndreKontaktBad()
        {
            // Arrange
            var kontaktPerson = new KontaktPerson
            {
                Id = 1, Adresse = "Tulleveien 9", Epost = "eksempel@post.com",
                Etternavn = "Etternavnsen", Fornavn = "Fornavn", Foedselsdato = "05-05-2000",
                Post = new Post {PostNummer = "6457", PostSted = "Bolsøya"}, Telefon = "91234567"
            };

            mockRep.Setup(b => b.ChangeKontaktPersonAsync(kontaktPerson.Id, kontaktPerson)).ReturnsAsync(()=>null);

            var reisendeController = new ReisendeController(mockRep.Object);
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reisendeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reisendeController.ChangeKontaktPerson(kontaktPerson.Id, kontaktPerson) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("Ingen person på id, eller ingen endringer", resultat.Value);
        }
        
        [Fact]
        public async Task EndreVoksenOk()
        {
            // Arrange
            var voksenperson = new Voksen
            {
                Etternavn = "Etternavnsen", Foedselsdato = "05-05-2000", Fornavn = "Fornavn", Id = 1
            };

            mockRep.Setup(b => b.ChangeVoksenAsync(voksenperson.Id, voksenperson)).ReturnsAsync(voksenperson);

            var reisendeController = new ReisendeController(mockRep.Object);
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reisendeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reisendeController.ChangeVoksen(voksenperson.Id, voksenperson) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<Voksen>(voksenperson, (Voksen)resultat.Value);
        }
        
        [Fact]
        public async Task EndreVoksenFail()
        {
            // Arrange
            var voksenperson = new Voksen
            {
                Etternavn = "Etternavnsen", Foedselsdato = "05-05-2000", Fornavn = "Fornavn", Id = 1
            };

            mockRep.Setup(b => b.ChangeVoksenAsync(voksenperson.Id, voksenperson)).ReturnsAsync(voksenperson);

            var reisendeController = new ReisendeController(mockRep.Object);
            
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reisendeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reisendeController.ChangeVoksen(voksenperson.Id, voksenperson) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized,resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        
        [Fact]
        public async Task EndreVoksenBad()
        {
            // Arrange
            var voksenperson = new Voksen
            {
                Etternavn = "Etternavnsen", Foedselsdato = "05-05-2000", Fornavn = "Fornavn", Id = 1
            };

            mockRep.Setup(b => b.ChangeVoksenAsync(voksenperson.Id, voksenperson)).ReturnsAsync(()=>null);

            var reisendeController = new ReisendeController(mockRep.Object);
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reisendeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reisendeController.ChangeVoksen(voksenperson.Id, voksenperson) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("Ingen person på id, eller ingen endringer", resultat.Value);
        }
        
        [Fact]
        public async Task EndreBarnOk()
        {
            // Arrange
            var barnperson = new Barn
            {
                Etternavn = "Etternavnsen", Foedselsdato = "05-05-2010", Fornavn = "Fornavn", Id = 1
            };

            mockRep.Setup(b => b.ChangeBarnAsync(barnperson.Id, barnperson)).ReturnsAsync(barnperson);

            var reisendeController = new ReisendeController(mockRep.Object);
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reisendeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reisendeController.ChangeBarn(barnperson.Id, barnperson) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<Barn>(barnperson, (Barn)resultat.Value);
        }
        
        [Fact]
        public async Task EndreBarnFail()
        {
            // Arrange
            var barnperson = new Barn
            {
                Etternavn = "Etternavnsen", Foedselsdato = "05-05-2010", Fornavn = "Fornavn", Id = 1
            };

            mockRep.Setup(b => b.ChangeBarnAsync(barnperson.Id, barnperson)).ReturnsAsync(barnperson);

            var reisendeController = new ReisendeController(mockRep.Object);
            
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reisendeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reisendeController.ChangeBarn(barnperson.Id, barnperson) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized,resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        
        [Fact]
        public async Task EndreBarnBad()
        {
            // Arrange
            var barnperson = new Barn
            {
                Etternavn = "Etternavnsen", Foedselsdato = "05-05-2010", Fornavn = "Fornavn", Id = 1
            };

            mockRep.Setup(b => b.ChangeBarnAsync(barnperson.Id, barnperson)).ReturnsAsync(()=>null);

            var reisendeController = new ReisendeController(mockRep.Object);
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reisendeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reisendeController.ChangeBarn(barnperson.Id, barnperson) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("Ingen person på id, eller ingen endringer", resultat.Value);
        }
    }
    
}