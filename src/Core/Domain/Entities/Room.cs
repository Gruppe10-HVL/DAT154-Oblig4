using DAT154Oblig4.Domain.Enums;

namespace DAT154Oblig4.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int BedCount { get; set; }
        public int Size { get; set; }
        public RoomQuality Quality { get; set; }
        public virtual ICollection<ServiceTask> ServiceTasks { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public Room() 
        {
            ServiceTasks = new List<ServiceTask>();
            Bookings = new List<Booking>();
        }
        public Room(int bedCount, int size, RoomQuality quality) : this()
        {
            BedCount = bedCount;
            Size = size;
            Quality = quality;
        }
    }
}
