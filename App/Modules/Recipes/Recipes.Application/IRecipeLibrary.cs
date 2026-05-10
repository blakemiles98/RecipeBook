using Recipes.Domain;

namespace Recipes.Application;

public interface IRecipeLibrary
{
    Task<IReadOnlyCollection<Recipe>> GetRecentRecipesAsync(CancellationToken cancellationToken = default);
}

