using System;
using Olivia.Models;
using Olivia.DataAccess;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;


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
                    command.CommandText = "insert into [User] Values(@username,@password,@email,@type,@prefe,@name,1)";

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

        public void UpdateProfile(int id, string name, string email, int pref) {

            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "Update[User] set Name = @user, Email = @email , Preference = @pref where Id_User = @id ;";
                command.Parameters.Add("@user", SqlDbType.VarChar).Value = name;
                command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                command.Parameters.Add("@pref", SqlDbType.VarChar).Value = pref;
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                command.ExecuteNonQuery();
            }
            con.Close();
        }

        public int CalculatePontos(int idUser) {
            Connection con = new Connection();
            int pontos = 0;
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "select SUM(Rating) AS Pontos from[Rating] inner join[Recipe] on Rating.Id_Recipe = Recipe.Id_Recipe where Creator = @id;";
                command.Parameters.Add("@id", SqlDbType.VarChar).Value = idUser;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result = new DataTable();
                    adapter.Fill(result);


                    if (result.Rows.Count > 0)
                    {

                        DataRow row = result.Rows[0];
                     
                            try
                            {
                                pontos = int.Parse(row["Pontos"].ToString());
                            }
                            catch (Exception ex)
                            {
                                pontos = 0;
                            }  



                        con.Close();


                    }

                }

            }
            return pontos;

        }

        public Dictionary<DateTime,Recipe> userHistory(int idUser)
        {
            Dictionary<DateTime, Recipe> result = new Dictionary<DateTime, Recipe>();
            Dictionary<DateTime, int> dic_id_recipe = new Dictionary<DateTime, int>();
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "select * from[History] where Id_User=@user";
                command.Parameters.Add("@user", SqlDbType.Int).Value=idUser;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_query = new DataTable();
                    adapter.Fill(result_query);

                    foreach(DataRow row in result_query.Rows)
                    {
                        DateTime value = (DateTime)row["Date"];
                        int idrecipe = int.Parse(row["Id_Recipe"].ToString());

                        dic_id_recipe.Add(value, idrecipe);
                    }
                }
            }
            RecipeDAO dao = new RecipeDAO();
            foreach(KeyValuePair<DateTime, int> pair in dic_id_recipe)
            {
                result.Add(pair.Key, dao.FindById(pair.Value));
            }
            con.Close();

            return result;
        }

        public void PromoteUser(int id_User)
        {
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "update [User] set Type=1 where Id_User=@Id_User";
                command.Parameters.Add("@Id_User", SqlDbType.Int).Value = id_User;

                command.ExecuteNonQuery();
            }
            con.Close();
        }

        public Utilizador FindById(int idUser)
        {
            Connection con = new Connection();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {

                command.CommandType = CommandType.Text;
                command.CommandText = "select * from[User] where Id_User=@user;";
                command.Parameters.Add("@user", SqlDbType.Int).Value = idUser;

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


        public List<int> IdUpgradeColab() { 

            List<int> able = new List<int>();
            Connection con = new Connection();
                using (SqlCommand command = con.Fetch().CreateCommand())
                {

                    command.CommandType = CommandType.Text;
                    command.CommandText = "select Id_User from (select [User].Id_User , sum(Rating) AS TOTAL_POINTS  from [User]  inner join Recipe on [User].Id_User = [Recipe].Creator AND [User].Type = 0 inner join Rating on [Rating].Id_Recipe = [Recipe].Id_Recipe GROUP BY [User].Id_User) AS able where TOTAL_POINTS > 20;"; 
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable result = new DataTable();
                        adapter.Fill(result);
    
                        
                    foreach (DataRow row in result.Rows)
                    {
                        try
                        {

                        int x = int.Parse(row["Id_user"].ToString());
                        able.Add(x);

                        }
                        catch(Exception e) { }
                    }

                    }

                }
                return able;
        
        }


    }
}
