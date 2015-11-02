using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Transaction
    {
        public double Amount { get; private set; }

        public DateTime TransactionDate { get; private set; }

        public Transaction(double amount)
        {
            Amount = amount;
            TransactionDate = DateTime.Now;
        }

        /// <summary>
        /// This is used for testing
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="timeStamp"></param>
        public Transaction(double amount, DateTime timeStamp)
        {
            Amount = amount;
            TransactionDate = timeStamp;
        }

    }
}
