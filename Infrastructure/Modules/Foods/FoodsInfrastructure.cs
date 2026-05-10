using Application.Modules.Foods;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Modules.Foods;

public static class FoodsInfrastructure
{
    public static IServiceCollection AddFoodStorage(
        this IServiceCollection services,
        IConfiguration configuration,
        string contentRootPath)
    {
        var connectionString = configuration.GetConnectionString("RecipeBook")
            ?? $"Data Source={Path.Combine(contentRootPath, "App_Data", "RecipeBook.db")}";

        connectionString = MakeSqlitePathAbsolute(connectionString, contentRootPath);
        var sqlitePath = GetSqlitePath(connectionString);
        if (!string.IsNullOrWhiteSpace(sqlitePath))
        {
            var directory = Path.GetDirectoryName(sqlitePath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        services.AddDbContext<RecipeBookDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<DatabaseInitializer>();
        services.AddScoped<IFoodCatalog, EfFoodCatalog>();

        return services;
    }

    private static string? GetSqlitePath(string connectionString)
    {
        const string dataSourcePrefix = "Data Source=";
        var parts = connectionString.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var dataSource = parts.FirstOrDefault(x => x.StartsWith(dataSourcePrefix, StringComparison.OrdinalIgnoreCase));

        return dataSource?[dataSourcePrefix.Length..];
    }

    private static string MakeSqlitePathAbsolute(string connectionString, string contentRootPath)
    {
        var sqlitePath = GetSqlitePath(connectionString);
        if (string.IsNullOrWhiteSpace(sqlitePath) || Path.IsPathRooted(sqlitePath) || sqlitePath.Equals(":memory:", StringComparison.OrdinalIgnoreCase))
        {
            return connectionString;
        }

        var absolutePath = Path.Combine(contentRootPath, sqlitePath);
        return connectionString.Replace($"Data Source={sqlitePath}", $"Data Source={absolutePath}", StringComparison.OrdinalIgnoreCase);
    }
}
