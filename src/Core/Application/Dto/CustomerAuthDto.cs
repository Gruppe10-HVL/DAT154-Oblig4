
namespace DAT154Oblig4.Application.Dto
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
