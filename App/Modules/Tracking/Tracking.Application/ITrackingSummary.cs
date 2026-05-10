namespace Tracking.Application;

public interface ITrackingSummary
{
    Task<DailyNutritionSummary> GetTodayAsync(CancellationToken cancellationToken = default);
}

