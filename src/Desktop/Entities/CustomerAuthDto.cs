using Desktop.Entities;

namespace Desktop.Entitites
{
    public class CustomerAuthDto
    {

        public CustomerEntity Customer { get; set; }
        public string JWT { get; set; }

        public CustomerAuthDto() { }
        public CustomerAuthDto(CustomerEntity customer, string jwt)
        {
            Customer = customer;
            JWT = jwt;
        }
    }
}
