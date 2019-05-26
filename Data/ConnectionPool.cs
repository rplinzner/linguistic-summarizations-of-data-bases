using System;
using System.Data.SQLite;
using System.IO;

namespace Data
{
    public class ConnectionPool
    {
        static string dbLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public SQLiteConnection DbConnection = new SQLiteConnection($"DataSource={dbLocation}\\covertype.db;Version=3;");

        public ConnectionPool()
        {
            DbConnection.Open();
        }
    }
}