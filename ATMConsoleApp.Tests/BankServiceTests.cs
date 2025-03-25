using ATMConsoleApp.Models;
using ATMConsoleApp.Services;

namespace ATMConsoleApp.Tests
{
    public class BankServiceTests
    {
        private readonly BankService _bankService;
        private readonly PinValidator _pinValidator;

        public BankServiceTests()
        {
            _pinValidator = new PinValidator();
            _bankService = new BankService(_pinValidator);

            // Set up accounts and PINs
            var account1 = new BankAccount("12345", 1000, "1234");
            _bankService.AddAccount(account1);
            _pinValidator.SetPin("12345", "1234");

            var account2 = new BankAccount("67890", 500, "5678");
            _bankService.AddAccount(account2);
            _pinValidator.SetPin("67890", "5678");
        }

        [Fact]
        public void WithdrawMoney_ShouldSucceed_WhenBalanceIsSufficient()
        {
            // Arrange
            var accountNumber = "12345";
            var amount = 500;
            var pin = "1234";

            // Act
            var result = _bankService.WithdrawMoney(accountNumber, amount, pin);

            // Assert
            Assert.True(result);
            Assert.Equal(500, _bankService.GetBalance(accountNumber));
        }

        [Fact]
        public void WithdrawMoney_ShouldFail_WhenIncorrectPin()
        {
            // Arrange
            var accountNumber = "12345";
            var amount = 500;
            var pin = "0000";

            // Act
            var result = _bankService.WithdrawMoney(accountNumber, amount, pin);

            // Assert
            Assert.False(result);
            Assert.Equal(1000, _bankService.GetBalance(accountNumber));
        }

        [Fact]
        public void WithdrawMoney_ShouldFail_WhenInsufficientBalance()
        {
            // Arrange
            var accountNumber = "12345";
            var amount = 1500;
            var pin = "1234";

            // Act
            var result = _bankService.WithdrawMoney(accountNumber, amount, pin);

            // Assert
            Assert.False(result);
            Assert.Equal(1000, _bankService.GetBalance(accountNumber)); 
        }

    }
}