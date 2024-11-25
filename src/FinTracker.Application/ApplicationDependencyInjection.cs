using FinTracker.Application.Services.Interfaces;
using FinTracker.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IBankAccountAppService, BankAccountAppService>();
            services.AddScoped<ITransactionAppService, TransactionAppService>();
            services.AddScoped<ITransactionCategoryAppService, TransactionCategoryAppService>();

            return services;
        }
    }
}
