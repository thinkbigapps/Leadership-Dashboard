using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess
{
    class DatabaseConnection
    {
        //db connection information removed for security
        const string ConnectionString = @"Server=*Enter Host*;uid=*Enter Username*;pwd=*Enter Password*;database=ex_eventDB";

        static public SqlConnection GetExEventDatabaseConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
