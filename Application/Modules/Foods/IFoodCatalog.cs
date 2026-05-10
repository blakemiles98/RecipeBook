using Domain.Modules.Foods;

namespace Application.Modules.Foods;

public interface IFoodCatalog
{
    Task<IReadOnlyCollection<Food>> GetRecentFoodsAsync(CancellationToken cancellationToken = default);
}
