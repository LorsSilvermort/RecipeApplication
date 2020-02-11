using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ListRecipes : ContentPage
    {
        public List<Recipe> Recipes { get; set; }
        public ListRecipes()
        {
            InitializeComponent();        
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
        public async void OnListViewItemSelected(Object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var r = e.SelectedItem as Recipe;
                ObservableCollection<Ingredient> o = new ObservableCollection<Ingredient>();
                foreach (var item in Recipes.Single(i => i.RecipeID == r.RecipeID).Ingredients) o.Add(item);
                await Navigation.PushAsync(new RecipeView
                {
                    BindingContext = new RecipeViewModel
                    {
                        Name = r.Name,
                        CookingTime = r.CookingTime,
                        Description = r.Description,
                        Ingredients = o
                    },
                    
                }
                );
            }
        }


    }
   
}