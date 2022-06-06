using MySql.Data.MySqlClient;

namespace transport_management_system.DB
{
    public class DBConnectionService
    {
        private readonly MySqlConnectionStringBuilder stringBuilder = new();

        private static DBConnectionService? instance = null;
        public static DBConnectionService Instance
        {
            get
            {
                if (instance == null)
                    instance = new DBConnectionService();
                return instance;
            }
        }
        public MySqlConnection Connection => new(stringBuilder.ToString());

        private DBConnectionService()
        {
            stringBuilder.Server = Properties.Settings.Default.server;
            stringBuilder.Database = Properties.Settings.Default.dbname;
            stringBuilder.UserID = Properties.Settings.Default.username;
            stringBuilder.Password = Properties.Settings.Default.password;
            stringBuilder.Port = Properties.Settings.Default.port;
        }
    }
}
