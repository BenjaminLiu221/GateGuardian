using FluentAssertions;
using GateGuardianWeb.Controllers;
using GateGuardianWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace GateGuardianWeb.Tests
{
    public class PasswordControllerTests
    {
        [Fact]
        public void ShouldReturnTrueIfLengthIsGreaterThanTen()
        {
            // Arrange
            PasswordController _passwordController = new PasswordController();
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
            PasswordController _passwordController = new PasswordController();
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
            PasswordController _passwordController = new PasswordController();
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
            PasswordController _passwordController = new PasswordController();

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
            var sut = new PasswordController();

            // Act
            var result = (OkObjectResult)await sut.Get();

            // Assert
            result.StatusCode.Should().Be(200);

        }

    }
}