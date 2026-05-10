namespace Tracking.Domain;

public sealed class MealLogItem
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public MealGroup MealGroup { get; set; }

    public Guid? FoodId { get; set; }

    public Guid? RecipeId { get; set; }

    public string? QuickItemName { get; set; }

    public decimal Quantity { get; set; }

    public required string Unit { get; set; }

    public decimal Calories { get; set; }

    public decimal ProteinGrams { get; set; }

    public decimal CarbohydrateGrams { get; set; }

    public decimal FatGrams { get; set; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}

