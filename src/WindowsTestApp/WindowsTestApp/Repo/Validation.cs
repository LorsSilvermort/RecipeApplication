using System;
using System.Collections.Generic;
using System.Text;
using WindowsTestApp.Models;
using WindowsTestApp.Models.ViewModels;

namespace WindowsTestApp.Repo
{
    public class Validation
    {
        public static bool IsNullOrEmpty(string isValid)
        {
            if (isValid == null) return false;
            return isValid.Length > 0 ? true : false;
        }
        public static bool IsRecipeValid(RecipeViewModel r)
        {
            return IsNullOrEmpty(r.Name) && IsNullOrEmpty(r.Description) && r.Ingredients.Count > 0;
        }
    }
}
