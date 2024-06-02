namespace CreditcardSystem.Infra.Persistence;

using CreditcardSystem.Application.Dtos.Services;
using CreditcardSystem.Application.Repositories;
using CreditcardSystem.Application.Services;
using CreditcardSystem.Domain.Models;
using CreditcardSystem.Infra.Data;
using CreditcardSystem.Infra.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureInfraServices
{
    public static void ConfigureInfra(this IServiceCollection services)
    {
        services.AddScoped<CredicardDataContext>();
        services.AddTransient<UserService>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<CreditcardService>();
        services.AddTransient<ICreditcardRepository, CreditcardRepository>();
        services.AddTransient<AuthService>();
    }
}
