using System;
using System.Threading.Tasks;
using FullstackService.DAL;
using FullstackService.Models;
using Moq;
using Xunit;

namespace UnitTest
{
    public class BestillingsTest
    {
        [Fact]
        public async Task Test1()
        {
            //arrange
            var reise = new Reise()
            {
                Id = 1,
                BildeLink = new Bilde()
                {
                    Url = "test.jpg",
                    Id = 1
                },
            };

            var mock = new Mock<IReiseRepo>();
            mock.Setup(r => r.AddOneReiseAsync(reise));
        }
    }
}