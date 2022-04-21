
namespace DAT154Oblig4.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Room Room { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public bool CheckedOut { get; set; }
        public bool Cancelled { get; set; }

        public Booking() { }

        public Booking(Customer customer, Room room, DateTime bookingStart, DateTime bookingEnd)
        {
            Customer = customer;
            Room = room;
            CheckedIn = false;
            BookingStart = bookingStart;
            BookingEnd = bookingEnd;
            CheckedOut = false;
            Cancelled = false;
        }
        public Booking(Customer customer, Room room, bool checkedIn, DateTime bookingStart, DateTime bookingEnd)
        {
            Customer = customer;
            Room = room;
            CheckedIn = checkedIn;
            BookingStart = bookingStart;
            BookingEnd = bookingEnd;
            CheckedOut = false;
            Cancelled = false;
        }
    }
}
