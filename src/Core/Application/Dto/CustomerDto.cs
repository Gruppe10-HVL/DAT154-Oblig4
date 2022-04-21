using DAT154Oblig4.Domain.Entities;
using Mapster;

namespace DAT154Oblig4.Application.Dto
{
    public class CustomerDto : IRegister
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Customer, CustomerDto>();
        }
            public CustomerDto() { }
    }
}
