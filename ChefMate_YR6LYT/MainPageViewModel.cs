using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefMate_YR6LYT
{
    [QueryProperty(nameof(ModifiedRecipe), "NewRecipe")]
    [QueryProperty(nameof(ModifiedIngredients), "NewIngredients")]
    public partial class MainPageViewModel : ObservableObject
    {
        private IChefMateDatabase database;
        public ObservableCollection<Recipes> RecipesList { get; set; }
        public ObservableCollection<Ingredients> IngredientsList { get; set; }

        [ObservableProperty]
        Recipes selectedRecipe;

        [ObservableProperty]
        private Recipes modifiedRecipe;

        [ObservableProperty]
        private Ingredients modifiedIngredients;

        public MainPageViewModel(IChefMateDatabase database)
        {
            this.database = database;
            RecipesList = new ObservableCollection<Recipes>();
            IngredientsList = new ObservableCollection<Ingredients>();
        }

        async partial void OnModifiedRecipeChanged(Recipes recipes)
        {
            if (recipes != null)
            {
                if (SelectedRecipe != null)
                {
                    RecipesList.Remove(SelectedRecipe);
                    SelectedRecipe = null;
                    await database.UpdateRecipeAsync(recipes);
                }
                else
                    await database.AddRecipeAsync(recipes);
                RecipesList.Add(recipes);
            }
        }

        async partial void OnModifiedIngredientsChanged(Ingredients ingredients)
        {
            if (ingredients != null)
            {
                if (SelectedRecipe != null)
                {
                    await database.UpdateIngredientAsync(ingredients);

                    var existing = IngredientsList.FirstOrDefault(i => i.Id == ingredients.Id);
                    ingredients.RecipeId = ModifiedRecipe.Id;

                    if (existing != null)
                        IngredientsList.Remove(existing);

                }
                else
                    await database.AddIngredientAsync(ingredients);
                IngredientsList.Add(ingredients);
            }
        }

        [RelayCommand]
        public async Task ShowAddRecipePageAsync()
        {
            SelectedRecipe = null;
            var param = new ShellNavigationQueryParameters
            {
                { "NewRecipe", new Recipes() { CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now } },
                { "NewIngredients", new Ingredients() { CreatedAt = DateTime.Now } }
            };
            await Shell.Current.GoToAsync("addrecipepage", param);
        }

        [RelayCommand]
        public async Task ShowEditRecipePageAsync()
        {
            if (SelectedRecipe != null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    {"SelectedRecipe", SelectedRecipe }
                };
                await Shell.Current.GoToAsync("editrecipepage", param);
            }
        }

        [RelayCommand]
        public async Task DeleteRecipeAsync()
        {
            if (SelectedRecipe != null)
            {
                int recipeId = SelectedRecipe.Id;
                await database.DeleteRecipeAsync(recipeId);
                RecipesList.Remove(SelectedRecipe);
            }
        }

        public async Task InitializeAsync()
        {
            var recipes = await database.GetAllRecipesAsync();
            RecipesList.Clear();
            recipes.ForEach(r => RecipesList.Add(r));
            var ingredients = await database.GetAllIngredientsAsync();
            IngredientsList.Clear();
            ingredients.ForEach(i => IngredientsList.Add(i));
        }
    }
}
