using DAT154Oblig4.Domain.Entities;
using DAT154Oblig4.Domain.Enums;


namespace DAT154Oblig4.Application.Dto
{
    public class RoomDto 
    {
        public int Id { get; set; }
        public int BedCount { get; set; }
        public int Size { get; set; }
        public RoomQuality Quality { get; set; }
        //public ICollection<ServiceTask> ServiceTasks { get; set; }
    }
}
