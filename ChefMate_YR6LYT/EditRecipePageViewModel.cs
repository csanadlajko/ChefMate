using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefMate_YR6LYT
{
    [QueryProperty(nameof(SelectedRecipe), "SelectedRecipe")]
    public partial class EditRecipePageViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipes selectedRecipe;

        [ObservableProperty]
        Recipes draftRecipe;

        [ObservableProperty]
        Ingredients draftIngredient;

        public void InitDraft()
        {
            DraftRecipe = SelectedRecipe.Copy();
        }

        [RelayCommand]
        public async Task CancelEdit()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task SaveRecipeChanges()
        {
            var param = new ShellNavigationQueryParameters
            {
                {"EditedRecipe", DraftRecipe }
            };
            await Shell.Current.GoToAsync("..", param);
        }
    }
}
