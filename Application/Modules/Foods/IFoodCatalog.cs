using Domain.Modules.Foods;

namespace Application.Modules.Foods;

public interface IFoodCatalog
{
    Task<IReadOnlyCollection<Food>> GetRecentFoodsAsync(CancellationToken cancellationToken = default);

    Task<Food?> GetFoodAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Food> CreateFoodAsync(FoodEditor food, CancellationToken cancellationToken = default);

    Task<bool> UpdateFoodAsync(Guid id, FoodEditor food, CancellationToken cancellationToken = default);

    Task<bool> DeleteFoodAsync(Guid id, CancellationToken cancellationToken = default);
}
