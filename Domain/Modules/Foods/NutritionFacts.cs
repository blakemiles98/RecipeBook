namespace Domain.Modules.Foods;

public sealed record NutritionFacts(
    decimal Calories,
    decimal ProteinGrams,
    decimal CarbohydrateGrams,
    decimal FatGrams,
    decimal FiberGrams,
    decimal SugarGrams,
    decimal SodiumMilligrams)
{
    public static NutritionFacts Empty { get; } = new(0, 0, 0, 0, 0, 0, 0);

    public NutritionFacts Scale(decimal multiplier)
    {
        return new NutritionFacts(
            Calories * multiplier,
            ProteinGrams * multiplier,
            CarbohydrateGrams * multiplier,
            FatGrams * multiplier,
            FiberGrams * multiplier,
            SugarGrams * multiplier,
            SodiumMilligrams * multiplier);
    }
}
