using Domain.Modules.Recipes;

namespace Application.Modules.Recipes;

public sealed class RecipeLibraryPlaceholder : IRecipeLibrary
{
    public Task<IReadOnlyCollection<Recipe>> GetRecentRecipesAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<Recipe> recipes =
        [
            new Recipe
            {
                Name = "Chicken burrito bowl",
                Description = "A sample recipe to anchor the first UI flow.",
                Servings = 4,
                PrepMinutes = 15,
                CookMinutes = 25
            }
        ];

        return Task.FromResult(recipes);
    }

    public Task<Recipe?> GetRecipeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<Recipe?>(null);
    }

    public Task<Recipe> CreateRecipeAsync(RecipeEditor recipe, CancellationToken cancellationToken = default)
    {
        var savedRecipe = new Recipe
        {
            Name = recipe.Name,
            Description = recipe.Description,
            Instructions = recipe.Instructions,
            PrepMinutes = recipe.PrepMinutes,
            CookMinutes = recipe.CookMinutes,
            Servings = recipe.Servings
        };

        return Task.FromResult(savedRecipe);
    }

    public Task<bool> UpdateRecipeAsync(Guid id, RecipeEditor recipe, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(false);
    }

    public Task<bool> DeleteRecipeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(false);
    }
}
