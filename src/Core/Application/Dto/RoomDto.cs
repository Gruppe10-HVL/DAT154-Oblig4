using System.Text.Json.Serialization;

using DAT154Oblig4.Domain.Enums;


namespace DAT154Oblig4.Application.Dto
{
    public class RoomDto 
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("bedCount")]
        public int BedCount { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("quality")]
        public RoomQuality Quality { get; set; }
        //public ICollection<ServiceTask> ServiceTasks { get; set; }
    }
}
