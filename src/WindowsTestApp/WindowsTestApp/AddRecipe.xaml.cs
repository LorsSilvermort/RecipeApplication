using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsTestApp.Models;
using WindowsTestApp.Models.ViewModels;
using WindowsTestApp.Repo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WindowsTestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecipe : ContentPage
    {
        private Recipe recipe; 
        public AddRecipe()
        {
            InitializeComponent();
            recipe = new Recipe();
            
        }
        public async void AddIngredients(Object sender, EventArgs e) {
            var validInput = await ValidateIngredients();
            if (!validInput) return;
            var model = (RecipeViewModel)BindingContext;
            model.Ingredients.Add(new Ingredient {Grams = model.Ingredient.Grams, IngredientName = model.Ingredient.IngredientName });
            listview.ItemsSource = model.Ingredients;
            await ClearUI();
        }

        private async Task ClearUI() {
            await Task.Run(() => ingredient.Text = string.Empty)
                      .ContinueWith(task => gram.Text = string.Empty);
        }
        public async void Remove_Item(Object sender, EventArgs e)
        {
            var btn = sender as Button;
            var ingredient = btn.BindingContext as Ingredient;
            var model = (RecipeViewModel)BindingContext;
            model.Ingredients.Remove(ingredient);
            listview.ItemsSource = await Task.Run(() => model.Ingredients);
        }
        private async Task<Boolean> ValidateIngredients() {
            var isIngredientValid = Validation.IsNullOrEmpty(ingredient.Text);
            var isGramValid = Validation.IsNullOrEmpty(gram.Text);
            if (!isIngredientValid || !isGramValid)
            {
                await DisplayPopup();
            }
            return isIngredientValid && isGramValid;
        }
        public async void SaveRecipe(object sender, EventArgs e) {
            var model = (RecipeViewModel)BindingContext;
            if (!Validation.IsRecipeValid(model))
            {
                await DisplayPopup(null, "Alla fält märkta * måste vara ifyllda och receptet måste innehålla minst en ingrediens");
                return;
            }
            var r = new Recipe
            {
                Name = model.Name,
                Description = model.Description,
                Ingredients = model.Ingredients.ToList(),
                Date = DateTime.UtcNow,
                CookingTime = model.CookingTime != null ? (int)model.CookingTime : 0 
            };
            await App.Database.SaveRecipeAsync(r);
            await Navigation.PopAsync();
        }
        private async Task DisplayPopup(string header = null, string message = null, string button = null) {
                await DisplayAlert(header != null ? header : "Varning", message != null ? message : "Alla fält måste vara ifyllda", button != null ? button : "OK");
        }
        

    }
}