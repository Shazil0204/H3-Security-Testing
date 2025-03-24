using BankWithdrawPinCode.Interfaces;

namespace BankWithdrawPinCode.Services
{
	public class TransactionManager : ITransactionManager
	{
		private readonly IPINValidator _pinValidator;

		// Constructor Dependency Injection for IPINValidator
		public TransactionManager(IPINValidator pinValidator)
		{
			_pinValidator = pinValidator;
		}

		public bool Withdraw(IAccount account, decimal amount, int pin)
		{
			if (_pinValidator.ValidatePIN(pin, account.PIN))
			{
				return account.Withdraw(amount);
			}
			return false;
		}

		public bool Deposit(IAccount account, decimal amount, int pin)
		{
			if (_pinValidator.ValidatePIN(pin, account.PIN))
			{
				account.Deposit(amount);
				return true;
			}
			return false;
		}
	}
}
