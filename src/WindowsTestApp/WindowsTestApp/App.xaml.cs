using System;
using System.IO;
using WindowsTestApp.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WindowsTestApp
{
    public partial class App : Application
    {
        public static RecipeDatabase database;
        public static RecipeDatabase Database {
            get {
                if (database == null) {
                    database = new RecipeDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Recipe.db3"));
                }
                return database;
            }
        }
        public static string FolderPath { get; set; }
        public App()
        {
            InitializeComponent();
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new NavigationPage(new StartPage());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
