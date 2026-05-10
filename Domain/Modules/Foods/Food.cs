namespace Domain.Modules.Foods;

public sealed class Food
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Name { get; set; }

    public string? Brand { get; set; }

    public decimal ServingSize { get; set; }

    public required string ServingUnit { get; set; }

    public NutritionFacts Nutrition { get; set; } = NutritionFacts.Empty;

    public FoodSource Source { get; set; } = FoodSource.Manual;

    public string? ExternalId { get; set; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? LastSyncedAt { get; set; }
}
