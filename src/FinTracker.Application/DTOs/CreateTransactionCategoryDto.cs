using FinTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.DTOs
{
    public class CreateTransactionCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
    }
}
