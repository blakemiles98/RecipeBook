using Application.Modules.Tracking;
using Domain.Modules.Tracking;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Modules.Tracking;

public sealed class EfGoalSettingsService(RecipeBookDbContext dbContext) : IGoalSettingsService
{
    public async Task<UserSettings> GetAsync(CancellationToken cancellationToken = default)
    {
        var settings = await dbContext.UserSettings.FirstOrDefaultAsync(cancellationToken);

        if (settings is not null)
        {
            return settings;
        }

        settings = new UserSettings();
        dbContext.UserSettings.Add(settings);
        await dbContext.SaveChangesAsync(cancellationToken);

        return settings;
    }

    public async Task<UserSettings> SaveAsync(GoalSettingsEditor settings, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(cancellationToken);

        entity.DailyCalorieGoal = settings.DailyCalorieGoal;
        entity.DailyProteinGoalGrams = settings.DailyProteinGoalGrams;
        entity.DailyCarbohydrateGoalGrams = settings.DailyCarbohydrateGoalGrams;
        entity.DailyFatGoalGrams = settings.DailyFatGoalGrams;
        entity.PreferredUnits = string.IsNullOrWhiteSpace(settings.PreferredUnits) ? "US" : settings.PreferredUnits.Trim();
        entity.UpdatedAt = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }
}

