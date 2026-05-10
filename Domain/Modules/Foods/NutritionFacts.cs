namespace Domain.Modules.Foods;

public sealed class NutritionFacts
{
    private NutritionFacts()
    {
    }

    public NutritionFacts(
        decimal calories,
        decimal proteinGrams,
        decimal carbohydrateGrams,
        decimal fatGrams,
        decimal fiberGrams,
        decimal sugarGrams,
        decimal sodiumMilligrams)
    {
        Calories = calories;
        ProteinGrams = proteinGrams;
        CarbohydrateGrams = carbohydrateGrams;
        FatGrams = fatGrams;
        FiberGrams = fiberGrams;
        SugarGrams = sugarGrams;
        SodiumMilligrams = sodiumMilligrams;
    }

    public static NutritionFacts Empty { get; } = new(0, 0, 0, 0, 0, 0, 0);

    public decimal Calories { get; private set; }

    public decimal ProteinGrams { get; private set; }

    public decimal CarbohydrateGrams { get; private set; }

    public decimal FatGrams { get; private set; }

    public decimal FiberGrams { get; private set; }

    public decimal SugarGrams { get; private set; }

    public decimal SodiumMilligrams { get; private set; }

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
