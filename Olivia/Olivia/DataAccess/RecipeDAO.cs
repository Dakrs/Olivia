using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Olivia.DataAccess;
using Olivia.Controllers;
using Olivia.Models;

namespace Olivia.DataAccess
{
    public static class RecipeDAO
    {
        public static int CreateRecipe(string name, string description, int type, float calories, float fat, float carbs, float protein, float fiber, float sodium)
        {
            Recipe recipe = new Recipe
            {
                Name = name,
                Description = description,
                Type = type,
                Calories = calories,
                Fat = fat,
                Carbs = carbs,
                Protein = protein,
                Fiber = fiber,
                Sodium = sodium

            };

            string sql = @"insert into dbo.Recipe  (Name, Description, Type, Calories, Fat, Carbs, Protein, Fiber, Sodium) 
                                            values (@Name, @Description, @Type, @Calories, @Fat, @Carbs, @Protein, @Fiber, @Sodium);";

            return SqlDataAccess.SaveData(sql, recipe);
        }

        public static int CreateRecipe(Recipe recipe)
        {
            string sql = @"insert into dbo.Recipe  (Name, Description, Type, Calories, Fat, Carbs, Protein, Fiber, Sodium) 
                                            values (@Name, @Description, @Type, @Calories, @Fat, @Carbs, @Protein, @Fiber, @Sodium);";

            return SqlDataAccess.SaveData(sql, recipe);
        }

        public static List<Recipe> LoadRecipes()
        {
            string sql = @"select * from dbo.Recipe;";

            return SqlDataAccess.LoadData<Recipe>(sql);
        }
    }
}