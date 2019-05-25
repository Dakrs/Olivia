using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using Olivia.Models;

namespace Olivia.DataAccess
{
    public class RecipeDAO : IDAO<Recipe>
    {
        private Connection _connection;

        public RecipeDAO()
        {
            _connection = new Connection();
        }

        public Collection<Recipe> ListAll()
        {
            Collection<Recipe> receitas = new Collection<Recipe>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Receita";

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach(DataRow row in tab.Rows)
                    {
                        Recipe r = new Recipe
                        {
                            Id = int.Parse(row["id_receita"].ToString()),
                            Name = row["nome"].ToString(),
                            Description = row["descricao"].ToString(),
                            CreatorId = int.Parse(row["autor"].ToString()),
                            Type = int.Parse(row["tipo"].ToString()),
                            Calories = float.Parse(row["calorias"].ToString()),
                            Protein = float.Parse(row["proteina"].ToString()),
                            Fat = float.Parse(row["gordura"].ToString()),
                            Carbs = float.Parse(row["carbohidratos"].ToString()),
                            Fiber = float.Parse(row["fibra"].ToString()),
                            Sodium = float.Parse(row["sodio"].ToString())
                        };
                        r.refreshInstructions();
                        receitas.Add(r);
                    }

                }
            }
            return receitas;
        }

        public Recipe FindById(int id)
        {
            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Receita Where id_receita=@key";
                command.Parameters.Add("@key", SqlDbType.Int).Value = id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result = new DataTable();
                    adapter.Fill(result);

                    Recipe r = null;
                    if (result.Rows.Count != 0) {

                        DataRow row = result.Rows[0];
                            r = new Recipe
                            {
                                Id = int.Parse(row["id_receita"].ToString()),
                                Name = row["nome"].ToString(),
                                Description = row["descricao"].ToString(),
                                CreatorId = int.Parse(row["autor"].ToString()),
                                Type = int.Parse(row["tipo"].ToString()),
                                Calories = float.Parse(row["calorias"].ToString()),
                                Protein = float.Parse(row["proteina"].ToString()),
                                Fat = float.Parse(row["gordura"].ToString()),
                                Carbs = float.Parse(row["carbohidratos"].ToString()),
                                Fiber = float.Parse(row["fibra"].ToString()),
                                Sodium = float.Parse(row["sodio"].ToString())
                            };
                        r.refreshInstructions();
                    }

                    return r;
                }
            }
        }

        public Recipe Insert(Recipe r)
        {
            throw new NotImplementedException();
        }

        public bool remove(Recipe r)
        {
            throw new NotImplementedException();
        }

        public bool Update(Recipe obj)
        {
            throw new NotImplementedException();
        }
    }
}
