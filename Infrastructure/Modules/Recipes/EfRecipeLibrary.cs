using Application.Modules.Recipes;
using Domain.Modules.Recipes;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Modules.Recipes;

public sealed class EfRecipeLibrary(RecipeBookDbContext dbContext) : IRecipeLibrary
{
    public async Task<IReadOnlyCollection<Recipe>> GetRecentRecipesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Recipes
            .AsNoTracking()
            .Include(x => x.Ingredients)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<Recipe?> GetRecipeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Recipes
            .AsNoTracking()
            .Include(x => x.Ingredients)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Recipe> CreateRecipeAsync(RecipeEditor recipe, CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var entity = new Recipe
        {
            Name = recipe.Name.Trim(),
            Description = NormalizeOptional(recipe.Description),
            Instructions = NormalizeOptional(recipe.Instructions),
            PrepMinutes = recipe.PrepMinutes,
            CookMinutes = recipe.CookMinutes,
            Servings = recipe.Servings,
            CreatedAt = now,
            UpdatedAt = now
        };

        AddIngredients(entity, recipe);

        dbContext.Recipes.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<bool> UpdateRecipeAsync(Guid id, RecipeEditor recipe, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Recipes
            .Include(x => x.Ingredients)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        entity.Name = recipe.Name.Trim();
        entity.Description = NormalizeOptional(recipe.Description);
        entity.Instructions = NormalizeOptional(recipe.Instructions);
        entity.PrepMinutes = recipe.PrepMinutes;
        entity.CookMinutes = recipe.CookMinutes;
        entity.Servings = recipe.Servings;
        entity.UpdatedAt = DateTimeOffset.UtcNow;

        entity.Ingredients.Clear();
        AddIngredients(entity, recipe);

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteRecipeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Recipes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        dbContext.Recipes.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static void AddIngredients(Recipe recipe, RecipeEditor editor)
    {
        var displayOrder = 0;

        foreach (var ingredient in editor.Ingredients.Where(x => x.FoodId != Guid.Empty && x.Quantity > 0))
        {
            recipe.Ingredients.Add(new RecipeIngredient
            {
                RecipeId = recipe.Id,
                FoodId = ingredient.FoodId,
                FoodName = ingredient.FoodName.Trim(),
                Quantity = ingredient.Quantity,
                Unit = ingredient.Unit.Trim(),
                DisplayOrder = displayOrder++
            });
        }
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}

