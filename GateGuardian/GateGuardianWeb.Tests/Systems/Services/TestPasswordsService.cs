using FluentAssertions;
using GateGuardianWeb.Models.Passwords;
using GateGuardianWeb.Tests.Fixtures;
using GateGuardianWeb.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GateGuardianWeb.Tests.Systems.Services
{
    public class TestPasswordsService
    {
        [Fact]
        public async Task GetAllPasswords_WhenCalled_InvokesHttpGetRequest()
        {
            // Arrange
            var expectedResponse = PasswordsFixtures.GetTestPasswords();
            var handlerMock = MockHttpMessageHandler<Password>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new PasswordsService(httpClient);

            // Act
            await sut.GetAllPasswords();

            // Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task GetAllPasswords_WhenHits404_ReturnsEmptyListOfPasswords()
        {
            // Arrange
            var expectedResponse = PasswordsFixtures.GetTestPasswords();
            var handlerMock = MockHttpMessageHandler<Password>
                .SetupReturn404(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new PasswordsService(httpClient);

            // Act
            var result = await sut.GetAllPasswords();

            // Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllPasswords_WhenCalled_ReturnsListOfPasswordsOfExpectedSize()
        {
            // Arrange
            var expectedResponse = PasswordsFixtures.GetTestPasswords();
            var handlerMock = MockHttpMessageHandler<Password>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new PasswordsService(httpClient);

            // Act
            var result = await sut.GetAllPasswords();

            // Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllPasswords_WhenCalled_InvokesConfiguredExternalUrl()
        {
            // Arrange
            var expectedResponse = PasswordsFixtures.GetTestPasswords();
            var endpoint = "https://example.com/passwords";
            var handlerMock = MockHttpMessageHandler<Password>
                .SetupBasicGetResourceList(expectedResponse, endpoint);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new PasswordsApiOptions
            {
                Endpoint = "https://example.com/passwords"
            });

            var sut = new PasswordsService(httpClient, config);

            // Act
            var result = await sut.GetAllPasswords();

            // Assert
            result.Count.Should().Be(expectedResponse.Count);
        }
    }
}
