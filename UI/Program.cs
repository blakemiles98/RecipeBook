using Microsoft.AspNetCore.DataProtection;
using Application.Modules.Foods;
using Application.Modules.Recipes;
using Application.Modules.Tracking;
using Infrastructure.Data;
using Infrastructure.Modules.Foods;
using Infrastructure.Modules.Recipes;
using Infrastructure.Modules.Tracking;
using UI.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "DataProtectionKeys")));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFoodStorage(builder.Configuration, builder.Environment.ContentRootPath);
builder.Services.AddRecipeStorage();
builder.Services.AddTrackingStorage();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    await database.InitializeAsync();
}

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
