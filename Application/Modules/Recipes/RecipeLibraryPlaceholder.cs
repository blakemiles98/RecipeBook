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
}
