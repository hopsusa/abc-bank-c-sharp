﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Bank
    {
        private List<Customer> customers;

        public Bank()
        {
            customers = new List<Customer>();
        }

        public void addCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public String customerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += string.Format("\n - {0} ({1})", c.Name, format(c.getNumberOfAccounts(), "account"));
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double totalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in customers)
                total += c.totalInterestEarned();
            return total;
        }

        public String getFirstCustomer()
        {
            try
            {
                customers = null;
                return customers[0].Name;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "Error";
            }
        }
    }
}
