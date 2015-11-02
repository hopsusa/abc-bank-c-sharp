using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class AccountTest
    {
        [Test]
        public void TestMaxiSavingsInterest()
        {
            Account maxiSavings = new Account(AccountType.MaxiSavings);
            maxiSavings.deposit(500);
            Assert.AreEqual(maxiSavings.interestEarned(), 10);

            maxiSavings.deposit(1000);

            Assert.AreEqual(maxiSavings.interestEarned(), 45);

            maxiSavings.deposit(1000);

            Assert.AreEqual(maxiSavings.interestEarned(), 120);

            maxiSavings.deposit(1000);

            Assert.AreEqual(maxiSavings.interestEarned(), 220);
        }

        [Test]
        public void TestSavingsInterest()
        {
            Account savings = new Account(AccountType.Savings);
            savings.deposit(500);
            Assert.AreEqual(savings.interestEarned(), 0.5);

            savings.deposit(1000);
            Assert.AreEqual(savings.interestEarned(), 2.0);
        }

        [Test]
        public void TestTenDayMaxiSavingsInterest()
        {
            Account tenDayMaxiSavings = new Account(AccountType.TenDayMaxiSavings);

            tenDayMaxiSavings.deposit(1000);

            Assert.AreEqual(tenDayMaxiSavings.interestEarned(), 1);

        }

        [Test]
        public void TestTenDayMaxiSavingsInterestAfterTenDays()
        {
            Account tenDayMaxiSavings = new Account(AccountType.TenDayMaxiSavings);

            tenDayMaxiSavings.deposit(1000, DateTime.Now.Subtract(new TimeSpan(11,0,0,0)));

            Assert.AreEqual(tenDayMaxiSavings.interestEarned(), 50);

        }
    }
}
