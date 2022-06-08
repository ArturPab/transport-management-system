using MySql.Data.MySqlClient;

namespace transport_management_system.MVVM.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Nip { get; set; }
        public string? Regon { get; set; }

        public Company(MySqlDataReader companyRes)
        {
            this.Id = companyRes.GetInt32("Id");
            this.Name = companyRes.GetString("Name");
            this.Nip = companyRes.GetString("Nip");
            this.Regon = companyRes.GetString("Regon");
        }
    }
}