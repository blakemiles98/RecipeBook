using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Application.Modules.Foods;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Modules.Foods;

public sealed class UsdaFoodDataCentralClient(HttpClient httpClient, IConfiguration configuration) : IFoodSearchProvider
{
    private const string DemoApiKey = "DEMO_KEY";

    public async Task<IReadOnlyCollection<FoodSearchResult>> SearchAsync(string query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return [];
        }

        var apiKey = configuration["Nutrition:Usda:ApiKey"] ?? DemoApiKey;
        var requestUri = $"foods/search?api_key={Uri.EscapeDataString(apiKey)}&query={Uri.EscapeDataString(query.Trim())}&pageSize=15";
        var response = await httpClient.GetFromJsonAsync<UsdaSearchResponse>(requestUri, cancellationToken);

        return response?.Foods
            .Where(food => food.FdcId is not null && !string.IsNullOrWhiteSpace(food.Description))
            .Select(MapSearchResult)
            .ToList() ?? [];
    }

    private static FoodSearchResult MapSearchResult(UsdaFoodSearchItem food)
    {
        var servingSize = food.ServingSize is > 0 ? food.ServingSize.Value : 100;
        var servingUnit = string.IsNullOrWhiteSpace(food.ServingSizeUnit) ? "g" : food.ServingSizeUnit;

        return new FoodSearchResult(
            SourceProvider: "USDA FoodData Central",
            ExternalId: food.FdcId?.ToString() ?? string.Empty,
            Name: food.Description ?? "USDA food",
            Brand: food.BrandOwner ?? food.BrandName,
            ServingSize: servingSize,
            ServingUnit: servingUnit,
            Calories: GetNutrient(food, "Energy", "KCAL"),
            ProteinGrams: GetNutrient(food, "Protein"),
            CarbohydrateGrams: GetNutrient(food, "Carbohydrate, by difference"),
            FatGrams: GetNutrient(food, "Total lipid (fat)"),
            FiberGrams: GetNutrient(food, "Fiber, total dietary"),
            SugarGrams: GetNutrient(food, "Sugars, total including NLEA", fallbackName: "Sugars, total"),
            SodiumMilligrams: GetNutrient(food, "Sodium, Na"));
    }

    private static decimal GetNutrient(
        UsdaFoodSearchItem food,
        string nutrientName,
        string? unitName = null,
        string? fallbackName = null)
    {
        var nutrient = food.FoodNutrients.FirstOrDefault(nutrient =>
            Matches(nutrient, nutrientName, unitName) ||
            (fallbackName is not null && Matches(nutrient, fallbackName, unitName)));

        return nutrient?.Value ?? 0;
    }

    private static bool Matches(UsdaFoodNutrient nutrient, string nutrientName, string? unitName)
    {
        var nameMatches = string.Equals(nutrient.NutrientName, nutrientName, StringComparison.OrdinalIgnoreCase);
        var unitMatches = unitName is null || string.Equals(nutrient.UnitName, unitName, StringComparison.OrdinalIgnoreCase);

        return nameMatches && unitMatches;
    }

    private sealed class UsdaSearchResponse
    {
        [JsonPropertyName("foods")]
        public List<UsdaFoodSearchItem> Foods { get; set; } = [];
    }

    private sealed class UsdaFoodSearchItem
    {
        [JsonPropertyName("fdcId")]
        public int? FdcId { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("brandOwner")]
        public string? BrandOwner { get; set; }

        [JsonPropertyName("brandName")]
        public string? BrandName { get; set; }

        [JsonPropertyName("servingSize")]
        public decimal? ServingSize { get; set; }

        [JsonPropertyName("servingSizeUnit")]
        public string? ServingSizeUnit { get; set; }

        [JsonPropertyName("foodNutrients")]
        public List<UsdaFoodNutrient> FoodNutrients { get; set; } = [];
    }

    private sealed class UsdaFoodNutrient
    {
        [JsonPropertyName("nutrientName")]
        public string? NutrientName { get; set; }

        [JsonPropertyName("unitName")]
        public string? UnitName { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
