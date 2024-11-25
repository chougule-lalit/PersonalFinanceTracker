using FinTracker.Application.DTOs;
using FinTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.Services.Interfaces
{
    public interface ITransactionCategoryAppService
    {
        Task<TransactionCategoryDto> CreateAsync(CreateTransactionCategoryDto input);
        Task<TransactionCategoryDto> GetByIdAsync(Guid id);
        Task<List<TransactionCategoryDto>> GetUserCategoriesAsync(Guid userId);
        Task<List<TransactionCategoryDto>> GetCategoriesByTypeAsync(TransactionType type);
        Task UpdateAsync(UpdateTransactionCategoryDto input);
        Task DeleteAsync(Guid id);
    }
}
