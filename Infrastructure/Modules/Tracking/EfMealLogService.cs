using Application.Modules.Tracking;
using Domain.Modules.Tracking;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Modules.Tracking;

public sealed class EfMealLogService(RecipeBookDbContext dbContext) : IMealLogService
{
    public async Task<MealLog> GetOrCreateAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        var log = await dbContext.MealLogs
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Date == date, cancellationToken);

        if (log is not null)
        {
            return log;
        }

        var now = DateTimeOffset.UtcNow;
        log = new MealLog
        {
            Date = date,
            CreatedAt = now,
            UpdatedAt = now
        };

        dbContext.MealLogs.Add(log);
        await dbContext.SaveChangesAsync(cancellationToken);

        return log;
    }

    public async Task<MealLogItem> AddItemAsync(MealLogEditor item, CancellationToken cancellationToken = default)
    {
        var log = await GetOrCreateAsync(item.Date, cancellationToken);
        var entity = new MealLogItem
        {
            MealLogId = log.Id,
            MealGroup = item.MealGroup,
            FoodId = item.FoodId,
            RecipeId = item.RecipeId,
            QuickItemName = NormalizeOptional(item.QuickItemName),
            Quantity = item.Quantity,
            Unit = item.Unit.Trim(),
            Calories = item.Calories,
            ProteinGrams = item.ProteinGrams,
            CarbohydrateGrams = item.CarbohydrateGrams,
            FatGrams = item.FatGrams,
            CreatedAt = DateTimeOffset.UtcNow
        };

        log.Items.Add(entity);
        log.UpdatedAt = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteItemAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        var item = await dbContext.MealLogItems.FirstOrDefaultAsync(x => x.Id == itemId, cancellationToken);

        if (item is null)
        {
            return false;
        }

        dbContext.MealLogItems.Remove(item);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}

