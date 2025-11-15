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
    public partial class AddRecipePageViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipes recipeToAdd;

        [ObservableProperty]
        Recipes draft;

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
                {"NewRecipe", RecipeToAdd }
            };
            await Shell.Current.GoToAsync("..", param);
        }

        public void InitDraft()
        {
            Draft = RecipeToAdd.Copy();
        }
    }
}
