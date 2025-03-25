using ATM.Entity;
using ATM.Interfaces;
using ATM.Services;
using Xunit;

namespace UnitTestingATM
{
	public class UnitTest1
	{
		[Fact]
		public void Deposit_ShouldIncreaseBalance()
		{
			// Arrange
			var account = new Account(1234, 1000);
			var pinValidator = new PinValidator(1234);
			var bankService = new BankService(account, pinValidator);

			// Act
			var result = bankService.Deposit(500);

			// Assert
			Assert.True(result);
			Assert.Equal(1500, account.GetBalance());
		}

		[Fact]
		public void Withdraw_ShouldDecreaseBalance()
		{
			// Arrange
			var account = new Account(1234, 1000);
			var pinValidator = new PinValidator(1234);
			var bankService = new BankService(account, pinValidator);

			// Act
			var result = bankService.Withdraw(500);

			// Assert
			Assert.True(result);
			Assert.Equal(500, account.GetBalance());
		}

		[Fact]
		public void Withdraw_ShouldFail_WhenInsufficientBalance()
		{
			// Arrange
			var account = new Account(1234, 1000);
			var pinValidator = new PinValidator(1234);
			var bankService = new BankService(account, pinValidator);

			// Act
			var result = bankService.Withdraw(1500);

			// Assert
			Assert.False(result);
			Assert.Equal(1000, account.GetBalance());
		}

		[Fact]
		public void GetBalance_ShouldReturnCorrectBalance()
		{
			// Arrange
			var account = new Account(1234, 1000);
			var pinValidator = new PinValidator(1234);
			var bankService = new BankService(account, pinValidator);

			// Act
			var balance = bankService.GetBalance();

			// Assert
			Assert.Equal(1000, balance);
		}
	}
}