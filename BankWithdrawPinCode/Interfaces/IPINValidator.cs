namespace BankWithdrawPinCode.Interfaces
{
	public interface IPINValidator
	{
		bool ValidatePIN(int inputPIN, int actualPIN);
	}
}