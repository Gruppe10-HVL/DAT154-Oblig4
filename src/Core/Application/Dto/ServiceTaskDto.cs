using System.Text.Json.Serialization;
using DAT154Oblig4.Domain.Enums;


namespace DAT154Oblig4.Application.Dto
{
    public class ServiceTaskDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("roomId")]
        public int RoomId { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("taskType")]
        public ServiceTaskType TaskType { get; set; }
        
        [JsonPropertyName("taskStatus")]
        public ServiceTaskStatus TaskStatus { get; set; }
        
        [JsonPropertyName("priority")]
        public ServiceTaskPriority Priority { get; set; }
        
        [JsonPropertyName("notes")]
        public string Notes { get; set; }

    }
}