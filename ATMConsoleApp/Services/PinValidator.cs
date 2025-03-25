using ATMConsoleApp.Interfaces;

namespace ATMConsoleApp.Services
{
    public class PinValidator : IPinValidator
    {
        private readonly Dictionary<string, string> _accountPins;

        public PinValidator()
        {
            _accountPins = new Dictionary<string, string>();
        }

        public void SetPin(string accountNumber, string pin)
        {
            _accountPins[accountNumber] = pin;
        }

        public bool ValidatePin(string accountNumber, string pin)
        {
            return _accountPins.ContainsKey(accountNumber) && _accountPins[accountNumber] == pin;
        }
    }
}