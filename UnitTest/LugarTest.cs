using System;
using System.Net;
using System.Threading.Tasks;
using FullstackService.Controllers;
using FullstackService.DAL;
using FullstackService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTest
{
    public class LugarTest
    {
        private readonly string _loggetInn = "logget inn";
        private readonly string _ikkeLoggetInn = "";

        private readonly Mock<ILugarRepo> mockRep = new Mock<ILugarRepo>();
        // put logger her

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHTTPSession mockSession = new MockHTTPSession();
        
        [Fact]
        public async Task CreateLugarTestOk()
        {
            // Arrange
            var lugar = new Lugar
            {
                Id = 1, ReiseId = 1, Antall = 4, Type = "***"
            };

            mockRep.Setup(l => l.CreateLugarAsync(lugar)).ReturnsAsync(lugar);
            
            var lugarController = new LugarController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lugarController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await lugarController.CreateLugar(lugar) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<Lugar>(lugar,(Lugar)resultat.Value);
            
        }
        
        [Fact]
        public async Task CreateLugarTestFail()
        {
            // Arrange
            var lugar = new Lugar
            {
                Id = 1, ReiseId = 1, Antall = 4, Type = "***"
            };

            mockRep.Setup(l => l.CreateLugarAsync(lugar)).ReturnsAsync(lugar);
            
            var lugarController = new LugarController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lugarController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await lugarController.CreateLugar(lugar) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized,resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
            
        }
        
        [Fact]
        public async Task UpdateLugarTestOk()
        {
            // Arrange
            var lugar = new Lugar
            {
                Id = 1, ReiseId = 1, Antall = 4, Type = "***"
            };

            mockRep.Setup(l => l.UpdateLugarAsync(lugar)).ReturnsAsync(lugar);
            
            var lugarController = new LugarController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lugarController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await lugarController.UpdateLugar(lugar) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal(lugar, resultat.Value);
            
        }
        
        [Fact]
        public async Task UpdateLugarTestFail()
        {
            // Arrange
            var lugar = new Lugar
            {
                Id = 1, ReiseId = 1, Antall = 4, Type = "***"
            };

            mockRep.Setup(l => l.UpdateLugarAsync(lugar)).ReturnsAsync(lugar);
            
            var lugarController = new LugarController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lugarController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await lugarController.UpdateLugar(lugar) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized,resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
            
        }
        
        [Fact]
        public async Task DeleteLugarTestOk()
        {
            // Arrange
            var lugar = new Lugar
            {
                Id = 1, ReiseId = 1, Antall = 4, Type = "***"
            };

            mockRep.Setup(l => l.DeleteLugarAsync(1)).ReturnsAsync(lugar);
            
            var lugarController = new LugarController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lugarController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await lugarController.DeleteLugarAsync(1) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal(lugar, resultat.Value);
            
        }
        
        [Fact]
        public async Task DeleteLugarTestFail()
        {
            // Arrange
            var lugar = new Lugar
            {
                Id = 1, ReiseId = 1, Antall = 4, Type = "***"
            };

            mockRep.Setup(l => l.DeleteLugarAsync(1)).ReturnsAsync(lugar);
            
            var lugarController = new LugarController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lugarController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await lugarController.DeleteLugarAsync(1) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized,resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
            
        }
        
        [Fact]
        public async Task DeleteLugarTestBad()
        {
            // Arrange
            var lugar = new Lugar
            {
                Id = 1, ReiseId = 1, Antall = 4, Type = "***"
            };

            mockRep.Setup(l => l.DeleteLugarAsync(1)).ReturnsAsync(() => null);
            
            var lugarController = new LugarController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lugarController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await lugarController.DeleteLugarAsync(1) as BadRequestResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            //Assert.Equal("", resultat.Value);
            
        }
        
    }
}