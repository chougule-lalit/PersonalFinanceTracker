using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Domain.Enums
{
    public enum AccountType
    {
        Savings,
        Current,
        CreditCard
    }

    public enum TransactionType
    {
        Income,
        Expense
    }
}
