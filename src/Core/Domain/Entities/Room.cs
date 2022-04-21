using DAT154Oblig4.Domain.Enums;

namespace DAT154Oblig4.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int BedCount { get; set; }
        public int Size { get; set; }
        public RoomQuality Quality { get; set; }

        public Room() { }
        public Room(int bedCount, int size, RoomQuality quality)
        {
            BedCount = bedCount;
            Size = size;
            Quality = quality;
        }
    }
}
