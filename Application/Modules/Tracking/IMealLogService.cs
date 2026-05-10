using Domain.Modules.Tracking;

namespace Application.Modules.Tracking;

public interface IMealLogService
{
    Task<MealLog> GetOrCreateAsync(DateOnly date, CancellationToken cancellationToken = default);

    Task<MealLogItem> AddItemAsync(MealLogEditor item, CancellationToken cancellationToken = default);

    Task<bool> DeleteItemAsync(Guid itemId, CancellationToken cancellationToken = default);
}

