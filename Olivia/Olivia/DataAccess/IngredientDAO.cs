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
                command.CommandText = "INSERT INTO [Ingredient] (Name, Category) VALUES (@Name, @Category)";
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = ingredient.Name;
                command.Parameters.Add("@Category", SqlDbType.VarChar).Value = "";//ingredient.Category;


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
            Ingredient i = null;
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "select top 1 * from dbo.Ingredient where Name=@Name;";
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;

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

        public RecipeIngredient GetRecipeIngredient(int id_recipe, int id_ingredient)
        {
            RecipeIngredient result = null;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Recipe_Ingredient] WHERE Id_Ingredient=@Id_Ingredient AND Id_Recipe=@Id_Recipe";
                command.Parameters.Add("@Id_Ingredient", SqlDbType.Int).Value = id_ingredient;
                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = id_recipe;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    if (result_querie.Rows.Count > 0)
                    {
                        DataRow row = result_querie.Rows[0];
                        result = new RecipeIngredient
                        {
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

        public List<RecipeIngredient> GetIngredients(int id_recipe)
        {
            List<RecipeIngredient> list = new List<RecipeIngredient>();


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
                        RecipeIngredient r = new RecipeIngredient
                        {
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
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "delete FROM Recipe_Ingredient where Id_Recipe=@Id_Recipe;";
                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = recipe_id;
                command.ExecuteNonQuery();
                
            }

            con.Close();
        }
    }
}