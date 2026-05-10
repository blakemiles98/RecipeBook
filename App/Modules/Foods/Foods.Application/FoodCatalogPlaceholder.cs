using Foods.Domain;

namespace Foods.Application;

public sealed class FoodCatalogPlaceholder : IFoodCatalog
{
    public Task<IReadOnlyCollection<Food>> GetRecentFoodsAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<Food> foods =
        [
            new Food
            {
                Name = "Chicken breast",
                Brand = "Example",
                ServingSize = 100,
                ServingUnit = "g",
                Nutrition = new NutritionFacts(165, 31, 0, 3.6m, 0, 0, 74)
            },
            new Food
            {
                Name = "Cooked white rice",
                ServingSize = 1,
                ServingUnit = "cup",
                Nutrition = new NutritionFacts(205, 4.3m, 45, 0.4m, 0.6m, 0.1m, 2)
            }
        ];

        return Task.FromResult(foods);
    }
}
