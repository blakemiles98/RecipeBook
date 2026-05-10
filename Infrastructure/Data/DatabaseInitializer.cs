using Domain.Modules.Foods;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class DatabaseInitializer(RecipeBookDbContext dbContext)
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        await EnsureRecipeTablesAsync(cancellationToken);

        if (await dbContext.Foods.AnyAsync(cancellationToken))
        {
            return;
        }

        dbContext.Foods.AddRange(
            new Food
            {
                Name = "Chicken breast",
                Brand = "Sample",
                ServingSize = 100,
                ServingUnit = "g",
                Nutrition = new NutritionFacts(165, 31, 0, 3.6m, 0, 0, 74)
            },
            new Food
            {
                Name = "Cooked white rice",
                Brand = "Sample",
                ServingSize = 1,
                ServingUnit = "cup",
                Nutrition = new NutritionFacts(205, 4.3m, 45, 0.4m, 0.6m, 0.1m, 2)
            });

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureRecipeTablesAsync(CancellationToken cancellationToken)
    {
        await dbContext.Database.ExecuteSqlRawAsync(
            """
            CREATE TABLE IF NOT EXISTS "Recipes" (
                "Id" TEXT NOT NULL CONSTRAINT "PK_Recipes" PRIMARY KEY,
                "Name" TEXT NOT NULL,
                "Description" TEXT NULL,
                "Instructions" TEXT NULL,
                "PrepMinutes" INTEGER NULL,
                "CookMinutes" INTEGER NULL,
                "Servings" INTEGER NOT NULL,
                "CreatedAt" TEXT NOT NULL,
                "UpdatedAt" TEXT NOT NULL
            );
            """,
            cancellationToken);

        await dbContext.Database.ExecuteSqlRawAsync(
            """
            CREATE TABLE IF NOT EXISTS "RecipeIngredients" (
                "Id" TEXT NOT NULL CONSTRAINT "PK_RecipeIngredients" PRIMARY KEY,
                "RecipeId" TEXT NOT NULL,
                "FoodId" TEXT NOT NULL,
                "FoodName" TEXT NOT NULL,
                "Quantity" TEXT NOT NULL,
                "Unit" TEXT NOT NULL,
                "DisplayOrder" INTEGER NOT NULL,
                CONSTRAINT "FK_RecipeIngredients_Recipes_RecipeId" FOREIGN KEY ("RecipeId") REFERENCES "Recipes" ("Id") ON DELETE CASCADE
            );
            """,
            cancellationToken);

        await dbContext.Database.ExecuteSqlRawAsync(
            """
            CREATE INDEX IF NOT EXISTS "IX_RecipeIngredients_RecipeId" ON "RecipeIngredients" ("RecipeId");
            """,
            cancellationToken);
    }
}
