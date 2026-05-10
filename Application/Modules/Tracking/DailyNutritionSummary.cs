namespace Application.Modules.Tracking;

public sealed record DailyNutritionSummary(
    DateOnly Date,
    decimal Calories,
    decimal CalorieGoal,
    decimal ProteinGrams,
    decimal ProteinGoalGrams,
    decimal CarbohydrateGrams,
    decimal CarbohydrateGoalGrams,
    decimal FatGrams,
    decimal FatGoalGrams,
    int LoggedItemCount = 0);
