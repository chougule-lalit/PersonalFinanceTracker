using FinTracker.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Domain.Entities.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<List<Transaction>> GetUserTransactionsAsync(Guid userId);
        Task<List<Transaction>> GetAccountTransactionsAsync(Guid accountId);
        Task<List<Transaction>> GetCardTransactionsAsync(Guid cardId);
    }
}
