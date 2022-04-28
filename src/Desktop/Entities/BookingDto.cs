using System;


namespace Desktop.Entities
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public bool CheckedOut { get; set; }
        public bool Cancelled { get; set; }
    }
}
