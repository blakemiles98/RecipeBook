using Application.Modules.Tracking;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Modules.Tracking;

public sealed class EfTrackingSummary(RecipeBookDbContext dbContext) : ITrackingSummary
{
    public async Task<DailyNutritionSummary> GetTodayAsync(CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var log = await dbContext.MealLogs
            .AsNoTracking()
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Date == today, cancellationToken);

        var items = log?.Items ?? [];

        return new DailyNutritionSummary(
            today,
            Calories: items.Sum(x => x.Calories),
            CalorieGoal: 2200,
            ProteinGrams: items.Sum(x => x.ProteinGrams),
            ProteinGoalGrams: 160,
            CarbohydrateGrams: items.Sum(x => x.CarbohydrateGrams),
            CarbohydrateGoalGrams: 220,
            FatGrams: items.Sum(x => x.FatGrams),
            FatGoalGrams: 70,
            LoggedItemCount: items.Count);
    }
}

