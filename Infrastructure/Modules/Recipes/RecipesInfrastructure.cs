using Application.Modules.Recipes;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Modules.Recipes;

public static class RecipesInfrastructure
{
    public static IServiceCollection AddRecipeStorage(this IServiceCollection services)
    {
        services.AddScoped<IRecipeLibrary, EfRecipeLibrary>();
        services.AddScoped<IRecipeNutritionCalculator, EfRecipeNutritionCalculator>();

        return services;
    }
}
