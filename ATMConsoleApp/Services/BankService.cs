using ATMConsoleApp.Interfaces;
using ATMConsoleApp.Models;

namespace ATMConsoleApp.Services
{
    public class BankService : IBankService
    {
        private readonly Dictionary<string, BankAccount> _accounts;
        private readonly IPinValidator _pinValidator;

        public BankService(IPinValidator pinValidator)
        {
            _accounts = new Dictionary<string, BankAccount>();
            _pinValidator = pinValidator;
        }

        public void AddAccount(BankAccount account)
        {
            _accounts[account.AccountNumber] = account;
        }

        public bool WithdrawMoney(string accountNumber, decimal amount, string pin)
        {
            if (_accounts.ContainsKey(accountNumber) && _pinValidator.ValidatePin(accountNumber, pin))
            {
                return _accounts[accountNumber].Withdraw(amount);
            }
            return false;
        }

        public void DepositMoney(string accountNumber, decimal amount)
        {
            if (_accounts.ContainsKey(accountNumber))
            {
                _accounts[accountNumber].Deposit(amount);
            }
        }

        public decimal GetBalance(string accountNumber)
        {
            return _accounts.ContainsKey(accountNumber) ? _accounts[accountNumber].Balance : 0;
        }

    }
}