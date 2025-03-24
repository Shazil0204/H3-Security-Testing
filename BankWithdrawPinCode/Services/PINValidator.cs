using BankWithdrawPinCode.Interfaces;

namespace BankWithdrawPinCode.Services
{
    public class PINValidator : IPINValidator
    {
        public bool ValidatePIN(int inputPIN, int actualPIN)
        {
            return inputPIN == actualPIN;
        }
    }
}