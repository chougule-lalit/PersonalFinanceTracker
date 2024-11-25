using AutoMapper;
using FinTracker.Application.Common.Models.Auth;
using FinTracker.Application.DTOs;
using FinTracker.Domain.Entities;
using FinTracker.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<RegisterRequest, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<CreateBankAccountDto, BankAccount>();
            CreateMap<UpdateBankAccountDto, BankAccount>();
            CreateMap<BankAccount, BankAccountDto>();

            CreateMap<CreateTransactionDto, Transaction>();
            CreateMap<UpdateTransactionDto, Transaction>();
            CreateMap<Transaction, TransactionDto>()
                .ForMember(d => d.CategoryName, opt =>
                    opt.MapFrom(s => s.Category.Name))
                .ForMember(d => d.AccountName, opt =>
                    opt.MapFrom(s => s.BankAccount != null ? $"{s.BankAccount.AccountName}-{s.BankAccount.BankName}" : s.CreditCard.CardName));

            CreateMap<CreateTransactionCategoryDto, TransactionCategory>();
            CreateMap<UpdateTransactionCategoryDto, TransactionCategory>();
            CreateMap<TransactionCategory, TransactionCategoryDto>();
        }
    }
}
