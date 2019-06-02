using System;
using System.Collections.Generic;
namespace Olivia.Models
{
    public class Menu
    {
        public int Id_Menu { get; set; }
        public DateTime StartingDate { get; set; }
        public List<Recipe> Recipes { get; set; }

        public Dictionary<DateTime, KeyValuePair<Recipe, Recipe>> OrderedRecipes()
        {
            Dictionary<DateTime, KeyValuePair<Recipe, Recipe>> result = new Dictionary<DateTime, KeyValuePair<Recipe, Recipe>>();
            for(int i = 0; i < 7; i++)
            {
                DateTime now = this.StartingDate.AddDays(i);
                KeyValuePair<Recipe, Recipe> day_recipes = new KeyValuePair<Recipe, Recipe>(this.Recipes[i], this.Recipes[i + 1]);
                result.Add(now, day_recipes);
            }

            return result;
        }
    }
}
