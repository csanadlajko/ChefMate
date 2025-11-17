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
                RecipeId = SelectedRecipe.Id,
                Name = string.Empty,
                Quantity = string.Empty,
                CreatedAt = DateTime.Now,
                Description = string.Empty,
                ImagePath = string.Empty
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

        private async Task SavePhotoAsync(FileResult image, Ingredients? ingredient)
        {
            if (image != null && ingredient != null)
            {
                var localUrl = Path.Combine(FileSystem.Current.AppDataDirectory, image.FileName);
                if (!File.Exists(localUrl))
                {
                    using Stream stream = await image.OpenReadAsync();
                    using FileStream fileStream = File.OpenWrite(localUrl);
                    await stream.CopyToAsync(fileStream);
                }
                ingredient.ImagePath = image.FullPath;
            }
        }

        [RelayCommand]
        async Task UploadPhotoAsync(Ingredients? ingredient)
        {
            var picked = await MediaPicker.Default.PickPhotoAsync();
            await SavePhotoAsync(picked, ingredient);
        }

        [RelayCommand]
        async Task TakePhotoAsync(Ingredients? ingredient)
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var takenPhoto = await MediaPicker.Default.CapturePhotoAsync();
                await SavePhotoAsync(takenPhoto, ingredient);
            }
            else
            {
                WeakReferenceMessenger.Default.Send("Photo capture is not supported on this device.");
                return;
            }
        }

    }
}
