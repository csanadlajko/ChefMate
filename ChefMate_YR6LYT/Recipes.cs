using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefMate_YR6LYT
{
    public partial class Recipes : ObservableObject
    {
        [property: SQLite.PrimaryKey]
        [property: SQLite.AutoIncrement]
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

        public Recipes Copy()
        {
            return (Recipes)this.MemberwiseClone();
        }

        public override string? ToString()
        {
            return $"Recipe name: {Name}\n" +
                $"Description: {Description}\n" +
                $"Created at: {CreatedAt}\n" +
                $"Last modified at: {UpdatedAt}\n" +
                $"Preparation time in minutes: {PreparationTimeMinutes}" +
                $"Difficulty: {Difficulty}";
        }
    }
}
