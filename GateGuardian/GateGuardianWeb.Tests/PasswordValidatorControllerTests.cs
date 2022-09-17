using GateGuardianWeb.Controllers;
using GateGuardianWeb.Models;
using Xunit;

namespace GateGuardianWeb.Tests
{
    public class PasswordValidatorControllerTests
    {
        [Fact]
        public void ShouldReturnTrueIfLengthIsGreaterThanTen()
        {
            // Arrange
            PasswordValidatorController _passwordValidatorController = new PasswordValidatorController();
            bool expected = true;

            // Act
            bool actual = _passwordValidatorController.LengthValidation("goodlength");

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
            PasswordValidatorController _passwordValidatorController = new PasswordValidatorController();
            bool expected = expectedResult;


            // Act
            bool actual = _passwordValidatorController.LengthValidation(password);

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
            PasswordValidatorController _passwordValidatorController = new PasswordValidatorController();
            bool expected = expectedResult;

            // Act
            bool actual = _passwordValidatorController.NumberValidation(password);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldReturnPasswordValidationResultObj()
        {
            // Arrange
            PasswordValidatorController _passwordValidatorController = new PasswordValidatorController();

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
            PasswordValidationResults actual = _passwordValidatorController.BuildPasswordValidationResult(_password);

            // Assert
            Assert.Equal(expected.Password, actual.Password);
            Assert.Equal(expected.LengthValidation, actual.LengthValidation);
            Assert.Equal(expected.NumberValidation, actual.NumberValidation);
            Assert.Equal(expected.CapitalizationValidation, actual.CapitalizationValidation);
        }
    }
}