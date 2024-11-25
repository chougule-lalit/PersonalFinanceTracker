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
    public class TransactionCategory : FullAuditedEntity<Guid>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TransactionType Type { get; set; }
        public bool IsDefault { get; set; }
        public Guid? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
