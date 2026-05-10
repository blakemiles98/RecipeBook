using Foods.Domain;

namespace Foods.Application;

public interface IFoodCatalog
{
    Task<IReadOnlyCollection<Food>> GetRecentFoodsAsync(CancellationToken cancellationToken = default);
}

