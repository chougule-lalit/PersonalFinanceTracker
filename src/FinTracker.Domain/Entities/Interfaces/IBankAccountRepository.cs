using FinTracker.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Domain.Entities.Interfaces
{
    public interface IBankAccountRepository : IGenericRepository<BankAccount>
    {
        Task<List<BankAccount>> GetUserAccountsAsync(Guid userId);
    }
}
