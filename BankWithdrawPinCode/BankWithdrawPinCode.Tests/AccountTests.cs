using BankWithdrawPinCode.Models;
using Xunit;
using Xunit.Abstractions;

namespace BankWithdrawPinCode.BankWithdrawPinCode.Tests
{
	[Collection("Account Collection")]
	public class AccountTests
	{
		private readonly ITestOutputHelper _output;

		public AccountTests(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void Deposit_Increases_Balance_By_Specified_Amount()
		{
			// Arrange
			var initialBalance = 1000;
			var depositAmount = 500;
			var account = new Account(1234, initialBalance, "Test User");

			_output.WriteLine($"[{DateTime.Now}] Initial Balance: {initialBalance}, Deposit Amount: {depositAmount}");

			// Act
			account.Deposit(depositAmount);

			_output.WriteLine($"[{DateTime.Now}] Balance after deposit: {account.Balance}");

			// Assert
			Assert.Equal(1500, account.Balance);
		}

		[Fact]
		public void Withdraw_Decreases_Balance_When_Sufficient_Funds()
		{
			// Arrange
			var initialBalance = 1000;
			var withdrawalAmount = 400;
			var account = new Account(1234, initialBalance, "Test User");

			_output.WriteLine($"[{DateTime.Now}] Initial Balance: {initialBalance}, Withdrawal Amount: {withdrawalAmount}");

			// Act
			var result = account.Withdraw(withdrawalAmount);

			_output.WriteLine($"[{DateTime.Now}] Result: {result}, Balance after withdrawal: {account.Balance}");

			// Assert
			Assert.True(result, "Expected withdrawal to succeed.");
			Assert.Equal(600, account.Balance);
		}

		[Fact]
		public void Withdraw_Fails_When_Insufficient_Funds()
		{
			// Arrange
			var initialBalance = 1000;
			var withdrawalAmount = 1500;
			var account = new Account(1234, initialBalance, "Test User");

			_output.WriteLine($"[{DateTime.Now}] Initial Balance: {initialBalance}, Withdrawal Amount: {withdrawalAmount}");

			// Act
			var result = account.Withdraw(withdrawalAmount);

			_output.WriteLine($"[{DateTime.Now}] Result: {result}, Balance after attempted withdrawal: {account.Balance}");

			// Assert
			Assert.False(result, "Expected withdrawal to fail due to insufficient balance.");
			Assert.Equal(initialBalance, account.Balance);
		}

		[Fact]
		public void Withdraw_Fails_When_Amount_Is_Negative()
		{
			// Arrange
			var initialBalance = 1000;
			var withdrawalAmount = -200;
			var account = new Account(1234, initialBalance, "Test User");

			_output.WriteLine($"[{DateTime.Now}] Initial Balance: {initialBalance}, Withdrawal Amount: {withdrawalAmount} (Negative)");

			// Act
			var result = account.Withdraw(withdrawalAmount);

			_output.WriteLine($"[{DateTime.Now}] Result: {result}, Balance after attempted withdrawal: {account.Balance}");

			// Assert
			Assert.False(result, "Expected withdrawal of negative amount to fail.");
			Assert.Equal(initialBalance, account.Balance);
		}

		[Fact]
		public void Deposit_Fails_When_Amount_Is_Negative()
		{
			// Arrange
			var initialBalance = 1000;
			var depositAmount = -300;
			var account = new Account(1234, initialBalance, "Test User");

			_output.WriteLine($"[{DateTime.Now}] Initial Balance: {initialBalance}, Deposit Amount: {depositAmount} (Negative)");

			// Act
			account.Deposit(depositAmount);

			_output.WriteLine($"[{DateTime.Now}] Balance after attempted deposit: {account.Balance}");

			// Assert
			Assert.Equal(initialBalance, account.Balance);
		}
	}
}
