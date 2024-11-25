using AutoMapper;
using FinTracker.Application.DTOs;
using FinTracker.Application.Services.Interfaces;
using FinTracker.Domain.Entities.Interfaces;
using FinTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinTracker.Domain.Common.Interfaces;

namespace FinTracker.Application.Services
{
    public class BankAccountAppService : IBankAccountAppService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public BankAccountAppService(
            IBankAccountRepository bankAccountRepository,
            ITransactionRepository transactionRepository,
            IMapper mapper,
            ICurrentUser currentUser)
        {
            _bankAccountRepository = bankAccountRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BankAccountDto> CreateAsync(CreateBankAccountDto input)
        {
            var account = _mapper.Map<BankAccount>(input);
            account.UserId = _currentUser.Id;
            account.CurrentBalance = input.InitialBalance;

            var createdAccount = await _bankAccountRepository.AddAsync(account);
            return _mapper.Map<BankAccountDto>(createdAccount);
        }

        public async Task<BankAccountDto> GetByIdAsync(Guid id)
        {
            var account = await _bankAccountRepository.GetByIdAsync(id);
            return _mapper.Map<BankAccountDto>(account);
        }

        public async Task<List<BankAccountDto>> GetUserAccountsAsync(Guid userId)
        {
            var accounts = await _bankAccountRepository.GetUserAccountsAsync(userId);
            return _mapper.Map<List<BankAccountDto>>(accounts);
        }

        public async Task UpdateAsync(UpdateBankAccountDto input)
        {
            var account = await _bankAccountRepository.GetByIdAsync(input.Id);
            _mapper.Map(input, account);
            await _bankAccountRepository.UpdateAsync(account);
        }

        public async Task DeleteAsync(Guid id)
        {
            var account = await _bankAccountRepository.GetByIdAsync(id);
            if (account != null)
            {
                await _bankAccountRepository.DeleteAsync(account);
            }
        }
    }
}
