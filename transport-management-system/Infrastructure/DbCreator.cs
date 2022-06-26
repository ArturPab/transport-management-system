using System.IO;
using MySql.Data.MySqlClient;

namespace transport_management_system.Infrastructure
{
    public static class DbCreator
    {
        public static void Create()
        {
            var stringBuilder = new MySqlConnectionStringBuilder
            {
                Server = Properties.Settings.Default.server,
                UserID = Properties.Settings.Default.username,
                Password = Properties.Settings.Default.password,
                Port = Properties.Settings.Default.port,
            };

            var connection = new MySqlConnection(stringBuilder.ToString());

            var query = File.ReadAllText(@"..\..\..\Resources\SQL\tmsDatabase.sql");

            var command = new MySqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
