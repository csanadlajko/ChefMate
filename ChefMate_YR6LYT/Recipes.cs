using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefMate_YR6LYT
{
    public partial class Recipes : ObservableObject
    {
        [ObservableProperty]
        int id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        DateTime createdAt;

        [ObservableProperty]
        DateTime updatedAt;

        [ObservableProperty]
        int preparationTimeMinutes;

        [ObservableProperty]
        string difficulty;

        [ObservableProperty]
        double longtitude;

        [ObservableProperty]
        double latitude;
    }
}
