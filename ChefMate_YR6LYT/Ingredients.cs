using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChefMate_YR6LYT
{
    public partial class Ingredients : ObservableObject
    {
        [ObservableProperty]
        int id;

        [ObservableProperty]
        int recipeId;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string quantity;

        [ObservableProperty]
        DateTime createdAt;

        [ObservableProperty]
        string imapePath;
    }
}
