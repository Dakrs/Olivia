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
    public class IngredientDAO
    {
        public int Insert(Ingredient ingredient)
        {
            int result = 0;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO [Ingredient] VALUES (@Name, @Category)";
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = ingredient.Name;
                command.Parameters.Add("@Category", SqlDbType.VarChar).Value = ingredient.Category;


                result = command.ExecuteNonQuery();
            }

            return result;
        }

        public List<Ingredient> LoadIngredients()
        {
            List<Ingredient> result = new List<Ingredient>();

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Ingredient]";

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    foreach (DataRow row in result_querie.Rows)
                    {
                        Ingredient r = new Ingredient
                        {
                            Id_Ingredient = int.Parse(row["Id_Ingredient"].ToString()),
                            Name = row["Name"].ToString(),
                            Category = row["Category"].ToString()
                        };

                        result.Add(r);
                    }
                }
            }
            con.Close();

            return result;
        }

        public Ingredient FindById(int id)
        {
            Ingredient i = null;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Ingredient] WHERE Id_Ingredient=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    if (result_querie.Rows.Count > 0)
                    {
                        DataRow row = result_querie.Rows[0];
                        i = new Ingredient
                        {
                            Id_Ingredient = int.Parse(row["Id_Ingredient"].ToString()),
                            Name = row["Name"].ToString(),
                            Category = row["Category"].ToString()
                        };
                    }
                }
            }
            con.Close();

            return i;
        }

        public Ingredient FindByName(string name)
        {
            string sql = @"select top 1 * from dbo.Ingredient where Name=@Name;";

            return SqlDataAccess.LoadData<Ingredient>(sql, new Ingredient() { Name = name}).Single<Ingredient>();
        }

        public RecipeIngredientData GetRecipeIngredient(int id_recipe, int id_ingredient)
        {
            RecipeIngredientData result = null;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Recipe_Ingredient] WHERE Id_Ingredient=@id AND Id_Recipe=@Id_Recipe";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id_ingredient;
                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = id_recipe;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    if (result_querie.Rows.Count > 0)
                    {
                        DataRow row = result_querie.Rows[0];
                        result = new RecipeIngredientData
                        {
                            Id_Recipe = int.Parse(row["Id_Recipe"].ToString()),
                            Id_Ingredient = int.Parse(row["Id_Ingredient"].ToString()),
                            Quantity = float.Parse(row["Quantity"].ToString()),
                            Unit = row["Unit"].ToString()
                        };
                    }
                }
            }
            con.Close();
            return result;
        }

        public List<IngredientRecipe> GetIngredients(int id_recipe)
        {
            List<IngredientRecipe> list = new List<IngredientRecipe>();


            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Recipe_Ingredient AS R, Ingredient AS I WHERE R.Id_Recipe = @Id_Recipe AND R.Id_Ingredient = I.Id_Ingredient";
                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = id_recipe;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    foreach(DataRow row in result_querie.Rows)
                    {
                        IngredientRecipe r = new IngredientRecipe
                        {
                            Id_Recipe = int.Parse(row["Id_Recipe"].ToString()),
                            Id_Ingredient = int.Parse(row["Id_Ingredient"].ToString()),
                            Name = row["Name"].ToString(),
                            Quantity = float.Parse(row["Quantity"].ToString()),
                            Unit = row["Unit"].ToString()
                        };
                        list.Add(r);
                    }
                }
            }
            con.Close();

            return list;
        }

        public void DeleteRecipeIngredients(int recipe_id)
        {
            string sql = @"delete from dbo.Recipe_Ingredient where Id_Recipe='" + recipe_id + "';";
            SqlDataAccess.SaveData(sql, sql);
        }
    }
}