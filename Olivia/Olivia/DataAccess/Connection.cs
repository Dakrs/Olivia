using System;
using System.Data.SqlClient;

namespace Olivia.DataAccess
{
    public class Connection
    {
        private SqlConnection _connection;

        public Connection()
        {
            //_connection = new SqlConnection("Server=localhost;DataBase=Olivia;User=sa;Password=yourStrong(!)Password");
            //_connection = new SqlConnection("Server=localhost;DataBase=Olivia;User=sa;Password=uwontfindme!unlessucheat");
            //_connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Olivia;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            _connection = new SqlConnection("Server=DESKTOP-0G0EQH4;Database=Olivia;Trusted_Connection=True;");
        }

        public void Close()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public SqlConnection Fetch()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                return _connection;
            }
            else
            {
                return this.Open();
            }
        }

        public SqlConnection Open()
        {
            _connection.Open();
            return _connection;
        }
    }
}
