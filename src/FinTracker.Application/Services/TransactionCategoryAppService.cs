using AutoMapper;
using FinTracker.Application.DTOs;
using FinTracker.Application.Services.Interfaces;
using FinTracker.Domain.Entities.Interfaces;
using FinTracker.Domain.Entities;
using FinTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinTracker.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinTracker.Application.Services
{
    public class TransactionCategoryAppService : ITransactionCategoryAppService
    {
        private readonly ITransactionCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public TransactionCategoryAppService(
            ITransactionCategoryRepository categoryRepository,
            IMapper mapper,
            ICurrentUser currentUser)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<TransactionCategoryDto> CreateAsync(CreateTransactionCategoryDto input)
        {
            var category = _mapper.Map<TransactionCategory>(input);
            category.UserId = _currentUser.Id;
            var createdCategory = await _categoryRepository.AddAsync(category);
            return _mapper.Map<TransactionCategoryDto>(createdCategory);
        }

        public async Task<TransactionCategoryDto> GetByIdAsync(Guid id)
        {
            var catRepo = _categoryRepository.GetQueryable();
            var category = await catRepo.Where(x => x.UserId == _currentUser.Id && x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<TransactionCategoryDto>(category);
        }

        public async Task<List<TransactionCategoryDto>> GetUserCategoriesAsync(Guid userId)
        {
            var categories = await _categoryRepository.GetUserCategoriesAsync(userId);
            return _mapper.Map<List<TransactionCategoryDto>>(categories);
        }

        public async Task<List<TransactionCategoryDto>> GetCategoriesByTypeAsync(TransactionType type)
        {
            var catRepo = _categoryRepository.GetQueryable();
            var categories = await catRepo.Where(x => x.UserId == _currentUser.Id && x.Type == type).ToListAsync();
            return _mapper.Map<List<TransactionCategoryDto>>(categories);
        }

        public async Task UpdateAsync(UpdateTransactionCategoryDto input)
        {
            var catRepo = _categoryRepository.GetQueryable();
            var category = await catRepo.Where(x => x.UserId == _currentUser.Id && x.Id == input.Id).FirstOrDefaultAsync();
            _mapper.Map(input, category);
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var catRepo = _categoryRepository.GetQueryable();
            var category = await catRepo.Where(x => x.UserId == _currentUser.Id && x.Id == id).FirstOrDefaultAsync();
            if (category != null && !category.IsDefault)
            {
                await _categoryRepository.DeleteAsync(category);
            }
        }
    }
}
