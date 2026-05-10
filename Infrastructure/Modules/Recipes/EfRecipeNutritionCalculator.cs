using Application.Modules.Recipes;
using Domain.Modules.Recipes;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Modules.Recipes;

public sealed class EfRecipeNutritionCalculator(RecipeBookDbContext dbContext) : IRecipeNutritionCalculator
{
    public async Task<RecipeNutritionSummary> CalculateTotalAsync(Recipe recipe, CancellationToken cancellationToken = default)
    {
        if (recipe.Ingredients.Count == 0)
        {
            return new RecipeNutritionSummary(0, 0, 0, 0);
        }

        var foodIds = recipe.Ingredients.Select(x => x.FoodId).ToHashSet();
        var foods = await dbContext.Foods
            .AsNoTracking()
            .Where(x => foodIds.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        decimal calories = 0;
        decimal protein = 0;
        decimal carbs = 0;
        decimal fat = 0;

        foreach (var ingredient in recipe.Ingredients)
        {
            if (!foods.TryGetValue(ingredient.FoodId, out var food) || food.ServingSize <= 0)
            {
                continue;
            }

            var multiplier = ingredient.Quantity / food.ServingSize;
            calories += food.Nutrition.Calories * multiplier;
            protein += food.Nutrition.ProteinGrams * multiplier;
            carbs += food.Nutrition.CarbohydrateGrams * multiplier;
            fat += food.Nutrition.FatGrams * multiplier;
        }

        return new RecipeNutritionSummary(calories, protein, carbs, fat);
    }
}

