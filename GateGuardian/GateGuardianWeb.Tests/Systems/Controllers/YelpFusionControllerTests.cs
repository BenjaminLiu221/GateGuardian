using GateGuardianWeb.Controllers;
using GateGuardianWeb.Data;
using GateGuardianWeb.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GateGuardianWeb.Tests.Systems.Controllers
{
    public class YelpFusionControllerTests
    {
        [Fact]
        // What behavior do we want our controller to do? What behavior do we want our actions to do?
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var dbContext = new Mock<ApplicationDbContext>(optionsBuilder.Options);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            var clientHandlerStub = new DelegatingHandlerStub();
            var client = new HttpClient(clientHandlerStub);

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            IHttpClientFactory factory = mockHttpClientFactory.Object;

            var expected = "OK";
            var sut = new YelpFusionController(dbContext.Object, factory);

            // Act
            var httpResponseMessage = sut.GetBusinesses();
            var result = httpResponseMessage.Status.ToString();

            // Assert
            Assert.Equal(expected, result);

        }
    }
}
