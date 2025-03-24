using BankWithdrawPinCode.Interfaces;
using BankWithdrawPinCode.Services;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace BankWithdrawPinCode.Tests
{
	[Collection("Transaction Manager Collection")]
	public class TransactionManagerTests
	{
		private readonly ITestOutputHelper _output;

		public TransactionManagerTests(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void Withdraw_Success_WhenPINIsCorrect_AndBalanceIsSufficient()
		{
			// Arrange
			var mockAccount = new Mock<IAccount>();
			mockAccount.Setup(a => a.PIN).Returns(1234);
			mockAccount.Setup(a => a.Withdraw(500)).Returns(true); // Simulate successful withdrawal

			var mockPINValidator = new Mock<IPINValidator>();
			mockPINValidator.Setup(p => p.ValidatePIN(1234, 1234)).Returns(true); // Simulate correct PIN

			var transactionManager = new TransactionManager(mockPINValidator.Object);

			_output.WriteLine($"[{DateTime.Now}] Setting up mock TransactionManager.");
			_output.WriteLine($"Account PIN: {mockAccount.Object.PIN}, Withdrawal Amount: 500");

			// Act
			_output.WriteLine($"[{DateTime.Now}] Calling Withdraw with correct PIN and sufficient balance...");
			var result = transactionManager.Withdraw(mockAccount.Object, 500, 1234);

			// Assert
			_output.WriteLine($"[{DateTime.Now}] Withdrawal result: {result}");
			Assert.True(result, "Withdrawal should succeed when PIN is correct and balance is sufficient.");
			mockAccount.Verify(a => a.Withdraw(500), Times.Once); // Ensure Withdraw was called once
		}

		[Fact]
		public void Withdraw_Fails_WhenPINIsIncorrect()
		{
			// Arrange
			var mockAccount = new Mock<IAccount>();
			mockAccount.Setup(a => a.PIN).Returns(1234);

			var mockPINValidator = new Mock<IPINValidator>();
			mockPINValidator.Setup(p => p.ValidatePIN(5678, 1234)).Returns(false); // Simulate incorrect PIN

			var transactionManager = new TransactionManager(mockPINValidator.Object);

			_output.WriteLine($"[{DateTime.Now}] Setting up mock TransactionManager.");
			_output.WriteLine($"Account PIN: {mockAccount.Object.PIN}, Withdrawal Amount: 500, Entered PIN: 5678");

			// Act
			_output.WriteLine($"[{DateTime.Now}] Calling Withdraw with incorrect PIN...");
			var result = transactionManager.Withdraw(mockAccount.Object, 500, 5678);

			// Assert
			_output.WriteLine($"[{DateTime.Now}] Withdrawal result: {result}");
			Assert.False(result, "Withdrawal should fail when the entered PIN is incorrect.");
			mockAccount.Verify(a => a.Withdraw(It.IsAny<decimal>()), Times.Never); // Ensure Withdraw was NOT called
		}

		[Fact]
		public void Withdraw_Fails_WhenBalanceIsInsufficient()
		{
			// Arrange
			var mockAccount = new Mock<IAccount>();
			mockAccount.Setup(a => a.PIN).Returns(1234);
			mockAccount.Setup(a => a.Withdraw(1000)).Returns(false); // Simulate insufficient balance

			var mockPINValidator = new Mock<IPINValidator>();
			mockPINValidator.Setup(p => p.ValidatePIN(1234, 1234)).Returns(true); // Simulate correct PIN

			var transactionManager = new TransactionManager(mockPINValidator.Object);

			_output.WriteLine($"[{DateTime.Now}] Setting up mock TransactionManager.");
			_output.WriteLine($"Account PIN: {mockAccount.Object.PIN}, Withdrawal Amount: 1000");

			// Act
			_output.WriteLine($"[{DateTime.Now}] Calling Withdraw with correct PIN but insufficient balance...");
			var result = transactionManager.Withdraw(mockAccount.Object, 1000, 1234);

			// Assert
			_output.WriteLine($"[{DateTime.Now}] Withdrawal result: {result}");
			Assert.False(result, "Withdrawal should fail when balance is insufficient.");
			mockAccount.Verify(a => a.Withdraw(1000), Times.Once); // Ensure Withdraw was attempted
		}
	}
}
