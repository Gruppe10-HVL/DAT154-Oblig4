using DAT154Oblig4.Domain.Enums;


namespace DAT154Oblig4.Application.Dto
{
    public class ServiceTaskDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Description { get; set; }
        public ServiceTaskType TaskType { get; set; }
        public ServiceTaskStatus TaskStatus { get; set; }
        public ServiceTaskPriority Priority { get; set; }
        public string Notes { get; set; }

    }
}
