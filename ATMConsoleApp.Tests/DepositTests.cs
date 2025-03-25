using ATMConsoleApp.Models;
using ATMConsoleApp.Services;

namespace ATMConsoleApp.Tests
{
    public class DepositTests
    {
        private readonly BankService _bankService;
        private readonly PinValidator _pinValidator;

        public DepositTests()
        {
            _pinValidator = new PinValidator();
            _bankService = new BankService(_pinValidator);

            // Set up accounts and PINs
            var account1 = new BankAccount("12345", 1000, "1234");
            _bankService.AddAccount(account1);
            _pinValidator.SetPin("12345", "1234");
        }

        [Fact]
        public void DepositMoney_ShouldIncreaseBalance_WhenDepositIsMade()
        {
            // Arrange
            var accountNumber = "12345";
            var depositAmount = 500;

            // Act
            _bankService.DepositMoney(accountNumber, depositAmount);

            // Assert
            Assert.Equal(1500, _bankService.GetBalance(accountNumber));
        }

        [Fact]
        public void DepositMoney_ShouldNotChangeBalance_WhenDepositIsZeroOrNegative()
        {
            // Arrange
            var accountNumber = "12345";
            var depositAmount = -100; // Invalid deposit

            // Act
            _bankService.DepositMoney(accountNumber, depositAmount);

            // Assert
            Assert.Equal(1000, _bankService.GetBalance(accountNumber));
        }


    }
}