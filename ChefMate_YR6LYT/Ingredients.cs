using System;
using SQLite;
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
        [property:PrimaryKey]
        [property:AutoIncrement]
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
        string imagePath;

        public Ingredients Copy()
        {
            return (Ingredients)this.MemberwiseClone();
        }
    }
}
