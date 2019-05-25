using System;
using System.Data.SqlClient;

namespace Olivia.DataAccess
{
    public interface IConnection
    {
        // Open our connection
        SqlConnection Open();

        //Fetch our connection. If not created, then create
        SqlConnection Fetch();

        //close our connection
        void Close();
    }
}
