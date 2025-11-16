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
    [QueryProperty(nameof(SelectedRecipe), "SelectedRecipe")]
    [QueryProperty(nameof(IngredientsForRecipe), "IngredientsForRecipe")]
    public partial class EditRecipePageViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipes selectedRecipe;

        [ObservableProperty]
        List<Ingredients> ingredientsForRecipe;

        [ObservableProperty]
        Recipes draftRecipe;

        [ObservableProperty]
        ObservableCollection<Ingredients> draftIngredients;

        public void InitDraft()
        {
            DraftRecipe = SelectedRecipe.Copy();
            if (IngredientsForRecipe != null)
            {
                DraftIngredients = new ObservableCollection<Ingredients>(IngredientsForRecipe.Select(ingredient => ingredient.Copy()));
            }
            else
            {
                DraftIngredients = new ObservableCollection<Ingredients>();
            }
        }

        [RelayCommand]
        public async Task CancelEdit()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task EditRecipeAsync()
        {
            var param = new ShellNavigationQueryParameters
            {
                { "NewRecipe", DraftRecipe },
                { "NewIngredients", DraftIngredients }
            };
            await Shell.Current.GoToAsync("..", param);
        }

        [RelayCommand]
        public void AddIngredient()
        {
            Ingredients newIngredient = new Ingredients
            {
                RecipeId = DraftRecipe.Id,
                Name = string.Empty,
                Quantity = string.Empty,
                CreatedAt = DateTime.Now,
                Description = string.Empty
            };
            DraftIngredients.Add(newIngredient);
        }

        [RelayCommand]
        public void RemoveIngredient(Ingredients ingredient)
        {
            if (ingredient != null)
            {
                DraftIngredients.Remove(ingredient);
            }
            else
                return;
        }
    }
}
