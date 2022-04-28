using System.Text.Json.Serialization;

namespace DAT154Oblig4.Application.Dto
{
    public class CustomerAuthDto
    {
        [JsonPropertyName("customer")]
        public CustomerDto Customer { get; set; }

        [JsonPropertyName("jwt")]
        public string JWT { get; set; }

        public CustomerAuthDto() { }
        public CustomerAuthDto(CustomerDto customer, string jwt)
        {
            Customer = customer;
            JWT = jwt;
        }
    }
}
