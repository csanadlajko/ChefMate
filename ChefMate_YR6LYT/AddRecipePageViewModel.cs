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
    [QueryProperty(nameof(RecipeToAdd), "NewRecipe")]
    [QueryProperty(nameof(IngredientsToAdd), "NewIngredients")]
    public partial class AddRecipePageViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipes recipeToAdd;

        [ObservableProperty]
        Ingredients ingredientsToAdd;

        [ObservableProperty]
        Recipes draftRecipe;

        [ObservableProperty]
        Ingredients draftIngredients;

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
                {"NewRecipe", DraftRecipe },
                {"NewIngredients", DraftIngredients }
            };
            await Shell.Current.GoToAsync("..", param);
        }

        public void InitDraft()
        {
            DraftRecipe = RecipeToAdd.Copy();
            DraftIngredients = IngredientsToAdd.Copy();
        }
    }
}
