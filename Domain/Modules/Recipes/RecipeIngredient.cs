namespace Domain.Modules.Recipes;

public sealed class RecipeIngredient
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RecipeId { get; set; }

    public Guid FoodId { get; set; }

    public required string FoodName { get; set; }

    public decimal Quantity { get; set; }

    public required string Unit { get; set; }

    public int DisplayOrder { get; set; }
}
