using Microsoft.AspNetCore.DataProtection;
using Application.Modules.Foods;
using Application.Modules.Recipes;
using Application.Modules.Tracking;
using UI.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "DataProtectionKeys")));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IFoodCatalog, FoodCatalogPlaceholder>();
builder.Services.AddScoped<IRecipeLibrary, RecipeLibraryPlaceholder>();
builder.Services.AddScoped<ITrackingSummary, TrackingSummaryPlaceholder>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
