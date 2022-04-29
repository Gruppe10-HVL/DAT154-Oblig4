using Desktop.Entities;

namespace Desktop.Entitites
{
    public class CustomerAuthDto
    {

        public CustomerDto Customer { get; set; }
        public string JWT { get; set; }

        public CustomerAuthDto() { }
        public CustomerAuthDto(CustomerDto customer, string jwt)
        {
            Customer = customer;
            JWT = jwt;
        }
    }
}
