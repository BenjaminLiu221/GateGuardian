using FluentAssertions;
using GateGuardianWeb.Controllers;
using GateGuardianWeb.Models;
using GateGuardianWeb.Models.Passwords;
using GateGuardianWeb.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GateGuardianWeb.Tests.Systems.Controllers;

public class PasswordControllerTests
{
    [Fact]
    public void ShouldReturnTrueIfLengthIsGreaterThanTen()
    {
        // Arrange
        var mockPasswordsService = new Mock<IPasswordsService>();

        PasswordsController _passwordController = new PasswordsController(mockPasswordsService.Object);

        bool expected = true;

        // Act
        bool actual = _passwordController.LengthValidation("goodlength");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(true, "goodlength")]
    [InlineData(false, "dispwbad")]
    [InlineData(false, null)]

    public void ShouldReturnTrueIfLengthIsGreaterThanTenElseFalse(bool expectedResult, string? password)
    {
        // Arrange
        var mockPasswordsService = new Mock<IPasswordsService>();

        PasswordsController _passwordController = new PasswordsController(mockPasswordsService.Object);

        bool expected = expectedResult;


        // Act
        bool actual = _passwordController.LengthValidation(password);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(true, "696969")]
    [InlineData(true, "aaaa3aaaa")]
    [InlineData(true, "3")]
    [InlineData(false, "nonumbers")]
    [InlineData(false, "idontseenumber$")]
    [InlineData(false, null)]

    public void ShouldReturnTrueIfContainsANumberElseFalse(bool expectedResult, string? password)
    {
        // Arrange
        var mockPasswordsService = new Mock<IPasswordsService>();

        PasswordsController _passwordController = new PasswordsController(mockPasswordsService.Object);

        bool expected = expectedResult;

        // Act
        bool actual = _passwordController.NumberValidation(password);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldReturnPasswordValidationResultObj()
    {
        // Arrange
        var mockPasswordsService = new Mock<IPasswordsService>();

        PasswordsController _passwordController = new PasswordsController(mockPasswordsService.Object);

        Password _password = new Password()
        {
            Characters = "thisisareallylongpassword"
        };

        PasswordValidationResults expected = new PasswordValidationResults()
        {
            Password = _password,
            LengthValidation = "Passed.",
            NumberValidation = "Failed.",
            CapitalizationValidation = "Failed.",
        };

        // Act
        PasswordValidationResults actual = _passwordController.BuildPasswordValidationResult(_password);

        // Assert
        Assert.Equal(expected.Password, actual.Password);
        Assert.Equal(expected.LengthValidation, actual.LengthValidation);
        Assert.Equal(expected.NumberValidation, actual.NumberValidation);
        Assert.Equal(expected.CapitalizationValidation, actual.CapitalizationValidation);
    }

    [Fact]
    // What behavior do we want our controller to do? What behavior do we want our actions to do?
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockPasswordsService = new Mock<IPasswordsService>();
        mockPasswordsService
            .Setup(service => service.GetAllPasswords())
            .ReturnsAsync(new List<Password>()
            {
                new()
                {
                    Id = 1,
                    Characters = "DefaultMockPassword"
                }
            });
            //.ReturnsAsync(PasswordsFixtures.GetTestPasswords());

        var sut = new PasswordsController(mockPasswordsService.Object);

        // Act
        var result = (OkObjectResult)await sut.Get();

        // Assert
        result.StatusCode.Should().Be(200);

    }

    [Fact]
    // What behavior do we want our controller to do? What behavior do we want our actions to do?
    public async Task Get_OnSuccess_InvokesPasswordsServiceExactlyOnce()
    {
        // Arrange
        var mockPasswordsService = new Mock<IPasswordsService>();
        mockPasswordsService
            .Setup(service => service.GetAllPasswords())
            .ReturnsAsync(new List<Password>());

        var sut = new PasswordsController(mockPasswordsService.Object);

        // Act
        var result = await sut.Get();

        // Assert
        mockPasswordsService.Verify(
            service => service.GetAllPasswords(),
            Times.Once()
        );
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfPasswords()
    {
        // Arrange
        var mockPasswordsService = new Mock<IPasswordsService>();

        mockPasswordsService
            .Setup(service => service.GetAllPasswords())
            .ReturnsAsync(PasswordsFixtures.GetTestPasswords());

        var sut = new PasswordsController(mockPasswordsService.Object);

        // Act
        var result = await sut.Get();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<Password>>();
    }

    [Fact]
    public async Task Get_OnNoPasswordsFound_Returns404()
    {
        // Arrange
        var mockPasswordsService = new Mock<IPasswordsService>();

        mockPasswordsService
            .Setup(service => service.GetAllPasswords())
            .ReturnsAsync(new List<Password>());

        var sut = new PasswordsController(mockPasswordsService.Object);

        // Act
        var result = await sut.Get();

        // Assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
    }
}
