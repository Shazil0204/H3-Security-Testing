namespace ATMConsoleApp.Interfaces
{
    public interface IPinValidator
    {
        bool ValidatePin(string accountNumber, string pin);

    }
}