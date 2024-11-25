using FinTracker.Domain.Entities;
using FinTracker.Domain.Entities.Interfaces;
using FinTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Infrastructure.Data.Repositories
{
    public class TransactionCategoryRepository : GenericRepository<TransactionCategory>, ITransactionCategoryRepository
    {
        public TransactionCategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<TransactionCategory>> GetUserCategoriesAsync(Guid? userId)
        {
            return await _dbSet
                .Where(x => x.UserId == userId || x.IsDefault)
                .ToListAsync();
        }

        public async Task<List<TransactionCategory>> GetCategoriesByTypeAsync(TransactionType type)
        {
            return await _dbSet
                .Where(x => x.Type == type)
                .ToListAsync();
        }
    }
}
