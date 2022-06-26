using MySql.Data.MySqlClient;

namespace transport_management_system.Domain;

public class Company
{
    public Company()
    {
    }

    public Company(MySqlDataReader reader)
    {
        Id = reader.GetInt32("Id");
        Name = reader.GetString("Name");
        Nip = reader.GetString("Nip");
        Regon = reader.GetString("Regon");
    }

    public Company(string name, string nip, string regon)
    {
        Name = name;
        Nip = nip;
        Regon = regon;
    }

    public int? Id { get; set; }
    public string Name { get; set; }
    public string Nip { get; set; }
    public string Regon { get; set; }
}