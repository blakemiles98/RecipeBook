using Application.Modules.Foods;
using Domain.Modules.Foods;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Modules.Foods;

public sealed class EfFoodCatalog(RecipeBookDbContext dbContext) : IFoodCatalog
{
    public async Task<IReadOnlyCollection<Food>> GetRecentFoodsAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Foods
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<Food?> GetFoodAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Foods
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Food> CreateFoodAsync(FoodEditor food, CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var entity = new Food
        {
            Name = food.Name.Trim(),
            Brand = NormalizeOptional(food.Brand),
            ServingSize = food.ServingSize,
            ServingUnit = food.ServingUnit.Trim(),
            Nutrition = food.ToNutritionFacts(),
            Source = food.Source,
            ExternalId = NormalizeOptional(food.ExternalId),
            CreatedAt = now,
            UpdatedAt = now
        };

        dbContext.Foods.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<bool> UpdateFoodAsync(Guid id, FoodEditor food, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Foods.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Name = food.Name.Trim();
        entity.Brand = NormalizeOptional(food.Brand);
        entity.ServingSize = food.ServingSize;
        entity.ServingUnit = food.ServingUnit.Trim();
        entity.Nutrition = food.ToNutritionFacts();
        entity.Source = food.Source;
        entity.ExternalId = NormalizeOptional(food.ExternalId);
        entity.UpdatedAt = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteFoodAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Foods.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        dbContext.Foods.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
