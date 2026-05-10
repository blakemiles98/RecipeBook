using Domain.Modules.Recipes;

namespace Application.Modules.Recipes;

public sealed class RecipeIngredientEditor
{
    public Guid FoodId { get; set; }

    public string FoodName { get; set; } = string.Empty;

    public decimal Quantity { get; set; } = 1;

    public string Unit { get; set; } = "serving";

    public int DisplayOrder { get; set; }

    public static RecipeIngredientEditor FromIngredient(RecipeIngredient ingredient)
    {
        return new RecipeIngredientEditor
        {
            FoodId = ingredient.FoodId,
            FoodName = ingredient.FoodName,
            Quantity = ingredient.Quantity,
            Unit = ingredient.Unit,
            DisplayOrder = ingredient.DisplayOrder
        };
    }
}

