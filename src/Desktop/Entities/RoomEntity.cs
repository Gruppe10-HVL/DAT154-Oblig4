using Desktop.Enums;

namespace Desktop.Entities
{
    public class RoomDto 
    {
        public int Id { get; set; }
        public int BedCount { get; set; }
        public int Size { get; set; }
        public RoomQuality Quality { get; set; }
        //public ICollection<ServiceTask> ServiceTasks { get; set; }

        public RoomDto() { }
    }
}
