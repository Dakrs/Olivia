using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Olivia.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "DefaultConnection")
        {
            //return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Olivia;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return "Server=localhost;DataBase=Olivia;User=sa;Password=uwontfindme!unlessucheat";
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }
    }
}