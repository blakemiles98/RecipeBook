using Domain.Modules.Recipes;

namespace Application.Modules.Recipes;

public interface IRecipeLibrary
{
    Task<IReadOnlyCollection<Recipe>> GetRecentRecipesAsync(CancellationToken cancellationToken = default);
}
