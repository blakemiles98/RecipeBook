using Domain.Modules.Foods;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class DatabaseInitializer(RecipeBookDbContext dbContext)
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);

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
}
