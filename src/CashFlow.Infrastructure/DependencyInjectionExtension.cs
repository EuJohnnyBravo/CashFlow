using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        AddRepositories(services);
        AddDbContext(services, config);
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
    }
    
    private static void AddDbContext(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Connection");;
        var serverVersion = new MySqlServerVersion(new Version(8,0,35));
        
        services.AddDbContext<CashFlowDbContext>(builder => 
            builder.UseMySql(connectionString, serverVersion));
    }
}