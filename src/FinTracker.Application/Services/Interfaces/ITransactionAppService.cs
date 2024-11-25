using FinTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.Services.Interfaces
{
    public interface ITransactionAppService
    {
        Task<TransactionDto> CreateAsync(CreateTransactionDto input);
        Task<TransactionDto> GetByIdAsync(Guid id);
        Task<List<TransactionDto>> GetUserTransactionsAsync(Guid userId);
        Task UpdateAsync(UpdateTransactionDto input);
        Task DeleteAsync(Guid id);
    }
}
