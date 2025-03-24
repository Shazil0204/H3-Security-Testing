using BankWithdrawPinCode.Interfaces;

namespace BankWithdrawPinCode.Models
{
	public class Account : IAccount
	{
		public int Id { get; set; }
		public int PIN { get; private set; }
		public decimal Balance { get; private set; }
		public string OwnerName { get; set; }

		// Parameterless constructor for EF Core
		public Account()
		{
			OwnerName = "Unknown"; // Default value to avoid null issues
		}

		// Main constructor for initializing an Account object
		public Account(int pin, decimal initialBalance, string ownerName)
		{
			PIN = pin;
			Balance = initialBalance;
			OwnerName = ownerName ?? "Unknown"; // Fallback to default if ownerName is null
		}

		public bool Withdraw(decimal amount)
		{
			if (amount > 0 && Balance >= amount)
			{
				Balance -= amount;
				return true;
			}
			return false;
		}

		public void Deposit(decimal amount)
		{
			if (amount > 0)
			{
				Balance += amount;
			}
		}
	}
}
