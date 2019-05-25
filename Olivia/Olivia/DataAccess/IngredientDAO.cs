using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Olivia.Models;

namespace Olivia.DataAccess
{
    public class IngredientDAO
    {
        private Connection _connection;

        public IngredientDAO()
        {
            _connection = new Connection();
        }

        public Dictionary<Ingredient,Quantity> IngredientsForRecipe(int id)
        {
            Dictionary<Ingredient, Quantity> result = new Dictionary<Ingredient, Quantity>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "Select * FROM Receita_Ingrediente AS R, Ingrediente AS I " +
                	"WHERE R.id_receita = @idr AND R.id_ingrediente = I.id_ingrediente";

                command.Parameters.Add("@idr", SqlDbType.Int).Value = id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Quantity q = new Quantity
                        {
                            Amount = float.Parse(row["quantidade"].ToString()),
                            Unity = row["unidade"].ToString()
                        };
                        Ingredient i = new Ingredient
                        {
                            Id = int.Parse(row["id_ingrediente"].ToString()),
                            Name = row["nome"].ToString(),
                            Category = row["categoria"].ToString()
                        };

                        result.Add(i, q);
                    }
                }

            }


            return result;
        }
    }
}
