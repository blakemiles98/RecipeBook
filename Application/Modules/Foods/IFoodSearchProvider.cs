namespace Application.Modules.Foods;

public interface IFoodSearchProvider
{
    Task<IReadOnlyCollection<FoodSearchResult>> SearchAsync(string query, CancellationToken cancellationToken = default);
}

