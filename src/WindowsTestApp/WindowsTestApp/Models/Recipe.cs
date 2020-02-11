using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using WindowsTestApp.Models;

namespace WindowsTestApp
{
    public class Recipe
    {
        public Recipe() {
            Ingredients = new List<Ingredient>();
        }
        [PrimaryKey, AutoIncrement]
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFavorite { get; set; } = false;
        public int CookingTime { get; set; }
        public DateTime Date { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Ingredient> Ingredients { get; set; }
    }
}
