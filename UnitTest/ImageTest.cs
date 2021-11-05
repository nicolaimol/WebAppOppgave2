using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FullstackService.Controllers;
using FullstackService.DAL;
using FullstackService.DTO;
using FullstackService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTest
{
    public class ImageTest
    {
        private readonly string _loggetInn = "logget inn";
        private readonly string _ikkeLoggetInn = "";

        private readonly Mock<IReiseRepo> mockRep = new Mock<IReiseRepo>();
        private readonly Mock<IWebHostEnvironment> mockLog = new Mock<IWebHostEnvironment>();
        // put logger her

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHTTPSession mockSession = new MockHTTPSession();
        
        
        [Fact]
        public async Task HentAlleTestOk()
        {
            // Arrange
    

            var bildeListe = new List<Bilde>();
            bildeListe.Add(new Bilde
            {
                Id = 1,
                Url = "test"
            });

            mockRep.Setup(b => b.GetAlleBilder()).ReturnsAsync(bildeListe);
            
            var imageController = new ImageController(mockLog.Object ,mockRep.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            imageController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await imageController.HentAlleBilder() as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal(bildeListe,(List<Bilde>)resultat.Value);
        }
    }
}