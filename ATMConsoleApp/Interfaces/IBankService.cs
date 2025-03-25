namespace ATMConsoleApp.Interfaces
{
    public interface IBankService
    {
        bool WithdrawMoney(string accountNumber, decimal amount, string pin);
        void DepositMoney(string accountNumber, decimal amount);
        decimal GetBalance(string accountNumber);

    }
}