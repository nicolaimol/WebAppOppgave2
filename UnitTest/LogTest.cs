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
using Moq;
using Xunit;

namespace UnitTest
{
    public class LogTest
    {
        private readonly string _loggetInn = "logget inn";
        private readonly string _ikkeLoggetInn = "";

        private readonly Mock<ILogRepo> mockRep = new Mock<ILogRepo>();
        // put logger her

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHTTPSession mockSession = new MockHTTPSession();
        
        [Fact]
        public async Task HentBrukerTestOk()
        {
            // Arrange
            var bruker1 = new BrukerDTO() {Id = 1, Brukernavn = "Per", Passord = "Askeladden"};
            var log = new Log
            {
                Id = 1,
                Beskrivelse = "test",
                Bruker = new Bruker
                {
                    Brukernavn = bruker1.Brukernavn,
                    Id = bruker1.Id
                },
                BrukerId = bruker1.Id,
                DatoEndret = DateTime.Now
            };

            var list = new List<Log>();
            list.Add(log);
            mockRep.Setup(b => b.HentLogAsync()).ReturnsAsync(list);
            var logController = new LogController(mockRep.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            logController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await logController.HentAlleLogAsync() as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(list, (List<Log>)resultat.Value);

        }
        
        [Fact]
        public async Task HentBrukerTestBad()
        {
            // Arrange
            var bruker1 = new BrukerDTO() {Id = 1, Brukernavn = "Per", Passord = "Askeladden"};
            var log = new Log
            {
                Id = 1,
                Beskrivelse = "test",
                Bruker = new Bruker
                {
                    Brukernavn = bruker1.Brukernavn,
                    Id = bruker1.Id
                },
                BrukerId = bruker1.Id,
                DatoEndret = DateTime.Now
            };

            var list = new List<Log>();
            list.Add(log);
            mockRep.Setup(b => b.HentLogAsync()).ReturnsAsync(list);
            var logController = new LogController(mockRep.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            logController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await logController.HentAlleLogAsync() as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);

        }
    }
}