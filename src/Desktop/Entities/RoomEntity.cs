using Desktop.Enums;
using System.Collections.Generic;

namespace Desktop.Entities
{
    public class RoomEntity 
    {
        public int Id { get; set; }
        public int BedCount { get; set; }
        public int Size { get; set; }
        public RoomQuality Quality { get; set; }
        public ICollection<ServiceTaskEntity> ServiceTasks { get; set; }

        public RoomEntity() { }
    }
}
