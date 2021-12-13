using Gadgets.Application.Clients;
using Gadgets.Application.Configurations;
using Gadgets.Application.Contracts.Authentication;
using Gadgets.Application.Contracts.Clients;
using Gadgets.Application.Contracts.Logics;
using Gadgets.Application.Logics;
using Gadgets.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Gadgets.Application
{
    public static class Injection
    {
        public static IServiceCollection RegisterApplicationServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication
                (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
                    (
                        options =>
                        {
                            options.TokenValidationParameters =
                            new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = configuration["JwtConfiguration:Issuer"],
                                ValidAudience = configuration["JwtConfiguration:Audience"],
                                IssuerSigningKey =
                                    new SymmetricSecurityKey
                                        (Encoding.UTF8.GetBytes(configuration["JwtConfiguration:Key"]))
                            };
                        }
                    ).AddCookie
                        (
                            options =>
                            {
                                options.LoginPath = configuration["CookieConfiguration:LoginPage"];
                                options.LogoutPath = configuration["CookieConfiguration:LogoutPage"];
                                options.AccessDeniedPath = configuration["CookieConfiguration:AccessDeniedPage"];
                                options.ReturnUrlParameter = configuration["CookieConfiguration:ReturnUrlParameter"];
                            }
                        );

            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
            
            services.Configure<ClientConfiguration>(configuration.GetSection("ClientConfiguration"));
            services.AddHttpClient<IGadgetsClient, GadgetsClient>();
            services.AddHttpClient<IWeatherClient, WeatherClient>();

            IServiceCollection serviceCollection2 = services.AddTransient<IGadgetLogic, GadgetLogic>();
            IServiceCollection serviceCollection = services.AddTransient<IWeatherLogic, WeatherLogic>();
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
