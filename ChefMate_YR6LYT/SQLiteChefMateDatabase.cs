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
        SQLiteAsyncConnection _database;

        public SQLiteChefMateDatabase(SQLiteAsyncConnection database)
        {
            this._database = database;
            // the tables below should be created in the viewmodel constructor
            //_database.CreateTableAsync<Recipes>().Wait();
            //_database.CreateTableAsync<Ingredients>().Wait();
        }

        public async Task AddIngredientAsync(Ingredients ingredient)
        {
            _database.Table<Ingredients>();
            await _database.InsertAsync(ingredient);
        }

        public async Task AddRecipeAsync(Recipes recipe)
        {
            _database.Table<Recipes>();
            await _database.InsertAsync(recipe);
        }

        public async Task DeleteIngredientAsync(int id)
        {
            await _database.DeleteAsync<Ingredients>(id);
        }

        public async Task DeleteRecipeAsync(int id)
        {
            await _database.DeleteAsync<Recipes>(id);
        }

        public async Task<List<Recipes>> GetAllRecipesAsync()
        {
            return await _database.Table<Recipes>().ToListAsync();
        }

        public async Task<List<Ingredients>> GetIngredientsForRecipeAsync(int recipeId)
        {
            return await _database.Table<Ingredients>().ToListAsync();
        }

        public async Task<Recipes> GetRecipeAsync(int id)
        {
            _database.Table<Recipes>();
            return await _database.GetAsync<Recipes>(id);
        }

        public async Task UpdateIngredientAsync(Ingredients ingredient)
        {
            _database.Table<Ingredients>();
            await _database.UpdateAsync(ingredient);
        }

        public async Task UpdateRecipeAsync(Recipes recipe)
        {
            _database.Table<Recipes>();
            await _database.UpdateAsync(recipe);
        }
    }
}
