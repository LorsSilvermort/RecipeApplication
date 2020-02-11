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
    public partial class ListFavorites : ContentPage
    {
        public List<Recipe> Recipes { get; set; }
        public ListFavorites()
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
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Run(() => {
                Device.BeginInvokeOnMainThread(() => {
                    listview.ItemsSource = Recipes;
                });

            });
        }
    }
}