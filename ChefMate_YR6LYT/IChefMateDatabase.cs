using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefMate_YR6LYT
{
    public interface IChefMateDatabase
    {
        // Create new recipe
        Task AddRecipeAsync(Recipes recipe);

        // Read all recipes
        Task<List<Recipes>> GetAllRecipesAsync();

        // Read a specific recipe by ID
        Task<Recipes> GetRecipeAsync(int id);

        // Update existing recipe
        Task UpdateRecipeAsync(Recipes recipe);

        // Delete recipe by ID
        Task DeleteRecipeAsync(int id);

        // Add ingredient to recipe
        Task AddIngredientAsync(Ingredients ingredient);

        // Get ingredients for a specific recipe
        Task<Ingredients> GetIngredientsForRecipeAsync(int recipeId);

        // Update ingredient under a recipe
        Task UpdateIngredientAsync(Ingredients ingredient);

        // Delete ingredient by ID
        Task DeleteIngredientAsync(int id);
    }
}
