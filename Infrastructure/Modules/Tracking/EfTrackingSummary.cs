using Application.Modules.Tracking;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Modules.Tracking;

public sealed class EfTrackingSummary(RecipeBookDbContext dbContext) : ITrackingSummary
{
    public async Task<DailyNutritionSummary> GetTodayAsync(CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var settings = await dbContext.UserSettings.FirstOrDefaultAsync(cancellationToken) ?? new Domain.Modules.Tracking.UserSettings();
        var log = await dbContext.MealLogs
            .AsNoTracking()
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Date == today, cancellationToken);

        var items = log?.Items ?? [];

        return new DailyNutritionSummary(
            today,
            Calories: items.Sum(x => x.Calories),
            CalorieGoal: settings.DailyCalorieGoal,
            ProteinGrams: items.Sum(x => x.ProteinGrams),
            ProteinGoalGrams: settings.DailyProteinGoalGrams,
            CarbohydrateGrams: items.Sum(x => x.CarbohydrateGrams),
            CarbohydrateGoalGrams: settings.DailyCarbohydrateGoalGrams,
            FatGrams: items.Sum(x => x.FatGrams),
            FatGoalGrams: settings.DailyFatGoalGrams,
            LoggedItemCount: items.Count);
    }
}
