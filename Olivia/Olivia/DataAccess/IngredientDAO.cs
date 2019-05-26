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

            return SqlDataAccess.LoadData<Ingredient>(sql, new Ingredient());
        }

        public Ingredient FindById(int id)
        {
            string sql = @"select * from dbo.Ingredient where Id=@Id_Ingredient;";

            return SqlDataAccess.LoadData<Ingredient>(sql, new Ingredient() { Id_Ingredient = id}).Single<Ingredient>();
        }

        public Ingredient FindByName(string name)
        {
            string sql = @"select top 1 * from dbo.Ingredient where Name=@Name;";

            return SqlDataAccess.LoadData<Ingredient>(sql, new Ingredient() { Name = name}).Single<Ingredient>();
        }

        public RecipeIngredientData GetRecipeIngredient(int id_recipe, int id_ingredient)
        {
            string sql = @"select * from dbo.Recipe_Ingredient where Id_Recipe=@Id_Recipe and Id_Ingredient=@Id_Ingredient;";

            try
            {
                return SqlDataAccess.LoadData<RecipeIngredientData>(sql, new RecipeIngredientData(id_recipe, id_ingredient, 0, "")).Single<RecipeIngredientData>();
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public List<IngredientRecipe> GetIngredients(int id_recipe)
        {
            string sql = @"select * from dbo.Recipe_Ingredient where Id_Recipe=@Id_Recipe;";
            List<IngredientRecipe> list = SqlDataAccess.LoadData<IngredientRecipe>(sql, new IngredientRecipe() { Id_Recipe = id_recipe});

            Ingredient aux = new Ingredient();
            foreach(IngredientRecipe ing in list)
            {
                aux.Id_Ingredient = ing.Id_Ingredient;
                sql = @"select * from dbo.Ingredient where Id_Ingredient=@Id_Ingredient;";
                aux = SqlDataAccess.LoadData<Ingredient>(sql, aux).Single();
                ing.Name = aux.Name;
            }

            return list;
        }

        public void DeleteRecipeIngredients(int recipe_id)
        {
            string sql = @"delete from dbo.Recipe_Ingredient where Id_Recipe='" + recipe_id + "';";
            SqlDataAccess.SaveData(sql, sql);
        }
    }
}