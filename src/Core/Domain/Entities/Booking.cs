using DAT154Oblig4.Domain.Enums.Booking;

namespace DAT154Oblig4.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public BookingStatus Status { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }

        public Booking() { }

        public Booking(int customerId, int roomId, DateTime bookingStart, DateTime bookingEnd)
        {
            CustomerId = customerId;
            RoomId = roomId;
            Status = BookingStatus.NotStarted;
            BookingStart = bookingStart;
            BookingEnd = bookingEnd;
        }

        public Booking(int customerId, int roomId, bool checkedIn, DateTime bookingStart, DateTime bookingEnd)
        {
            CustomerId = customerId;
            RoomId = roomId;
            Status = checkedIn ? BookingStatus.CheckedIn : BookingStatus.NotStarted;
            BookingStart = bookingStart;
            BookingEnd = bookingEnd;
        }
    }
}
