using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefMate_YR6LYT
{
    public class SQLiteChefMateDatabase : IChefMateDatabase
    {
        string dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "chefmate.db3");

        SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;

        SQLiteAsyncConnection database;

        public SQLiteChefMateDatabase()
        {
            database = new SQLiteAsyncConnection(dbPath, Flags);
            database.CreateTableAsync<Recipes>().Wait();
            database.CreateTableAsync<Ingredients>().Wait();
        }

        public async Task AddIngredientAsync(Ingredients ingredient)
        {
            await database.InsertAsync(ingredient);
        }

        public async Task AddRecipeAsync(Recipes recipe)
        {
            await database.InsertAsync(recipe);
        }

        public async Task DeleteIngredientAsync(int id)
        {
            await database.DeleteAsync<Ingredients>(id);
        }

        public async Task DeleteRecipeAsync(int id)
        {
            await database.DeleteAsync<Recipes>(id);
        }

        public async Task<List<Recipes>> GetAllRecipesAsync()
        {
            return await database.Table<Recipes>().ToListAsync();
        }

        public async Task<List<Ingredients>> GetAllIngredientsAsync()
        {
            return await database.Table<Ingredients>().ToListAsync();
        }

        public async Task<List<Ingredients>> GetIngredientsForRecipeAsync(int recipeId)
        {
            return await database.Table<Ingredients>().Where(ingr => ingr.RecipeId == recipeId).ToListAsync();
        }

        public async Task<Recipes> GetRecipeAsync(int id)
        {
            database.Table<Recipes>();
            return await database.Table<Recipes>().Where(rec => rec.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateIngredientAsync(Ingredients ingredient)
        {
            await database.UpdateAsync(ingredient);
        }

        public async Task UpdateRecipeAsync(Recipes recipe)
        {
            await database.UpdateAsync(recipe);
        }
    }
}
