using FinTracker.Domain.Entities;
using FinTracker.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Infrastructure.Data.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Transaction>> GetUserTransactionsAsync(Guid userId)
        {
            return await _dbSet
                .Where(x => x.UserId == userId)
                .Include(x => x.Category)
                .Include(x=> x.BankAccount)
                .Include(x=> x.CreditCard)
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetAccountTransactionsAsync(Guid accountId)
        {
            return await _dbSet
                .Where(x => x.BankAccountId == accountId)
                .Include(x => x.Category)
                .Include(x => x.BankAccount)
                .Include(x => x.CreditCard)
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetCardTransactionsAsync(Guid cardId)
        {
            return await _dbSet
                .Where(x => x.CreditCardId == cardId)
                .Include(x => x.Category)
                .ToListAsync();
        }
    }
}
