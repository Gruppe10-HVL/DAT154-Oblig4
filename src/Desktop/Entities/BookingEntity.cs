using Desktop.Enums;
using System;


namespace Desktop.Entities
{
    public class BookingEntity
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public int RoomId { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime BookingStart { get; set; }

        public DateTime BookingEnd { get; set; }
    }
}
