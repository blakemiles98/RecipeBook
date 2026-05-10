namespace Application.Modules.Tracking;

public sealed class TrackingSummaryPlaceholder : ITrackingSummary
{
    public Task<DailyNutritionSummary> GetTodayAsync(CancellationToken cancellationToken = default)
    {
        var summary = new DailyNutritionSummary(
            DateOnly.FromDateTime(DateTime.Today),
            Calories: 0,
            CalorieGoal: 2200,
            ProteinGrams: 0,
            ProteinGoalGrams: 160,
            CarbohydrateGrams: 0,
            CarbohydrateGoalGrams: 220,
            FatGrams: 0,
            FatGoalGrams: 70);

        return Task.FromResult(summary);
    }
}
