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
            var bestilling1 = new Bestilling
            {
                Id = 1, Pris = 10, Referanse = "hei", Registreringsnummer = null,
                AntallLugarer = 1, UtreiseDato = "12-12-2021", ReiseId = 1, 
                LugarType = {Id = 1}, Barn = new List<Barn>(), Reise = {Id = 1}, HjemreiseDato = null, 
                Voksne = new List<Voksen>(), KontaktPerson = {Id = 1}
            };
            var bestilling2 = new Bestilling
            {
                Id = 2, Pris = 10, Referanse = "hellu", Registreringsnummer = null,
                AntallLugarer = 1, UtreiseDato = "11-12-2021", ReiseId = 2, 
                LugarType = {Id = 2}, Barn = new List<Barn>(), Reise = {Id = 2}, HjemreiseDato = null, 
                Voksne = new List<Voksen>(), KontaktPerson = {Id = 2}
            };
            var bestilling3 = new Bestilling
            {
                Id = 3, Pris = 10, Referanse = "wallah", Registreringsnummer = null,
                AntallLugarer = 0, UtreiseDato = "10-12-2021", ReiseId = 3, 
                LugarType = null, Barn = new List<Barn>(), Reise = {Id = 3}, HjemreiseDato = null, 
                Voksne = new List<Voksen>(), KontaktPerson = {Id = 3}
            };

            var bestillingsListe = new List<Bestilling>();
            bestillingsListe.Add(bestilling1);
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
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<List<Bestilling>>(bestillingsListe,(List<Bestilling>)resultat.Value);
        }
    }
}