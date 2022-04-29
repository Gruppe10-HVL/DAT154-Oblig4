using Desktop.Enums;

namespace Desktop.Entities
{
    public class ServiceTaskEntity
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
