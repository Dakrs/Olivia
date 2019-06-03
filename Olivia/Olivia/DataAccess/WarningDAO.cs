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
    public class WarningDAO
    {
        public List<string> getWarnings(int id)
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


    }
}