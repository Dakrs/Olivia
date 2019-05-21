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
            string sql = @"insert into dbo.Recipe  (Name, Description, Type, Calories, Fat, Carbs, Protein, Fiber, Sodium, Active) 
                                            values (@Name, @Description, @Type, @Calories, @Fat, @Carbs, @Protein, @Fiber, @Sodium, '1');";

            return SqlDataAccess.SaveData(sql, recipe);
        }

        public static int EditRecipe(Recipe recipe)
        {
            string sql = @"update dbo.Recipe set Name=@Name, Description=@Description, Type=@Type, Calories=@Calories, 
                                                    Fat=@Fat, Carbs=@Carbs, Protein=@Protein, Fiber=@Fiber, Sodium=@Sodium where Id=@Id;";

            return SqlDataAccess.SaveData(sql, recipe);
        }

        public static List<Recipe> LoadRecipes()
        {
            string sql = @"select * from dbo.Recipe where Active='1';";

            return SqlDataAccess.LoadData<Recipe>(sql);
        }


        public static Recipe getRecipe(int id)
        {
            var rec = new Recipe
            {
                Id = id
            };

            string sql = @"select * from dbo.Recipe where Id=@Id and Active='1';";

            return SqlDataAccess.getSingle<Recipe>(sql, rec);
        }

        public static int DeleteRecipe(Recipe r)
        {
            string sql = @"update dbo.Recipe set Active='0' where Id=@Id";

            return SqlDataAccess.SaveData(sql, r);
        }
    }
}