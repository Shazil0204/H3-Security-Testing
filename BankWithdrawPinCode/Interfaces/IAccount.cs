namespace BankWithdrawPinCode.Interfaces
{
	public interface IAccount
	{
		int PIN { get; }
		decimal Balance { get; }
		bool Withdraw(decimal amount);
		void Deposit(decimal amount);
	}
}