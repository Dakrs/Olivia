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
    public class InstructionDAO
    {
        public int Insert(Instruction instruction)
        {
            int result = 0;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO [Instruction] VALUES (@Designation, @Position , @Id_Recipe)";
                command.Parameters.Add("@Designation", SqlDbType.VarChar).Value = instruction.Designation;
                command.Parameters.Add("@Position", SqlDbType.Int).Value = instruction.Position;
                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = instruction.Id_Recipe;

                result = command.ExecuteNonQuery();
            }
            con.Close();

            return result;
        }

        public Instruction GetInstruction(int recipe_id, int pos)
        {
            Instruction result = null;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "Select * from [Instruction] WHERE Id_receita=@recep AND Position=@pos";

                command.Parameters.Add("@recep", SqlDbType.Int).Value = recipe_id;
                command.Parameters.Add("@pos", SqlDbType.Int).Value = pos;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    if (tab.Rows.Count > 0)
                    {
                        DataRow row = tab.Rows[0];
                        result = new Instruction
                        {
                            Id_Recipe = int.Parse(row["Id_Recipe"].ToString()),
                            Position = int.Parse(row["Position"].ToString()),
                            Designation = row["Designation"].ToString(),
                        };
                    }
                }

            }
            con.Close();

            return result;
        }

        public List<Instruction> GetInstructions(int id_recipe)
        {
            List<Instruction> result = new List<Instruction>();

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Instruction WHERE Id_recipe=@recep ORDER BY Position ASC";

                command.Parameters.Add("@recep", SqlDbType.Int).Value = id_recipe;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable tab = new DataTable();
                    adapter.Fill(tab);

                    foreach (DataRow row in tab.Rows)
                    {
                        Instruction i = new Instruction
                        {
                            Designation = row["Designation"].ToString(),
                        };
                        result.Add(i);
                    }
                }

            }
            con.Close();

            return result;
        }

        public void DeleteInstruction(int recipe_id, int position)
        {
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM [Instruction] WHERE  Postion=@Position AND Id_Recipe=@Id_Recipe)";
                command.Parameters.Add("@Position", SqlDbType.Int).Value = position;
                command.Parameters.Add("@Id_Recipe", SqlDbType.Int).Value = recipe_id;

               command.ExecuteNonQuery();
            }
        }
    }
}