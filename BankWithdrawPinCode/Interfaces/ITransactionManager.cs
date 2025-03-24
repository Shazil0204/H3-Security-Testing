namespace BankWithdrawPinCode.Interfaces
{
	public interface ITransactionManager
	{
		bool Withdraw(IAccount account, decimal amount, int pin);
		bool Deposit(IAccount account, decimal amount, int pin);
	}
}
