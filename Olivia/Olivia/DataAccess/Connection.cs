using System;
using System.Data.SqlClient;

namespace Olivia.DataAccess
{
    public class Connection
    {
        private SqlConnection _connection;

        public Connection()
        {
            _connection = new SqlConnection("Server=localhost;DataBase=Olivia;User=sa;Password=uwontfindme!unlessucheat");
            //_connection = new SqlConnection("Server=DESKTOP-S2U35UH;Database=Olivia;Trusted_Connection=True;");
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
