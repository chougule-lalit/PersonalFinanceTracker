using FinTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.DTOs
{
    public class CreateBankAccountDto
    {
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public AccountType AccountType { get; set; }
        public decimal InitialBalance { get; set; }
        public string AccountNumber { get; set; }
    }
}
