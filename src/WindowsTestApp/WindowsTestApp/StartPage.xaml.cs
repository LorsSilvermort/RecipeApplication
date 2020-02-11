using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WindowsTestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : TabbedPage
    {
        private ListFavorites ListFavorites;
        public StartPage()
        {
            InitializeComponent();
            this.Children.Add(new SearchPage());
            ListFavorites = new ListFavorites();
            this.Children.Add(ListFavorites);
        }
        protected override async void OnAppearing()
        {
            if (ListFavorites.Recipes == null) ListFavorites.Recipes = await App.Database.GetRecipesAsync();
        }


    }
}