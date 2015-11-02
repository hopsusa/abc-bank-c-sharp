using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {

        [Test] //Test customer statement generation
        public void testApp()
        {
            Customer henry = SetupCustomerWithTwoAccounts();

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", henry.getStatement());
        }

        private Customer SetupCustomerWithTwoAccounts()
        {
            Account checkingAccount = new Account(AccountType.Checking);
            Account savingsAccount = new Account(AccountType.Savings);

            Customer henry = new Customer("Henry").openAccount(checkingAccount).openAccount(savingsAccount);

            checkingAccount.deposit(100.0);
            savingsAccount.deposit(4000.0);
            savingsAccount.withdraw(200.0);
            return henry;
        }

        [Test]
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(AccountType.Savings));
            Assert.AreEqual(1, oscar.getNumberOfAccounts());
        }

        [Test]
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(AccountType.Savings));
            oscar.openAccount(new Account(AccountType.Checking));
            Assert.AreEqual(2, oscar.getNumberOfAccounts());
        }

        [Ignore]
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(AccountType.Savings));
            oscar.openAccount(new Account(AccountType.Checking));
            Assert.AreEqual(3, oscar.getNumberOfAccounts());
        }

        [Test]
        public void TestTransfer()
        {
            Customer henry = SetupCustomerWithTwoAccounts();

            henry.TransferFunds(henry.Accounts[0], henry.Accounts[1], 50);
            Assert.AreEqual(henry.Accounts[0].Balance(), 50);
            Assert.AreEqual(henry.Accounts[1].Balance(), 3850);
        }

        [Test]
        public void TestFailedTransfer()
        {
            Customer henry = SetupCustomerWithTwoAccounts();

            try
            {
                henry.TransferFunds(henry.Accounts[0], henry.Accounts[1], 200);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Insufficient funds in source account.\r\nYour account balance is 100\r\nYou attempted to withdraw 200");
            }
        }
    }
}
