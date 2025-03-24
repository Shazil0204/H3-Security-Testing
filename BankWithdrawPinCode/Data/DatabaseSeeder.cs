using BankWithdrawPinCode.Models;

namespace BankWithdrawPinCode.Data
{
	internal class DatabaseSeeder
	{
		public static void Seed(BankContext context)
		{
			// Ensure the database is created
			context.Database.EnsureCreated();

			// Check if data already exists
			if (!context.Accounts.Any())
			{
				// Use Bogus to generate fake data
				var faker = new Bogus.Faker<Account>()
					.RuleFor(a => a.Id, f => f.IndexFaker + 1) // Sequential IDs
					.RuleFor(a => a.OwnerName, f => f.Name.FullName()) // Random full names
					.RuleFor(a => a.PIN, f => f.Random.Number(1000, 9999)) // Random PIN
					.RuleFor(a => a.Balance, f => f.Finance.Amount(0, 10000)); // Random balance

				// Generate a set number of fake accounts
				var fakeAccounts = faker.Generate(50); // 50, in this case

				// Add generated accounts to the database
				context.Accounts.AddRange(fakeAccounts);

				// Save changes to the database
				context.SaveChanges();
			}
		}
	}
}
