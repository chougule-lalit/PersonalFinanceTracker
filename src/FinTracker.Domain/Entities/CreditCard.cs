using FinTracker.Domain.Common;
using FinTracker.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Domain.Entities
{
    public class CreditCard : FullAuditedEntity<Guid>
    {
        public string? CardName { get; set; }
        public string? BankName { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime DueDate { get; set; }
        public string? LastFourDigits { get; set; }
        public Guid UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
