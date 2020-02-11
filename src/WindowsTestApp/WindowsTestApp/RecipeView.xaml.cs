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
    public partial class RecipeView : ContentPage
    {
        private ObservableCollection<Ingredient> _Ingredients;
        public RecipeView()
        {
            InitializeComponent();
            _Ingredients = new ObservableCollection<Ingredient>(); 
             
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var model = (RecipeViewModel)BindingContext;
            await Task.Run(() => {
                Device.BeginInvokeOnMainThread(() => {
                    listview.ItemsSource = model.Ingredients;
                    foreach (var i in model.Ingredients) _Ingredients.Add(new Ingredient {ID = i.ID,
                                                                                          Grams = i.Grams,
                                                                                          IngredientName = i.IngredientName,
                                                                                          RecipeID = i.RecipeID});
                });

            });
        }

        private void slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var StepValue = 1.0;
            var newStep = Math.Round(e.NewValue / StepValue);
            var newValue = newStep * StepValue;
            slider.Value = newValue;
     
        }

        private void slider_DragCompleted(object sender, EventArgs e)
        {
            var model = (RecipeViewModel)BindingContext;
            model.Ingredients.Clear();
            foreach (var item in _Ingredients) model.Ingredients.Add(new Ingredient {RecipeID = item.RecipeID,
                                                                                     Grams = item.Grams,
                                                                                     ID = item.ID,
                                                                                     IngredientName = item.IngredientName});
            foreach (var i in model.Ingredients)i.Grams = (i.Grams * slider.Value);

            listview.ItemsSource = model.Ingredients;

        }
    }
}