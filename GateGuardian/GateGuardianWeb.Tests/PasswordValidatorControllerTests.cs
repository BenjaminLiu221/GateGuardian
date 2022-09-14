using GateGuardianWeb.Controllers;
using Xunit;

namespace GateGuardianWeb.Tests
{
    public class PasswordValidatorControllerTests
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            PasswordValidatorController _passwordValidatorController = new PasswordValidatorController();
            bool expected = true;

            // Act
            bool actual = _passwordValidatorController.LengthValidation("goodlength");

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}