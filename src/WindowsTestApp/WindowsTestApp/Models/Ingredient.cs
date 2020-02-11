using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WindowsTestApp.Models
{
    public class Ingredient : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string IngredientName { get; set; }

        private double? _grams;
        public double? Grams { 
            get 
            {
                return _grams;
            }
            set 
            {
                _grams = value;
                OnPropertyChanged();
            } 
        } 

        [ForeignKey(typeof(Recipe))]
        public int RecipeID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
