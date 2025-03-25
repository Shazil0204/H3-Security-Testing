using ATMConsoleApp.Interfaces;

namespace ATMConsoleApp.Controller
{
    public class BankController
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        public string Withdraw(string accountNumber, decimal amount, string pin)
        {
            return _bankService.WithdrawMoney(accountNumber, amount, pin)
                ? "Withdrawal successful."
                : "Withdrawal failed. Check balance or PIN.";
        }

        public string Deposit(string accountNumber, decimal amount)
        {
            _bankService.DepositMoney(accountNumber, amount);
            return "Deposit successful.";
        }

        public string GetBalance(string accountNumber)
        {
            return $"Current Balance: {_bankService.GetBalance(accountNumber)}";
        }

    }
}