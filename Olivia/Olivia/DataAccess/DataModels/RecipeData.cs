using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Olivia.DataAccess;

namespace Olivia.Models
{
    public class RecipeData
    {

        public int Id_Recipe { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Creator { get; set; }
        public int Type { get; set; }
        public float Calories { get; set; }
        public float Fat { get; set; }
        public float Carbs { get; set; }
        public float Protein { get; set; }
        public float Fiber { get; set; }
        public float Sodium { get; set; }

        public RecipeData(Recipe recipe)
        {
            Id_Recipe = recipe.Id_Recipe;
            Name = recipe.Name;
            Description = recipe.Description;
            Creator = recipe.Creator;
            Type = recipe.Type;
            Calories = recipe.Calories;
            Fat = recipe.Fat;
            Carbs = recipe.Carbs;
            Protein = recipe.Protein;
            Fiber = recipe.Fiber;
            Name = recipe.Name;
            Sodium = recipe.Sodium;
        }

    }


}
