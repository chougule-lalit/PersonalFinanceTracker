using FinTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.Services.Interfaces
{
    public interface IBankAccountAppService
    {
        Task<BankAccountDto> CreateAsync(CreateBankAccountDto input);
        Task<BankAccountDto> GetByIdAsync(Guid id);
        Task<List<BankAccountDto>> GetUserAccountsAsync(Guid userId);
        Task UpdateAsync(UpdateBankAccountDto input);
        Task DeleteAsync(Guid id);
    }
}
