using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Olivia.DataAccess;
using Olivia.Controllers;
using Olivia.Models;
using System.Data.SqlClient;
using System.Data;

namespace Olivia.DataAccess
{
    public class RecipeDAO
    {
        public void Insert(Recipe recipe)
        {
            /**
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
            }*/

        }

        public void Edit(Recipe recipe)
        {
        /**
            RecipeData data = new RecipeData(recipe);

            string sql = @"update dbo.Recipe set Name=@Name, Description=@Description, Type=@Type, Calories=@Calories, Fat=@Fat, Carbs=@Carbs, Protein=@Protein, Fiber=@Fiber, Sodium=@Sodium
                                            where Id_Recipe='" + recipe.Id_Recipe + "';";
            SqlDataAccess.SaveData(sql, data);

            int recipe_id = recipe.Id_Recipe;
            recipe.DeleteIngredients();
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

                RecipeIngredientData data2 = new RecipeIngredientData(recipe_id, current.Id_Ingredient, ing.Quantity, ing.Unit);


                sql = @"insert into dbo.Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit)
                                                            values (@Id_Recipe, @Id_Ingredient, @Quantity, @Unit);";
                SqlDataAccess.SaveData(sql, data2);
            }

            for (int i = 0; i < recipe.Instructions.Count; i++)
            {
                if (recipe.Instructions[i].Designation == null)
                {
                    recipe.DeleteInstruction(i);
                    continue;
                }
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

                    sql = @"delete from dbo.Instruction where Id_Recipe=@Id_Recipe and Position=@Position;";
                    SqlDataAccess.SaveData(sql, new Instruction { Id_Recipe = recipe_id, Position = i});

                    sql = @"insert into dbo.Instruction (Designation, Duration, Position, Id_Recipe)
                                                    values (@Designation, @Duration, @Position, @Id_Recipe);";
                    SqlDataAccess.SaveData(sql, ins);

                }
            }
            */
        }


        public List<Recipe> LoadRecipes()
        {
            List<Recipe> result = new List<Recipe>();

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Recipe] WHERE Active=1";

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    foreach (DataRow row in result_querie.Rows)
                    {
                        Recipe r = new Recipe
                        {
                            Id_Recipe = int.Parse(row["Id_Recipe"].ToString()),
                            Name = row["Name"].ToString(),
                            Description = row["Description"].ToString(),
                            Creator = int.Parse(row["Creator"].ToString()),
                            Type = int.Parse(row["Type"].ToString()),
                            Calories = float.Parse(row["Calories"].ToString()),
                            Protein = float.Parse(row["Protein"].ToString()),
                            Fat = float.Parse(row["Fat"].ToString()),
                            Carbs = float.Parse(row["Carbs"].ToString()),
                            Fiber = float.Parse(row["Fiber"].ToString()),
                            Sodium = float.Parse(row["Sodium"].ToString())
                        };

                        result.Add(r);
                    }
                }
            }

            return result;
        }

        public Recipe FindById(int id)
        {
            Recipe r = null;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Recipe] WHERE Id_Recipe=@id AND Active=1";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    if (result_querie.Rows.Count > 0)
                    {
                        DataRow row = result_querie.Rows[0];
                        r = new Recipe
                        {
                            Id_Recipe = int.Parse(row["Id_Recipe"].ToString()),
                            Name = row["Name"].ToString(),
                            Description = row["Description"].ToString(),
                            Creator = int.Parse(row["Creator"].ToString()),
                            Type = int.Parse(row["Type"].ToString()),
                            Calories = float.Parse(row["Calories"].ToString()),
                            Protein = float.Parse(row["Protein"].ToString()),
                            Fat = float.Parse(row["Fat"].ToString()),
                            Carbs = float.Parse(row["Carbs"].ToString()),
                            Fiber = float.Parse(row["Fiber"].ToString()),
                            Sodium = float.Parse(row["Sodium"].ToString())
                        };
                        r.Instructions = r.InstructionDAO.GetInstructions(r.Id_Recipe);
                        r.Ingredients = r.IngredientDAO.GetIngredients(r.Id_Recipe);
                    }
                }
            }
            con.Close();

            return r;
        }

        public Recipe FindByName(string name)
        {
            Recipe r = null;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from [Recipe] where Name=@name AND Active=1";
                command.Parameters.Add("@name", SqlDbType.VarChar);
                command.Parameters["@name"].Value = name;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    if (result_querie.Rows.Count > 0)
                    {
                        DataRow row = result_querie.Rows[0];
                        r = new Recipe
                        {
                            Id_Recipe = int.Parse(row["Id_Recipe"].ToString()),
                            Name = row["Name"].ToString(),
                            Description = row["Description"].ToString(),
                            Creator = int.Parse(row["Creator"].ToString()),
                            Type = int.Parse(row["Type"].ToString()),
                            Calories = float.Parse(row["Calories"].ToString()),
                            Protein = float.Parse(row["Protein"].ToString()),
                            Fat = float.Parse(row["Fat"].ToString()),
                            Carbs = float.Parse(row["Carbs"].ToString()),
                            Fiber = float.Parse(row["Fiber"].ToString()),
                            Sodium = float.Parse(row["Sodium"].ToString())
                        };
                        r.Instructions = r.InstructionDAO.GetInstructions(r.Id_Recipe);
                        r.Ingredients = r.IngredientDAO.GetIngredients(r.Id_Recipe);
                    }
                }
            }
            con.Close();

            return r;
        }

        public void Delete(int id)
        {
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE [Recipe] SET Active=0 WHERE Id_Recipe=@rec";
                command.Parameters.Add("@rec", SqlDbType.Int).Value = id;

                command.ExecuteNonQuery();
            }
            con.Close();

        }

        public void AddToFavourite(int idUser,int idRecipe)
        {   

            Recipe flag = FindById(idRecipe);
            if (flag == null)
            {
                return;
            }
            bool exists = false;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "select User_Key FROM [Favorite] WHERE User_Key=@user AND Recipe_key=@rec";
                command.Parameters.Add("@user", SqlDbType.Int).Value = idUser;
                command.Parameters.Add("@rec", SqlDbType.Int).Value = idRecipe;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result = new DataTable();
                    adapter.Fill(result);

                    if (result.Rows.Count > 0)
                    {
                        exists = true;
                    }
                }

            }
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                if (exists)
                    command.CommandText = "DELETE FROM [Favorite] WHERE User_Key=@user AND Recipe_key=@rec";
                else command.CommandText = "INSERT INTO [Favorite] VALUES(@user,@rec)";
                command.Parameters.Add("@user", SqlDbType.Int).Value = idUser;
                command.Parameters.Add("@rec", SqlDbType.Int).Value = idRecipe;

                command.ExecuteNonQuery();
            }



            con.Close();
        }

        public List<Recipe> getFavorites(int idUser)
        {
            List<Recipe> result = new List<Recipe>();

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Recipe] AS R, [Favorite] AS F WHERE F.User_key = @user AND R.Active = 1 AND R.Id_Recipe = F.User_key";
                command.Parameters.Add("@user", SqlDbType.Int).Value = idUser;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    foreach(DataRow row in result_querie.Rows)
                    {
                        Recipe r = new Recipe
                        {
                            Id_Recipe = int.Parse(row["Id_Recipe"].ToString()),
                            Name = row["Name"].ToString(),
                            Description = row["Description"].ToString(),
                            Creator = int.Parse(row["Creator"].ToString()),
                            Type = int.Parse(row["Type"].ToString()),
                            Calories = float.Parse(row["Calories"].ToString()),
                            Protein = float.Parse(row["Protein"].ToString()),
                            Fat = float.Parse(row["Fat"].ToString()),
                            Carbs = float.Parse(row["Carbs"].ToString()),
                            Fiber = float.Parse(row["Fiber"].ToString()),
                            Sodium = float.Parse(row["Sodium"].ToString())
                        };

                        result.Add(r);
                    }
                }
            }
            con.Close();

            return result;
        }


        public float RecipeRating(int idRecipe)
        {
            float rating = 0;
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT AVG(Rating) AS AVG FROM [Rating] WHERE Rating.Id_Recipe = @rec";
                command.Parameters.Add("@rec", SqlDbType.Int).Value = idRecipe;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    rating = float.Parse(result_querie.Rows[0].ToString());
                }
            }
            con.Close();


            return rating;
        }
    }
}