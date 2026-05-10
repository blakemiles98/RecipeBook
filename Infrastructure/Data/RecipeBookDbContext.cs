using Domain.Modules.Foods;
using Domain.Modules.Recipes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options) : DbContext(options)
{
    public DbSet<Food> Foods => Set<Food>();

    public DbSet<Recipe> Recipes => Set<Recipe>();

    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var food = modelBuilder.Entity<Food>();

        food.ToTable("Foods");
        food.HasKey(x => x.Id);
        food.Property(x => x.Name).HasMaxLength(200).IsRequired();
        food.Property(x => x.Brand).HasMaxLength(200);
        food.Property(x => x.ServingUnit).HasMaxLength(50).IsRequired();
        food.Property(x => x.Source).HasConversion<string>().HasMaxLength(50);
        food.Property(x => x.ExternalId).HasMaxLength(150);

        food.OwnsOne(x => x.Nutrition, nutrition =>
        {
            nutrition.Property(x => x.Calories).HasColumnName("Calories").HasPrecision(10, 2);
            nutrition.Property(x => x.ProteinGrams).HasColumnName("ProteinGrams").HasPrecision(10, 2);
            nutrition.Property(x => x.CarbohydrateGrams).HasColumnName("CarbohydrateGrams").HasPrecision(10, 2);
            nutrition.Property(x => x.FatGrams).HasColumnName("FatGrams").HasPrecision(10, 2);
            nutrition.Property(x => x.FiberGrams).HasColumnName("FiberGrams").HasPrecision(10, 2);
            nutrition.Property(x => x.SugarGrams).HasColumnName("SugarGrams").HasPrecision(10, 2);
            nutrition.Property(x => x.SodiumMilligrams).HasColumnName("SodiumMilligrams").HasPrecision(10, 2);
        });

        var recipe = modelBuilder.Entity<Recipe>();

        recipe.ToTable("Recipes");
        recipe.HasKey(x => x.Id);
        recipe.Property(x => x.Name).HasMaxLength(200).IsRequired();
        recipe.Property(x => x.Description).HasMaxLength(500);
        recipe.Property(x => x.Instructions);
        recipe.HasMany(x => x.Ingredients)
            .WithOne()
            .HasForeignKey(x => x.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        var ingredient = modelBuilder.Entity<RecipeIngredient>();

        ingredient.ToTable("RecipeIngredients");
        ingredient.HasKey(x => x.Id);
        ingredient.Property(x => x.FoodName).HasMaxLength(200).IsRequired();
        ingredient.Property(x => x.Unit).HasMaxLength(50).IsRequired();
    }
}
