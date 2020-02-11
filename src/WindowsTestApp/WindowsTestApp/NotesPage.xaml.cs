using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listview.ItemsSource = await App.Database.GetRecipesAsync();
        }
        public async void OnNoteClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new NoteEntryPage
            {
                BindingContext = new Note()
            });
        }
        public async void AddRecipe(Object sender, EventArgs e) {
            await Navigation.PushAsync(new AddRecipe
            {
                BindingContext = new RecipeViewModel { Ingredient = new Ingredient()}
            }); ;
        }
        public async void OnListViewItemSelected(Object sender, SelectedItemChangedEventArgs e) {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new NoteEntryPage
                {
                    BindingContext = e.SelectedItem as Note
                }
                );
            }
        }

    }
}