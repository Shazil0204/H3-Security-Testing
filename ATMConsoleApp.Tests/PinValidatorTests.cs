using ATMConsoleApp.Services;

namespace ATMConsoleApp.Tests
{
    public class PinValidatorTests
    {
        private readonly PinValidator _pinValidator;

        public PinValidatorTests()
        {
            _pinValidator = new PinValidator();
            // Set up some sample accounts
            _pinValidator.SetPin("12345", "1234");
            _pinValidator.SetPin("67890", "5678");
        }

        [Fact]
        public void ValidatePin_ShouldReturnTrue_WhenCorrectPin()
        {
            // Arrange
            var accountNumber = "12345";
            var correctPin = "1234";

            // Act
            var result = _pinValidator.ValidatePin(accountNumber, correctPin);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidatePin_ShouldReturnFalse_WhenIncorrectPin()
        {
            // Arrange
            var accountNumber = "12345";
            var incorrectPin = "0000";

            // Act
            var result = _pinValidator.ValidatePin(accountNumber, incorrectPin);

            // Assert
            Assert.False(result);
        }

    }
}