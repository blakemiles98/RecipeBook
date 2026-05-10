namespace Domain.Modules.Tracking;

public sealed class MealLog
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public string? Notes { get; set; }

    public List<MealLogItem> Items { get; } = [];

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
