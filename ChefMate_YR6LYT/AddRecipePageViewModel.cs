using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefMate_YR6LYT
{
    [QueryProperty(nameof(RecipeToAdd), "NewRecipe")]
    [QueryProperty(nameof(IngredientsToAdd), "NewIngredients")]
    public partial class AddRecipePageViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipes recipeToAdd;

        [ObservableProperty]
        ObservableCollection<Ingredients> ingredientsToAdd;

        private IChefMateDatabase database;

        public AddRecipePageViewModel(IChefMateDatabase database)
        {
            this.database = database;
            IngredientsToAdd = new ObservableCollection<Ingredients>();
        }

        [RelayCommand]
        public async Task CancelAdd()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task AddRecipe()
        {
            var param = new ShellNavigationQueryParameters
            {
                {"NewRecipe", RecipeToAdd },
                {"NewIngredients", IngredientsToAdd.ToList() }
            };
            await Shell.Current.GoToAsync("..", param);
        }

        [RelayCommand]
        public async Task DeleteNewIngredient(Ingredients ingredient)
        {
            if (ingredient != null)
            {
                await database.DeleteIngredientAsync(ingredient.Id);
                IngredientsToAdd.Remove(ingredient);
            }
            else
                WeakReferenceMessenger.Default.Send("Ingredient adding failed!");
        }

        [RelayCommand]
        public void AddNewIngredient()
        {
            Ingredients newIngredient = new Ingredients
            {
                RecipeId = RecipeToAdd.Id,
                Name = string.Empty,
                Description = string.Empty,
                Quantity = string.Empty,
                CreatedAt = DateTime.Now
            };
            IngredientsToAdd.Add(newIngredient);
        }
    }
}
