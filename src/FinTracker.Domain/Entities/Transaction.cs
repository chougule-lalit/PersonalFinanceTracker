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
    public class Transaction : FullAuditedEntity<Guid>
    {
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? BankAccountId { get; set; }
        public Guid? CreditCardId { get; set; }
        public Guid UserId { get; set; }
        public virtual TransactionCategory Category { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual BankAccount? BankAccount { get; set; }
        public virtual CreditCard? CreditCard { get; set; }
    }
}
