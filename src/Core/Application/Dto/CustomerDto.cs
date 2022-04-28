using DAT154Oblig4.Domain.Entities;
using System.Text.Json.Serialization;

using Mapster;

namespace DAT154Oblig4.Application.Dto
{
    public class CustomerDto : IRegister
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Customer, CustomerDto>();
        }
            public CustomerDto() { }
    }
}
