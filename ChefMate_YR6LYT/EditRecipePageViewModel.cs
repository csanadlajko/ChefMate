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

        private IChefMateDatabase database;

        public EditRecipePageViewModel(IChefMateDatabase database)
        {
            this.database = database;
        }

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
                { "NewIngredients", DraftIngredients.ToList() }
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
        public async Task RemoveIngredient(Ingredients ingredient)
        {
            if (ingredient != null)
            {
                await database.DeleteIngredientAsync(ingredient.Id);
                DraftIngredients.Remove(ingredient);
            }
            else
                WeakReferenceMessenger.Default.Send("Deleting ingredient failed!");
                return;
        }
    }
}
