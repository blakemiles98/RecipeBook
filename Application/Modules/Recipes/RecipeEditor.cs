using Domain.Modules.Recipes;

namespace Application.Modules.Recipes;

public sealed class RecipeEditor
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Instructions { get; set; }

    public int? PrepMinutes { get; set; }

    public int? CookMinutes { get; set; }

    public int Servings { get; set; } = 1;

    public List<RecipeIngredientEditor> Ingredients { get; set; } = [];

    public static RecipeEditor FromRecipe(Recipe recipe)
    {
        return new RecipeEditor
        {
            Name = recipe.Name,
            Description = recipe.Description,
            Instructions = recipe.Instructions,
            PrepMinutes = recipe.PrepMinutes,
            CookMinutes = recipe.CookMinutes,
            Servings = recipe.Servings,
            Ingredients = recipe.Ingredients
                .OrderBy(x => x.DisplayOrder)
                .Select(RecipeIngredientEditor.FromIngredient)
                .ToList()
        };
    }
}

