using Domain.Modules.Foods;

namespace Application.Modules.Foods;

public sealed class FoodEditor
{
    public string Name { get; set; } = string.Empty;

    public string? Brand { get; set; }

    public decimal ServingSize { get; set; } = 1;

    public string ServingUnit { get; set; } = "serving";

    public decimal Calories { get; set; }

    public decimal ProteinGrams { get; set; }

    public decimal CarbohydrateGrams { get; set; }

    public decimal FatGrams { get; set; }

    public decimal FiberGrams { get; set; }

    public decimal SugarGrams { get; set; }

    public decimal SodiumMilligrams { get; set; }

    public FoodSource Source { get; set; } = FoodSource.Manual;

    public string? ExternalId { get; set; }

    public NutritionFacts ToNutritionFacts()
    {
        return new NutritionFacts(
            Calories,
            ProteinGrams,
            CarbohydrateGrams,
            FatGrams,
            FiberGrams,
            SugarGrams,
            SodiumMilligrams);
    }

    public static FoodEditor FromFood(Food food)
    {
        return new FoodEditor
        {
            Name = food.Name,
            Brand = food.Brand,
            ServingSize = food.ServingSize,
            ServingUnit = food.ServingUnit,
            Calories = food.Nutrition.Calories,
            ProteinGrams = food.Nutrition.ProteinGrams,
            CarbohydrateGrams = food.Nutrition.CarbohydrateGrams,
            FatGrams = food.Nutrition.FatGrams,
            FiberGrams = food.Nutrition.FiberGrams,
            SugarGrams = food.Nutrition.SugarGrams,
            SodiumMilligrams = food.Nutrition.SodiumMilligrams,
            Source = food.Source,
            ExternalId = food.ExternalId
        };
    }

    public static FoodEditor FromSearchResult(FoodSearchResult result)
    {
        return new FoodEditor
        {
            Name = result.Name,
            Brand = result.Brand,
            ServingSize = result.ServingSize,
            ServingUnit = result.ServingUnit,
            Calories = result.Calories,
            ProteinGrams = result.ProteinGrams,
            CarbohydrateGrams = result.CarbohydrateGrams,
            FatGrams = result.FatGrams,
            FiberGrams = result.FiberGrams,
            SugarGrams = result.SugarGrams,
            SodiumMilligrams = result.SodiumMilligrams,
            Source = FoodSource.UsdaFoodDataCentral,
            ExternalId = result.ExternalId
        };
    }
}
