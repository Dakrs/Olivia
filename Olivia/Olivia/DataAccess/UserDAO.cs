using System;
using Olivia.Models;
using Olivia.DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text;


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
                command.CommandText = "select * from[User] where Username=@Username;";
                command.Parameters.Add("@Username", SqlDbType.VarChar);
                command.Parameters["@Username"].Value = user;

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

        public int LogIn(string user,string password)
        {
            int id = -1;

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "select Id_User from[User] where Username=@Username AND Password=@Password";
                command.Parameters.Add("@Username", SqlDbType.VarChar);
                command.Parameters.Add("@Password", SqlDbType.VarChar);
                command.Parameters["@Username"].Value = user;
                command.Parameters["@Password"].Value = Utilizador.HashPassword(password);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result = new DataTable();
                    adapter.Fill(result);

                    if (result.Rows.Count > 0)
                    {

                        DataRow row = result.Rows[0];
                        id = int.Parse(row["Id_User"].ToString());

                    }

                    con.Close();

                }

            }



            return id;
        }

        
    }
}
