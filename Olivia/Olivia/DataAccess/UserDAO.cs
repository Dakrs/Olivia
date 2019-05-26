using System;
using Olivia.Models;
using Olivia.DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace Olivia.DataAccess
{
    public class UserDAO
    {
        public bool Insert(Utilizador u)
        {
            Utilizador flag = FindByUsername(u.Username);

            if (flag == null)
            {
                Connection con = new Connection();
                using (SqlCommand command = con.Fetch().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "insert into [User] Values(@username,@password,@email,@type,@prefe,@name,0)";

                    command.Parameters.Add("@username", SqlDbType.Text).Value = u.Username;
                    command.Parameters.Add("@password", SqlDbType.Text).Value = u.Password;
                    command.Parameters.Add("@email", SqlDbType.Text).Value = u.Email;
                    command.Parameters.Add("@type", SqlDbType.Int).Value = u.Type;
                    command.Parameters.Add("@prefe", SqlDbType.Int).Value = u.Preferencia;
                    command.Parameters.Add("@name", SqlDbType.Text).Value = u.Nome;

                    command.ExecuteScalar();

                    con.Close();
                    return true;
                }
            }
            return false;
        }

        public Utilizador FindByUsername(string user)
        {
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from [User] where Username='Dakrs'";

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result = new DataTable();
                    adapter.Fill(result);


                    if (result.Rows.Count > 0)
                    {

                        DataRow row = result.Rows[0];

                        Utilizador u = new Utilizador(
                            int.Parse(row["Id_User"].ToString()),
                            row["Username"].ToString(),
                            row["Password"].ToString(),
                            row["Email"].ToString(),
                            int.Parse(row["Type"].ToString()),
                            int.Parse(row["Preference"].ToString()),
                            row["Name"].ToString());

                        con.Close();
                        return u;
                    }
                }
            }
            return null;
        }
    }
}
