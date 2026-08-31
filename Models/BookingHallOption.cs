public class BookingHallOption
{
    public int BookingId { get; set; }

    public int ServiceId { get; set; }

    public Booking Booking { get; set; } = null!;

    public HallOption HallOption { get; set; } = null!;
}