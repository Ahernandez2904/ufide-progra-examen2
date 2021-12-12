using Gadgets.Application.Contracts.Contexts;
using Gadgets.Application.Contracts.Data;
using Gadgets.Domain.Entities;
using Gadgets.Infrastructure.Contexts;
using Gadgets.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gadgets.Infrastructure
{
    public static class Injection
    {
        public static IServiceCollection RegisterInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
                (
                    option =>
                        option.UseSqlServer
                            (configuration.GetConnectionString("DefaultConnectionString"))
                );

            services.AddIdentity<User, IdentityRole>
                (
                    options =>
                    {
                        options.SignIn.RequireConfirmedAccount = false;
                        options.SignIn.RequireConfirmedEmail = false;

                        options.Password.RequiredLength = 6;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireDigit = true;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequiredUniqueChars = 1;
                    }
                )
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<IGadgetContext, GadgetContext>();

            return services;
        }
    }
}
