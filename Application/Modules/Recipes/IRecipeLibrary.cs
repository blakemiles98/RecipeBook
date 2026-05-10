using Domain.Modules.Recipes;

namespace Application.Modules.Recipes;

public interface IRecipeLibrary
{
    Task<IReadOnlyCollection<Recipe>> GetRecentRecipesAsync(CancellationToken cancellationToken = default);

    Task<Recipe?> GetRecipeAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Recipe> CreateRecipeAsync(RecipeEditor recipe, CancellationToken cancellationToken = default);

    Task<bool> UpdateRecipeAsync(Guid id, RecipeEditor recipe, CancellationToken cancellationToken = default);

    Task<bool> DeleteRecipeAsync(Guid id, CancellationToken cancellationToken = default);
}
