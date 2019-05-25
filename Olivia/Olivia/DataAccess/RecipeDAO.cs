using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Olivia.DataAccess;
using Olivia.Controllers;
using Olivia.Models;

namespace Olivia.DataAccess
{
    public class RecipeDAO
    {
        public void Insert(Recipe recipe)
        {
            RecipeData data = new RecipeData(recipe);

            string sql = @"insert into dbo.Recipe  (Name, Description, Type, Calories, Fat, Carbs, Protein, Fiber, Sodium) 
                                            values (@Name, @Description, @Type, @Calories, @Fat, @Carbs, @Protein, @Fiber, @Sodium);";
            SqlDataAccess.SaveData(sql, data);

            int recipe_id = FindByName(recipe.Name).Id_Recipe;
            foreach (IngredientRecipe ing in recipe.Ingredients)
            {
                if (ing.Name == null)
                    continue;
                Ingredient current = new Ingredient();
                try
                {
                    current = current.IngredientDAO.FindByName(ing.Name);
                }
                catch (Exception e)
                {
                    current.Name = ing.Name;
                    current.IngredientDAO.Insert(current);
                    current = current.IngredientDAO.FindByName(ing.Name);
                }

                RecipeIngredientData data2 = new RecipeIngredientData(recipe_id, current.Id_Ingredient, ing.Quantity , ing.Unit);
                

                sql = @"insert into dbo.Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit)
                                                            values (@Id_Recipe, @Id_Ingredient, @Quantity, @Unit);";
                SqlDataAccess.SaveData(sql, data2);
            }

            for (int i = 0; i < recipe.Instructions.Count ; i++)
            {
                if (recipe.Instructions[i].Designation == null)
                    continue;

                recipe.Instructions[i].Position = i;
                recipe.Instructions[i].Id_Recipe = recipe_id;
                InstructionData data3 = new InstructionData(recipe.Instructions[i]);
                sql = @"insert into dbo.Instruction (Designation, Duration, Position, Id_Recipe)
                                                    values (@Designation, @Duration, @Position, @Id_Recipe);";
                SqlDataAccess.SaveData(sql, data3);
            }

        }

        public void Edit(Recipe recipe)
        {
            RecipeData data = new RecipeData(recipe);

            string sql = @"update dbo.Recipe set Name=@Name, Description=@Description, Type=@Type, Calories=@Calories, Fat=@Fat, Carbs=@Carbs, Protein=@Protein, Fiber=@Fiber, Sodium=@Sodium
                                            where Id_Recipe='" + recipe.Id_Recipe + "';";
            SqlDataAccess.SaveData(sql, data);

            int recipe_id = recipe.Id_Recipe;
            foreach (IngredientRecipe ing in recipe.Ingredients)
            {
                if (ing.Name == null)
                    continue;
                Ingredient current = new Ingredient();
                try
                {
                    current = current.IngredientDAO.FindByName(ing.Name);
                    try
                    {
                        RecipeIngredientData ri = current.FindRecipeUsage(recipe_id);
                        sql = @"update dbo.Recipe_Ingredient set Quantity=@Quantity, Unit=@Unit where Id_Recipe=@Id_Recipe and Id_Ingredient=@Id_Ingredient;";
                        SqlDataAccess.SaveData(sql, ri);
                    } catch (Exception e)
                    {
                        RecipeIngredientData ri = new RecipeIngredientData(recipe_id, current.Id_Ingredient, ing.Quantity, ing.Unit);
                        sql = @"insert into dbo.Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit)
                                                            values (@Id_Recipe, @Id_Ingredient, @Quantity, @Unit);";
                        SqlDataAccess.SaveData(sql, ri);
                    }                  
                }
                catch (Exception e)
                {
                    current.Name = ing.Name;
                    current.IngredientDAO.Insert(current);
                    current = current.IngredientDAO.FindByName(ing.Name);

                    RecipeIngredientData ri = new RecipeIngredientData(recipe_id, current.Id_Ingredient, ing.Quantity, ing.Unit);

                    sql = @"insert into dbo.Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit)
                                                            values (@Id_Recipe, @Id_Ingredient, @Quantity, @Unit);";
                    SqlDataAccess.SaveData(sql, ri);
                }
            }

            for (int i = 0; i < recipe.Instructions.Count; i++)
            {
                if (recipe.Instructions[i].Designation == null)
                    continue;
                try
                {
                    InstructionData ins = recipe.GetInstructionByPosition(i);
                    ins.Designation = recipe.Instructions[i].Designation;
                    ins.Duration = recipe.Instructions[i].Duration;

                    sql = @"update dbo.Instruction set Designation=@Designation, Duration=@Duration where Id_Recipe=@Id_Recipe and Position=@Position;";
                    SqlDataAccess.SaveData(sql, ins);
                } catch (Exception e)
                {
                    recipe.Instructions[i].Position = i;
                    recipe.Instructions[i].Id_Recipe = recipe_id;
                    InstructionData ins = new InstructionData(recipe.Instructions[i]);

                    sql = @"delete from dbo.Instruction where Id_Recipe='" + recipe_id + "' and Position='" + i + "';";
                    SqlDataAccess.SaveData(sql, ins);

                    sql = @"insert into dbo.Instruction (Designation, Duration, Position, Id_Recipe)
                                                    values (@Designation, @Duration, @Position, @Id_Recipe);";
                    SqlDataAccess.SaveData(sql, ins);


                }
            }

        }


        public List<Recipe> LoadRecipes()
        {
            string sql = @"select * from dbo.Recipe where Active='1';";

            return SqlDataAccess.LoadData<Recipe>(sql);
        }

        public Recipe FindById(int id)
        {
            string sql = @"select * from dbo.Recipe where Id_Recipe='" + id + "' and Active='1';";
            try
            {
                Recipe r = SqlDataAccess.LoadData<Recipe>(sql).Single<Recipe>();
                r.Instructions = r.InstructionDAO.GetInstructions(r.Id_Recipe);
                r.Ingredients = r.IngredientDAO.GetIngredients(r.Id_Recipe);
                return r;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Recipe FindByName(string name)
        {
            string sql = @"select * from dbo.Recipe where Name='" + name + "' and Active='1';";

            return SqlDataAccess.LoadData<Recipe>(sql).First<Recipe>();
        }

        public void Delete(int id)
        {
            string sql = @"update dbo.Recipe set Active='0' where Id_Recipe='" + id + "';";


                SqlDataAccess.SaveData(sql, 1);         

        }
    }
}