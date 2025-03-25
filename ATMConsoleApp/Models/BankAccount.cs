namespace ATMConsoleApp.Models
{
    public class BankAccount
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; private set; }
        public string Pin { get; private set; }

        public BankAccount(string accountNumber, decimal initialBalance, string pin)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            Pin = pin;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
        }
    }
}