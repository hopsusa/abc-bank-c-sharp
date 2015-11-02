using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class AccountState
    {
        public double CurrentBalance { get; set; }
        public DateTime LastTransactionDate { get; set; }

        public bool HasTransactionInLast_N_Days(int days)
        {
            DateTime priorNDays = DateTime.Now.Subtract(new TimeSpan(days, 0, 0, 0));

            return LastTransactionDate.CompareTo(priorNDays) > 0;
        }
    }
}
