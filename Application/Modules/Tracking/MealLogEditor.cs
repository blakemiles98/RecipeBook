using Domain.Modules.Tracking;

namespace Application.Modules.Tracking;

public sealed class MealLogEditor
{
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public MealGroup MealGroup { get; set; } = MealGroup.Breakfast;

    public Guid? FoodId { get; set; }

    public Guid? RecipeId { get; set; }

    public string? QuickItemName { get; set; }

    public decimal Quantity { get; set; } = 1;

    public string Unit { get; set; } = "serving";

    public decimal Calories { get; set; }

    public decimal ProteinGrams { get; set; }

    public decimal CarbohydrateGrams { get; set; }

    public decimal FatGrams { get; set; }
}

