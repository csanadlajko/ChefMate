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
    public partial class AddRecipePageViewModel : ObservableObject
    {
        [ObservableProperty]
        Recipes selectedRecipe;
    }
}
