using Domain.Modules.Tracking;

namespace Application.Modules.Tracking;

public interface IGoalSettingsService
{
    Task<UserSettings> GetAsync(CancellationToken cancellationToken = default);

    Task<UserSettings> SaveAsync(GoalSettingsEditor settings, CancellationToken cancellationToken = default);
}

