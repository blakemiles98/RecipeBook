using Domain.Modules.Recipes;

namespace Application.Modules.Recipes;

public interface IRecipeNutritionCalculator
{
    Task<RecipeNutritionSummary> CalculateTotalAsync(Recipe recipe, CancellationToken cancellationToken = default);
}
