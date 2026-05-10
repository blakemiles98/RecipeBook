# RecipeBook Project Plan

## Vision

RecipeBook is a personal recipe manager and calorie tracker. It should let a user save recipes, pull nutrition data from public nutrition APIs, reuse saved food items as recipe ingredients, and log foods or recipe servings eaten each day.

The first version will be a Blazor app with a C# backend and a local relational database. The app should work well for a single user first, with room to add accounts and cloud hosting later.

## Chosen Stack

- Frontend: Blazor
- Backend: ASP.NET Core / C#
- App layout:
  - `UI`
  - `Domain/Modules/Foods`
  - `Domain/Modules/Recipes`
  - `Domain/Modules/Tracking`
  - `Application/Modules/Foods`
  - `Application/Modules/Recipes`
  - `Application/Modules/Tracking`
  - `Infrastructure/Modules/Foods`
  - `Infrastructure/Modules/Recipes`
  - `Infrastructure/Modules/Tracking`
- Database: SQLite for local development
- ORM: Entity Framework Core
- Nutrition APIs:
  - USDA FoodData Central for primary food search and nutrient data
  - Open Food Facts for packaged foods and barcode lookup
  - Optional later: Nutritionix for natural-language food logging

## Product Areas

### Recipes

Recipes are reusable meal definitions made from ingredients. Each recipe should include:

- Name
- Description or notes
- Ingredients
- Ingredient quantities
- Instructions
- Prep time
- Cook time
- Number of servings
- Tags or categories
- Calculated nutrition totals
- Calculated nutrition per serving

### Foods

Foods are saved nutrition items that can be used directly in meal logs or as recipe ingredients.

Foods may come from:

- Manual user entry
- USDA FoodData Central search
- Open Food Facts search or barcode lookup

Each saved food should include:

- Name
- Brand, when available
- Serving size
- Serving unit
- Calories
- Protein
- Carbohydrates
- Fat
- Fiber
- Sugar
- Sodium
- Source provider
- External source ID
- Last synced date

### Meal Log

The meal log tracks what the user ate on a given date.

The user should be able to log:

- A saved food
- A serving or partial serving of a recipe
- A manually entered quick calorie item

Meal log entries should support:

- Date
- Meal group: breakfast, lunch, dinner, snack
- Quantity
- Unit or serving count
- Calories and macros calculated from the selected food or recipe

### Dashboard

The dashboard should summarize the current day.

It should show:

- Calories consumed
- Calorie goal
- Calories remaining
- Protein, carbohydrate, and fat totals
- Macro goal progress
- Foods and recipes logged today

## Data Model Draft

### Food

- `Id`
- `Name`
- `Brand`
- `ServingSize`
- `ServingUnit`
- `Calories`
- `ProteinGrams`
- `CarbohydrateGrams`
- `FatGrams`
- `FiberGrams`
- `SugarGrams`
- `SodiumMilligrams`
- `SourceProvider`
- `ExternalId`
- `CreatedAt`
- `UpdatedAt`
- `LastSyncedAt`

### Recipe

- `Id`
- `Name`
- `Description`
- `Instructions`
- `PrepMinutes`
- `CookMinutes`
- `Servings`
- `CreatedAt`
- `UpdatedAt`

### RecipeIngredient

- `Id`
- `RecipeId`
- `FoodId`
- `Quantity`
- `Unit`
- `DisplayOrder`

### MealLog

- `Id`
- `Date`
- `Notes`
- `CreatedAt`
- `UpdatedAt`

### MealLogItem

- `Id`
- `MealLogId`
- `MealGroup`
- `FoodId`
- `RecipeId`
- `QuickItemName`
- `Quantity`
- `Unit`
- `Calories`
- `ProteinGrams`
- `CarbohydrateGrams`
- `FatGrams`
- `CreatedAt`

Only one of `FoodId`, `RecipeId`, or `QuickItemName` should be used for a meal log item.

### UserSettings

- `Id`
- `DailyCalorieGoal`
- `DailyProteinGoalGrams`
- `DailyCarbohydrateGoalGrams`
- `DailyFatGoalGrams`
- `PreferredUnits`

## API Integration Plan

### USDA FoodData Central

Use USDA FoodData Central as the main search provider for generic foods and high-quality nutrient data.

Needed app behavior:

- Store the API key in user secrets or environment variables.
- Never commit API keys.
- Search foods by query.
- Fetch food details by FDC ID.
- Map USDA nutrients into the app's normalized nutrition fields.
- Cache selected foods locally.

### Open Food Facts

Use Open Food Facts for packaged foods and barcode lookup.

Needed app behavior:

- Search product names.
- Lookup product by barcode.
- Map per-serving or per-100g nutrition to normalized fields.
- Mark data as user/community sourced so it can be reviewed before saving.

### Nutritionix

Keep Nutritionix as a later option.

Possible use:

- Natural-language input such as "2 eggs and 1 slice of toast."
- Restaurant and branded food search.

Do not add Nutritionix to the first MVP unless USDA and Open Food Facts are not enough.

## Build Phases

### Phase 1: App Skeleton

Goal: Create a runnable Blazor app with a C# backend and database.

Tasks:

- Scaffold the solution.
- Add Blazor UI project.
- Add backend/API project if using hosted Blazor.
- Add shared models or contracts project if useful.
- Add EF Core.
- Configure SQLite.
- Add initial migrations.
- Add basic navigation:
  - Dashboard
  - Recipes
  - Foods
  - Meal Log
  - Settings

Done when:

- The app runs locally.
- The database is created.
- Placeholder pages are reachable from navigation.

### Phase 2: Local Food Library

Goal: Save and manage nutrition items locally.

Status: In progress. Local SQLite-backed food storage and manual food CRUD are being added before nutrition API import.

Tasks:

- Create `Food` entity.
- Add food CRUD service.
- Add food list page.
- Add create/edit food form.
- Add validation for serving size and nutrition fields.
- Add tests for food calculations or service logic.

Done when:

- Foods can be created, edited, deleted, and listed.
- Nutrition fields are stored in the database.

### Phase 3: Nutrition API Search

Goal: Import foods from external nutrition APIs.

Status: In progress. USDA FoodData Central search and import are being added first.

Tasks:

- Add USDA FoodData Central client.
- Add API key configuration.
- Build food search UI.
- Show search results with key nutrition preview.
- Add food details lookup.
- Save selected API result as a local `Food`.
- Add Open Food Facts client.
- Add barcode/product lookup flow.

Done when:

- A user can search USDA, select a food, and save it.
- A user can lookup an Open Food Facts product and save it.
- Saved API foods appear in the local food library.

### Phase 4: Recipe Builder

Goal: Create recipes from saved foods.

Tasks:

- Create `Recipe` and `RecipeIngredient` entities.
- Add recipe CRUD service.
- Add recipe list page.
- Add recipe create/edit page.
- Add ingredient picker from saved foods.
- Add quantity/unit input.
- Calculate total recipe nutrition.
- Calculate nutrition per serving.

Done when:

- Recipes can be created from saved foods.
- Recipe nutrition totals and per-serving nutrition are visible.

### Phase 5: Meal Logging

Goal: Track what the user ate each day.

Tasks:

- Create `MealLog` and `MealLogItem` entities.
- Add daily meal log page.
- Add food-to-log flow.
- Add recipe-serving-to-log flow.
- Add quick calorie item flow.
- Group entries by breakfast, lunch, dinner, and snack.
- Calculate daily totals.

Done when:

- A user can log foods and recipe servings for today.
- The app shows daily calorie and macro totals.

### Phase 6: Goals and Dashboard

Goal: Make the calorie tracker useful at a glance.

Tasks:

- Create `UserSettings` entity.
- Add settings page for daily goals.
- Show daily calorie target.
- Show calories consumed and remaining.
- Show macro progress.
- Show today's logged items.

Done when:

- Dashboard gives a clear current-day summary.
- User can update calorie and macro goals.

### Phase 7: Polish and Quality

Goal: Make the app feel reliable and pleasant to use.

Tasks:

- Add loading and error states.
- Add empty states.
- Improve mobile layout.
- Add confirmation for destructive actions.
- Add recipe duplication.
- Add favorite foods and recipes.
- Add copy-yesterday meal log action.
- Add import/export backup.
- Add broader test coverage.

Done when:

- Common workflows feel smooth.
- Data is protected from accidental loss.

## MVP Definition

The MVP is complete when the app can:

- Run locally as a Blazor/C# app.
- Store foods in SQLite.
- Search at least one nutrition API.
- Save API foods locally.
- Create recipes from saved foods.
- Calculate recipe nutrition per serving.
- Log foods and recipe servings eaten.
- Show daily calorie and macro totals.
- Store daily calorie and macro goals.

## Early Decisions To Make

- Blazor Server, Blazor Web App, or Blazor WebAssembly hosted.
- Whether user accounts are needed in version 1.
- Whether this is local-only first or intended for deployment soon.
- Preferred styling approach.
- Whether barcode scanning should be mobile-camera based or manual barcode entry first.

## Recommended First Implementation Choice

Start with a .NET Blazor Web App using interactive server rendering and SQLite. Keep it single-user for the first version. This keeps the app simple, fast to build, and easy to run locally while leaving a clear path to add authentication and hosting later.

