using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsTestApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WindowsTestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEntryPage : ContentPage
    {
        public NoteEntryPage()
        {
            InitializeComponent();
        }
        async void OnSaveButtonClicked(Object sender, EventArgs e) {
            var note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;
            //await App.Database.SaveNoteAsync(note);
            await Navigation.PopAsync();
        }
        async void OnDeleteButtonClicked(Object sender, EventArgs e) {
            var note = (Note)BindingContext;
           // await App.Database.DeleteNoteAsync(note);
            await Navigation.PopAsync();
        }
    }
}