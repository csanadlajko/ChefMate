using CommunityToolkit.Mvvm.ComponentModel;
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
        public ObservableCollection<Recipes> Recipes { get; set; }

        [ObservableProperty]
        Recipes selectedRecipe;


    }
}
