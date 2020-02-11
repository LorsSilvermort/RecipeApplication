using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsTestApp.Models;
using WindowsTestApp.Models.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WindowsTestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }
        public async void AddRecipe(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRecipe
            {
                BindingContext = new RecipeViewModel { Ingredient = new Ingredient() }
            });
        }
        public async void SearchRecipe(Object sender, EventArgs e)
        {
            if (searchBox.Text == null) {
                ListAll(sender, e);
                return;
            }
            running.IsRunning = true;
            var recipes = await App.Database.GetRecipesAsync(searchBox.Text);
            NavigateToList(recipes);
        }
        public async void ListAll(Object sender, EventArgs e)
        {
            running.IsRunning = true;
            var recipes = await App.Database.GetRecipesAsync();
            NavigateToList(recipes);
        }
        public async void NavigateToList(List<Recipe> r)
        {
            await Navigation.PushAsync(new ListRecipes
            {
                BindingContext = new Recipe(),
                Recipes = r
            });
            running.IsRunning = false;
        }


    }
}