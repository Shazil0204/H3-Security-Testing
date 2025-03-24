using BankWithdrawPinCode.Interfaces;
using BankWithdrawPinCode.Models;
using BankWithdrawPinCode.Services;
using BankWithdrawPinCode.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

// Please ensure that the namespace is updated to officially align with ours
namespace BankWithdrawPinCode
{
	class Program
	{
		private readonly ITransactionManager _transactionManager;
		private readonly IPINValidator _pinValidator;
		private IAccount _account;

		public Program()
		{
			// Setup Dependency Injection
			var serviceProvider = new ServiceCollection()
				.AddDbContext<BankContext>(options =>
					options.UseSqlite("Data Source=bank.db")) // Database
				.AddTransient<IPINValidator, PINValidator>()
				.AddTransient<ITransactionManager, TransactionManager>()
				.BuildServiceProvider();

			// Retrieve required services
			_pinValidator = serviceProvider.GetService<IPINValidator>()
				?? throw new InvalidOperationException("IPINValidator not registered.");
			_transactionManager = serviceProvider.GetService<ITransactionManager>()
				?? throw new InvalidOperationException("ITransactionManager not registered.");

			// Retrieve the database context
			var context = serviceProvider.GetService<BankContext>()
				?? throw new InvalidOperationException("BankContext not registered.");

			// Perform manual database seeding
			DatabaseSeeder.Seed(context);

			// Fetch the first account from the database
			_account = context.Accounts.FirstOrDefault()
				?? throw new InvalidOperationException("No accounts found in the database.");

			Console.WriteLine($"Loaded account for {(_account as Account)?.OwnerName ?? "Unknown Owner"}.");
		}

		static void Main(string[] args)
		{
			var program = new Program();
			program.Run();
		}

		private void Run()
		{
			Console.WriteLine("Welcome to the ATM!");

			bool isRunning = true;
			while (isRunning)
			{
				//Console.Clear();
				Console.WriteLine("\n1. Enter PIN");
				Console.WriteLine("2. Withdraw Money");
				Console.WriteLine("3. Check Balance");
				Console.WriteLine("4. Exit");
				Console.Write("Choose an option: ");

				string choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						ValidateUserPIN();
						break;

					case "2":
						HandleWithdrawal();
						break;

					case "3":
						CheckBalance();
						break;

					case "4":
						isRunning = false;
						Console.WriteLine("Good-bye, money-person!");
						break;

					default:
						Console.WriteLine("Invalid choice. Please try again.");
						break;
				}
			}
		}

		private void ValidateUserPIN()
		{
			Console.Write("Enter your PIN: ");
			if (int.TryParse(Console.ReadLine(), out int inputPin))
			{
				if (_pinValidator.ValidatePIN(inputPin, _account.PIN))
				{
					Console.Clear();
					Console.WriteLine("PIN validated!");
				}
				else
				{
					Console.Clear();
					Console.WriteLine("Invalid PIN.\nTry again.");
				}
			}
			else
			{
				Console.Clear();
				Console.WriteLine("Invalid PIN format.\nPlease enter numbers only.");
			}
		}

		private void HandleWithdrawal()
		{
			Console.Clear();
			Console.Write("Enter amount to withdraw: ");
			if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
			{
				Console.Write("Enter your PIN: ");
				if (int.TryParse(Console.ReadLine(), out int inputPin))
				{
					if (_transactionManager.Withdraw(_account, amount, inputPin))
					{
						using var context = new BankContext(new DbContextOptionsBuilder<BankContext>()
							.UseSqlite("Data Source=bank.db").Options);
						context.SaveChanges();

						Console.Clear();
						Console.WriteLine($"Withdrawal successful! Remaining balance: {_account.Balance}");
					}
					else
					{
						Console.Clear();
						Console.WriteLine("Insufficient balance or invalid PIN.");
					}
				}
				else
				{
					Console.Clear();
					Console.WriteLine("Invalid PIN format. Please enter numbers only.");
				}
			}
			else
			{
				Console.Clear();
				Console.WriteLine("Invalid amount. Please enter a positive number.");
			}
		}

		private void CheckBalance()
		{
			Console.Clear();
			Console.WriteLine($"Your current balance is: {_account.Balance}");
			Console.WriteLine($"Account Owner: {(_account as Account)?.OwnerName ?? "Unknown Owner"}"); // Fallback to Uknown if no name is fouuuund
		}
	}
}
