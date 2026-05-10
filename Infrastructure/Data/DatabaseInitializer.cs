using Domain.Modules.Foods;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class DatabaseInitializer(RecipeBookDbContext dbContext)
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        await EnsureRecipeTablesAsync(cancellationToken);
        await EnsureTrackingTablesAsync(cancellationToken);

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

    private async Task EnsureTrackingTablesAsync(CancellationToken cancellationToken)
    {
        await dbContext.Database.ExecuteSqlRawAsync(
            """
            CREATE TABLE IF NOT EXISTS "MealLogs" (
                "Id" TEXT NOT NULL CONSTRAINT "PK_MealLogs" PRIMARY KEY,
                "Date" TEXT NOT NULL,
                "Notes" TEXT NULL,
                "CreatedAt" TEXT NOT NULL,
                "UpdatedAt" TEXT NOT NULL
            );
            """,
            cancellationToken);

        await dbContext.Database.ExecuteSqlRawAsync(
            """
            CREATE UNIQUE INDEX IF NOT EXISTS "IX_MealLogs_Date" ON "MealLogs" ("Date");
            """,
            cancellationToken);

        await dbContext.Database.ExecuteSqlRawAsync(
            """
            CREATE TABLE IF NOT EXISTS "MealLogItems" (
                "Id" TEXT NOT NULL CONSTRAINT "PK_MealLogItems" PRIMARY KEY,
                "MealLogId" TEXT NOT NULL,
                "MealGroup" TEXT NOT NULL,
                "FoodId" TEXT NULL,
                "RecipeId" TEXT NULL,
                "QuickItemName" TEXT NULL,
                "Quantity" TEXT NOT NULL,
                "Unit" TEXT NOT NULL,
                "Calories" TEXT NOT NULL,
                "ProteinGrams" TEXT NOT NULL,
                "CarbohydrateGrams" TEXT NOT NULL,
                "FatGrams" TEXT NOT NULL,
                "CreatedAt" TEXT NOT NULL,
                CONSTRAINT "FK_MealLogItems_MealLogs_MealLogId" FOREIGN KEY ("MealLogId") REFERENCES "MealLogs" ("Id") ON DELETE CASCADE
            );
            """,
            cancellationToken);

        await dbContext.Database.ExecuteSqlRawAsync(
            """
            CREATE INDEX IF NOT EXISTS "IX_MealLogItems_MealLogId" ON "MealLogItems" ("MealLogId");
            """,
            cancellationToken);
    }
}
