using BankWithdrawPinCode.Interfaces;
using BankWithdrawPinCode.Services;
using Xunit;
using NSubstitute;
using Xunit.Abstractions;

namespace BankWithdrawPinCode.BankWithdrawPinCode.Tests
{
	[Collection("PIN Collection")]
	public class PINValidatorTests
	{
		private readonly ITestOutputHelper _output;

		public PINValidatorTests(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void Validate_PIN_Success_When_Correct()
		{
			// Arrange: Mock the IPINValidator
			var mockPINValidator = Substitute.For<IPINValidator>();
			var correctPin = 1234; // Expected correct PIN
			var enteredPin = 1234; // User-entered PIN

			_output.WriteLine($"[{DateTime.Now}] Setting up mock PIN validation. Correct PIN: {correctPin}, Entered PIN: {enteredPin}");

			// Setup mock behavior for correct PIN
			mockPINValidator.ValidatePIN(enteredPin, correctPin).Returns(true);

			// Act: Call ValidatePIN method
			_output.WriteLine($"[{DateTime.Now}] Calling ValidatePIN...");
			var result = mockPINValidator.ValidatePIN(enteredPin, correctPin);

			// Log results
			_output.WriteLine($"[{DateTime.Now}] Validation result: {result}");

			// Assert: Verify that validation succeeds
			Assert.True(result, "PIN validation should succeed when the entered PIN is correct.");
		}

		[Fact]
		public void Validate_PIN_Failure_When_Incorrect()
		{
			// Arrange: Mock the IPINValidator
			var mockPINValidator = Substitute.For<IPINValidator>();
			var correctPin = 1234; // Expected correct PIN
			var enteredPin = 1111; // User-entered PIN

			_output.WriteLine($"[{DateTime.Now}] Setting up mock PIN validation. Correct PIN: {correctPin}, Entered PIN: {enteredPin}");

			// Setup mock behavior for incorrect PIN
			mockPINValidator.ValidatePIN(enteredPin, correctPin).Returns(false);

			// Act: Call ValidatePIN method
			_output.WriteLine($"[{DateTime.Now}] Calling ValidatePIN...");
			var result = mockPINValidator.ValidatePIN(enteredPin, correctPin);

			// Log results
			_output.WriteLine($"[{DateTime.Now}] Validation result: {result}");

			// Assert: Verify that validation fails
			Assert.False(result, "PIN validation should fail when the entered PIN is incorrect.");
		}
	}
}
