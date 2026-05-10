namespace Application.Modules.Recipes;

public sealed record RecipeNutritionSummary(
    decimal Calories,
    decimal ProteinGrams,
    decimal CarbohydrateGrams,
    decimal FatGrams);

