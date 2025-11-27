using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
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
        private List<Ingredients> modifiedIngredients;

        [ObservableProperty]
        private bool isItemSelected;

        public MainPageViewModel(IChefMateDatabase database)
        {
            this.database = database;
            RecipesList = new ObservableCollection<Recipes>();
            IngredientsList = new ObservableCollection<Ingredients>();
        }

        partial void OnSelectedRecipeChanged(Recipes value)
        {
            IsItemSelected = value != null;
        }

        async partial void OnModifiedRecipeChanged(Recipes recipes)
        {
            if (recipes != null)
            {
                
                bool editing = selectedRecipe != null;
                if (editing)
                {
                    RecipesList.Remove(SelectedRecipe);
                    SelectedRecipe = null;
                    await database.UpdateRecipeAsync(recipes);
                }
                else
                    await database.AddRecipeAsync(recipes);

                RecipesList.Add(recipes);

                if (ModifiedIngredients != null && !editing)
                {
                    foreach (var ingredient in ModifiedIngredients)
                    {
                        ingredient.RecipeId = recipes.Id;
                        await database.AddIngredientAsync(ingredient);
                        IngredientsList.Add(ingredient);
                    }
                }
                ModifiedIngredients = null;
            }
        }

        async partial void OnModifiedIngredientsChanged(List<Ingredients> ingredients)
        {
            if (ingredients != null)
            {
                var recipeId = 0;

                if (ModifiedRecipe != null)
                    recipeId = ModifiedRecipe.Id;
                else if (SelectedRecipe != null)
                    recipeId = SelectedRecipe.Id;

                if (recipeId == 0)
                {
                    return;
                }

                foreach (var ingredient in ingredients)
                {
                    if (ingredient.Id != 0)
                    {
                        ingredient.RecipeId = recipeId;

                        await database.UpdateIngredientAsync(ingredient);

                        var existing = IngredientsList.FirstOrDefault(i => i.Id == ingredient.Id);

                        if (existing != null)
                            IngredientsList.Remove(existing);
                    }
                    else
                        await database.AddIngredientAsync(ingredient);
                    IngredientsList.Add(ingredient);
                }
            }
            ModifiedIngredients = null;
        }

        [RelayCommand]
        public async Task ShowAddRecipePageAsync()
        {
            SelectedRecipe = null;
            var param = new ShellNavigationQueryParameters
            {
                { "NewRecipe", new Recipes() { CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now } },
                { "NewIngredients", new ObservableCollection<Ingredients>() }
            };
            await Shell.Current.GoToAsync("addrecipepage", param);
        }

        [RelayCommand]
        public async Task ShowEditRecipePageAsync()
        {
            if (SelectedRecipe != null)
            {
                var ingredients = await database.GetIngredientsForRecipeAsync(SelectedRecipe.Id);
                var param = new ShellNavigationQueryParameters
                {
                    {"SelectedRecipe", SelectedRecipe },
                    {"IngredientsForRecipe", ingredients }
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

        [RelayCommand]
        public async Task ShareRecipe(Recipes? recipe)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var ingredients = await database.GetIngredientsForRecipeAsync(SelectedRecipe.Id);
                string ingredientTotalString = "";
                if (ingredients != null && ingredients.Count > 0)
                {
                    foreach (var ingredient in ingredients)
                    {
                        ingredientTotalString += ingredient.ToString();
                    }
                }

                await Share.Default.RequestAsync(new ShareTextRequest
                {
                    Title = "Share Recipe",
                    Text = $"{SelectedRecipe.ToString()}\n{ingredientTotalString}"
                });
            }
            else
                WeakReferenceMessenger.Default.Send("No internet access");
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
