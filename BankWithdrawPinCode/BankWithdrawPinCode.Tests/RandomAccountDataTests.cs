using BankWithdrawPinCode.Models;
using Bogus;
using Xunit;
using Xunit.Abstractions;

namespace BankWithdrawPinCode.BankWithdrawPinCode.Tests
{
	[Collection("Account Data Collection")]
	public class RandomAccountDataTests
	{
		private readonly ITestOutputHelper _output;
		private readonly Faker<Account> _accountFaker;

		public RandomAccountDataTests(ITestOutputHelper output)
		{
			_output = output;

			// Initialize the Faker for generating random account data
			_accountFaker = new Faker<Account>()
				.RuleFor(a => a.Id, f => f.IndexFaker + 1) // Sequential IDs
				.RuleFor(a => a.OwnerName, f => f.Name.FullName()) // Random full names
				.RuleFor(a => a.PIN, f => f.Random.Number(1000, 9999)) // Random 4-digit PINs
				.RuleFor(a => a.Balance, f => f.Finance.Amount(0, 10000)); // Random balance (0 to 10,000)
		}

		[Fact]
		public void Generated_Account_ShouldHaveValidData()
		{
			// Act: Generate a single fake account
			var account = _accountFaker.Generate();

			// Log details about the generated account
			_output.WriteLine($"[{DateTime.Now}] Generated Account Details:");
			_output.WriteLine($"ID: {account.Id}");
			_output.WriteLine($"Owner Name: {account.OwnerName}");
			_output.WriteLine($"PIN: {account.PIN}");
			_output.WriteLine($"Balance: {account.Balance}");

			// Assert: Validate that the generated account has valid data
			Assert.NotNull(account);
			Assert.True(account.Id > 0, "Account ID should be greater than 0.");
			Assert.False(string.IsNullOrWhiteSpace(account.OwnerName), "Owner name should not be empty.");
			Assert.InRange(account.PIN, 1000, 9999); // Valid range check
			Assert.InRange(account.Balance, 0, 10000); // Valid range check
		}

		[Fact]
		public void Generate_Multiple_Accounts_ShouldHaveUniqueIds()
		{
			// Act: Generate a list of 100 fake accounts
			var accounts = _accountFaker.Generate(100);

			// Log details about the generated accounts
			_output.WriteLine($"[{DateTime.Now}] Generating 100 Accounts with Unique IDs:");
			foreach (var account in accounts)
			{
				_output.WriteLine($"ID: {account.Id}, Owner: {account.OwnerName}, PIN: {account.PIN}, Balance: {account.Balance}");
			}

			// Assert: Validate that each account has a unique ID
			var uniqueIds = accounts.Select(a => a.Id).Distinct().Count();
			_output.WriteLine($"[{DateTime.Now}] Expected unique IDs: 100, Actual unique IDs: {uniqueIds}");
			Assert.Equal(100, uniqueIds);
		}

		[Fact]
		public void Generate_Multiple_Accounts_ShouldHaveValidData()
		{
			// Act: Generate a list of 50 fake accounts
			var accounts = _accountFaker.Generate(50);

			// Log details about the generated accounts
			_output.WriteLine($"[{DateTime.Now}] Validating Data for 50 Generated Accounts:");
			foreach (var account in accounts)
			{
				_output.WriteLine($"ID: {account.Id}, Owner: {account.OwnerName}, PIN: {account.PIN}, Balance: {account.Balance}");

				// Assert: Validate that the generated account has valid data
				Assert.NotNull(account);
				Assert.True(account.Id > 0, $"Account ID should be greater than 0. Got {account.Id}.");
				Assert.False(string.IsNullOrWhiteSpace(account.OwnerName), "Owner name should not be empty.");
				Assert.InRange(account.PIN, 1000, 9999); // Valid range check
				Assert.InRange(account.Balance, 0, 10000); // Valid range check
			}
		}
	}
}
