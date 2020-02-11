using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WindowsTestApp.Repo;

namespace WindowsTestApp.Models.ViewModels
{
    public class RecipeViewModel
    {
        public RecipeViewModel() {
            Ingredients = new ObservableCollection<Ingredient>();
        }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? CookingTime { get; set; } = null;
        public Ingredient Ingredient { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
    }
}
