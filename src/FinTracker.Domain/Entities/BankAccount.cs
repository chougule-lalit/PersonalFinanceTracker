using FinTracker.Domain.Common;
using FinTracker.Domain.Entities.Auth;
using FinTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Domain.Entities
{
    public class BankAccount : FullAuditedEntity<Guid>
    {
        public string? AccountName { get; set; }
        public string? BankName { get; set; }
        public AccountType AccountType { get; set; }
        public decimal CurrentBalance { get; set; }
        public string? AccountNumber { get; set; }
        public Guid UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
