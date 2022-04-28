using DAT154Oblig4.Domain.Entities;
using DAT154Oblig4.Domain.Enums.Booking;
using Mapster;
using System.Text.Json.Serialization;

namespace DAT154Oblig4.Application.Dto
{
    public class BookingDto : IRegister
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; }

        [JsonPropertyName("roomId")]
        public int RoomId { get; set; }

        [JsonPropertyName("status")]
        public BookingStatus Status { get; set; }

        [JsonPropertyName("bookingStart")]
        public DateTime BookingStart { get; set; }

        [JsonPropertyName("bookingEnd")]
        public DateTime BookingEnd { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Booking, BookingDto>()
                .Map(x => x.CustomerName, x => x.Customer.Name);
        }
    }
}
