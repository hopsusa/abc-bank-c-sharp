using System;
using System.Collections.Generic;

namespace AbcBank
{
    public enum AccountType
    {
        Checking = 0,
        Savings = 1,
        MaxiSavings = 2,
        TenDayMaxiSavings = 3
    }
    public class Account
    {
        public AccountType AccountType { get; private set; }
        public List<Transaction> transactions;
        private const String KGreaterThanZero = "amount must be greater than zero";

        public Account(AccountType accountType)
        {
            AccountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void deposit(double amount)
        {
            deposit(amount, DateTime.Now);
        }

        public void deposit(double amount, DateTime timeStamp)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(KGreaterThanZero);
            }
            else
            {
                transactions.Add(new Transaction(amount, timeStamp));
            }
        }

        public void withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(KGreaterThanZero);
            }
            else
            {
                transactions.Add(new Transaction(-amount));
            }
        }

        public double interestEarned()
        {
            double amount = Balance();
            switch (AccountType)
            {
                case AccountType.Savings:
                    return CalculateSavingsInterestEarned(amount);
                case AccountType.MaxiSavings:
                    return CalculateMaxiSavingsInterestEarned(amount);
                case AccountType.TenDayMaxiSavings:
                    return TenDayMaxiSavingsInterestEarned(amount);
                default:
                    return amount * 0.001;
            }
        }

        public double CalculateSavingsInterestEarned(double amount)
        {
            if (amount <= 1000)
                return amount*0.001;
            else
                return 1 + (amount - 1000)*0.002;
        }

        public double CalculateMaxiSavingsInterestEarned(double amount)
        {
            if (amount <= 1000)
                return amount * 0.02;
            if (amount <= 2000)
                return 20 + (amount - 1000) * 0.05;
            return 70 + (amount - 2000) * 0.1;
        }

        public double TenDayMaxiSavingsInterestEarned(double amount)
        {
            AccountState accountState = GetAccountState();

            if (accountState.HasTransactionInLast_N_Days(10))
                return amount * 0.001;
            else
                return amount * 0.05;
        }

        public double Balance()
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.Amount;
            return amount;
        }

        public AccountState GetAccountState()
        {
            AccountState state = new AccountState();
            foreach (Transaction t in transactions)
            {
                state.CurrentBalance += t.Amount;

                if (t.TransactionDate.CompareTo(state.LastTransactionDate) > 0)
                {
                    state.LastTransactionDate = t.TransactionDate;
                }
            }
            return state;
        }

        public AccountType getAccountType()
        {
            return AccountType;
        }

    }
}
