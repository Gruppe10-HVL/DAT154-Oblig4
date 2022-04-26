
namespace DAT154Oblig4.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public bool CheckedIn { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public bool CheckedOut { get; set; }
        public bool Cancelled { get; set; }

        public Booking() { }

        public Booking(int customerId, int roomId, DateTime bookingStart, DateTime bookingEnd)
        {
            CustomerId = customerId;
            RoomId = roomId;
            CheckedIn = false;
            BookingStart = bookingStart;
            BookingEnd = bookingEnd;
            CheckedOut = false;
            Cancelled = false;
        }
        public Booking(int customerId, int roomId, bool checkedIn, DateTime bookingStart, DateTime bookingEnd)
        {
            CustomerId = customerId;
            RoomId = roomId;
            CheckedIn = checkedIn;
            BookingStart = bookingStart;
            BookingEnd = bookingEnd;
            CheckedOut = false;
            Cancelled = false;
        }
    }
}
