using MySql.Data.MySqlClient;

namespace transport_management_system.Infrastructure
{
    public class DbConnectionService
    {
        private readonly MySqlConnectionStringBuilder _stringBuilder = new();

        private static DbConnectionService? _instance;
        public static DbConnectionService Instance => _instance ??= new DbConnectionService();
        public MySqlConnection Connection => new(_stringBuilder.ToString());

        private DbConnectionService()
        {
            _stringBuilder.Server = Properties.Settings.Default.server;
            _stringBuilder.Database = Properties.Settings.Default.dbname;
            _stringBuilder.UserID = Properties.Settings.Default.username;
            _stringBuilder.Password = Properties.Settings.Default.password;
            _stringBuilder.Port = Properties.Settings.Default.port;
        }
    }
}
