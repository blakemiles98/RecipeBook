using Domain.Modules.Tracking;

namespace Application.Modules.Tracking;

public sealed class GoalSettingsEditor
{
    public decimal DailyCalorieGoal { get; set; } = 2200;

    public decimal DailyProteinGoalGrams { get; set; } = 160;

    public decimal DailyCarbohydrateGoalGrams { get; set; } = 220;

    public decimal DailyFatGoalGrams { get; set; } = 70;

    public string PreferredUnits { get; set; } = "US";

    public static GoalSettingsEditor FromSettings(UserSettings settings)
    {
        return new GoalSettingsEditor
        {
            DailyCalorieGoal = settings.DailyCalorieGoal,
            DailyProteinGoalGrams = settings.DailyProteinGoalGrams,
            DailyCarbohydrateGoalGrams = settings.DailyCarbohydrateGoalGrams,
            DailyFatGoalGrams = settings.DailyFatGoalGrams,
            PreferredUnits = settings.PreferredUnits
        };
    }
}

