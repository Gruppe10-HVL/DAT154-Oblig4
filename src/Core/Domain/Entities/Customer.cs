
namespace DAT154Oblig4.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Customer(string name, string username, string password)
        {
            Name = name;
            Username = username;
            Password = password;
        }

    }
}
