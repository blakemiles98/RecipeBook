namespace Domain.Modules.Tracking;

public sealed class UserSettings
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public decimal DailyCalorieGoal { get; set; } = 2200;

    public decimal DailyProteinGoalGrams { get; set; } = 160;

    public decimal DailyCarbohydrateGoalGrams { get; set; } = 220;

    public decimal DailyFatGoalGrams { get; set; } = 70;

    public string PreferredUnits { get; set; } = "US";

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}

