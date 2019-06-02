﻿using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Olivia.Models;
using System.Data;

namespace Olivia.DataAccess
{
    public class MenuDAO
    {
        public Menu getLastestMenu(int idUser)
        {
            Connection con = new Connection();
            Menu result = new Menu();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT MAX(Id_Menu)  AS Menu,Date FROM Menu WHERE Id_User=@user GROUP BY Date";
                command.Parameters.Add("@user", SqlDbType.Int).Value = idUser;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    if (result_querie.Rows.Count == 0)
                        return null;

                    DataRow row = result_querie.Rows[0];
                    int idMenu;
                    bool flag = int.TryParse(row["Menu"].ToString(), out idMenu);
                    DateTime date = (DateTime)row["Date"];

                    if (!flag)
                        return null;
                    result.Id_Menu = idMenu;
                    result.StartingDate = date;
                }
            }

            List<int> aux = new List<int>();
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Menu_Recipe WHERE Id_Menu = @menu ORDER BY Position DESC";
                command.Parameters.Add("@menu", SqlDbType.Int).Value = result.Id_Menu;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable result_querie = new DataTable();
                    adapter.Fill(result_querie);

                    foreach (DataRow row in result_querie.Rows)
                    {
                        aux.Add((int)row["Id_Recipe"]);
                    }
                }
            }
            result.Recipes = new List<Recipe>();
            RecipeDAO dao = new RecipeDAO();
            foreach (int i in aux)
            {
                result.Recipes.Add(dao.FindById(i));
            }
            con.Close();

            return result;
        }

        public void Insert(DateTime date,List<Recipe> recipes,int idUser)
        {
            Connection con = new Connection();
            int idMenu;
            using (SqlCommand command = con.Fetch().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "Insert into Menu VALUES (@user,@date)";
                command.Parameters.Add("@user", SqlDbType.Int).Value = idUser;
                command.Parameters.AddWithValue("@date", date);

                idMenu = (int)command.ExecuteScalar();
            }
            int i = 0;
            foreach(Recipe r in recipes)
            {
                using (SqlCommand command = con.Fetch().CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "Insert into Menu_Recipe VALUES (@recipe,@menu,@pos)";
                    command.Parameters.Add("@recipe", SqlDbType.Int).Value = r.Id_Recipe;
                    command.Parameters.Add("@menu", SqlDbType.Int).Value = idMenu;
                    command.Parameters.Add("@pos", SqlDbType.Int).Value = i;
                    i++;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
