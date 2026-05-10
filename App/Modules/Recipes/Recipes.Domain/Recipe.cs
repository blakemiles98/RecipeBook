namespace Recipes.Domain;

public sealed class Recipe
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? Instructions { get; set; }

    public int? PrepMinutes { get; set; }

    public int? CookMinutes { get; set; }

    public int Servings { get; set; } = 1;

    public List<RecipeIngredient> Ingredients { get; } = [];

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}

