using AutoMapper;
using FinTracker.Application.DTOs;
using FinTracker.Application.Services.Interfaces;
using FinTracker.Domain.Common.Interfaces;
using FinTracker.Domain.Entities;
using FinTracker.Domain.Entities.Interfaces;
using FinTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public TransactionAppService(
            ITransactionRepository transactionRepository,
            IBankAccountRepository bankAccountRepository,
            ICreditCardRepository creditCardRepository,
            IMapper mapper,
            ICurrentUser currentUser)
        {
            _transactionRepository = transactionRepository;
            _bankAccountRepository = bankAccountRepository;
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<TransactionDto> CreateAsync(CreateTransactionDto input)
        {
            var transaction = _mapper.Map<Transaction>(input);
            transaction.UserId = _currentUser.Id;
            await UpdateAccountBalance(transaction);

            var createdTransaction = await _transactionRepository.AddAsync(transaction);
            return _mapper.Map<TransactionDto>(createdTransaction);
        }

        public async Task<TransactionDto> GetByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<List<TransactionDto>> GetUserTransactionsAsync(Guid userId)
        {
            var transactions = await _transactionRepository.GetUserTransactionsAsync(userId);
            return _mapper.Map<List<TransactionDto>>(transactions);
        }

        public async Task UpdateAsync(UpdateTransactionDto input)
        {
            var oldTransaction = await _transactionRepository.GetByIdAsync(input.Id);
            await ReverseBalanceUpdate(oldTransaction);

            _mapper.Map(input, oldTransaction);
            await UpdateAccountBalance(oldTransaction);
            await _transactionRepository.UpdateAsync(oldTransaction);
        }

        public async Task DeleteAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction != null)
            {
                await ReverseBalanceUpdate(transaction);
                await _transactionRepository.DeleteAsync(transaction);
            }
        }

        private async Task UpdateAccountBalance(Transaction transaction)
        {
            decimal amount = transaction.TransactionType == TransactionType.Income ? transaction.Amount : transaction.Amount * -1;

            if (transaction.BankAccountId.HasValue)
            {
                var account = await _bankAccountRepository.GetByIdAsync(transaction.BankAccountId.Value);
                account.CurrentBalance += amount;
                await _bankAccountRepository.UpdateAsync(account);
            }
            else if (transaction.CreditCardId.HasValue)
            {
                var card = await _creditCardRepository.GetByIdAsync(transaction.CreditCardId.Value);
                card.CurrentBalance += amount;
                await _creditCardRepository.UpdateAsync(card);
            }
        }

        private async Task ReverseBalanceUpdate(Transaction transaction)
        {
            decimal amount = transaction.TransactionType == TransactionType.Income ?
                -transaction.Amount : transaction.Amount;

            if (transaction.BankAccountId.HasValue)
            {
                var account = await _bankAccountRepository.GetByIdAsync(transaction.BankAccountId.Value);
                account.CurrentBalance += amount;
                await _bankAccountRepository.UpdateAsync(account);
            }
            else if (transaction.CreditCardId.HasValue)
            {
                var card = await _creditCardRepository.GetByIdAsync(transaction.CreditCardId.Value);
                card.CurrentBalance += amount;
                await _creditCardRepository.UpdateAsync(card);
            }
        }
    }
}
