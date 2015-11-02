using System;
using System.Collections.Generic;
using System.Text;

namespace AbcBank
{
    public class Customer
    {
        public String Name { get; private set; }
        public List<Account> Accounts { get; private set; }

        public Customer(String name)
        {
            Name = name;
            Accounts = new List<Account>();
        }

        public Customer openAccount(Account account)
        {
            Accounts.Add(account);
            return this;
        }

        public int getNumberOfAccounts()
        {
            return Accounts.Count;
        }

        public double totalInterestEarned()
        {
            double total = 0;
            foreach (Account a in Accounts)
                total += a.interestEarned();
            return total;
        }

        public void TransferFunds(Account sourceAccount, Account targetAccount, double amount)
        {
            if (sourceAccount.Balance() > amount)
            {
                sourceAccount.withdraw(amount);
                targetAccount.deposit(amount);
            }
            else
            {
                String msg = GetInsufficientFundsMessage(sourceAccount.Balance(), amount);

                throw new Exception(msg);
            }
        }

        private String GetInsufficientFundsMessage(double sourceBalance, double requestedAmount)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Insufficient funds in source account.");
            sb.AppendLine();
            sb.Append(String.Format("Your account balance is {0}", sourceBalance));
            sb.AppendLine();
            sb.Append(String.Format("You attempted to withdraw {0}", requestedAmount));

            return sb.ToString();
        }

        /// <summary>
        /// Returns a statement
        /// </summary>
        /// <returns>String</returns>
        public String getStatement()
        {
            //JIRA-123 Change by Joe Bloggs 29/7/1988 start
            String statement = null; //reset statement to null here
            //JIRA-123 Change by Joe Bloggs 29/7/1988 end
            statement = "Statement for " + Name + "\n";
            double total = 0.0;
            foreach (Account a in Accounts)
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.Balance();
            }
            statement += "\nTotal In All Accounts " + toDollars(total);
            return statement;
        }

        private String statementForAccount(Account a)
        {
            String s = "";

            //Translate to pretty account type
            switch (a.getAccountType())
            {
                case AccountType.Checking:
                    s += "Checking Account\n";
                    break;
                case AccountType.Savings:
                    s += "Savings Account\n";
                    break;
                case AccountType.MaxiSavings:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions)
            {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + toDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + toDollars(total);
            return s;
        }

        private String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }
    }
}
