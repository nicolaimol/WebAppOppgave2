
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
using UnitTest;
using Xunit;

namespace UnitTest
{
    public class ReiserTest
    {
        private readonly string _loggetInn = "logget inn";
        private readonly string _ikkeLoggetInn = "";

        private readonly Mock<IReiseRepo> mockRep = new Mock<IReiseRepo>();
        // put logger her

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHTTPSession mockSession = new MockHTTPSession();
        
        [Fact]
        public async Task HentAlleTestOk()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };
            var reise2 = new Reise
            {
                Id = 2, Info = "hellu", Strekning = "Oslo-Kiel", MaLugar = true,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 2, Url = "res/bilde2.jpg"}
            };
            var reise3 = new Reise
            {
                Id = 3, Info = "jippi", Strekning = "Oslo-Strömstad", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 3, Url = "res/bilde3.jpg"}
            };

            var reiseListe = new List<Reise>();
            reiseListe.Add(reise1);
            reiseListe.Add(reise2);
            reiseListe.Add(reise3);

            mockRep.Setup(b => b.GetAllReiseAsync()).ReturnsAsync(reiseListe);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.GetAllReiserAsync() as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<List<Reise>>(reiseListe,(List<Reise>)resultat.Value);
            
        }
        
        [Fact]
        public async Task HentByIdTestOk()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.GetReiseByIdAsync(reise1.Id)).ReturnsAsync(reise1);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.GetReiseByIdAsync(reise1.Id) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<Reise>(reise1,(Reise)resultat.Value);
            
        }
        
        [Fact]
        public async Task HentByIdTestFail()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.GetReiseByIdAsync(reise1.Id)).ReturnsAsync(reise1);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.GetReiseByIdAsync(reise1.Id) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized,resultat.StatusCode);
            Assert.Equal("Ikke logget inn",resultat.Value);
            
        }
        
        [Fact]
        public async Task HentByIdTestBad()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.GetReiseByIdAsync(reise1.Id)).ReturnsAsync(()=>null);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.GetReiseByIdAsync(reise1.Id) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("Ingen reise funnet",resultat.Value);
            
        }
        
        [Fact]
        public async Task CreateReiseTestOk()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.AddOneReiseAsync(reise1)).ReturnsAsync(true);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.CreateOneReiseAsync(reise1) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<Reise>(reise1,(Reise)resultat.Value);
            
        }
        
        [Fact]
        public async Task CreateReiseTestBad()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.AddOneReiseAsync(reise1)).ReturnsAsync(false);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.CreateOneReiseAsync(reise1) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("No reise created",resultat.Value);
            
        }
        
        [Fact]
        public async Task UpdateReiseTestOk()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.UpdateReiseAsync(reise1.Id, reise1)).ReturnsAsync(reise1);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.UpdateReise(reise1.Id, reise1) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<Reise>(reise1,(Reise)resultat.Value);
            
        }
        
        [Fact]
        public async Task UpdateReiseTestFail()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.UpdateReiseAsync(reise1.Id, reise1)).ReturnsAsync(reise1);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.UpdateReise(reise1.Id, reise1) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized,resultat.StatusCode);
            Assert.Equal("Ikke logget inn",resultat.Value);
            
        }
        
        [Fact]
        public async Task UpdateReiseTestBad()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.UpdateReiseAsync(reise1.Id, reise1)).ReturnsAsync(()=>null);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.UpdateReise(reise1.Id, reise1) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("No reise at id 1",resultat.Value);
            
        }
        
        [Fact]
        public async Task DeleteReiseTestOk()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.DeleteReiseAsync(reise1.Id)).ReturnsAsync(reise1);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.DeleteReise(reise1.Id) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<Reise>(reise1,(Reise)resultat.Value);
            
        }
        
        [Fact]
        public async Task DeleteReiseTestFail()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.DeleteReiseAsync(reise1.Id)).ReturnsAsync(reise1);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.DeleteReise(reise1.Id) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized,resultat.StatusCode);
            Assert.Equal("Ikke logget inn",resultat.Value);
            
        }
        
        [Fact]
        public async Task DeleteReiseTestBad()
        {
            // Arrange
            var reise1 = new Reise
            {
                Id = 1, Info = "Hejdå", Strekning = "Larvik-Hirtshals", MaLugar = false,
                PrisBil = 200, PrisPerGjest = 500, BildeLink = new Bilde {Id = 1, Url = "res/bilde.jpg"}
            };

            mockRep.Setup(b => b.DeleteReiseAsync(reise1.Id)).ReturnsAsync(()=>null);
            
            var reiseController = new ReiserController(mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            reiseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await reiseController.DeleteReise(reise1.Id) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("No reise found at: 1",resultat.Value);
            
        }
        
    }
    
}