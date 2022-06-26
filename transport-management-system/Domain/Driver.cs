using MySql.Data.MySqlClient;

namespace transport_management_system.MVVM.Model
{
    public class Driver
    {
        public Driver()
        {
        }

        public Driver(MySqlDataReader reader)
        {
            Id = reader.GetInt32("Id");
            FirstName = reader.GetString("FirstName");
            LastName = reader.GetString("LastName");
            Pesel = reader.GetString("Pesel");
            Salary = reader.GetDecimal("Salary");
        }

        public Driver(string firstName, string lastName, string pesel, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
            Salary = salary;
        }

        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Pesel { get; set; }
        public decimal? Salary { get; set; }
    }
}