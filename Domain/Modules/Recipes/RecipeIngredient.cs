namespace Domain.Modules.Recipes;

public sealed class RecipeIngredient
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid FoodId { get; set; }

    public required string FoodName { get; set; }

    public decimal Quantity { get; set; }

    public required string Unit { get; set; }

    public int DisplayOrder { get; set; }
}
