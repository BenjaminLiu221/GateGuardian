using GateGuardianWeb.Controllers;
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
    }
}