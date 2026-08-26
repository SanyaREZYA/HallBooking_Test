public class Booking
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int CustomerId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal TotalPrice { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Confirmed;

    public DateTime CreatedAt { get; set; }

    public Hall Hall { get; set; } = null!;

    public Customer Customer { get; set; } = null!;

    public ICollection<BookingService> BookingServices { get; set; } =
        new List<BookingService>();
}