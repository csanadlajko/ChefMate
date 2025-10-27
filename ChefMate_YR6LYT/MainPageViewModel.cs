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
    public partial class MainPageViewModel : ObservableObject
    {
        private IChefMateDatabase database;
        public ObservableCollection<Recipes> Recipes { get; set; }

        [ObservableProperty]
        Recipes selectedRecipe;

        public MainPageViewModel(IChefMateDatabase database)
        {
            this.database = database;
            Recipes = new ObservableCollection<Recipes>();
        }

        [RelayCommand]
        public async Task ShowAddRecipePageAsync()
        {
            await Shell.Current.GoToAsync("addrecipepage");
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
    }
}
