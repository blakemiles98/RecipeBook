namespace Application.Modules.Tracking;

public interface ITrackingSummary
{
    Task<DailyNutritionSummary> GetTodayAsync(CancellationToken cancellationToken = default);
}
