using System;
using System.Data.SqlClient;
using Olivia.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.Collections.Generic;

namespace Olivia.DataAccess
{
    public class InstructionDAO
    {
        private Connection _connection;

        public InstructionDAO()
        {
            _connection = new Connection();
        }

        public List<Instruction> InstructionsForRecipe(int id)
        {
            List<Instruction> result = new List<Instruction>();

            using (SqlCommand command = _connection.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Instrucao WHERE id_receita=@recep ORDER BY posicao ASC";

                command.Parameters.Add("@recep", SqlDbType.Int).Value = id;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach(DataRow row in tab.Rows)
                    {
                        Instruction i = new Instruction
                        {
                            Description = row["designacao"].ToString(),
                            Duration = int.Parse(row["duracao"].ToString())
                        };
                        result.Add(i);
                    }
                }

            }

            return result;
        }


    }
}
