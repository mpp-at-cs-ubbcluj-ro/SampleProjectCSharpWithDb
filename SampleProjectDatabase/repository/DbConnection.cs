using System.Configuration;
using System.Data;
using log4net;
using Microsoft.Data.Sqlite;

namespace SampleProjectDatabase.repository;

public class DbConnection
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(DbConnection)); 
    
    public static string Name = "identificatorDb";
    private static IDbConnection _connection;

    public static IDbConnection GetConnection()
    {
        if (_connection == null)
        {
            string dbUrl = ConfigurationManager.ConnectionStrings[Name].ConnectionString;
            Log.Info("Am gasit in app.config la name " + Name + " dbUrl " + dbUrl);
            _connection = new SqliteConnection(dbUrl);
            _connection.Open();
        }

        return _connection;
    }
}