# RecipeBook

Blazor and C# recipe book with calorie tracking.

## Architecture

- `UI`: Blazor Web App host.
- `Domain`: domain models, organized by module under `Domain/Modules`.
- `Application`: application services and use cases, organized by module under `Application/Modules`.
- `Infrastructure`: persistence and external integrations, organized by module under `Infrastructure/Modules`.

See `PROJECT_PLAN.md` for the build roadmap.

## Run Locally

```powershell
dotnet restore RecipeBook.slnx
dotnet run --project UI/UI.csproj --no-launch-profile --urls http://localhost:5222
```

The app creates a local SQLite database at `UI/App_Data/RecipeBook.db`.
