using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Olivia.DataAccess;
using Olivia.Controllers;
using Olivia.Models;

namespace Olivia.DataAccess
{
    public class IngredientDAO
    {
        public int Insert(Ingredient ingredient)
        {

            string sql = @"insert into dbo.Ingredient  (Name, Category) 
                                            values (@Name, @Category);";

            IngredientData data = new IngredientData(ingredient);

            return SqlDataAccess.SaveData(sql, data);
        }

        public List<Ingredient> LoadIngredients()
        {
            string sql = @"select * from dbo.Ingredient;";

            return SqlDataAccess.LoadData<Ingredient>(sql);
        }

        public Ingredient FindById(int id)
        {
            string sql = @"select * from dbo.Ingredient where Id='" + id + "';";

            return SqlDataAccess.LoadData<Ingredient>(sql).Single<Ingredient>();
        }

        public Ingredient FindByName(string name)
        {
            string sql = @"select top 1 * from dbo.Ingredient where Name='" + name + "';";

            return SqlDataAccess.LoadData<Ingredient>(sql).Single<Ingredient>();
        }

        public RecipeIngredientData GetRecipeIngredient(int id_recipe, int id_ingredient)
        {
            string sql = @"select * from dbo.Recipe_Ingredient where Id_Recipe='" + id_recipe + "' and Id_Ingredient='" + id_ingredient + "';";

            try
            {
                return SqlDataAccess.LoadData<RecipeIngredientData>(sql).Single<RecipeIngredientData>();
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public List<IngredientRecipe> GetIngredients(int id_recipe)
        {
            string sql = @"select * from dbo.Recipe_Ingredient where Id_Recipe='" + id_recipe + "';";
            List<IngredientRecipe> list = SqlDataAccess.LoadData<IngredientRecipe>(sql);

            foreach(IngredientRecipe ing in list)
            {
                sql = @"select Name from dbo.Ingredient where Id_Ingredient='" + ing.Id_Ingredient + "';";
                ing.Name = SqlDataAccess.LoadData<string>(sql).Single();
            }

            return list;
        }
    }
}