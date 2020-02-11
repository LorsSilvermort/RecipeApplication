using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using WindowsTestApp.Models;

namespace WindowsTestApp.Data
{
    public class RecipeDatabase
    {
        public readonly SQLiteAsyncConnection _database;
        public RecipeDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Recipe>().Wait();
            _database.CreateTableAsync<Ingredient>().Wait();
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            var ingredients = await _database.Table<Ingredient>().ToListAsync();
            var recipes = await _database.Table<Recipe>().ToListAsync();
            recipes.ForEach(i => i.Ingredients.AddRange(ingredients.Where(r => r.RecipeID == i.RecipeID).ToList()) );
            return recipes;
        }
        public async Task<List<Ingredient>> GetIngredientsAsync(int recipeID)
        {
            var ingredients = await _database.Table<Ingredient>().ToListAsync();
            return ingredients.Where(i => i.RecipeID == recipeID).ToList();
        }
        public async Task<List<Recipe>> GetRecipesAsync(string searchString)
        {
            
            var recipes = await _database.Table<Recipe>().ToListAsync();
            List<Recipe> result = new List<Recipe>();
            foreach(var i in recipes)
            {
                if (i.Name.ToLower() == searchString.ToLower() || i.Name.ToLower().StartsWith(searchString.ToLower()))
                    result.Add(i);
            }
            var ingredients = await _database.Table<Ingredient>().ToListAsync();
            result.ForEach(i => i.Ingredients.AddRange(ingredients.Where(r => r.RecipeID == i.RecipeID).ToList()));
            return result;
        }
        public Task<Recipe> GetRecipeAsync(int id) => _database.Table<Recipe>().Where(i => i.RecipeID == id).FirstOrDefaultAsync();



        public async Task SaveRecipeAsync(Recipe r)
        {
            await _database.InsertAsync(r);
            r.Ingredients.ForEach(i => i.RecipeID = r.RecipeID);
            await _database.InsertAllAsync(r.Ingredients);
        }
        public Task<int> DeleteRecipeAsync(Recipe r) => _database.DeleteAsync(r);
    }
}
