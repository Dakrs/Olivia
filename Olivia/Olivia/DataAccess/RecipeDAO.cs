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
        public int Insert(Recipe recipe)
        {

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into dbo.Recipe  (Name, Description, Creator, Type, Duration, Calories, Fat, Carbs, Protein, Fiber, Sodium) " +
                                            "values(@Name, @Description, @Creator, @Type, @Duration ,@Calories, @Fat, @Carbs, @Protein, @Fiber, @Sodium); ";

                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = recipe.Name;
                command.Parameters.Add("@Description", SqlDbType.Text).Value = recipe.Description;
                command.Parameters.Add("@Creator", SqlDbType.Int).Value = recipe.Creator;
                command.Parameters.Add("@Type", SqlDbType.Int).Value = recipe.Type;
                command.Parameters.Add("@Calories", SqlDbType.Float).Value = recipe.Calories;
                command.Parameters.Add("@Fat", SqlDbType.Float).Value = recipe.Fat;
                command.Parameters.Add("@Carbs", SqlDbType.Float).Value = recipe.Carbs;
                command.Parameters.Add("@Protein", SqlDbType.Float).Value = recipe.Protein;
                command.Parameters.Add("@Fiber", SqlDbType.Float).Value = recipe.Fiber;
                command.Parameters.Add("@Sodium", SqlDbType.Float).Value = recipe.Sodium;
                command.Parameters.Add("@Duration", SqlDbType.Int).Value = recipe.Duration;


                command.ExecuteNonQuery();

            }

            int recipe_id = FindByNameAdmin(recipe.Name).Id_Recipe;
            foreach (RecipeIngredient ing in recipe.Ingredients)
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
                if (current == null) {
                    current = new Ingredient();
                    current.Name = ing.Name;
                    current.IngredientDAO.Insert(current);
                    current = current.IngredientDAO.FindByName(ing.Name);
                }
                using (SqlCommand command = con.Fetch().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into dbo.Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit)" +
                                                             "values (@Id_Recipe, @Id_Ingredient, @Quantity, @Unit);";

                    command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = recipe_id;
                    command.Parameters.Add("@Id_Ingredient", SqlDbType.Int).Value = current.Id_Ingredient;
                    command.Parameters.Add("@Quantity", SqlDbType.Float).Value = ing.Quantity;
                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = ing.Unit;


                    command.ExecuteNonQuery();

                }

            }

            for (int i = 0; i < recipe.Instructions.Count ; i++)
            {
                if (recipe.Instructions[i].Designation == null)
                    continue;

                recipe.Instructions[i].Position = i;
                recipe.Instructions[i].Id_Recipe = recipe_id;

                using (SqlCommand command = con.Fetch().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into dbo.Instruction (Designation, Position, Id_Recipe)" +
                                                    "values(@Designation, @Position, @Id_Recipe);";

                    command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = recipe.Instructions[i].Designation;
                    command.Parameters.Add("@Position", SqlDbType.Int).Value = recipe.Instructions[i].Position;
                    command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = recipe.Instructions[i].Id_Recipe;


                    command.ExecuteNonQuery();

                }
            }
            con.Close();
            return recipe_id;
        }

        public void Edit(Recipe recipe)
        {

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "update dbo.Recipe set Name=@Name, Description=@Description, Creator=@Creator, Type=@Type,Duration=@Duration ,Calories=@Calories, Fat=@Fat, Carbs=@Carbs, Protein=@Protein, Fiber=@Fiber, Sodium=@Sodium" +
                                                " where Id_Recipe=@Id_Recipe;";

                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = recipe.Id_Recipe;
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = recipe.Name;
                command.Parameters.Add("@Description", SqlDbType.Text).Value = recipe.Description;
                command.Parameters.Add("@Creator", SqlDbType.Int).Value = recipe.Creator;
                command.Parameters.Add("@Type", SqlDbType.Int).Value = recipe.Type;
                command.Parameters.Add("@Calories", SqlDbType.Float).Value = recipe.Calories;
                command.Parameters.Add("@Fat", SqlDbType.Float).Value = recipe.Fat;
                command.Parameters.Add("@Carbs", SqlDbType.Float).Value = recipe.Carbs;
                command.Parameters.Add("@Protein", SqlDbType.Float).Value = recipe.Protein;
                command.Parameters.Add("@Fiber", SqlDbType.Float).Value = recipe.Fiber;
                command.Parameters.Add("@Sodium", SqlDbType.Float).Value = recipe.Sodium;
                command.Parameters.Add("@Duration", SqlDbType.Int).Value = recipe.Duration;


                command.ExecuteNonQuery();

            }

            recipe.DeleteIngredients();
            foreach (RecipeIngredient ing in recipe.Ingredients)
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

                using (SqlCommand command = con.Fetch().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into dbo.Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit)" +
                                                                " values (@Id_Recipe, @Id_Ingredient, @Quantity, @Unit);";

                    command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = recipe.Id_Recipe;
                    command.Parameters.Add("@Id_Ingredient", SqlDbType.Int).Value = current.Id_Ingredient;
                    command.Parameters.Add("@Quantity", SqlDbType.Float).Value = ing.Quantity;
                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = ing.Unit;
                    command.ExecuteNonQuery();
                }


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
                    Instruction ins = recipe.GetInstructionByPosition(i);
                    ins.Designation = recipe.Instructions[i].Designation;

                    using (SqlCommand command = con.Fetch().CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "update dbo.Instruction set Designation=@Designation where Id_Recipe=@Id_Recipe and Position=@Position;";

                        command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = ins.Designation;
                        command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = ins.Id_Recipe;
                        command.Parameters.Add("@Position", SqlDbType.Int).Value = ins.Position;
                        command.ExecuteNonQuery();
                    }
                } catch (Exception e)
                {
                    recipe.Instructions[i].Position = i;
                    recipe.Instructions[i].Id_Recipe = recipe.Id_Recipe;
                    using (SqlCommand command = con.Fetch().CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "delete from dbo.Instruction where Id_Recipe=@Id_Recipe and Position=@Position;";

                        command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = recipe.Instructions[i].Id_Recipe;
                        command.Parameters.Add("@Position", SqlDbType.Int).Value = recipe.Instructions[i].Position;
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = con.Fetch().CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "insert into dbo.Instruction (Designation, Position, Id_Recipe)" +
                                                    " values(@Designation, @Position, @Id_Recipe); ";

                        command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = recipe.Instructions[i].Designation;
                        command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = recipe.Instructions[i].Id_Recipe;
                        command.Parameters.Add("@Position", SqlDbType.Int).Value = recipe.Instructions[i].Position;
                        command.ExecuteNonQuery();
                    }

                }
            }
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
                            Sodium = float.Parse(row["Sodium"].ToString()),
                            Duration = int.Parse(row["Duration"].ToString())
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
                            Sodium = float.Parse(row["Sodium"].ToString()),
                            Duration = int.Parse(row["Duration"].ToString())
                        };
                        r.Instructions = r.InstructionDAO.GetInstructions(r.Id_Recipe);
                        r.Ingredients = r.IngredientDAO.GetIngredients(r.Id_Recipe);
                        r.Warnings = GetWarnings(id);
                    }
                }
            }
            con.Close();

            return r;
        }

        public int NumberReceipts(int idUser)
        {

                int nRecepits = 0;
                Connection con = new Connection();
                using (SqlCommand command = con.Fetch().CreateCommand())
                {

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT COUNT(*) AS CREATED FROM [Recipe] WHERE Creator=@autor";
                    command.Parameters.Add("@autor", SqlDbType.Int).Value = idUser;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable result_querie = new DataTable();
                        adapter.Fill(result_querie);
                        if (result_querie.Rows.Count > 0)
                        {

                            DataRow row = result_querie.Rows[0];
                            nRecepits = int.Parse(row["CREATED"].ToString());
                        }
                        else nRecepits = -1;

                    }
                }
                con.Close();

                return nRecepits;
        }

        public int NumberRated(int idUser)
        {

            int nRecepits = 0;
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT COUNT(*) AS RATED FROM [Rating] WHERE Id_User=@id;";
                command.Parameters.Add("@id", SqlDbType.Int).Value = idUser;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);
                    if (result_querie.Rows.Count > 0)
                    {

                        DataRow row = result_querie.Rows[0];
                        nRecepits = int.Parse(row["RATED"].ToString());
                    }
                    else nRecepits = -1;

                }
            }
            con.Close();

            return nRecepits;
        }

        public int NumberFavourites(int idUser)
        {

            int nRecepits = 0;
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT COUNT(*) AS FAVOURITES FROM [Favorite] WHERE User_key=@autor;";
                command.Parameters.Add("@autor", SqlDbType.Int).Value = idUser;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);
                    if (result_querie.Rows.Count > 0)
                    {

                        DataRow row = result_querie.Rows[0];
                        nRecepits = int.Parse(row["FAVOURITES"].ToString());
                    }
                    else nRecepits = -1;

                }
            }
            con.Close();

            return nRecepits;
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
                            Sodium = float.Parse(row["Sodium"].ToString()),
                            Duration = int.Parse(row["Duration"].ToString())
                        };
                        r.Instructions = r.InstructionDAO.GetInstructions(r.Id_Recipe);
                        r.Ingredients = r.IngredientDAO.GetIngredients(r.Id_Recipe);
                        r.Warnings = GetWarnings(r.Id_Recipe);
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
                command.CommandText = "SELECT * FROM [Recipe] AS R, [Favorite] AS F WHERE F.User_key = @user AND R.Active = 1 AND R.Id_Recipe = F.Recipe_key";
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
                            Sodium = float.Parse(row["Sodium"].ToString()),
                            Duration = int.Parse(row["Duration"].ToString())
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

        public void AddRating(int idUser, int idRecipe, int value)
        {

            bool flag = false;
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Rating] WHERE Rating.Id_Recipe =@rec AND Rating.Id_User=@user";
                command.Parameters.Add("@rec", SqlDbType.Int).Value = idRecipe;
                command.Parameters.Add("@user", SqlDbType.Int).Value = idUser;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    if (result_querie.Rows.Count > 0)
                        flag = true;
                }
            }
            Console.WriteLine(flag);
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                if (flag)
                    command.CommandText = "UPDATE [Rating] SET Rating.Rating=@rate WHERE Rating.Id_Recipe =@rec AND Rating.Id_User=@user";
                else command.CommandText = "INSERT INTO [Rating] VALUES(@user,@rec,@rate)";
                command.Parameters.Add("@rec", SqlDbType.Int).Value = idRecipe;
                command.Parameters.Add("@user", SqlDbType.Int).Value = idUser;
                command.Parameters.Add("@rate", SqlDbType.Int).Value = value;

                command.ExecuteNonQuery();
            }

            con.Close();
        }

        public void AddHistory(int idUser,int idRecipe)
        {
            DateTime date = DateTime.Now;
            string date_sql_format = date.ToString("yyyy-MM-dd HH:mm:ss.fff");

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO [History] VALUES(@user,@rec,GETDATE())";
                command.Parameters.Add("@rec", SqlDbType.Int).Value = idRecipe;
                command.Parameters.Add("@user", SqlDbType.Int).Value = idUser;
                //command.Parameters.Add("@date", SqlDbType.Date).Value = date_sql_format;

                command.ExecuteNonQuery();
            }

            con.Close();
        }

        public Dictionary<int,float> allRatings()
        {
            Dictionary<int, float> result = new Dictionary<int, float>();

            Connection con = new Connection();

            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "Select Recipe.Id_Recipe,AVG(cast (Rating as FLOAT)) AS AVERAGE FROM [Rating] RIGHT JOIN [Recipe] ON Rating.Id_Recipe = Recipe.Id_Recipe GROUP By Recipe.Id_Recipe;";

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    foreach(DataRow row in result_querie.Rows)
                    {
                        int id_recipe = int.Parse(row["Id_Recipe"].ToString());
                        float rate;
                        bool flag = float.TryParse(row["AVERAGE"].ToString(),out rate);

                        if (flag == false)
                            rate = 0.0f;

                        result.Add(id_recipe, rate);
                    }
                }
            }
            con.Close();

            return result;
        }

        public List<Recipe> searchByWords(List<string> words)
        {
            Dictionary<int, Recipe> dic_recipe = new Dictionary<int, Recipe>();

            Connection con = new Connection();
            foreach (string word in words)
            {
                using (SqlCommand command = con.Fetch().CreateCommand())
                {

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM [Recipe] WHERE Name LIKE Concat('%',@word,'%')";
                    command.Parameters.Add("@word", SqlDbType.VarChar);
                    command.Parameters["@word"].Value = word;

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
                                Sodium = float.Parse(row["Sodium"].ToString()),
                                Duration = int.Parse(row["Duration"].ToString())
                            };
                            dic_recipe.Add(r.Id_Recipe, r);
                        }
                    }
                }
            }
            con.Close();
            return dic_recipe.Values.ToList();
        }
        public List<string> GetWarnings(int id)
        {
            List<string> result = new List<string>();
            string s;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT Warning FROM [Warning] WHERE Id_Recipe = @id;";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    foreach (DataRow row in result_querie.Rows)
                    {
                        s = row["Warning"].ToString();
                        result.Add(s);
                    }
                }
            }
            con.Close();
            return result;
        }

        public List<Recipe> LoadRecipeByType(int type)
        {
            List<Recipe> result = new List<Recipe>();

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Recipe] WHERE Active=1 AND Type=@type";
                command.Parameters.Add("@type", SqlDbType.Int).Value = type;
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
                            Sodium = float.Parse(row["Sodium"].ToString()),
                            Duration = int.Parse(row["Duration"].ToString())
                        };

                        result.Add(r);
                    }
                }
            }
            return result;
        }

        public void InsertImage(int id_recipe, byte[] array)
        {
            Connection con = new Connection();

            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from dbo.Recipe_Image where" +
                                                         " Id_Recipe=@Id_Recipe;";

                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = id_recipe;
                command.ExecuteNonQuery();
            }



            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into dbo.Recipe_Image (Id_Recipe, Image)" +
                                                         "values (@Id_Recipe, @Image);";

                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = id_recipe;
                command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = array;


                command.ExecuteNonQuery();
            }
            con.Close();
        }

        public byte[] GetRecipeImage(int id_recipe)
        {
            Connection con = new Connection();
            byte[] b = null;
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT Image FROM [Recipe_Image] WHERE Id_Recipe=@Id_Recipe";
                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = id_recipe;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    if (result_querie.Rows.Count > 0)
                    {
                        DataRow row = result_querie.Rows[0];
                        b = (byte[])row["Image"];
                    }
                }
            }
            con.Close();

            return b;
        }


        public List<Recipe> NeedAproval()
        {
            List<Recipe> result = new List<Recipe>();

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Recipe] WHERE Active=0";

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
                            Sodium = float.Parse(row["Sodium"].ToString()),
                            Duration = int.Parse(row["Duration"].ToString())
                        };

                        result.Add(r);
                    }
                }
            }

            return result;
        }


        public void ApproveRecipe(int id_Recipe)
        {
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "update Recipe set Active=1 where Id_Recipe=@Id_Recipe";
                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = id_Recipe;

                command.ExecuteNonQuery();
            }
            con.Close();
        }

        public Recipe FindByIdAdmin(int id)
        {
            Recipe r = null;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Recipe] WHERE Id_Recipe=@id";
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
                            Sodium = float.Parse(row["Sodium"].ToString()),
                            Duration = int.Parse(row["Duration"].ToString())
                        };
                        r.Instructions = r.InstructionDAO.GetInstructions(r.Id_Recipe);
                        r.Ingredients = r.IngredientDAO.GetIngredients(r.Id_Recipe);
                        r.Warnings = GetWarnings(id);
                    }
                }
            }
            con.Close();

            return r;
        }

        public Recipe FindByNameAdmin(string name)
        {
            Recipe r = null;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from [Recipe] where Name=@name";
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
                            Sodium = float.Parse(row["Sodium"].ToString()),
                            Duration = int.Parse(row["Duration"].ToString())
                        };
                        r.Instructions = r.InstructionDAO.GetInstructions(r.Id_Recipe);
                        r.Ingredients = r.IngredientDAO.GetIngredients(r.Id_Recipe);
                        r.Warnings = GetWarnings(r.Id_Recipe);
                    }
                }
            }
            con.Close();

            return r;
        }


        public int IsActive(int idRecipe)
        {

            int ativo = 0;
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT Active FROM [Recipe] WHERE Id_Recipe = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = idRecipe;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);
                    if (result_querie.Rows.Count > 0)
                    {

                        DataRow row = result_querie.Rows[0];
                        ativo = int.Parse(row["Active"].ToString());
                    }

                }
            }
            con.Close();

            return ativo;
        }



    }
}
