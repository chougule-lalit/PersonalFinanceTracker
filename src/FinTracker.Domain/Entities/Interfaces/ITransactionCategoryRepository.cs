using FinTracker.Domain.Common.Interfaces;
using FinTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Domain.Entities.Interfaces
{
    public interface ITransactionCategoryRepository : IGenericRepository<TransactionCategory>
    {
        Task<List<TransactionCategory>> GetUserCategoriesAsync(Guid? userId);
        Task<List<TransactionCategory>> GetCategoriesByTypeAsync(TransactionType type);
    }
}
