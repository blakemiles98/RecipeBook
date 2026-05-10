namespace Application.Modules.Foods;

public sealed record FoodSearchResult(
    string SourceProvider,
    string ExternalId,
    string Name,
    string? Brand,
    decimal ServingSize,
    string ServingUnit,
    decimal Calories,
    decimal ProteinGrams,
    decimal CarbohydrateGrams,
    decimal FatGrams,
    decimal FiberGrams,
    decimal SugarGrams,
    decimal SodiumMilligrams);

